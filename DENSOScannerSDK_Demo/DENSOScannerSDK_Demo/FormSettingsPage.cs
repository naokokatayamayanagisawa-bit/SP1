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
    public partial class FormSettingsPage : Form
    {
        private const String TAG = "FormSettings";

        CommonBase m_hCommonBase = CommonBase.GetInstance();

        private CommScannerParams? commParams;
        private RFIDScannerSettings? rfidSettings;
        private bool isAutoLinkProfileEnabled = false;
        private BarcodeScannerSettings? barcodeSettings;

        // ブザー音量
        // Buzzer volume
        Dictionary<string, CommScannerParams.BuzzerVolume> buzzerVolumeMap = new Dictionary<string, CommScannerParams.BuzzerVolume>();
        // トリガモード
        // Trigger mode
        Dictionary<string, BarcodeScannerSettings.Scan.TriggerMode> triggerModeMap = new Dictionary<string, BarcodeScannerSettings.Scan.TriggerMode>();
        // 偏波設定
        // Polarization setting
        Dictionary<string, RFIDScannerSettings.Scan.Polarization> polarizationMap = new Dictionary<String, RFIDScannerSettings.Scan.Polarization>();

        // この画面が生成された時に、アプリがスキャナと接続されていたかどうか
        // この画面にいる間に接続が切断された場合でも、この画面の生成時にスキャナと接続されていた場合は通信エラーが出るようにする
        // Whether the application was connected to the scanner when this screen was generated.
        // Even if the connection is disconnected while on this screen, make sure that a communication error occurs when connected to the scanner when generating this screen.
        private bool scannerConnectedOnCreate = false;

        private DialogResult mFormDialogResult;


        public FormSettingsPage()
        {
            InitializeComponent();

            button_navigate_up.Text = Properties.Resource.navigate_up;
            title_settings.Text = Properties.Resource.settings;
            text_scanner_version_head.Text = Properties.Resource.scanner_version;
            text_scanner_version_value.Text = Properties.Resource.number_0_00;

            text_read_power_level_head.Text = Properties.Resource.read_power_level;
            for (int i = 4; i <= 30; i++)
            {
                text_read_power_level_value.Items.Add(i.ToString());
            }
            text_read_power_level_value.SelectedIndex = 0;
            text_read_power_level_unit.Text = Properties.Resource.read_power_level_unit;

            text_session_head.Text = Properties.Resource.session;
            text_session_value.Items.Add(Properties.Resource.session_s0);
            text_session_value.Items.Add(Properties.Resource.session_s1);
            text_session_value.Items.Add(Properties.Resource.session_s2);
            text_session_value.Items.Add(Properties.Resource.session_s3);

            text_session_value.SelectedIndex = 0;

            text_report_unique_tags_head.Text = Properties.Resource.report_unique_tags;

            text_channel_head.Text = Properties.Resource.channel;

            text_channel5.Text = Properties.Resource.channel5;
            text_channel11.Text = Properties.Resource.channel11;
            text_channel17.Text = Properties.Resource.channel17;
            text_channel23.Text = Properties.Resource.channel23;
            text_channel24.Text = Properties.Resource.channel24;
            text_channel25.Text = Properties.Resource.channel25;

            text_q_factor_head.Text = Properties.Resource.q_factor;

            for (int i = 0; i <= 7; i++)
            {
                text_q_factor_value.Items.Add(i.ToString());
            }
            text_q_factor_value.SelectedIndex = 0;

            text_auto_link_profile_head.Text = Properties.Resource.auto_link_profile;

            text_link_profile_head.Text = Properties.Resource.link_profile;

            text_link_profile_value.Items.Add(Properties.Resource.link_profile_1);
            text_link_profile_value.Items.Add(Properties.Resource.link_profile_4);
            text_link_profile_value.Items.Add(Properties.Resource.link_profile_5);
            text_link_profile_value.SelectedIndex = 0;

            text_polarization_head.Text = Properties.Resource.polarization;

            text_polarization_value.Items.Add(Properties.Resource.polarization_vertical);
            text_polarization_value.Items.Add(Properties.Resource.polarization_horizontal);
            text_polarization_value.Items.Add(Properties.Resource.polarization_both);
            text_polarization_value.SelectedIndex = 2;

            text_power_save_head.Text = Properties.Resource.power_save;
            text_buzzer_head.Text = Properties.Resource.buzzer;
            text_buzzer_volume_head.Text = Properties.Resource.buzzer_volume;

            text_buzzer_volume_value.Items.Add(Properties.Resource.buzzer_volume_low);
            text_buzzer_volume_value.Items.Add(Properties.Resource.buzzer_volume_middle);
            text_buzzer_volume_value.Items.Add(Properties.Resource.buzzer_volume_loud);
            text_buzzer_volume_value.SelectedIndex = 2;

            text_barcode_head.Text = Properties.Resource.barcode_uppercase;
            text_trigger_mode_head.Text = Properties.Resource.trigger_mode;

            text_trigger_mode_value.Items.Add(Properties.Resource.trigger_mode_auto_off);
            text_trigger_mode_value.Items.Add(Properties.Resource.trigger_mode_momentary);
            text_trigger_mode_value.Items.Add(Properties.Resource.trigger_mode_alternate);
            text_trigger_mode_value.Items.Add(Properties.Resource.trigger_mode_continuous);
            text_trigger_mode_value.Items.Add(Properties.Resource.trigger_mode_trigger_release);

            text_trigger_mode_value.SelectedIndex = 0;

            text_enable_all_1d_codes_head.Text = Properties.Resource.enable_all_1d_codes;
            text_enable_all_2d_codes_head.Text = Properties.Resource.enable_all_2d_codes;

            // 画像ファイルを指定してImageプロパティを設定
            image_scanner_battery.Image = Properties.Resource.battery_0; // 画像ファイルのパスを指定
        }

        private void FormSettingsPage_Load(object sender, EventArgs e)
        {
            mFormDialogResult = DialogResult.OK;

            m_hCommonBase.OnScannerClosedDelegateStackPush(OnScannerClosedFormPage);

            scannerConnectedOnCreate = m_hCommonBase.IsScannerConnected();

            bool result = true;

            if (scannerConnectedOnCreate)
            {
                // SP1の情報を読み込む
                // Read information on SP1
                result = LoadScannerInfo();
            }
            else
            {
                // SP1が見つからなかったときはエラーメッセージ表示
                // Show error message if SP1 is not found.
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_NO_CONNECTION);
                result = false; // 未接続 disconnected
            }

            // 設定値を読み込む
            // Read the setting value
            if (result == true)
            {
                LoadSettings();
            }
        }

        public void OnScannerClosedFormPage()
        {
            //切断
            mFormDialogResult = DialogResult.Cancel;

            this.Invoke(new Action(() => this.Close()));
        }

        private void FormSettingsPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            NavigateUp();

            m_hCommonBase.OnScannerClosedDelegateStackPop();

            this.DialogResult = mFormDialogResult;
        }

        private void button_navigate_up_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /**
         * 画面遷移でいう上の階層に移動する
         * Move to the upper level in the screen transition.
         */
        private void NavigateUp()
        {
            // SP1設定値送信・保存
            // SP1 Send / save value
            Save();
        }

        /**
         * スキャナーの情報を読み込む
         * Read information on scanner.
         */
        private bool LoadScannerInfo()
        {
            // SP1のバージョン取得
            // Obtain version of SP1
            try
            {
                string ver = m_hCommonBase.GetCommScanner().GetVersion();
                text_scanner_version_value.Text = ver.Replace("Ver.", "");
            }
            catch (Exception e)
            {
                return false;
            }

            // SP1のバッテリー残量を読み込む
            // Read the battery level of SP1
            DENSOScannerSDK.CommConst.CommBattery? battery = null;
            try
            {
                battery = m_hCommonBase.GetCommScanner().GetRemainingBattery();
            }
            catch (CommException e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
            if (battery != null)
            {
                switch (battery)
                {
                    case DENSOScannerSDK.CommConst.CommBattery.UNDER10:
                        image_scanner_battery.Image = Properties.Resource.battery_1;
                        break;
                    case DENSOScannerSDK.CommConst.CommBattery.UNDER40:
                        image_scanner_battery.Image = Properties.Resource.battery_2;
                        break;
                    case DENSOScannerSDK.CommConst.CommBattery.OVER40:
                        image_scanner_battery.Image = Properties.Resource.battery_full;
                        break;
                }
            }
            else
            {
                //showMessage("バッテリー残量取得失敗");
                return false;
            }

            return true;
        }

        /**
         * 設定値の読み込み
         * SharedPreferencesからアプリ内に保存している設定値を読み込み、各UIに適用する
         * Load setting
         * Load the setting values saved in the application from SharedPreferences and apply them to each UI
         */
        private void LoadSettings()
        {
            // Map作成
            // Create Map
            SetMap();

            try
            {
                // RFID関連設定値を取得
                // Acquire RFID related setting value
                rfidSettings = m_hCommonBase.GetCommScanner().GetRFIDScanner().GetSettings();

                // 共通パラメータの設定値を取得
                // Acquire common parameter setting value
                commParams = m_hCommonBase.GetCommScanner().GetParams();

                // バーコード関連設定値を取得
                // Acquire bar code related setting value
                barcodeSettings = m_hCommonBase.GetCommScanner().GetBarcodeScanner().GetSettings();

            }
            catch (Exception e)
            {
                if (e is CommException || e is RFIDException || e is BarcodeException)
                {
                    this.rfidSettings = null;
                    this.commParams = null;
                    this.barcodeSettings = null;
                    AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                    return;
                }
            }

            try
            {
                //Auto Link Profile
                isAutoLinkProfileEnabled = m_hCommonBase.GetCommScanner().GetRFIDScanner().GetAutoLinkProfile();
            }
            catch (Exception e)
            {
                if (e is RFIDException && !(((RFIDException)e).GetErrorCode() == DENSOScannerSDK.ExceptionError.ErrorCode.NOT_SUPPORT_COMMAND))
                {
                    AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                    return;
                }
            }

            // [RFID関連設定値] [RFID related setting value]
            // read_power_level
            text_read_power_level_value.SelectedItem = rfidSettings.mScan.mPowerLevelRead.ToString();

            // session
            text_session_value.SelectedItem = rfidSettings.mScan.mSessionFlag.ToString();

            // report unigue tags
            bool unigue = (true == this.rfidSettings.mScan.mDoubleReading.Equals(RFIDScannerSettings.Scan.DoubleReading.PREVENT1)) ? true : false;
            checkbox_report_unique_tags.Checked = unigue;

            // channel5 : 0x00000001
            bool onoff = ((this.rfidSettings.mScan.mChannel & 0x00000001) > 0) ? true : false;
            checkbox_channel5.Checked = onoff;

            // channel11 : 0x00000002
            onoff = ((this.rfidSettings.mScan.mChannel & 0x00000002) > 0) ? true : false;
            checkbox_channel11.Checked = onoff;

            // channel17 : 0x00000004
            onoff = ((this.rfidSettings.mScan.mChannel & 0x00000004) > 0) ? true : false;
            checkbox_channel17.Checked = onoff;

            // channel23 : 0x00000008
            onoff = ((this.rfidSettings.mScan.mChannel & 0x00000008) > 0) ? true : false;
            checkbox_channel23.Checked = onoff;

            // channel24 : 0x00000010
            onoff = ((this.rfidSettings.mScan.mChannel & 0x00000010) > 0) ? true : false;
            checkbox_channel24.Checked = onoff;

            // channel25 : 0x00000020
            onoff = ((this.rfidSettings.mScan.mChannel & 0x00000020) > 0) ? true : false;
            checkbox_channel25.Checked = onoff;

            // q_factor
            text_q_factor_value.SelectedItem = rfidSettings.mScan.mQParam.ToString();

            //auto_link_profile
            switch_auto_link_profile.Checked = isAutoLinkProfileEnabled;

            // link_profile
            // valueOf?
            text_link_profile_value.SelectedItem = rfidSettings.mScan.mLinkProfile.ToString();

            // polarization
            string strPolarization = "";
            if (this.polarizationMap.ContainsValue(this.rfidSettings.mScan.mPolarization) == true)
            {
                foreach (KeyValuePair<string, RFIDScannerSettings.Scan.Polarization> pEntry in polarizationMap)
                {
                    if (pEntry.Value.Equals(rfidSettings.mScan.mPolarization))
                    {
                        strPolarization = pEntry.Key;
                        break;
                    }
                }
            }

            text_polarization_value.SelectedItem = strPolarization;

            // [共通パラメータの設定値] [Common parameter setting value]
            // power_save
            checkbox_power_save.Checked = commParams.mPowerSave;

            // buzzer
            onoff = (this.commParams.mNotification.mSound.mBuzzer.Equals(CommScannerParams.Notification.Sound.Buzzer.ENABLE)) ? true : false;
            checkbox_buzzer.Checked = onoff;

            // buzzer_volumes
            string strBuzzerVolume = "";
            if (buzzerVolumeMap.ContainsValue(commParams.mBuzzerVolume) == true)
            {
                foreach (KeyValuePair<string, CommScannerParams.BuzzerVolume> pEntry in buzzerVolumeMap)
                {
                    if (pEntry.Value.Equals(commParams.mBuzzerVolume))
                    {
                        strBuzzerVolume = pEntry.Key;
                        break;
                    }
                }
            }

            text_buzzer_volume_value.SelectedItem = strBuzzerVolume;

            // [バーコード関連設定値] [Bar code related setting value]
            // trigger_mode
            String strTriggerMode = "";
            if (triggerModeMap.ContainsValue(this.barcodeSettings.mScan.mTriggerMode) == true)
            {
                foreach (KeyValuePair<string, BarcodeScannerSettings.Scan.TriggerMode> pEntry in triggerModeMap)
                {
                    if (pEntry.Value.Equals(barcodeSettings.mScan.mTriggerMode))
                    {
                        strTriggerMode = pEntry.Key;
                        break;
                    }
                }
            }

            text_trigger_mode_value.SelectedItem = strTriggerMode;

            // enable_all_1d_codes
            onoff = CheckEnable1dCodes(barcodeSettings);
            checkbox_enable_all_1d_codes.Checked = onoff;

            // enable_all_2d_codes
            onoff = CheckEnable2dCodes(barcodeSettings);
            checkbox_enable_all_2d_codes.Checked = onoff;

        }

        /**
         * Map作成
         * Create Map
         * @param
         */
        private void SetMap()
        {
            // ブザー音量Map
            // Buzzer volume map
            buzzerVolumeMap.Add(Properties.Resource.buzzer_volume_low, CommScannerParams.BuzzerVolume.LOW);
            buzzerVolumeMap.Add(Properties.Resource.buzzer_volume_middle, CommScannerParams.BuzzerVolume.MIDDLE);
            buzzerVolumeMap.Add(Properties.Resource.buzzer_volume_loud, CommScannerParams.BuzzerVolume.LOUD);

            // バーコードトリガモードMap
            // bar code trigger mode map.
            triggerModeMap.Add(Properties.Resource.trigger_mode_auto_off, BarcodeScannerSettings.Scan.TriggerMode.AUTO_OFF);
            triggerModeMap.Add(Properties.Resource.trigger_mode_momentary, BarcodeScannerSettings.Scan.TriggerMode.MOMENTARY);
            triggerModeMap.Add(Properties.Resource.trigger_mode_alternate, BarcodeScannerSettings.Scan.TriggerMode.ALTERNATE);
            triggerModeMap.Add(Properties.Resource.trigger_mode_continuous, BarcodeScannerSettings.Scan.TriggerMode.CONTINUOUS);
            triggerModeMap.Add(Properties.Resource.trigger_mode_trigger_release, BarcodeScannerSettings.Scan.TriggerMode.TRIGGER_RELEASE);

            // 偏波設定Map
            // Polarization setting map
            polarizationMap.Add(Properties.Resource.polarization_vertical, RFIDScannerSettings.Scan.Polarization.V);
            polarizationMap.Add(Properties.Resource.polarization_horizontal, RFIDScannerSettings.Scan.Polarization.H);
            polarizationMap.Add(Properties.Resource.polarization_both, RFIDScannerSettings.Scan.Polarization.Both);
        }

        /**
         * 1次元バーコードの読取許可・禁止を設定する
         * Setting permission / prohibition of reading of one-dimensional barcode
         * @param settings　バーコード設定 Bar code setting
         */
        private bool CheckEnable1dCodes(BarcodeScannerSettings settings)
        {
            // EAN-13 UPC-A
            if (settings.mDecode.mSymbologies.mEan13upcA.mEnabled == false)
            {
                return false;
            }
            // EAN-8
            if (settings.mDecode.mSymbologies.mEan8.mEnabled == false)
            {
                return false;
            }
            // UPC-E
            if (settings.mDecode.mSymbologies.mUpcE.mEnabled == false)
            {
                return false;
            }
            // ITF
            if (settings.mDecode.mSymbologies.mItf.mEnabled == false)
            {
                return false;
            }
            // STF
            if (settings.mDecode.mSymbologies.mStf.mEnabled == false)
            {
                return false;
            }
            // Codabar
            if (settings.mDecode.mSymbologies.mCodabar.mEnabled == false)
            {
                return false;
            }
            // Code39
            if (settings.mDecode.mSymbologies.mCode39.mEnabled == false)
            {
                return false;
            }
            // Code93
            if (settings.mDecode.mSymbologies.mCode93.mEnabled == false)
            {
                return false;
            }
            // Code128
            if (settings.mDecode.mSymbologies.mCode128.mEnabled == false)
            {
                return false;
            }
            // GS1 Databar
            if (settings.mDecode.mSymbologies.mGs1DataBar.mEnabled == false)
            {
                return false;
            }
            // GS1 Databar Limited
            if (settings.mDecode.mSymbologies.mGs1DataBarLimited.mEnabled == false)
            {
                return false;
            }
            // GS1 Databar Expanded
            if (settings.mDecode.mSymbologies.mGs1DataBarExpanded.mEnabled == false)
            {
                return false;
            }

            return true;
        }

        /**
         * 2次元バーコードの読取許可・禁止を設定する
         * Setting permission / prohibition of reading of two-dimensional barcode
         * @param settings　バーコード設定 Bar code setting
         */
        private bool CheckEnable2dCodes(BarcodeScannerSettings settings)
        {
            // QR Code
            if (settings.mDecode.mSymbologies.mQrCode.mEnabled == false)
            {
                return false;
            }
            // QR Code.Model1
            if (settings.mDecode.mSymbologies.mQrCode.mModel1.mEnabled == false)
            {
                return false;
            }
            // QR Code.Model2
            if (settings.mDecode.mSymbologies.mQrCode.mModel2.mEnabled == false)
            {
                return false;
            }
            // rMQR
            if (settings.mDecode.mSymbologies.mRMQR.mEnabled == false)
            {
                return false;
            }
            // QR Code.Micro QR
            if (settings.mDecode.mSymbologies.mMicroQr.mEnabled == false)
            {
                return false;
            }
            // iQR Code
            if (settings.mDecode.mSymbologies.mIqrCode.mEnabled == false)
            {
                return false;
            }
            // iQR Code.Square
            if (settings.mDecode.mSymbologies.mIqrCode.mSquare.mEnabled == false)
            {
                return false;
            }
            // iQR Code.Rectangle
            if (settings.mDecode.mSymbologies.mIqrCode.mRectangle.mEnabled == false)
            {
                return false;
            }
            // Data Matrix
            if (settings.mDecode.mSymbologies.mDataMatrix.mEnabled == false)
            {
                return false;
            }
            // Data Matrix.Square
            if (settings.mDecode.mSymbologies.mDataMatrix.mSquare.mEnabled == false)
            {
                return false;
            }
            // Data Matrix.Rectangle
            if (settings.mDecode.mSymbologies.mDataMatrix.mRectangle.mEnabled == false)
            {
                return false;
            }
            // PDF417
            if (settings.mDecode.mSymbologies.mPdf417.mEnabled == false)
            {
                return false;
            }
            // Micro PDF417
            if (settings.mDecode.mSymbologies.mMicroPdf417.mEnabled == false)
            {
                return false;
            }
            // Maxi Code
            if (settings.mDecode.mSymbologies.mMaxiCode.mEnabled == false)
            {
                return false;
            }
            // GS1 Composite
            if (settings.mDecode.mSymbologies.mGs1Composite.mEnabled == false)
            {
                return false;
            }

            return true;
        }

        /**
         *　RFIDScannerSettingsの設定値をコマンド送信する(コマンド：setSettings)
         *　Send command set value of RFIDS scanner settings (command: setSettings)
         */
        private void SendRFIDScannerSettings()
        {
            try
            {
                // powerLevelRead
                rfidSettings.mScan.mPowerLevelRead = int.Parse((string)(text_read_power_level_value.SelectedItem));

                // session
                string strSelectedItem = "";
                strSelectedItem = (string)(text_session_value.SelectedItem);
                if (strSelectedItem == Properties.Resource.session_s0)
                {
                    rfidSettings.mScan.mSessionFlag = RFIDScannerSettings.Scan.SessionFlag.S0;
                }
                else if (strSelectedItem == Properties.Resource.session_s1)
                {
                    rfidSettings.mScan.mSessionFlag = RFIDScannerSettings.Scan.SessionFlag.S1;
                }
                else if (strSelectedItem == Properties.Resource.session_s2)
                {
                    rfidSettings.mScan.mSessionFlag = RFIDScannerSettings.Scan.SessionFlag.S2;
                }
                else if (strSelectedItem == Properties.Resource.session_s3)
                {
                    rfidSettings.mScan.mSessionFlag = RFIDScannerSettings.Scan.SessionFlag.S3;
                }
                rfidSettings.mScan.mTriggerMode = RFIDScannerSettings.Scan.TriggerMode.CONTINUOUS1;

                // report unigue tags
                RFIDScannerSettings.Scan.DoubleReading doubleReading;
                if (checkbox_report_unique_tags.Checked == true)
                {
                    doubleReading = RFIDScannerSettings.Scan.DoubleReading.PREVENT1;
                }
                else
                {
                    doubleReading = RFIDScannerSettings.Scan.DoubleReading.Free;
                }
                rfidSettings.mScan.mDoubleReading = doubleReading;

                // channel(channel5～25)
                long channel = 0L;
                // channel5
                if (checkbox_channel5.Checked == true)
                {
                    channel += 1L;
                }
                // channel11
                if (checkbox_channel11.Checked == true)
                {
                    channel += 2L;
                }
                // channel17
                if (checkbox_channel17.Checked == true)
                {
                    channel += 4L;
                }
                // channel23
                if (checkbox_channel23.Checked == true)
                {
                    channel += 8L;
                }
                // channel24
                if (checkbox_channel24.Checked == true)
                {
                    channel += 16L;
                }
                // channel25
                if (checkbox_channel25.Checked == true)
                {
                    channel += 32L;
                }
                this.rfidSettings.mScan.mChannel = channel;

                // q factor
                rfidSettings.mScan.mQParam = short.Parse((string)text_q_factor_value.SelectedItem);

                // link profile
                rfidSettings.mScan.mLinkProfile = short.Parse((string)text_link_profile_value.SelectedItem);

                // polarization
                string? strSelectedPolarization = (string?)text_polarization_value.SelectedItem;
                if (polarizationMap.ContainsKey(strSelectedPolarization) == true)
                {
                    rfidSettings.mScan.mPolarization = polarizationMap[strSelectedPolarization];
                }

                // SP1設定値送信
                // Send setting value to SP1
                m_hCommonBase.GetCommScanner().GetRFIDScanner().SetSettings(this.rfidSettings);
            }
            catch (Exception e)
            {
                if (e is CommException || e is RFIDException)
                {
                    throw;
                }
            }
        }

        /**
         *　AutoLinkProfileの設定値をコマンド送信する(コマンド：setAutoLinkProfile)
         *　Send command set value of Auto Link Profile (command: setAutoLinkProfile)
         */
        private void SendAutoLinkProfile()
        {
            try
            {
                isAutoLinkProfileEnabled = switch_auto_link_profile.Checked;

                // SP1設定値送信
                // Send setting value to SP1
                m_hCommonBase.GetCommScanner().GetRFIDScanner().SetAutoLinkProfile(isAutoLinkProfileEnabled);
            }
            catch (Exception e)
            {
                if (e is CommException || e is RFIDException)
                {
                    throw;
                }
            }
        }

        /**
         * CommScannerParamsの設定値をコマンド送信する(コマンド：setParams, saveParams)
         * Send command of CommScannerParams setting value (command：setParams, saveParams)
         */
        private void SendCommScannerParams()
        {
            try
            {
                // powerSave
                commParams.mPowerSave = checkbox_power_save.Checked;

                // settings(オートパワーオフ)の設定
                // powerSaveとリンクさせる
                // auto power off setting
                // Link with powerSave

                // buzzer
                CommScannerParams.Notification.Sound.Buzzer buzzer;
                if (checkbox_buzzer.Checked == true)
                {
                    buzzer = CommScannerParams.Notification.Sound.Buzzer.ENABLE;
                }
                else
                {
                    buzzer = CommScannerParams.Notification.Sound.Buzzer.DISABLE;
                }

                commParams.mNotification.mSound.mBuzzer = buzzer;

                // buzzer Volume
                string? strSelectedItem = (string?)(text_buzzer_volume_value.SelectedItem);

                if (buzzerVolumeMap.ContainsKey(strSelectedItem) == true)
                {
                    commParams.mBuzzerVolume = buzzerVolumeMap[strSelectedItem];
                }

                // SP1設定値送信
                // Send setting value to SP1
                m_hCommonBase.GetCommScanner().SetParams(this.commParams);

                // SP1設定値保存
                // Save SP1 setting value
                m_hCommonBase.GetCommScanner().SaveParams();
            }
            catch (CommException e)
            {
                throw;
            }
        }

        /**
         * BarcodeScannerSettingsの設定値をコマンド送信する(コマンド：setSettings)
         * Send the setting value of BarcodeScannerSettings command (command: setSettings)
         */
        private void SendBarcodeScannerSettings()
        {
            try
            {
                // Trigger mode
                string? strSelectedItem = (string?)(text_trigger_mode_value.SelectedItem);
                if (triggerModeMap.ContainsKey(strSelectedItem) == true)
                {
                    this.barcodeSettings.mScan.mTriggerMode = this.triggerModeMap[strSelectedItem];
                }

                // enable_all_1d_codes
                bool enable1dFlg = checkbox_enable_all_1d_codes.Checked;

                // enable_all_2d_codes
                bool enable2dFlg = checkbox_enable_all_2d_codes.Checked;

                // SP1設定値送信
                // Send setting value to SP1
                SetEnable1dCodes(this.barcodeSettings, enable1dFlg);
                SetEnable2dCodes(this.barcodeSettings, enable2dFlg);
                m_hCommonBase.GetCommScanner().GetBarcodeScanner().SetSettings(this.barcodeSettings);
            }
            catch (Exception e)
            {
                if (e is CommException || e is BarcodeException)
                {
                    throw;
                }
            }
        }

        /**
         * 1次元バーコードの読取許可・禁止を設定する
         * Setting permission / prohibition of reading of one-dimensional barcode
         * @param settings　バーコード設定 Bar code setting
         * @param enable1dFlg　許可・禁止  permission / prohibition
         */
        private void SetEnable1dCodes(BarcodeScannerSettings settings, bool enable1dFlg)
        {
            // EANコードはチェックの有無によらず常に許可する
            // Always allow EAN code regardless of checking
            settings.mDecode.mSymbologies.mEan13upcA.mEnabled = true; // EAN-13 UPC-A
            settings.mDecode.mSymbologies.mEan8.mEnabled = true; // EAN-8
            settings.mDecode.mSymbologies.mUpcE.mEnabled = enable1dFlg; // UPC-E
            settings.mDecode.mSymbologies.mItf.mEnabled = enable1dFlg; // ITF
            settings.mDecode.mSymbologies.mStf.mEnabled = enable1dFlg; // STF
            settings.mDecode.mSymbologies.mCodabar.mEnabled = enable1dFlg; // Codabar
            settings.mDecode.mSymbologies.mCode39.mEnabled = enable1dFlg; // Code39
            settings.mDecode.mSymbologies.mCode93.mEnabled = enable1dFlg; // Code93
            settings.mDecode.mSymbologies.mCode128.mEnabled = enable1dFlg; // Code128
            settings.mDecode.mSymbologies.mMsi.mEnabled = enable1dFlg; // MSI
            settings.mDecode.mSymbologies.mGs1DataBar.mEnabled = enable1dFlg; // GS1 Databar
            settings.mDecode.mSymbologies.mGs1DataBarLimited.mEnabled = enable1dFlg; // GS1 Databar Limited
            settings.mDecode.mSymbologies.mGs1DataBarExpanded.mEnabled = enable1dFlg; // GS1 Databar Expanded
        }

        /**
         * 2次元バーコードの読取許可・禁止を設定する
         * Setting permission / prohibition of reading of two-dimensional barcode
         * @param settings　バーコード設定 Bar code setting
         * @param enable1dFlg　許可・禁止  permission / prohibition
         */
        private void SetEnable2dCodes(BarcodeScannerSettings settings, bool enable2dFlg)
        {
            settings.mDecode.mSymbologies.mQrCode.mEnabled = enable2dFlg;   // QR Code
            settings.mDecode.mSymbologies.mQrCode.mModel1.mEnabled = enable2dFlg;    // QR Code.Model1
            settings.mDecode.mSymbologies.mQrCode.mModel2.mEnabled = enable2dFlg;    // QR Code.Model2
            settings.mDecode.mSymbologies.mRMQR.mEnabled = enable2dFlg; // rMQR
            settings.mDecode.mSymbologies.mMicroQr.mEnabled = enable2dFlg; // QR Code.Micro QR
            settings.mDecode.mSymbologies.mIqrCode.mEnabled = enable2dFlg; // iQR Code
            settings.mDecode.mSymbologies.mIqrCode.mSquare.mEnabled = enable2dFlg; // iQR Code.Square
            settings.mDecode.mSymbologies.mIqrCode.mRectangle.mEnabled = enable2dFlg; // iQR Code.Rectangle
            settings.mDecode.mSymbologies.mDataMatrix.mEnabled = enable2dFlg; // Data Matrix
            settings.mDecode.mSymbologies.mDataMatrix.mSquare.mEnabled = enable2dFlg; // Data Matrix.Square
            settings.mDecode.mSymbologies.mDataMatrix.mRectangle.mEnabled = enable2dFlg; // Data Matrix.Rectangle
            settings.mDecode.mSymbologies.mPdf417.mEnabled = enable2dFlg; // PDF417
            settings.mDecode.mSymbologies.mMicroPdf417.mEnabled = enable2dFlg; // Micro PDF417
            settings.mDecode.mSymbologies.mMaxiCode.mEnabled = enable2dFlg; // Maxi Code
            settings.mDecode.mSymbologies.mGs1Composite.mEnabled = enable2dFlg; // GS1 Composite
            settings.mDecode.mSymbologies.mPlessey.mEnabled = enable2dFlg;  // Plessey
            settings.mDecode.mSymbologies.mAztec.mEnabled = enable2dFlg; // Aztec
        }

        /**
         * SP1設定値送信・保存
         * SP1 Send / save value
         */
        private void Save()
        {
            // Setting画面遷移時にSP1との接続が無かった場合は処理を中断
            // Setting aborts if there is no connection with SP1 at screen transition
            if (!scannerConnectedOnCreate)
            {
                return;
            }

            // SP1設定値送信・保存
            // SP1 Send / save value
            bool failedSettings = this.rfidSettings == null || this.commParams == null || this.barcodeSettings == null;
            Exception? exception = null;
            if (!failedSettings)
            {
                try
                {
                    // スキャナと接続されているときに限り、設定中を示すtoastを表示
                    // Show toast only when connected to scanner
                    if (m_hCommonBase.IsScannerConnected())
                    {
                        AutoMessageBox.ShowMessage(Properties.Resource.I_MSG_PROGRESS_SETTING);
                    }

                    // RFIDScannerSettingsの設定値送信
                    // Send RFIDScannerSettings
                    SendRFIDScannerSettings();

                    // CommScannerParamsの設定値送信
                    // Send CommScannerParams
                    SendCommScannerParams();

                    // BarcodeScannerSettingsの設定値送信
                    // Send BarcodeScannerSettings
                    SendBarcodeScannerSettings();
                }
                catch (Exception e)
                {
                    if (e is CommException || e is RFIDException || e is BarcodeException)
                    {
                        failedSettings = true;
                        exception = e;
                    }
                }

                try
                {
                    // Auto Link Profileの設定値送信
                    // Send Auto Link Profile
                    SendAutoLinkProfile();
                }
                catch (Exception e)
                {
                    if (e is RFIDException && !(((RFIDException)e).GetErrorCode() == DENSOScannerSDK.ExceptionError.ErrorCode.NOT_SUPPORT_COMMAND))
                    {
                        failedSettings = true;
                        exception = e;
                    }
                }
            }

            if (failedSettings)
            {
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_SAVE_SETTINGS);
                if (exception != null)
                {
                    System.Diagnostics.Debug.WriteLine(exception.StackTrace);
                }
                return;
            }

            // SP1のコマンド送受信に時間が掛かるため、待ってから画面を抜ける
            // Since it takes time to send and receive SP1 command, wait and exit the screen
            try
            {
                Thread.Sleep(1500);
            }
            catch (Exception e)
            {

            }

            // 保存完了メッセージ
            // Save complete message
            if (this.commParams != null)
            {
                AutoMessageBox.ShowMessage(Properties.Resource.I_MSG_SAVE_SETTINGS);
            }
        }
    }
}
