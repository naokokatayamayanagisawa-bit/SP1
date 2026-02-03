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
    public partial class FormLocateTagSettingPage : Form
    {
        private const String TAG = "FormLocateTagSettingPage";

        CommonBase m_hCommonBase = CommonBase.GetInstance();

        // 本体に保存していて、アプリを終了後も保持するフィルタ設定値
        // SetFilterが成功したときのみに書き込みを行い、Loadボタン押下時に保持している値をテキストエリアに表示
        SaveSettingsWrapper m_pSaveSettingsWrapper = SaveSettingsWrapper.GetInstance();

        private DialogResult mFormDialogResult;

        public FormLocateTagSettingPage()
        {
            InitializeComponent();

            text_title_locate_tag.Text = Properties.Resource.range_setting_title;

            image_setting_radar.Image = Properties.Resource.locate_tag_setting_radar;

            stage2_max_read_power_level_on_search.Text = Math.Abs(Math.Round((m_hCommonBase.fStage2_Max_Read_Power_Level * 10.0))).ToString();
            stage3_max_read_power_level_on_search.Text = Math.Abs(Math.Round((m_hCommonBase.fStage3_Max_Read_Power_Level * 10.0))).ToString();
            stage4_max_read_power_level_on_search.Text = Math.Abs(Math.Round((m_hCommonBase.fStage4_Max_Read_Power_Level * 10.0))).ToString();
            stage5_max_read_power_level_on_search.Text = Math.Abs(Math.Round((m_hCommonBase.fStage5_Max_Read_Power_Level * 10.0))).ToString();

        }

        private void FormLocateTagSettingPage_Load(object sender, EventArgs e)
        {
            mFormDialogResult = DialogResult.OK;

            m_hCommonBase.OnScannerClosedDelegateStackPush(OnScannerClosedFormPage);
        }

        public void OnScannerClosedFormPage()
        {
            //切断
            mFormDialogResult = DialogResult.Cancel;

            this.Invoke(new Action(() => this.Close()));
        }

        private void FormLocateTagSettingPage_FormClosed(object sender, FormClosedEventArgs e)
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
            if (SaveData() == true)
            {

            }
        }

        private bool SaveData()
        {
            float fStage2 = 0.0f;
            float fStage3 = 0.0f;
            float fStage4 = 0.0f;
            float fStage5 = 0.0f;
            try
            {
                fStage2 = (float)(int.Parse(stage2_max_read_power_level_on_search.Text)) / -10.0f;
                fStage3 = (float)(int.Parse(stage3_max_read_power_level_on_search.Text)) / -10.0f;
                fStage4 = (float)(int.Parse(stage4_max_read_power_level_on_search.Text)) / -10.0f;
                fStage5 = (float)(int.Parse(stage5_max_read_power_level_on_search.Text)) / -10.0f;
            }
            catch (Exception e)
            {
                //Error Message
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_LOCATION_TAG_RANGE);
                return false;
            }

            //条件check
            if (!(fStage2 > fStage3 && fStage3 > fStage4 && fStage4 > fStage5))
            {
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_LOCATION_TAG_RANGE);
                return false;
            }

            //保存
            m_hCommonBase.fStage2_Max_Read_Power_Level = fStage2;
            m_hCommonBase.fStage3_Max_Read_Power_Level = fStage3;
            m_hCommonBase.fStage4_Max_Read_Power_Level = fStage4;
            m_hCommonBase.fStage5_Max_Read_Power_Level = fStage5;

            if (m_pSaveSettingsWrapper.SaveSettings(new SaveSettingsParam(SaveSettingsParam.SaveTypes.FLOAT, Properties.Resource.pref_stage2_max_read_power_level_on_search, fStage2),
                                                    new SaveSettingsParam(SaveSettingsParam.SaveTypes.FLOAT, Properties.Resource.pref_stage3_max_read_power_level_on_search, fStage3),
                                                    new SaveSettingsParam(SaveSettingsParam.SaveTypes.FLOAT, Properties.Resource.pref_stage4_max_read_power_level_on_search, fStage4),
                                                    new SaveSettingsParam(SaveSettingsParam.SaveTypes.FLOAT, Properties.Resource.pref_stage5_max_read_power_level_on_search, fStage5)))
            {
                AutoMessageBox.ShowMessage(Properties.Resource.I_MSG_LOCATION_TAG_SETTING);
            }
            else
            {
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_LOCATION_TAG_SAVE_ERROR);
                return false;
            }

            return true;
        }
    }
}
