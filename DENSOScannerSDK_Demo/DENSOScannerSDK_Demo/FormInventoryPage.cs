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
    public partial class FormInventoryPage : Form, RFIDDataDelegate
    {
        private const String TAG = "FormInventoryPage";

        CommonBase m_hCommonBase = CommonBase.GetInstance();

        private ReadAction nextReadAction = new ReadAction(ReadAction.ReadActionType.START);

        // この画面が生成された時に、アプリがスキャナと接続されていたかどうか
        // この画面にいる間に接続が切断された場合でも、この画面の生成時にスキャナと接続されていた場合は通信エラーが出るようにする
        // Whether the application was connected to the scanner when this screen was generated.
        // Even if the connection is disconnected while on this screen, make sure that a communication error occurs when connected to the scanner when generating this screen.
        private bool scannerConnectedOnCreate = false;

        // ダミーデータの利用数
        // The number of usage of dummy data
        private readonly int DEFAULT_NO_STORED_TAG_LINE_NUMBER = 6;

        private int storedRFIDdataCount = 0;

        private bool mFormLoadCompletedFlg = false;

        private DialogResult mFormDialogResult;

        /**
         * タグを追加する
         * Add tag.
         * @param tagText 追加するタグのテキスト Text of the tag to be added.
         */
        public void AddTag(String tagText)
        {
            // 未格納タグがあれば設定、なければ追加
            // If there is an unstored tag, set it, otherwise add.
            // 未格納タグがあればそこに設定、なければ新規タグを追加
            if (storedRFIDdataCount < list_view.Items.Count)
            {
                list_view.Items[storedRFIDdataCount].Text = tagText;
            }
            else
            {
                //ListViewItemオブジェクトの作成
                ListViewItem lvi = new ListViewItem(tagText);
                lvi.UseItemStyleForSubItems = false;

                //フォントを設定にする
                FontStyle fs = lvi.Font.Style;
                Font newFont = new Font(lvi.Font.Name, 12, fs);
                lvi.Font = newFont;

                //アイテムを追加
                list_view.Items.Add(lvi);

                if ( (storedRFIDdataCount % 2) == 0)
                {
                    list_view.Items[storedRFIDdataCount].SubItems[0].BackColor = Color.LightBlue;       // 偶数行の背景色
                }
                else
                {
                    list_view.Items[storedRFIDdataCount].SubItems[0].BackColor = Color.LightCyan;      // 奇数行の背景色
                }

                list_view.Items[storedRFIDdataCount].EnsureVisible();
            }

            storedRFIDdataCount++;
        }

        public void AddDummydata(int ItemsIndex)
        {
            if (ItemsIndex < list_view.Items.Count)
            {
                list_view.Items[storedRFIDdataCount].Text = "";
            }
            else
            {
                //ListViewItemオブジェクトの作成
                ListViewItem lvi = new ListViewItem("");
                lvi.UseItemStyleForSubItems = false;

                //フォントを設定にする
                FontStyle fs = lvi.Font.Style;
                Font newFont = new Font(lvi.Font.Name, 12, fs);
                lvi.Font = newFont;

                //アイテムを追加
                list_view.Items.Add(lvi);

                if ( (ItemsIndex % 2) == 0)
                {
                    list_view.Items[ItemsIndex].SubItems[0].BackColor = Color.LightBlue;       // 偶数行の背景色
                }
                else
                {
                    list_view.Items[ItemsIndex].SubItems[0].BackColor = Color.LightCyan;      // 奇数行の背景色
                }

                list_view.Items[ItemsIndex].EnsureVisible();
            }
        }

        /**
         * タグをクリアする
         * 境界線表示のための未格納タグは配置している状態にもどしている
         * Clear the tag.
         * The non-stored tag for displaying the boundary line is returned to the state where it is arranged.
         */
        public void ClearTags()
        {
            // 一旦すべてのタグを削除する
            // Delete all tags once
            list_view.Clear();

            // 行全体を選択可能に設定
            list_view.FullRowSelect = true;

            // ListViewの設定
            list_view.View = View.Details;

            // ヘッダー定義(幅:自動調整)
            list_view.Columns.Add("");
            list_view.Columns[0].Width = -2;

            // ListViewの列設定(初期化設定:空データ設定)
            list_view.SmallImageList = new ImageList() { ImageSize = new Size(1, 50) };
            for (int i = 0; i < DEFAULT_NO_STORED_TAG_LINE_NUMBER; i++)
            {
                AddDummydata(i);
            }

            // Barcodeデータ格納数クリア
            storedRFIDdataCount = 0;

            RefreshTotalTags();
        }

        /**
         * 読み込みアクション
         * Read Action
         */
        private class ReadAction
        {
            public enum ReadActionType
            {
                START      // 読み込みを開始する start reading 
                , STOP     // 読み込みを停止する stop reading
            }

            public ReadAction(ReadActionType IN_ReadActionType)
            {
                m_ReadActionType = IN_ReadActionType;
            }

            public ReadActionType m_ReadActionType { get; set; }

            /**
             * リソース に変換する
             * Convert to resources
             * 
             * @return リソース resources
             */
            public string ToResourceString()
            {
                switch (m_ReadActionType)
                {
                    case ReadActionType.START:
                        return Properties.Resource.start;
                    case ReadActionType.STOP:
                        return Properties.Resource.stop;
                    default:
                        throw new Exception();
                }
            }
        }

        public void OnRFIDDataReceived(CommScanner scanner, RFIDDataReceivedEvent rfidEvent)
        {
            this.BeginInvoke(
                (MethodInvoker)delegate ()
                {
                    ReadData(rfidEvent);
                    // タグ数refreshTotalTagsに更新があるのでTotalTagsを更新する
                    // Updated TotalTags because there is an update in tag number refreshTotalTags
                    RefreshTotalTags();
                }
            );
        }

        public FormInventoryPage()
        {
            InitializeComponent();

            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
            button_navigate_up.Text = Properties.Resource.navigate_up;
            text_title_inventory.Text = Properties.Resource.inventory;
            text_total_tags_head.Text = Properties.Resource.total_tags;

        }

        private void FormInventoryPage_Load(object sender, EventArgs e)
        {
            mFormDialogResult = DialogResult.OK;

            m_hCommonBase.OnScannerClosedDelegateStackPush(OnScannerClosedFormPage);

            ClearTags();

            //Shift-JIS 対応
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            OnAppearing();

            mFormLoadCompletedFlg = true;
        }
        private void OnAppearing()
        {
            scannerConnectedOnCreate = m_hCommonBase.IsScannerConnected();

            if (scannerConnectedOnCreate)
            {
                try
                {
                    m_hCommonBase.GetCommScanner().GetRFIDScanner().SetDataDelegate(this);
                }
                catch (Exception e)
                {
                    // データリスナーの登録に失敗
                    // Failed to set data listener.
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

        private void FormInventoryPage_SizeChanged(object sender, EventArgs e)
        {
            if (mFormLoadCompletedFlg == true)
            {
                list_view.Columns[0].Width = -2;
            }
        }

        public void OnScannerClosedFormPage()
        {
            //切断
            mFormDialogResult = DialogResult.Cancel;

            this.Invoke(new Action(() => this.Close()));
        }

        private void FormInventoryPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            NavigateUp();

            m_hCommonBase.OnScannerClosedDelegateStackPop();

            this.DialogResult = mFormDialogResult;
        }



        /**
         * TotalTagsを更新する
         * Update the total number of tags.
         */
        private void RefreshTotalTags()
        {
            Text_Total_Tags_Value.Text = storedRFIDdataCount.ToString();

        }

        /**
         * 読み込みアクションを実行する
         * Execute the reading action
         */
        private void RunReadAction()
        {
            if (scannerConnectedOnCreate == false)
            {
                if (m_hCommonBase.IsScannerConnected() == true)
                {
                    OnAppearing();
                }
            }

            // 設定された読み込みアクションを実行する
            // Execute the set reading action.
            switch (nextReadAction.m_ReadActionType)
            {
                case ReadAction.ReadActionType.START:
                    ClearTags();

                    // タグ読み込み開始
                    // Execute the set reading action
                    if (scannerConnectedOnCreate)
                    {
                        try
                        {
                            m_hCommonBase.GetCommScanner().GetRFIDScanner().OpenInventory();
                        }
                        catch (Exception e)
                        {
                            AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_NO_CONNECTION);
                            System.Diagnostics.Debug.WriteLine(e.StackTrace);
                        }
                    }
                    break;

                case ReadAction.ReadActionType.STOP:
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
                            AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_NO_CONNECTION);
                            System.Diagnostics.Debug.WriteLine(e.StackTrace);
                        }
                    }
                    break;
            }

            // 次の読み込みアクションを設定する 前の読み込みアクションがSTARTならSTOPに、STOPならSTARTに切り替える
            // Set next read action. If the previous reading action is START switch to STOP, STOP switch to START.
            nextReadAction.m_ReadActionType = nextReadAction.m_ReadActionType == ReadAction.ReadActionType.START ? ReadAction.ReadActionType.STOP : ReadAction.ReadActionType.START;

            // ボタンには、次に実行するアクション名を設定する
            // Set the name of the action to be executed next to the button
            button_switch.Text = nextReadAction.ToResourceString();

        }

        /**
         * 画面遷移でいう上の階層に移動する
         * Move to the upper level in the screen transition
         */
        private void NavigateUp()
        {
            if (scannerConnectedOnCreate)
            {
                // タグ読み込みが開始中の場合
                // Next action is stop.
                if (nextReadAction.m_ReadActionType == ReadAction.ReadActionType.STOP)
                {
                    // タグ読み込み終了
                    // Finish reading tags.
                    RunReadAction();
                }

                // delegateの除去
                // Remove listener
                m_hCommonBase.GetCommScanner().GetRFIDScanner().SetDataDelegate(null);
            }
        }

        private void button_navigate_up_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_switch_Clicked(object sender, EventArgs e)
        {
            RunReadAction();
        }

        public void ReadData(RFIDDataReceivedEvent rfidDataReceivedEvent)
        {
            for (int i = 0; i < rfidDataReceivedEvent.RFIDData.Count; i++)
            {
                String data = "";
                byte[] uii = rfidDataReceivedEvent.RFIDData[i].GetUII();
                for (int loop = 0; loop < uii.Length; loop++)
                {
                    data += uii[loop].ToString("X2");
                }

                AddTag(data);
            }
        }
    }
}
