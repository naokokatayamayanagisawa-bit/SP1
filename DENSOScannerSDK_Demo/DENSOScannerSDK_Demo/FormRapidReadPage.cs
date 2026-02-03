
using System;
using DENSOScannerSDK;
using DENSOScannerSDK.Barcode;
using DENSOScannerSDK.Common;
using DENSOScannerSDK.Dto;
using DENSOScannerSDK.Interface;
using DENSOScannerSDK.Util;
using DENSOScannerSDK.RFID;

namespace DENSOScannerSDK_Demo
{
    public partial class FormRapidReadPage : Form, RFIDDataDelegate
    {
        private const String TAG = "FormRapidReadPage";

        CommonBase m_hCommonBase = CommonBase.GetInstance();

        private int tagCount = 0;

        private DateTime readStartDate;
        private long readTimeMilliseconds = 0;

        private ReadState readState = new ReadState(ReadState.ReadStateType.STANDBY);

        // この画面が生成された時に、アプリがスキャナと接続されていたかどうか
        // この画面にいる間に接続が切断された場合でも、この画面の生成時にスキャナと接続されていた場合は通信エラーが出るようにする
        // Whether the application was connected to the scanner when this screen was generated.
        // Even if the connection is disconnected while on this screen, make sure that a communication error occurs when connected to the scanner when generating this screen.
        private bool scannerConnectedOnCreate = false;

        CancellationTokenSource? m_pTS = null;
        CancellationTokenSource? m_pTS2 = null;

        private DialogResult mFormDialogResult;

        public FormRapidReadPage()
        {
            InitializeComponent();

            button_navigate_up.Text = Properties.Resource.navigate_up;
            text_title_rapid_read.Text = Properties.Resource.rapid_read;
            text_read_tags_head.Text = Properties.Resource.read_tag_head;
            text_read_tags_unit.Text = Properties.Resource.pcs;
            text_read_tags_per_second_unit.Text = Properties.Resource.tags_per_second_unit;
            button_switch.Text = Properties.Resource.start;
            button_clear.Text = Properties.Resource.clear;
        }

        public void OnRFIDDataReceived(CommScanner scanner, RFIDDataReceivedEvent rfidEvent)
        {
            ReadData(rfidEvent);
            RefreshReadTagsView();
        }

        private void FormRapidReadPage_Load(object sender, EventArgs e)
        {
            mFormDialogResult = DialogResult.OK;

            m_hCommonBase.OnScannerClosedDelegateStackPush(OnScannerClosedFormPage);

            OnAppearing();
        }
        private void OnAppearing()
        {
            scannerConnectedOnCreate = m_hCommonBase.IsScannerConnected();

            if (scannerConnectedOnCreate)
            {
                try
                {
                    m_hCommonBase.GetCommScanner().GetRFIDScanner().SetDataDelegate(this);

                    SetSessionInit(true);
                }
                catch (Exception e)
                {
                    // データリスナーの登録に失敗
                    // Failed to set listener.
                    AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
                }
            }
            else
            {
                // SP1が見つからなかった時はエラーメッセージ表示
                // Show error message if SP1 is not found.
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_NO_CONNECTION);
            }
        }


        public void OnScannerClosedFormPage()
        {
            //切断
            mFormDialogResult = DialogResult.Cancel;

            this.Invoke(new Action(() => this.Close()));
        }

        private void FormRapidReadPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            NavigateUp();

            m_hCommonBase.OnScannerClosedDelegateStackPop();

            this.DialogResult = mFormDialogResult;
        }

        private void button_navigate_up_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /**
         * 画面遷移でいう上の階層に移動する
         * Move to the upper level in the screen transition
         */
        private void NavigateUp()
        {
            try
            {
                // 読み込みを停止してから遷移する
                // Transition after stopping reading.
                RunReadAction(new ReadAction(ReadAction.ReadActionType.STOP));

                // リスナーを登録解除してから遷移する
                // Transition another screen after removing listener
                if (scannerConnectedOnCreate)
                {
                    m_hCommonBase.GetCommScanner().GetRFIDScanner().SetDataDelegate(null);

                    SetSessionInit(false);
                }
            }
            catch (Exception e)
            {
                if (e is CommException || e is RFIDException)
                {
                    AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                }
            }
        }

        /**
        * 受信イベントからデータを読み込む
        * Read data from receive event
        * @param event RFID受信イベント RFID receive event
        */
        private void ReadData(RFIDDataReceivedEvent IN_pEvent)
        {
            tagCount += IN_pEvent.RFIDData.Count;
        }

        /**
         * データをクリアする
         * Clear data
         */
        private void ClearData()
        {
            tagCount = 0;
            readTimeMilliseconds = 0;
        }

        /**
         * 読み込んだタグの表示を更新
         * タグ数に変更があった時に即時更新する
         * Update read tag display
         * Immediately update tag number when there is change
         */
        private void RefreshReadTagsView()
        {
            // タグ数はカンマ区切りで表示する
            // Display the number of tags in comma - separated values
            this.BeginInvoke(
                (MethodInvoker)delegate ()
                {
                    text_read_tags_value.Text = tagCount.ToString();
                }
            );
        }

        /**
         * 読みこんだ時間の表示を更新
         * 読み込み開始から一定時間ごとに自動更新する
         * Updated reading time display
         * Automatically update every fixed time from the start of reading
         */
        private void RefreshReadTimeView()
        {
            // 読み込んだミリ秒単位の総時間を、[分]:[秒]:[ミリ秒(二桁)]の表記に分割して表示する
            // Display the total time in milliseconds read and divided into the notation [minute]: [second]: [millisecond (two digits)]
            int separatedMinutes = (int)(readTimeMilliseconds / 1000 / 60);
            int separatedSeconds = (int)(readTimeMilliseconds / 1000 % 60);
            int separatedMilliseconds = (int)(readTimeMilliseconds % 1000 / 10);

            this.BeginInvoke(
                (MethodInvoker)delegate ()
                {
                    text_read_time.Text = string.Format("{0}:{1}:{2}", separatedMinutes.ToString("D2"), separatedSeconds.ToString("D2"), separatedMilliseconds.ToString("D2"));
                }
            );
        }

        /**
         * 1秒あたりに読み込んだタグの表示を更新
         * 読み込み開始から一定時間ごとに自動更新する
         * Updated display of tags read per second
         * Automatically update every fixed time from the start of reading
         */
        private void RefreshReadTagsPerSecondView()
        {
            // 小数点以下は切り捨てる
            // Truncate decimal places
            int readTagsPerSecond = readTimeMilliseconds > 0 ?
                    (int)((float)tagCount / readTimeMilliseconds * 1000) : 0;
            this.BeginInvoke(
                (MethodInvoker)delegate ()
                {
                    text_read_tags_per_second_value.Text = readTagsPerSecond.ToString();
                }
            );
        }

        /**
        * 読み込みアクションを実行する
        * 待機中は開始、読み込み中は停止といった、実行可能なアクションの時のみ実行する
        * Perform a load action
        * Execute only when executable actions such as start while waiting, stopping while reading
        * @param action 読み込みアクション action to read
        */
        private void RunReadAction(ReadAction action)
        {
            // 実行可能なアクションのみ受け付ける
            // Accept only executable actions
            if (readState.Runnable(action) == false)
            {
                return;
            }

            if( scannerConnectedOnCreate == false)
            {
                if (m_hCommonBase.IsScannerConnected() == true)
                {
                    //切断→接続への移行時
                    OnAppearing();
                }
            }

            // 設定されたアクションを実行する
            // Execute the set action
            switch (action.m_ReadActionType)
            {
                case ReadAction.ReadActionType.START:
                    StartRead();
                    break;
                case ReadAction.ReadActionType.STOP:
                    StopRead();
                    break;
            }

            // アクションを実行したので次の状態に切り替える
            // Since the action was executed, switch to the next state
            readState = ReadState.NextState(action);

            // 読み込み切り替えボタンには、次に実行するアクション名を設定する
            // Set the action name to be executed next for the read toggle button
            button_switch.Text = readState.NextAction().ToResourceString();
        }

        /**
         * 読み込みを開始する
         * Start loading
         */
        private void StartRead()
        {
            // 読み込み中はClearボタンを無効にする
            // Disable Clear button during loading
            button_clear.Enabled = false;
            button_clear.BackColor = SystemColors.Control;
            button_clear.UseVisualStyleBackColor = true;

            // 計測開始
            // この計測開始時刻から差分をとって時刻を表示する
            // 計測再開時は、読み込んだ時間分だけ前の時刻を計測開始時刻とする
            // Start measurement
            // A difference is taken from this measurement start time and the time is displayed
            // At the measurement restart time, the time before the read time is taken as the measurement start time

            readStartDate = new DateTime(DateTime.Now.Ticks - readTimeMilliseconds * TimeSpan.TicksPerMillisecond);

            m_pTS = new CancellationTokenSource();
            CancellationToken ct = m_pTS.Token;

            Task.Run(async () =>
            {
                while (true)
                {
                    if (ct.IsCancellationRequested)
                    {
                        return;
                    }

                    // 計測開始時からの差分を保持
                    // Maintain the difference from the start of measurement
                    readTimeMilliseconds = (long)((DateTime.Now - readStartDate).TotalMilliseconds);

                    RefreshReadTimeView();

                    await Task.Delay(10);
                }

            }, ct);

            m_pTS2 = new CancellationTokenSource();
            CancellationToken ct2 = m_pTS2.Token;

            Task.Run(async () =>
            {
                while (true)
                {
                    if (ct2.IsCancellationRequested)
                    {
                        return;
                    }

                    // 表示更新
                    // Display update
                    RefreshReadTagsPerSecondView();

                    await Task.Delay(1000);
                }

            }, ct2);

            // タグ読み込み開始
            // Tag loading starts
            if (scannerConnectedOnCreate)
            {
                try
                {
                    m_hCommonBase.GetCommScanner().GetRFIDScanner().OpenInventory();
                }
                catch (RFIDException e)
                {
                    AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
                    System.Diagnostics.Debug.Print(e.StackTrace);
                }
            }
        }

        private Int64 GetTime()
        {
            Int64 retval = 0;
            var st = new DateTime(1970, 1, 1);
            TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
            retval = (Int64)(t.TotalMilliseconds + 0.5);
            return retval;
        }

        /**
        * 読み込みを停止する
        * Stop loading
        */
        private void StopRead()
        {
            // タグ読み込み終了
            // Finish reading tags
            if (scannerConnectedOnCreate)
            {
                try
                {
                    m_hCommonBase.GetCommScanner().GetRFIDScanner().Close();
                }
                catch (Exception e)
                {
                    AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                }
            }

            // 時間表示の自動更新を止める
            // Stop automatic updating of time display
            m_pTS.Cancel();
            m_pTS2.Cancel();

            // 計測一時停止
            // Measurement pause
            readStartDate = new DateTime(DateTime.Now.Ticks - readTimeMilliseconds * TimeSpan.TicksPerMillisecond);

            // 自動更新から停止時点までで計測結果に誤差が発生しうるため、停止時点の計測結果を表示する
            // Display the measurement result at the time of stop because an error may occur in the measurement result from the automatic update to the stop time. 
            RefreshReadTimeView();
            RefreshReadTagsPerSecondView();

            // 読み込みを終えるのでClearボタンを有効にする
            // Enable Clear button as reading is finished
            button_clear.Enabled = true;
            button_clear.BackColor = Color.FromArgb(159, 191, 190);
            button_clear.UseVisualStyleBackColor = true;
        }

        private void button_switch_Clicked(object sender, EventArgs e)
        {
            RunReadAction(readState.NextAction());
        }

        private void button_clear_Clicked(object sender, EventArgs e)
        {
            ClearData();
            RefreshReadTagsView();
            RefreshReadTimeView();
            RefreshReadTagsPerSecondView();
        }

        void SetSessionInit(bool bEnable)
        {
            RFIDScannerSettings pSettings = m_hCommonBase.GetCommScanner().GetRFIDScanner().GetSettings();
            pSettings.mScan.mSessionInit = bEnable;
            m_hCommonBase.GetCommScanner().GetRFIDScanner().SetSettings(pSettings);
        }
    }

    public class ReadAction
    {
        public enum ReadActionType
        {
            START = 0,
            STOP
        }

        public static readonly Dictionary<ReadActionType, string> READ_ACTION_TYPE_NAMES = new Dictionary<ReadActionType, string>
        {
            { ReadActionType.START, Properties.Resource.start },
            { ReadActionType.STOP, Properties.Resource.stop },
        };

        public ReadActionType m_ReadActionType { get; set; }

        public ReadAction(ReadActionType IN_ReadActionType)
        {
            m_ReadActionType = IN_ReadActionType;
        }

        public string ToResourceString()
        {
            return READ_ACTION_TYPE_NAMES[m_ReadActionType];
        }

        public override bool Equals(object obj)
        {
            return m_ReadActionType == ((ReadAction)obj).m_ReadActionType;
        }
    }

    /**
     * 読み込み状態
     * Read state
     */
    class ReadState
    {
        public enum ReadStateType
        {
            STANDBY = 0   // 待機中 Waiting
            , READING     // 読み込み中 Loading
        }

        public ReadStateType m_ReadStateType { get; set; }

        public ReadState(ReadStateType IN_ReadStateType)
        {
            m_ReadStateType = IN_ReadStateType;
        }

        /**
         * 次に実行するアクションを返す
         * Return the action to be executed next
         * @return 次に実行するアクション the action to be executed next
         */
        public ReadAction NextAction()
        {
            // 待機中は開始を次に実行、読み込み中は停止を次に実行する
            return m_ReadStateType == ReadStateType.STANDBY ? new ReadAction(ReadAction.ReadActionType.START) : new ReadAction(ReadAction.ReadActionType.STOP);
        }

        /**
         * 指定したアクションが実行できるかを返す
         * Returns whether the specified action can be executed.
         * @param action アクション specified action 
         * @return 指定したアクションが実行できればtrue、実行できなければfalseを返す Returns true if the specified action can be executed, false if it can not be executed.
         */
        public bool Runnable(ReadAction action)
        {
            // 状態ごとに次のアクションが決まっており、それと異なるアクションは実行できない
            // The next action is decided for each state, and different actions can not be executed.
            return action.Equals(NextAction());
        }

        /**
         * 指定したアクションを実行した後の状態を返す
         * Returns the state after executing the specified action
         * @param action 指定したアクション the specified action
         * @return 指定したアクションを実行した後の状態 the state after executing the specified action
         */
        public static ReadState NextState(ReadAction action)
        {
            // 開始したら読み込み状態、停止したら待機状態に移行する
            // When it starts, it shifts to the read state, and when it stops it enters the standby state.
            return action.m_ReadActionType == ReadAction.ReadActionType.START ? new ReadState(ReadStateType.READING) : new ReadState(ReadStateType.STANDBY);
        }
    }
}