using System;
using System.Text;
using DENSOScannerSDK;
using DENSOScannerSDK.Barcode;
using DENSOScannerSDK.Common;
using DENSOScannerSDK.Dto;
using DENSOScannerSDK.Interface;
using DENSOScannerSDK.Util;
using DENSOScannerSDK.RFID;
using System.Diagnostics;

namespace DENSOScannerSDK_Demo
{
    public partial class FormBarcodePage : Form, BarcodeDataDelegate
    {
        private const String TAG = "FormBarcodePage";

        CommonBase m_hCommonBase = CommonBase.GetInstance();

        private ReadAction nextReadAction = new ReadAction(ReadAction.ReadActionType.START);

        private BarcodeScanner? mBarcodeScanner = null;


        // ダミーデータの利用数
        // The number of usage of dummy data
        private readonly int DEFAULT_NO_STORED_TAG_LINE_NUMBER = 6;

        private int storedBarcodeCount = 0;

        private bool mFormLoadCompletedFlg = false;

        private DialogResult mFormDialogResult;

        /**
        * Barcodeを追加する
        * Add barcodeText.
        * @param tagText 追加するタグのテキスト Text of the tag to be added
        */
        public void AddBarcode(String barcodeText)
        {
            // 未格納のBarcodeがあれば設定、なければ追加
            // If there is an unstored Barcode, set it, otherwise add.
            // 未格納Barcodeがあればそこに設定、なければ新規Barcodeを追加
            if (storedBarcodeCount < list_view.Items.Count)
            {
                list_view.Items[storedBarcodeCount].Text = barcodeText;
            }
            else
            {
                //ListViewItemオブジェクトの作成
                ListViewItem lvi = new ListViewItem(barcodeText);
                lvi.UseItemStyleForSubItems = false;

                //フォントを設定にする
                FontStyle fs = lvi.Font.Style;
                Font newFont = new Font(lvi.Font.Name, 12, fs);
                lvi.Font = newFont;

                //アイテムを追加
                list_view.Items.Add(lvi);

                if ((storedBarcodeCount % 2) == 0)
                {
                    list_view.Items[storedBarcodeCount].SubItems[0].BackColor = Color.LightBlue;       // 偶数行の背景色
                }
                else
                {
                    list_view.Items[storedBarcodeCount].SubItems[0].BackColor = Color.LightCyan;      // 奇数行の背景色
                }

                list_view.Items[storedBarcodeCount].EnsureVisible();
            }

            //格納数更新
            storedBarcodeCount++;
        }

        public void AddDummydata(int ItemsIndex)
        {
            if (ItemsIndex < list_view.Items.Count)
            {
                list_view.Items[ItemsIndex].Text = "";
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

                if ((ItemsIndex % 2) == 0)
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
         * Barcodeをクリアする
         * 境界線表示のための未格納タグは配置している状態にもどしている
         * Clear the Barcode.
         * The non-stored Barcode for displaying the boundary line is returned to the state where it is arranged.
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
            storedBarcodeCount = 0;
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

        public FormBarcodePage()
        {
            InitializeComponent();

            button_navigate_up.Text = Properties.Resource.navigate_up;
            text_title_barcode.Text = Properties.Resource.barcode;
            button_switch.Text = Properties.Resource.start;

        }

        private void FormBarcodePage_SizeChanged(object sender, EventArgs e)
        {
            if (mFormLoadCompletedFlg == true)
            {
                list_view.Columns[0].Width = -2;
            }
        }

        private void FormBarcodePage_Load(object sender, EventArgs e)
        {
            mFormDialogResult = DialogResult.OK;

            m_hCommonBase.OnScannerClosedDelegateStackPush(OnScannerClosedFormPage);

            ClearTags();

            StartSessionAndBarcodeScan();

            //Shift-JIS 対応
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            mFormLoadCompletedFlg = true;
        }

        void StartSessionAndBarcodeScan()
        {
            StartSession();

            if (nextReadAction.m_ReadActionType == ReadAction.ReadActionType.STOP)
            {
                StartBarcodeScan();
            }
        }

        public void OnScannerClosedFormPage()
        {
            //切断
            mFormDialogResult = DialogResult.Cancel;

            this.Invoke(new Action(() => this.Close()));
        }

        private void FormBarcodePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopBarcodeScan();

            m_hCommonBase.OnScannerClosedDelegateStackPop();

            this.DialogResult = mFormDialogResult;
        }

        private bool OnBackButtonPressed()
        {
            StopBarcodeScan();
            return true;
        }

        private void button_switch_Clicked(object sender, EventArgs e)
        {
            RunReadAction();
        }

        private void button_navigate_up_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        public void OnBarcodeDataReceived(CommScanner scanner, BarcodeDataReceivedEvent barcodeEvent)
        {
            if (barcodeEvent != null)
            {
                this.BeginInvoke(
                    (MethodInvoker)delegate ()
                    {
                        foreach (DENSOScannerSDK.Barcode.BarcodeData barcodedata in barcodeEvent.BarcodeData)
                        {
                            String data = barcodedata.GetSymbologyDenso() + "(" + barcodedata.GetSymbologyAim() + ")\r\n";
                            data += Encoding.GetEncoding("Shift_JIS").GetString(barcodedata.GetData());
                            AddBarcode(data);
                        }
                    }
                );
            }
        }

        /**
        * 読み込みアクションを実行する
        * Do the reading action
        */
        private void RunReadAction()
        {
            // 設定された読み込みアクションを実行する
            // Do the set reading action.
            switch (nextReadAction.m_ReadActionType)
            {
                case ReadAction.ReadActionType.START:
                    ClearTags();
                    StartBarcodeScan();
                    break;

                case ReadAction.ReadActionType.STOP:
                    // タグ読み込み終了
                    // Finish reading tags
                    StopBarcodeScan();
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
            OnBackButtonPressed();
        }

        /**
         * バーコードスキャナーのインスタンスを取得する
         * Get an instance of a barcode scanner
         */
        private void StartSession()
        {
            if (m_hCommonBase.IsScannerConnected())
            {
                mBarcodeScanner = m_hCommonBase.GetCommScanner().GetBarcodeScanner();
            }
        }

        /**
         * バーコードのスキャンを開始する
         * Start barcode scanning
         */
        private void StartBarcodeScan()
        {

            StartSession();

            if (mBarcodeScanner != null)
            {
                try
                {

                    // リスナーをセット
                    // Set listener
                    mBarcodeScanner.SetDataDelegate(this);

                    // スキャン開始
                    // Start scanning
                    mBarcodeScanner.OpenReader();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            }
        }

        /**
        * バーコードのスキャンを停止する
         * Stop barcode scanning
         */
        private void StopBarcodeScan()
        {
            if (mBarcodeScanner != null)
            {
                try
                {
                    mBarcodeScanner.CloseReader();
                    mBarcodeScanner.SetDataDelegate(null);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
