using System;
using System.Globalization;
using System.Text;
using DENSOScannerSDK;
using DENSOScannerSDK.Barcode;
using DENSOScannerSDK.Common;
using DENSOScannerSDK.Dto;
using DENSOScannerSDK.Interface;
using DENSOScannerSDK.Util;
using DENSOScannerSDK.RFID;

namespace DENSOScannerSDK_Demo
{
    public partial class FormLocateTagPage : Form, RFIDDataDelegate
    {
        private const String TAG = "FormFormLocateTagPage";

        // 検索用タグUII
        // Search tag UII
        private static TagUII? _searchTagUII = null;

        private int? readPowerLevelOnReadSearchTag = null;

        private float? readPowerLevelOnSearch = null;
        private ReadPowerStage? readPowerStageOnSearch = null;

        private MatchingMethod matchingMethod = MatchingMethod.FORWARD;

        private LocateTagState locateTagState = LocateTagState.STANDBY;

        // この画面が生成された時に、アプリがスキャナと接続されていたかどうか
        // この画面にいる間に接続が切断された場合でも、この画面の生成時にスキャナと接続されていた場合は通信エラーが出るようにする
        // Whether the application was connected to the scanner when this screen was generated.
        // Even if the connection is disconnected while on this screen, make sure that a communication error occurs when connected to the scanner when generating this screen.
        private bool scannerConnectedOnCreate = false;

        private bool m_bTransitionToSettingScreen = false;

        CommonBase m_hCommonBase = CommonBase.GetInstance();

        CancellationTokenSource? m_pCancellationSource = null;

        private Form? mFormPage = null;

        private DialogResult mFormDialogResult;

        // 本体に保存していて、アプリを終了後も保持するフィルタ設定値
        // SetFilterが成功したときのみに書き込みを行い、Loadボタン押下時に保持している値をテキストエリアに表示
        SaveSettingsWrapper m_pSaveSettingsWrapper = SaveSettingsWrapper.GetInstance();

        private System.Media.SoundPlayer? m_playSearchSound = null;
        private ReadPowerStage? m_stageSearchSound = null;

        public FormLocateTagPage()
        {
            InitializeComponent();

            image_search_radar.Image = Properties.Resource.locate_tag_radar_background;
            //image_search_circle.Image = Properties.Resource.locate_tag_circle_under_75;

            //親要素に設定する
            image_search_circle.Parent = image_search_radar;
            //重ねて表示するために位置を(0,0)に設定
            image_search_circle.Location = new Point(0, 0);
            //透過させる
            image_search_circle.BackColor = Color.Transparent;

            button_navigate_up.Text = Properties.Resource.navigate_up;
            text_title_locate_tag.Text = Properties.Resource.locate_tag;

            button_read_search_tag.Text = Properties.Resource.read;
            text_search_tag_uii_head.Text = Properties.Resource.uii;

            picker_match_direction.Items.Add(Properties.Resource.forward_match);
            picker_match_direction.Items.Add(Properties.Resource.backward_match);
            picker_match_direction.SelectedIndex = 0;

            text_read_power_value_on_search.Text = Properties.Resource.number_0_0;
            text_read_power_unit_on_search.Text = Properties.Resource.read_power_level_unit;
            button_search_tag_toggle.Text = Properties.Resource.search;

            button_setting.Text = Properties.Resource.range_setting;

            m_hCommonBase.fStage2_Max_Read_Power_Level = float.Parse(Properties.Resource.stage2_max_read_power_level_on_search);
            m_hCommonBase.fStage3_Max_Read_Power_Level = float.Parse(Properties.Resource.stage3_max_read_power_level_on_search);
            m_hCommonBase.fStage4_Max_Read_Power_Level = float.Parse(Properties.Resource.stage4_max_read_power_level_on_search);
            m_hCommonBase.fStage5_Max_Read_Power_Level = float.Parse(Properties.Resource.stage5_max_read_power_level_on_search);

            m_hCommonBase.fStage2_Max_Read_Power_Level = m_pSaveSettingsWrapper.GetFloat
                                                (Properties.Resource.pref_stage2_max_read_power_level_on_search, float.Parse(Properties.Resource.stage2_max_read_power_level_on_search));
            m_hCommonBase.fStage3_Max_Read_Power_Level = m_pSaveSettingsWrapper.GetFloat
                                                (Properties.Resource.pref_stage3_max_read_power_level_on_search, float.Parse(Properties.Resource.stage3_max_read_power_level_on_search));
            m_hCommonBase.fStage4_Max_Read_Power_Level = m_pSaveSettingsWrapper.GetFloat
                                               (Properties.Resource.pref_stage4_max_read_power_level_on_search, float.Parse(Properties.Resource.stage4_max_read_power_level_on_search));
            m_hCommonBase.fStage5_Max_Read_Power_Level = m_pSaveSettingsWrapper.GetFloat
                                                (Properties.Resource.pref_stage5_max_read_power_level_on_search, float.Parse(Properties.Resource.stage5_max_read_power_level_on_search));


            m_playSearchSound = new System.Media.SoundPlayer(Properties.Resource.track1);
            m_stageSearchSound = ReadPowerStage.STAGE_1;
        }

        private void FormLocateTagPage_Load(object sender, EventArgs e)
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
                    // リスナー登録
                    // Set listener
                    m_hCommonBase.GetCommScanner().GetRFIDScanner().SetDataDelegate(this);
                }
                catch (Exception ex)
                {
                    // データリスナーの登録に失敗
                    // Failed to set listener.
                    AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);

                }
            }
            else
            {
                // SP1が見つからなかったときはエラーメッセージ表示
                // Show error message if SP1 is not found.
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
                SetEnableSearchTag(false);
            }

            if (m_bTransitionToSettingScreen == false)
            {
                // UIをセットアップする
                // Set up UI.
                SetupReadPowerLevelSpinner();

                // 前回保存した検索用タグUIIを反映する
                // Load tag uii which was stored before.
                LoadSearchTagUII();

                if (text_search_tag_uii_value.Text == "" || text_search_tag_uii_value.Text == null)
                {
                    SetSearchTagUII(null);
                    SetEnableSearchTag(false);
                    return;
                }
            }

            m_bTransitionToSettingScreen = false;
        }

        public void OnScannerClosedFormPage()
        {
            //切断
            mFormDialogResult = DialogResult.Cancel;

            this.Invoke(new Action(() => this.Close()));
        }

        private void FormLocateTagPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            NavigateUp();

            m_hCommonBase.OnScannerClosedDelegateStackPop();

            this.DialogResult = mFormDialogResult;

        }

        /**
         * 画面遷移でいう上の階層に移動する
         * Move to the upper level in the screen transition.
         */
        private void NavigateUp()
        {
            // タグの読み込みや検索を停止してから遷移する
            // Transition after stopping tag reading and search.
            RunLocateTagAction(new LocateTagAction(LocateTagAction.LocateTagActionType.STOP));

            // リスナーを登録解除してから遷移する
            // Transition another screen after removing listener
            if (scannerConnectedOnCreate)
            {
                m_hCommonBase.GetCommScanner().GetRFIDScanner().SetDataDelegate(null);
            }
        }

        private void button_navigate_up_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_read_search_tag_Clicked(object sender, EventArgs e)
        {
            RunLocateTagAction(new LocateTagAction(LocateTagAction.LocateTagActionType.READ_SEARCH_TAG));
        }

        private void button_search_tag_toggle_Clicked(object sender, EventArgs e)
        {
            LocateTagAction action = locateTagState == LocateTagState.STANDBY ?
            new LocateTagAction(LocateTagAction.LocateTagActionType.SEARCH_TAG) : new LocateTagAction(LocateTagAction.LocateTagActionType.STOP);
            RunLocateTagAction(action);
        }

        /**
         * データ受信時の処理
         * Processing when receiving data
         * @param rfidDataReceivedEvent 受信イベント Receive event
         */
        public void OnRFIDDataReceived(CommScanner scanner, RFIDDataReceivedEvent rfidDataReceivedEvent)
        {
            Task.Factory.StartNew(async () =>
            {
                this.BeginInvoke(
                    (MethodInvoker)delegate ()
                    {
                        if (locateTagState == LocateTagState.READING_SEARCH_TAG)
                        {
                            ReadDataOnReadSearchTag(rfidDataReceivedEvent);
                        }
                        else if (locateTagState == LocateTagState.SEARCHING_TAG)
                        {
                            ReadDataOnSearch(rfidDataReceivedEvent);
                        }
                    }
                );
            });
        }

        /**
         * 検索用のデータを読み込むときのデータ受信処理
         * Data reception processing when retrieval data is read.
         * @param event RFID受信イベント RFID receive event
         */
        private void ReadDataOnReadSearchTag(RFIDDataReceivedEvent IN_pEvent)
        {
            // 複数のタグが読み込まれた場合、最初のタグのUIIを設定する
            // When multiple tags are read, set UII of the first tag.
            List<RFIDData> dataList = new List<RFIDData>(IN_pEvent.RFIDData);
            RFIDData firstData = dataList.ElementAt(0);

            try
            {
                SetSearchTagUII(TagUII.ValueOf((firstData.GetUII())));
            }
            catch (OverflowBitException e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }

            // 1回読み込んだらすぐに読み込みを終える
            // Finish loading immediately after loading once.
            StopReadSearchTag();
        }

        /**
         * 検索しているときのデータ受信処理
         * Data reception processing when searching
         * @param event RFID受信イベント RFID receive event
         */
        private void ReadDataOnSearch(RFIDDataReceivedEvent IN_pEvent)
        {
            // 検索用タグのUIIに該当するデータを検出する
            // 複数のデータが読み込まれた場合、最後のデータのUIIを設定する
            // Detect data corresponding to UII of search tag.
            // If more than one data is loaded, set the UII of the last data.
            List<RFIDData> dataList = new List<RFIDData>(IN_pEvent.RFIDData);
            RFIDData matchedData = null;
            for (int i = dataList.Count - 1; i >= 0; i--)
            {
                RFIDData data = dataList.ElementAt(i);
                if (CheckMatchSearchTagUIIHexString(data.GetUII()))
                {
                    matchedData = data;
                    break;
                }
            }

            // 検索用タグのUIIに該当するデータがなければ何もしない
            // If there is no data corresponding to UII of the search tag, it does nothing
            if (matchedData == null)
            {
                return;
            }

            // RSSIからRead出力値を計算して表示と音に反映する
            // (Read出力値[dBm]) = (RSSI) / 10
            // Calculate Read output value from RSSI and reflect it on display and sound
            // (Read output value[dBm]) = (RSSI) / 10
            int rssi = matchedData.GetRSSI();
            float readPowerLevel = rssi / 10.0f;

            // 読み込みから1.5秒経ったとき、表示を初期状態にする
            // 1.5秒以内に次の読み込みが行われるとremoveCallbacksAndMessagesが呼び出されキャンセルされる
            // When 1.5 seconds have elapsed since reading, put the display in the initial state
            // If the next reading is done within 1.5 seconds removeCallbacksAndMessages will be called and canceled
            if (m_pCancellationSource != null)
            {
                m_pCancellationSource.Cancel();
            }

            SetReadPowerLevelOnSearch(readPowerLevel);

            m_pCancellationSource = new CancellationTokenSource();
            Task.Delay(int.Parse(Properties.Resource.locate_tag_initialize_time)).ContinueWith(messageHandler =>
            {
                Task.Factory.StartNew(async () =>
                {
                    this.BeginInvoke(
                        (MethodInvoker)delegate ()
                        {
                            SetReadPowerLevelOnSearch(null);
                        }
                    );
                });
            }, m_pCancellationSource.Token);
        }

        /**
         * 検索用タグのUIIの16進数文字列が検索対象タグのものに合致しているか
         * バイト配列として合致していなくても、文字列として合致していれば合致しているものとする
         * 
         * 例: 検索用タグのUII:"A12" 検索対象タグのUII:"A123"の場合
         * マッチング方法:先頭からのマッチング の場合、バイト配列でマッチングすると検索用タグのUIIは[0A, 12]、検索対象タグのUIIは[A1, 23]で合致しない。
         * しかし文字列によるマッチングなので先頭の"A12"の部分が合致していることから合致しているものとする。
         * 
         * Whether the UII hexadecimal character string of the search tag matches that of the search target tag.
         * Even if it does not match as a byte array, it matches if it matches as a character string.
         * 
         * Matching method: In case of matching from the beginning, when matching by byte array, the UII of the search tag is [0 A, 12] and the UII of the search target tag does not match [A 1, 23].
         * However, since it matches with a character string, it coincides with the part of "A12" at the head which matches, so it matches.
         * 
         * @param targetTagUII 検索対象タグのUII Search tag UII
         * @return 検索用タグのUIIの16進数文字列が検索対象タグのものに合致していればtrue、合致していなければfalseを返す True if the UII hexadecimal character string of the search tag matches that of the search target tag, false if it does not match
         */
        private bool CheckMatchSearchTagUIIHexString(byte[] targetTagUII)
        {
            // 検索用タグのUIIがなければ、マッチング自体が不要なので常に該当しているものとする
            // If there is no UII for search tags, matching itself is unnecessary so it always applies
            if (GetSearchTagUII() == null)
            {
                return true;
            }

            // 検索用タグのUIIが検索対象のUIIより長い場合、常に該当しない
            // It is not always applicable when UII of search tag is longer than search UII
            TagUII searchTagUII = GetSearchTagUII();
            if (searchTagUII.GetBytes().Length > targetTagUII.Length)
            {
                return false;
            }

            // 検索文字列が検索対象文字列の先頭もしくは末尾にあれば該当しているものとする
            // If the search character string is at the beginning or the end of the search target character string, it is assumed to be applicable
            String searchString = searchTagUII.GetHexString();
            String targetString = BytesToHexString(targetTagUII);
            switch (matchingMethod)
            {
                case MatchingMethod.FORWARD:
                    int searchStringFirstIndex = targetString.IndexOf(searchString);
                    return searchStringFirstIndex == 0;
                case MatchingMethod.BACKWARD:      // 検索文字列が検索対象文字列の末尾 // Search string at the end of search string
                    int searchStringLastIndex = targetString.LastIndexOf(searchString);
                    return searchStringLastIndex == targetString.Length - searchString.Length;
                default:
                    return false;
            }
        }

        /**
         * Read出力値を選択するスピナーをセットアップする
         * Set up the spinner to select Read output value.
         */
        private void SetupReadPowerLevelSpinner()
        {
            spinner_power_level_value_on_read_search_tag.Items.Add(Properties.Resource.locate_tag_read_power_levels_item0);
            spinner_power_level_value_on_read_search_tag.Items.Add(Properties.Resource.locate_tag_read_power_levels_item1);
            spinner_power_level_value_on_read_search_tag.Items.Add(Properties.Resource.locate_tag_read_power_levels_item2);
            spinner_power_level_value_on_read_search_tag.Items.Add(Properties.Resource.locate_tag_read_power_levels_item3);

            // 初期状態は最小値
            // The initial state is the minimum value
            readPowerLevelOnReadSearchTag = int.Parse((string)spinner_power_level_value_on_read_search_tag.Items[0]);
            spinner_power_level_value_on_read_search_tag.SelectedIndex = 0;
        }

        private void spinner_power_level_value_on_read_search_tag_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 選択時にRead出力値を設定
            // Set Read output value when selecting
            readPowerLevelOnReadSearchTag = int.Parse((string)spinner_power_level_value_on_read_search_tag.SelectedItem);
        }

        /**
         * UIIを編集する
         * Edit UII
         */
        private void EditTagUII()
        {
            //未実装 Unimplemented

            //StringInputContents contents = new StringInputContents();
            //contents.title = getString(R.string.uii);
            //contents.startString = getSearchTagUII() != null ? getSearchTagUII().getHexString() : "";
            //contents.inputListener = new StringInputFragment.InputListener() {

            //OnInput
            // ...
            // ...

            //
            // TODO
            //ShowUpperAlphaInput(contents);
        }

        /**
         * タグを見つける上でのアクションを実行する
         * Perform actions on finding tags
         * @param action タグを見つける上でのアクション Action on finding tags
         */
        private void RunLocateTagAction(LocateTagAction action)
        {
            switch (action.m_ReadActionType)
            {
                case LocateTagAction.LocateTagActionType.READ_SEARCH_TAG:
                    if (locateTagState == LocateTagState.STANDBY)
                    {
                        StartReadSearchTag();
                    }
                    break;
                case LocateTagAction.LocateTagActionType.SEARCH_TAG:
                    if (locateTagState == LocateTagState.STANDBY)
                    {
                        StartSearchTag();
                    }
                    break;
                case LocateTagAction.LocateTagActionType.STOP:
                    if (locateTagState == LocateTagState.READING_SEARCH_TAG)
                    {
                        StopReadSearchTag();
                    }
                    else if (locateTagState == LocateTagState.SEARCHING_TAG)
                    {
                        StopSearchTag();
                    }
                    break;
            }
        }

        /**
         * 検索用タグの読み込みを開始する
         * Start loading search tags
         */
        private void StartReadSearchTag()
        {
            locateTagState = LocateTagState.READING_SEARCH_TAG;

            // 検索用タグ読み込み中はナビゲーター以外のUI操作を無効にする
            // Disable UI operations other than Navigator while loading search tags
            SetEnableInteractiveUIWithoutNavigatorAndSearchTag(false);
            SetEnableSearchTag(false);

            // Read出力値に設定してインベントリを開く
            // Set to Read output value and open inventory
            try
            {
                SetScannerSettings((int)readPowerLevelOnReadSearchTag, null);
                OpenScannerInventory();
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
         * 検索用タグの読み込みを停止する
         * Stop loading search tags
         */
        private void StopReadSearchTag()
        {
            // インベントリを閉じる
            // Close inventory
            try
            {
                CloseScannerInventory();
            }
            catch (Exception e)
            {
                if (e is CommException || e is RFIDException)
                {
                    AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                }
            }

            // 待機中はUI操作を有効にする
            // Enable UI operation while waiting
            SetEnableInteractiveUIWithoutNavigatorAndSearchTag(true);
            SetEnableSearchTag(true);

            locateTagState = LocateTagState.STANDBY;

        }

        /**
         * タグ検索を開始する
         * Start tag search
         */
        private void StartSearchTag()
        {
            locateTagState = LocateTagState.SEARCHING_TAG;

            // タグ検索中はナビゲーターと停止ボタン（タグ検索切り替えボタン）以外のUI操作を無効にする
            // Disable UI operations other than navigator and stop button (tag search switch button) while tag searching
            SetEnableInteractiveUIWithoutNavigatorAndSearchTag(false);

            // タグ検索切り替えボタンのテキストに停止のアクション名を設定する
            // Set the stop action name to the text of the tag search switch button
            button_search_tag_toggle.Text = new LocateTagAction(LocateTagAction.LocateTagActionType.STOP).ToResourceString();

            // 規定のRead出力値とセッションS0を設定しインベントリを開く
            // Set prescribed Read output value and session S0 and open inventory
            int searchReadPowerLevel = int.Parse(Properties.Resource.read_power_level_on_search_tag);

            RFIDScannerSettings.Scan.SessionFlag searchSessionFlag = RFIDScannerSettings.Scan.SessionFlag.S0;   // session_on_search = "S0"
            try
            {
                SetScannerSettings(searchReadPowerLevel, searchSessionFlag);
                OpenScannerInventory();
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
         * タグ検索を停止する
         * Stop tag search
         */
        private void StopSearchTag()
        {
            // インベントリを閉じる
            // Close inventory
            try
            {
                CloseScannerInventory();
            }
            catch (Exception e)
            {
                if (e is CommException || e is RFIDException)
                {
                    AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                }
            }

            // 待機中はUI操作を有効にする
            // Enable UI operation while waiting
            SetEnableInteractiveUIWithoutNavigatorAndSearchTag(true);

            // タグ検索切り替えボタンのテキストにタグ検索のアクション名を設定する
            // Set the action name of tag search in the text of the tag search switch button
            button_search_tag_toggle.Text = new LocateTagAction(LocateTagAction.LocateTagActionType.SEARCH_TAG).ToResourceString();

            // Read出力値の表示と音をなくす
            // Read output value display and sound is lost
            SetReadPowerLevelOnSearch(null);

            locateTagState = LocateTagState.STANDBY;
        }

        /**
         * スキャナのインベントリを開く
         * Open inventory of scanner
         * @throws CommException CommScannerに関する例外 Exception on CommScanner
         * @throws RFIDException RFIDScannerに関する例外 Exception on RFIDScanner
         */
        private void OpenScannerInventory()
        {
            if (!scannerConnectedOnCreate)
            {
                return;
            }

            try
            {
                m_hCommonBase.GetCommScanner().GetRFIDScanner().OpenInventory();
            }
            catch (Exception e)
            {
                if (e is CommException || e is RFIDException)
                {
                    throw e;
                }
            }
        }

        /**
         * スキャナのインベントリを閉じる
         * Close the scanner inventory
         * @throws CommException CommScannerに関する例外 Exception on CommScanner
         * @throws RFIDException RFIDScannerに関する例外 Exception on RFIDScanner
         */
        private void CloseScannerInventory()
        {
            if (!scannerConnectedOnCreate)
            {
                return;
            }

            try
            {
                m_hCommonBase.GetCommScanner().GetRFIDScanner().Close();
            }
            catch (Exception e)
            {
                if (e is CommException || e is RFIDException)
                {
                    throw e;
                }
            }
        }

        /**
         * スキャナを設定する
         * Set up the scanner
         * @param readPowerLevel Read出力値 Read output value
         * @param sessionFlag nullを指定した場合はセッションフラグを設定しない If null is specified, no session flag is set
         * @throws CommException CommScannerに関する例外 Exception on CommScanner
         * @throws RFIDException RFIDScannerに関する例外 Exception on RFIDScanner
         */
        private void SetScannerSettings(int readPowerLevel, RFIDScannerSettings.Scan.SessionFlag? sessionFlag)
        {
            if (!scannerConnectedOnCreate)
            {
                return;
            }

            // 設定する値以外の設定値を既存のものから取得する
            // Acquire setting values ​​other than the value to be set from the existing one
            RFIDScannerSettings settings = m_hCommonBase.GetCommScanner().GetRFIDScanner().GetSettings();

            // 値を設定する
            // Set value
            settings.mScan.mPowerLevelRead = readPowerLevel;
            if (sessionFlag != null)
            {
                settings.mScan.mSessionFlag = (RFIDScannerSettings.Scan.SessionFlag)sessionFlag;
            }

            m_hCommonBase.GetCommScanner().GetRFIDScanner().SetSettings(settings);
        }

        /**
         * ナビゲーターとタグ検索切り替えボタン以外のUI操作に関して有効/無効を設定する
         * Enable / disable UI operations other than navigator and tag search switching button
         * @param enabled 有効ならtrue、無効ならfalseを指定する Specify true if it is enabled, false if it is invalid
         */
        private void SetEnableInteractiveUIWithoutNavigatorAndSearchTag(bool enabled)
        {
            // 該当するUIを 有効化/無効化 する
            // 基本の色がデフォルトのテキストを 有効/無効 な見た目になるよう表示する            
            // Activate / deactivate the corresponding UI

            // Display basic text so that default text is valid / ineffective
            spinner_power_level_value_on_read_search_tag.Enabled = enabled;
            button_read_search_tag.Enabled = enabled;
            text_search_tag_uii_value.Enabled = enabled;
            picker_match_direction.Enabled = enabled;
            button_setting.Enabled = enabled;

            

            if (enabled == true)
            {
                button_setting.BackColor = Color.FromArgb(159, 191, 190);
                button_read_search_tag.BackColor = Color.FromArgb(159, 191, 190);
            }
            else
            {
                button_setting.BackColor = SystemColors.Control;
                button_read_search_tag.BackColor = SystemColors.Control;
            }
            button_setting.UseVisualStyleBackColor = true;
            button_read_search_tag.UseVisualStyleBackColor = true;


            // 基本の色が強調色のテキストを 有効/無効 な見た目になるよう表示する
            // Display the basic color so that the highlighted text becomes valid / ineffective
            text_search_tag_uii_head.Enabled = enabled;

        }

        /**
         * タグ検索切り替えボタンの有効/無効を設定する
         * Enable / disable tag search switch button
         * @param enabled 有効ならtrue、無効ならfalseを指定する Specify true if it is enabled, false if it is invalid
         */
        private void SetEnableSearchTag(bool enabled)
        {
            button_search_tag_toggle.Enabled = enabled;

            if (enabled == true)
            {
                button_search_tag_toggle.BackColor = Color.FromArgb(159, 191, 190);
            }
            else
            {
                button_search_tag_toggle.BackColor = SystemColors.Control;
            }
            button_search_tag_toggle.UseVisualStyleBackColor = true;

        }

        /**
         * 検索時におけるRead出力値の表示と音を設定する
         * Display Read output value and set sound during search
         * @param readPowerLevel 検索時におけるRead出力値
         * @ param readPowerLevel Read output value when searching
         */
        private void SetReadPowerLevelOnSearch(float? readPowerLevel)
        {
            // Read出力値に変更がなければ何もしない
            // Do nothing if there is no change in Read output value
            if (readPowerLevelOnSearch == null && readPowerLevel == null ||
                    readPowerLevelOnSearch != null && readPowerLevel != null && readPowerLevelOnSearch.Equals(readPowerLevel))
            {
                return;
            }
            readPowerLevelOnSearch = readPowerLevel;

            // 変更後のRead出力値を表示に反映する
            // Reflect the changed Read output value in the display
            string? text = readPowerLevel != null ? readPowerLevel.ToString() : "0.0";
            text_read_power_value_on_search.Text = text;

            // Read出力値の段階に変更がなければこれ以降何もしない
            // If there is no change in the level of Read output value, do not do anything after this
            ReadPowerStage? readPowerStage = GetReadPowerStage(readPowerLevel);

            if (readPowerStageOnSearch == readPowerStage)
            {
                return;
            }
            readPowerStageOnSearch = readPowerStage;

            // 変更後のRead出力値の段階を円と音に反映する
            // Reflect the stage of Read output value after change to yen and sound
            SetSearchCircle(readPowerStage);
            SetSearchSound(readPowerStage);
        }

        /**
         * Read出力値の段階に応じて円の表示を設定する
         * Set display of circle according to the level of Read output value
         * @param readPowerStage 円の表示のもとになるRead出力値の段階 nullを指定した場合は表示しない Do not display when the stage of the Read output value that is the source of the circle display is specified null
         */
        private void SetSearchCircle(ReadPowerStage? readPowerStage)
        {
            // Read出力値の段階がnullであれば表示しない
            // Do not display if the level of Read output value is null
            if (readPowerStage != null)
            {
                image_search_circle.Visible = true;
            }
            else
            {
                image_search_circle.Visible = false;
                return;
            }

            // Read出力値の段階に応じて円の表示を設定する
            // Set display of circle according to the level of Read output value
            switch (readPowerStage)
            {
                case ReadPowerStage.STAGE_1:
                    image_search_circle.Image = Properties.Resource.locate_tag_circle_over_35;
                    break;
                case ReadPowerStage.STAGE_2:
                    image_search_circle.Image = Properties.Resource.locate_tag_circle_48_to_36;
                    break;
                case ReadPowerStage.STAGE_3:
                    image_search_circle.Image = Properties.Resource.locate_tag_circle_62_to_49;
                    break;
                case ReadPowerStage.STAGE_4:
                    image_search_circle.Image = Properties.Resource.locate_tag_circle_74_to_63;
                    break;
                case ReadPowerStage.STAGE_5:
                    image_search_circle.Image = Properties.Resource.locate_tag_circle_under_75;
                    break;
                default:
                    return;
            }
        }

        /**
         * Read出力値の段階に応じて音を設定する
         * Set sound according to the level of Read output value
         * @param readPowerStage Read出力値の段階 nullを指定した場合は再生しない stage of Read output value. Do not play if null is specified.
         */
        private void SetSearchSound(ReadPowerStage? readPowerStage)
        {
            // Read出力値の段階がnullであればオーディオトラックを停止する
            // Stop the audio track if the Read output value stage is null
            if (readPowerStage == null)
            {
                m_playSearchSound.Stop();
                return;
            }

            // 再生するオーディオトラック名を求める
            // Find the name of the audio track to play
            System.IO.Stream playAudioTrackStream;
            switch (readPowerStage)
            {
                case ReadPowerStage.STAGE_1:
                    playAudioTrackStream = Properties.Resource.track1;
                    break;
                case ReadPowerStage.STAGE_2:
                    playAudioTrackStream = Properties.Resource.track2;
                    break;
                case ReadPowerStage.STAGE_3:
                    playAudioTrackStream = Properties.Resource.track3;
                    break;
                case ReadPowerStage.STAGE_4:
                    playAudioTrackStream = Properties.Resource.track4;
                    break;
                case ReadPowerStage.STAGE_5:
                    playAudioTrackStream = Properties.Resource.track5;
                    break;
                default:
                    return;
            }

            // 該当するオーディオトラックを再生する
            // Play the corresponding audio track
            m_playSearchSound.Stop();
            m_playSearchSound.Stream = playAudioTrackStream;
            m_playSearchSound.Play();
        }

        #region Set/Get/Load search tag UII with UI

        /**
         * 検索用タグのUIIを設定する
         * 該当するTextView及びButtonに反映する
         * Set UII of search tag
         * Reflected in applicable TextView and Button
         * @param searchTagUII 検索用タグのUII nullを指定した場合空として保持する Search tag UII. If null is specified, it is held as empty
         */
        private void SetSearchTagUII(TagUII? searchTagUII)
        {
            _searchTagUII = searchTagUII;
            text_search_tag_uii_value.Text = searchTagUII != null ? searchTagUII.GetHexString() : null;
        }

        /**
         * 検索用タグのUIIを取得する
         * Get the UII of the search tag
         * @return 検索用タグのUII 値がない場合はnullを返す Search tag UII. Returns null if there is no value.
         */
        private TagUII GetSearchTagUII()
        {
            return _searchTagUII;
        }

        /**
         * アプリ実行中の間常に保持する検索用タグUIIを読みこむ
         * Read search tag UII which is always held during application execution
         */
        private void LoadSearchTagUII()
        {
            SetSearchTagUII(_searchTagUII);
        }

        #endregion

        #region Convert hex string and bytes

        /**
         * 16進数の文字列からバイトのリストに変換する
         * Convert hexadecimal string to byte list
         * @param hexString 16進数の文字列 Hexadecimal character string
         * @return 16進数の文字列をもとにしたバイトのリスト List of bytes based on hexadecimal character string
         */
        private static byte[] HexStringToBytes(String hexString)
        {
            // 空文字の場合は要素 0
            // In case of null character, element 0
            if (hexString.Length == 0)
            {
                return new byte[0];
            }

            // 16進数の文字列をバイト単位で切り出してリストに格納する
            // 1バイトは16進数2文字分なので2文字ずつ切り出す
            // 文字列長にかかわらず2文字ずつ切り出せるようにするため、文字列が奇数長の場合は0を先頭に追加する
            // Cut out hexadecimal character string in byte units and store it in the list
            // Since 1 byte is equivalent to 2 hexadecimal characters, it cuts out two characters at a time
            // In order to be able to cut out two characters at a time irrespective of the length of the character string, 0 is added to the beginning if the character string is an odd number length
            String workHexString = hexString.Length % 2 == 0 ? hexString : "0" + hexString;
            byte[] bytes = new byte[workHexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                // そのままByte.parseByteすると0x80以上のときにオーバーフローしてしまう
                // 一旦大きい型にパースしてからbyteにキャストすることで、0x80以上の値を負の値として入れ込む
                // Byte.parseByte as it is It overflows when 0x80 or more
                // By parsing to a larger type and then casting it to byte, insert a value of 0x80 or more as a negative value
                string hex2Characters = CommandBuilder.JavaStyleSubstring(workHexString, i * 2, i * 2 + 2);
                short number = short.Parse(hex2Characters, NumberStyles.HexNumber);
                bytes[i] = (byte)number;
            }
            return bytes;
        }

        /**
         * バイトのリストから16進数の文字列に変換する
         * Convert from byte list to hexadecimal character string
         * @param bytes バイトのリスト List of bytes
         * @return バイトのリストをもとにした16進数の文字列 Hexadecimal character string based on the list of bytes
         */
        private static string BytesToHexString(byte[] bytes)
        {
            StringBuilder hexStringBuilder = new StringBuilder();
            foreach (byte byteNumber in bytes)
            {
                string hex2Characters = byteNumber.ToString("X2");
                hexStringBuilder.Append(hex2Characters);
            }

            return hexStringBuilder.ToString();
        }

        #endregion


        #region Tag UII class

        /**
         * タグのUIIを表すクラス
         * Class representing UII of tag
         * タグのUIIには範囲および16進数表記の制約があるため、ラップして表現する
         * Since the UII of the tag has constraints of range and hexadecimal notation, it wraps and expresses it
         */
        private class TagUII
        {

            private static char[] hexCharacters =
                    {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

            // 16進数のUII値
            // UIIは256ビット分( (0or1) * 256 )に及ぶため、文字列で指定する
            // Hexadecimal UII value
            // Since UII extends over 256 bits((0or1) * 256), it is specified by a character string
            private string hexString;

            // バイトリストのUII値
            // 最高位バイトからリストに入れていく
            // 例えば文字列が"ABC"の場合、{0xA, 0xBC} になる
            // UII value of byte list
            // Put in the list from the highest byte
            // For example, when the character string is "ABC", it becomes {0 × A, 0 × BC }
            private byte[] bytes;

            /**
             * 16進数の文字列をもとにタグのUIIを返す
             * Returns tag UII based on hexadecimal character string
             * @param hexString 16進数の文字列 Hexadecimal character string
             * @return 指定した16進数の文字列をもとにしたタグのUII UII of the tag based on the specified hexadecimal character string
             * @throws NotCapitalException 指定した文字列の形式に小文字が含まれている場合
             * @throws NotHexException 指定した文字列の形式が16進数でない場合 When the format of the specified character string is not a hexadecimal number
             * @throws OverflowBitException 指定した文字列のビット数が規定のビット数を超えている場合 When the number of bits of the specified character string exceeds the specified number of bits
             */
            public static TagUII ValueOf(String hexString)
            {
                if (hexString.Any(char.IsLower))
                {
                    throw new NotCapitalException();
                }
                if (!CheckHexString(hexString))
                {
                    throw new NotHexException();
                }
                if (hexString.Length > 64)
                {
                    // 256ビットまで入力可能、16進数の64桁分は256ビット分にあたる 
                    // Up to 256 bits can be input, and 64 digits of hexadecimal correspond to 256 bits
                    throw new OverflowBitException(256);
                }

                return new TagUII(hexString);
            }

            /**
             * バイトリストをもとにタグのUIIを返す
             * Return tag UII based on byte list 
             * @param bytes バイトリスト Byte list
             * @return 指定したバイトリストをもとにしたタグのUII The UII of the tag based on the specified byte list
             * @throws OverflowBitException 指定したバイトリストのビット数が規定のビット数を超えている場合 When the number of bits in the specified byte list exceeds the specified number of bits
             */
            public static TagUII ValueOf(byte[] bytes)
            {
                if (bytes.Length > 32)
                {
                    // 256ビットまで入力可能。1バイトは8ビットの為、32バイトは256ビットにあたる
                    // Up to 256 bits can be input. Since 1 byte is 8 bits, 32 bytes correspond to 256 bits
                    throw new OverflowBitException(256);
                }

                return new TagUII(bytes);
            }

            /**
             * 16進数の文字列から初期化する
             * Initialize from hexadecimal character string
             * @param hexString 16進数の文字列 Hexadecimal character string
             */
            private TagUII(String hexString)
            {
                this.hexString = hexString;

                // バイトのリストを求める
                // Find a list of bytes
                bytes = HexStringToBytes(hexString);
            }

            /**
             * バイトリストから初期化する
             * Initialize from byte list
             * @param bytes バイトリスト byte list
             */
            private TagUII(byte[] bytes)
            {
                this.bytes = bytes;

                // 16進数の文字列を求める
                // Find a hexadecimal character string
                hexString = BytesToHexString(bytes);
            }

            /**
             * バイトのリストとして返す
             * Return as a list of bytes
             * @return バイトのリスト list of bytes
             */
            public byte[] GetBytes()
            {
                return bytes;
            }

            /**
             * 16進数の文字列として返す
             * Return as hexadecimal character string
             * @return 16進数の文字列 Hexadecimal character string
             */
            public String GetHexString()
            {
                return hexString;
            }

            /**
             * 16進数の文字列であるか検証する
             * Verify whether it is a hexadecimal character string
             * @param string 対象となる文字列 Target character string
             * @return 16進数の文字列であればtrue、そうでなければfalseを返す Returns true if it is a hexadecimal character string, false otherwise
             */
            private static bool CheckHexString(string IN_string)
            {
                for (int i = 0; i < IN_string.Length; i++)
                {
                    char character = IN_string.ElementAt(i);

                    if (!CheckHexCharacter(character))
                    {
                        return false;
                    }
                }

                return true;
            }

            /**
             * 16進数の文字であるか検証する
             * Verify that it is a hexadecimal character
             * @param character 対象となる文字 Characters of interest
             * @return 16進数の文字であればtrue、そうでなければfalseを返す  Returns true if it is a hexadecimal character, false otherwise
             */
            private static bool CheckHexCharacter(char character)
            {
                foreach (char hexCharacter in hexCharacters)
                {
                    if (character == hexCharacter)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /**
         * ビット数がオーバーフローしている場合にこの例外がスローされる
         * This exception is thrown if the number of bits is overflowing
         */
        private class OverflowBitException : Exception
        {

            /**
             * ビット数から初期化する
             * Initialize from the number of bits
             * @param bitNumber ビット数 Number of bits
             */
            public OverflowBitException(int bitNumber) : base(string.Format(CultureInfo.CurrentCulture, "指定できる値は %d bitまでです。", bitNumber))
            {

            }
        }

        private class NotCapitalException : Exception
        {
            /**
             * 初期化する
             */
            public NotCapitalException() : base("指定できる値は大文字でなければいけません。")
            {

            }
        }

        /**
         * 数値が16進数でない場合にこの例外がスローされる
         * This exception is thrown if the number is not a hexadecimal number
         */
        private class NotHexException : Exception
        {
            /**
             * 初期化する
             * initialize
             */
            public NotHexException() : base("指定できる値は16進数でなければいけません。")
            {

            }
        }

        #endregion

        #region Enums

        /**
         * Read出力値の段階(1～5段階)
         * Read output stage (1 to 5 steps)
         */
        private enum ReadPowerStage
        {
            STAGE_1
            , STAGE_2
            , STAGE_3
            , STAGE_4
            , STAGE_5
        }

        /**
         * Read出力値に対応する段階を返す
         * @param readPowerLevel Read出力値 
         * @return Read出力値に対応する段階 引数にnullを指定した場合はnullを返す
         * 
         * Returns the stage corresponding to the Read output value
         * @param readPowerLevel Read output 
         * @return Step corresponding to Read output value. If null is specified as an argument, it returns null.
         * 
         */
        private ReadPowerStage? GetReadPowerStage(float? readPowerLevel)
        {
            if (readPowerLevel == null)
            {
                return null;
            }

            if (readPowerLevel <= m_hCommonBase.fStage5_Max_Read_Power_Level)
            {
                return ReadPowerStage.STAGE_5;
            }
            else if (readPowerLevel <= m_hCommonBase.fStage4_Max_Read_Power_Level)
            {
                return ReadPowerStage.STAGE_4;
            }
            else if (readPowerLevel <= m_hCommonBase.fStage3_Max_Read_Power_Level)
            {
                return ReadPowerStage.STAGE_3;
            }
            else if (readPowerLevel <= m_hCommonBase.fStage2_Max_Read_Power_Level)
            {
                return ReadPowerStage.STAGE_2;
            }
            else
            {
                return ReadPowerStage.STAGE_1;
            }
        }

        /**
         * タグのマッチング方法
         * Tag matching method
         */
        private enum MatchingMethod
        {
            FORWARD
            , BACKWARD
        }

        /**
         * タグを見つける上での状態
         * Status on finding tags
         */
        private enum LocateTagState
        {
            STANDBY                 // 待機中 waiting 
            , READING_SEARCH_TAG    // 検索用のタグを読み込み中 Loading search tags
            , SEARCHING_TAG         // タグを検索中 Searching for tags
        }

        /**
         * タグを見つける上でのアクション
         * Action on finding tags
         */
        private class LocateTagAction
        {
            public enum LocateTagActionType
            {
                READ_SEARCH_TAG      // 検索用のタグを読み込む read search tag
                , SEARCH_TAG         // タグを検索する search tag
                , STOP               // アクションを停止する stop action
            }

            public LocateTagAction(LocateTagActionType IN_ReadActionType)
            {
                m_ReadActionType = IN_ReadActionType;
            }

            public LocateTagActionType m_ReadActionType { get; set; }

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
                    case LocateTagActionType.READ_SEARCH_TAG:
                        return Properties.Resource.read;
                    case LocateTagActionType.SEARCH_TAG:
                        return Properties.Resource.search;
                    case LocateTagActionType.STOP:
                        return Properties.Resource.stop;
                    default:
                        throw new ArgumentException();
                }
            }
        }
        #endregion

        private void text_search_tag_uii_value_Completed(object sender, EventArgs e)
        {
            string strInput = text_search_tag_uii_value.Text;

            // 空文字列の場合はnullを設定する
            // set to null for an empty string 
            if (strInput == "" || strInput == null)
            {
                SetSearchTagUII(null);
                SetEnableSearchTag(false);
                return;
            }

            // 数値を検証する
            // Validate numerical values
            TagUII uii;
            try
            {
                uii = TagUII.ValueOf(strInput);
            }
            catch (NotCapitalException)
            {
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_FILTER_NOT_CAPITAL);
                SetEnableSearchTag(false);
                return;
            }
            catch (NotHexException)
            {
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_FILTER_INVALID_PATTERN);
                SetEnableSearchTag(false);
                return;
            }
            catch (OverflowBitException)
            {
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_FILTER_OUT_OF_RANGE_PATTERN);

                SetEnableSearchTag(false);
                return;
            }

            // 数値が正常値なので設定する
            // Set numerical value as normal value
            SetSearchTagUII(uii);

            if (scannerConnectedOnCreate)
            {
                SetEnableSearchTag(true);
            }
        }

        private void text_search_tag_uii_value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // Enterキーが押された場合
            {
                text_search_tag_uii_value_Completed(sender, e);
            }
        }

        private void button_setting_Clicked(object sender, EventArgs e)
        {
            // リスナーを登録解除してから遷移する
            // Transition another screen after removing listener
            if (scannerConnectedOnCreate)
            {
                m_hCommonBase.GetCommScanner().GetRFIDScanner().SetDataDelegate(null);
            }

            m_bTransitionToSettingScreen = true;

            mFormPage = new FormLocateTagSettingPage();
            DialogResult result = mFormPage.ShowDialog();

            //切断有り
            if (result == DialogResult.Cancel)
            {
                //切断
                mFormDialogResult = DialogResult.Cancel;

                this.Close();
            }
            else
            {
                OnAppearing();
            }
            mFormPage = null;
        }

        private void Picker_match_direction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? strSelectedItem = "";
            strSelectedItem = (string?)picker_match_direction.SelectedItem;


            if (strSelectedItem == Properties.Resource.forward_match)
            {
                matchingMethod = MatchingMethod.FORWARD;
            }
            else if (strSelectedItem == Properties.Resource.backward_match)
            {
                matchingMethod = MatchingMethod.BACKWARD;
            }
        }
    }
}
