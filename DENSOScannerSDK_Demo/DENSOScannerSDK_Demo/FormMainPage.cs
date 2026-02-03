using System;
using System.Diagnostics;
using System.Reflection;
using DENSOScannerSDK;
using DENSOScannerSDK.Barcode;
using DENSOScannerSDK.Common;
using DENSOScannerSDK.Dto;
using DENSOScannerSDK.Interface;
using DENSOScannerSDK.Util;
using DENSOScannerSDK.RFID;

namespace DENSOScannerSDK_Demo
{
    public partial class FormMainPage : Form, ScannerAcceptStatusListener
    {
        CommonBase m_hCommonBase = CommonBase.GetInstance();
        SaveSettingsWrapper m_pSaveSettingsWrapper = SaveSettingsWrapper.GetInstance();


        List<CommScanner>? m_ListScanners = null;

        private Form? mFormPage = null;

        private const String TAG = "FormMainPage";

        CancellationTokenSource? m_pTS = null;
        private DateTime m_dtConnectionStartTime;
        private long m_lConnectionElapsedTime = 0;

        private bool mFormLoadCompletedFlg = false;

        private const int SLAVE_MODE_TIMEOUT = 3000;

        public FormMainPage()
        {
            InitializeComponent();

            // アプリバージョンを設定 
            // Set application version
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            AppVersion.Text = string.Format(Properties.Resource.app_version_format, fileVersionInfo.FileVersion);

            connection_status.Text = Properties.Resource.waiting_for_connection;
            button_barcode.Text = Properties.Resource.barcode;
            button_inventory.Text = Properties.Resource.inventory;
            button_locate_tag.Text = Properties.Resource.locate_tag;
            button_pre_filters.Text = Properties.Resource.pre_filters;
            button_rapid_read.Text = Properties.Resource.rapid_read;
            button_settings.Text = Properties.Resource.settings;

            AutoMessageBox.ShowMessage("DENSOScannerSDK Demo APP " + fileVersionInfo.FileVersion);

            m_hCommonBase.OnScannerClosedDelegateStackPush(OnScannerClosedFormMainPage);
        }

        private void FormMainPage_Load(object sender, EventArgs e)
        {
            StartConnection();

            //前回の画面サイズ
            mFormLoadCompletedFlg = false;
            Control c = (Control)sender;
            c.Width = (int)m_pSaveSettingsWrapper.GetLong(Properties.Resource.form_width, c.Width);
            c.Height = (int)m_pSaveSettingsWrapper.GetLong(Properties.Resource.form_height, c.Height);
            mFormLoadCompletedFlg = true;
            
        }
        private void FormMainPage_Resize(object sender, EventArgs e)
        {
            if (mFormLoadCompletedFlg == true)
            {
                Control c = (Control)sender;
                m_pSaveSettingsWrapper.SaveSettings(new SaveSettingsParam(SaveSettingsParam.SaveTypes.STRING, Properties.Resource.form_width, c.Width.ToString()),
                                                      new SaveSettingsParam(SaveSettingsParam.SaveTypes.STRING, Properties.Resource.form_height, c.Height.ToString()));
            }
        }

        public void StartConnection()
        {
            // 接続されてなかった場合、接続状態を描画
            // If not connected, draw connection status
            connection_status.Text = Properties.Resource.waiting_for_connection;

            System.Diagnostics.Debug.WriteLine("Slave Mode Connection Start");

            m_pTS = new CancellationTokenSource();
            CancellationToken ct = m_pTS.Token;

            try
            {
                Task.Run(async () =>
                {
                    m_dtConnectionStartTime = DateTime.Now;
                    while (true)
                    {
                        //Slave Mode接続を回すとき、Thread Cancelをチェック
                        if (ct.IsCancellationRequested)
                        {
                            System.Diagnostics.Debug.WriteLine("Slave Mode Connection End (Cancelled)");
                            //m_pSemaphore_Connection.Release();
                            return;
                        }

                        //スレーブモードの接続を行う。
                        CommScanner? pFoundScanner = CommManagerGetScanners();
                        if (pFoundScanner != null)
                        {
                            try
                            {
                                System.Diagnostics.Debug.WriteLine("Slave Mode Claim");
                                pFoundScanner.Claim();
                                System.Diagnostics.Debug.WriteLine("Slave Mode Success");

                                //エラーなくClaimに成功したらローブ終了。
                                m_hCommonBase.SetConnectedCommScanner(pFoundScanner);

                                CommScanner? pCommScanner = m_hCommonBase.GetCommScanner();
                                string btLocalName = pCommScanner.GetBTLocalName();
                                this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate ()
                                {
                                    connection_status.Text = btLocalName;

                                });

                                //Slave Mode成功 -> 関数動作終了
                                return;
                            }
                            catch (CommException ex)
                            {
                                m_lConnectionElapsedTime = (long)((DateTime.Now - m_dtConnectionStartTime).TotalMilliseconds);

                                if (m_lConnectionElapsedTime >= SLAVE_MODE_TIMEOUT)
                                {
                                    //Master Modeに入る
                                    break;
                                }

                                //Slave Modeを続ける
                                await Task.Delay(10);
                                continue;
                            }
                        }
                        else
                        {
                            m_lConnectionElapsedTime = (long)((DateTime.Now - m_dtConnectionStartTime).TotalMilliseconds);

                            if (m_lConnectionElapsedTime >= SLAVE_MODE_TIMEOUT)
                            {
                                //Master Modeに入る
                                break;
                            }

                            //Slave Modeを続ける
                            await Task.Delay(10);
                            continue;
                        }
                    }

                    System.Diagnostics.Debug.WriteLine("Slave Mode Connection Timeout. ");

                    if (ct.IsCancellationRequested)
                    {
                        System.Diagnostics.Debug.WriteLine("Connection Thread End. (Cancelled)");
                        return;
                    }

                    System.Diagnostics.Debug.WriteLine("Master Mode Connection Start");
                    CommManager.AddAcceptStatusListener(this);
                    System.Diagnostics.Debug.WriteLine("AddAcceptStatusListener");
                    System.Diagnostics.Debug.WriteLine("StartAccept Start");
                    CommManager.StartAccept();
                    System.Diagnostics.Debug.WriteLine("StartAccept End");
                }, ct);
            }
            catch (TaskCanceledException ex)
            {

            }

        }

        CommScanner? CommManagerGetScanners()
        {
            List<CommScanner> ScannerList = CommManager.GetScanners();
            if (ScannerList != null)
            {
                foreach (CommScanner Scanner in ScannerList)
                {
                    if (Scanner.GetBTLocalName().StartsWith("SP1"))
                    {
                        return Scanner;
                    }
                }
            }

            return null;
        }

        public void OnScannerAppeared(CommScanner scanner)
        {
            Debug.WriteLine("OnScannerAppeared()", typeof(FormMainPage).Name);

            bool successFlag = false;
            try
            {
                scanner.Claim();

                CommManager.RemoveAcceptStatusListener(this);
                CommManager.EndAccept();

                System.Diagnostics.Debug.WriteLine("OnScannerAppeared", "claim");

                CommonBase.GetInstance().SetConnectedCommScanner(scanner);
                successFlag = true;
            }
            catch (CommException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            // 画面に、接続したスキャナのBTLocalNameを表示
            // runOnUIThreadを使用してUI Threadで実行 
            // Display BTLocalName of connected scanner on screen
            // Run with UI Thread using runOnUIThread
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate ()
            {
                bool finalSuccessFlag = successFlag;
                CommScanner? pCommScanner = m_hCommonBase.GetCommScanner();
                string btLocalName = pCommScanner.GetBTLocalName();
                if (finalSuccessFlag)
                {
                    connection_status.Text = btLocalName;
                }
                else
                {
                    connection_status.Text = Properties.Resource.connection_error;
                }
            });

            Debug.WriteLine("ScannerAcceptStatusListener#onScannerAppeared", "LOG CONFIRM");
        }

        public void OnScannerClosedFormMainPage()
        {
            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate ()
            {
                // テキストを更新
                connection_status.Text = Properties.Resource.waiting_for_connection;

                // 未接続メッセージ表示
                // show no connect message
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
            });

            m_hCommonBase.DisconnectCommScanner();

            CommManager.AddAcceptStatusListener(this);
            CommManager.StartAccept();
        }


        private void FormMainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_hCommonBase.IsScannerConnected())
            {
                //e.Cancel = true; // フォームの閉鎖をキャンセル時
            }
        }

        private void FormMainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_hCommonBase.IsScannerConnected())
            {
                m_hCommonBase.DisconnectCommScanner();
            }
            CommManager.EndAccept();
        }

        private void mFormPageShowDialog(Form mFormPage)
        {
            if (mFormPage != null)
            {
                DialogResult result = mFormPage.ShowDialog();

                //切断有り
                if (result == DialogResult.Cancel)
                {
                    // テキストを更新
                    connection_status.Text = Properties.Resource.waiting_for_connection;

                    // 未接続メッセージ表示
                    // show no connect message
                    AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);

                    m_hCommonBase.DisconnectCommScanner();
                    CommManager.AddAcceptStatusListener(this);
                    CommManager.StartAccept();
                }
            }
        }

        private void button_rapid_read_Clicked(object sender, EventArgs e)
        {
            mFormPage = new FormRapidReadPage();
            mFormPageShowDialog(mFormPage);
            mFormPage = null;
        }

        private void button_inventory_Clicked(object sender, EventArgs e)
        {
            mFormPage = new FormInventoryPage();
            mFormPageShowDialog(mFormPage);
            mFormPage = null;
        }

        private void button_barcode_Clicked(object sender, EventArgs e)
        {
            mFormPage = new FormBarcodePage();
            mFormPageShowDialog(mFormPage);
            mFormPage = null;
        }

        private void button_settings_Clicked(object sender, EventArgs e)
        {
            mFormPage = new FormSettingsPage();
            mFormPageShowDialog(mFormPage);
            mFormPage = null;
        }

        private void button_locate_tag_Clicked(object sender, EventArgs e)
        {
            mFormPage = new FormLocateTagPage();
            mFormPageShowDialog(mFormPage);
            mFormPage = null;
        }

        private void button_pre_filters_Clicked(object sender, EventArgs e)
        {
            mFormPage = new FormPreFiltersPage();
            mFormPageShowDialog(mFormPage);
            mFormPage = null;
        }

        void EnableButtons(bool isEnabled)
        {
            button_rapid_read.Enabled = isEnabled;
            button_inventory.Enabled = isEnabled;
            button_barcode.Enabled = isEnabled;
            button_settings.Enabled = isEnabled;
            button_locate_tag.Enabled = isEnabled;
            button_pre_filters.Enabled = isEnabled;
        }
    }

    public class AutoMessageBox
    {
        private static NotifyIcon? m_notifyIconMessage = null;

        public static void ShowMessage(string messageText, string messageCaption = "DENSOScannerSDK Demo APP", int messageTimeout = 100)
        {
            if (m_notifyIconMessage == null)
            {
                m_notifyIconMessage = new NotifyIcon();
                m_notifyIconMessage.Visible = false;                 // アイコンを表示
                m_notifyIconMessage.Icon = SystemIcons.Application; // アイコンの設定
                m_notifyIconMessage.BalloonTipClosed += (s, e) =>
                {
                    m_notifyIconMessage.Visible = false;
                };
                m_notifyIconMessage.BalloonTipClicked += (s, e) =>
                {
                    m_notifyIconMessage.Visible = false;
                };

            }
            m_notifyIconMessage.Visible = true;
            m_notifyIconMessage.BalloonTipTitle = messageCaption;   // タイトルの設定
            m_notifyIconMessage.BalloonTipText = messageText;       // テキストの設定
            m_notifyIconMessage.ShowBalloonTip(messageTimeout);
        }
    }
}
