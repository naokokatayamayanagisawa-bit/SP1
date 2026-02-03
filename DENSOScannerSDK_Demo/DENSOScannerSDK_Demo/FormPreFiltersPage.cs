using System;
using System.Globalization;
using DENSOScannerSDK;
using DENSOScannerSDK.Barcode;
using DENSOScannerSDK.Common;
using DENSOScannerSDK.Dto;
using DENSOScannerSDK.Interface;
using DENSOScannerSDK.Util;
using DENSOScannerSDK.RFID;

namespace DENSOScannerSDK_Demo
{
    public partial class FormPreFiltersPage : Form
    {
        private const String TAG = "FormPreFiltersPage";

        CommonBase m_hCommonBase = CommonBase.GetInstance();

        // これらのフィルタは必ずしも値を持っているわけではない
        // フィルタがnullのときはUIに空文字列として表示する
        // また、この変数にはgetter/setterメソッドからアクセスすること
        private FilterBank? _filterBank = null;
        private FilterOffset? _filterOffset = null;
        private FilterPattern? _filterPattern = null;

        // 本体に保存していて、アプリを終了後も保持するフィルタ設定値
        // SetFilterが成功したときのみに書き込みを行い、Loadボタン押下時に保持している値をテキストエリアに表示
        SaveSettingsWrapper m_pSaveSettingsWrapper = SaveSettingsWrapper.GetInstance();

        // 生成時にスキャナと接続されていたか
        // この画面にいる間に接続が切断された場合でも、生成時にスキャナと接続されていた場合は通信エラーが出るようにする
        private bool scannerConnectedOnCreate = false;

        private DialogResult mFormDialogResult;

        public FormPreFiltersPage()
        {
            InitializeComponent();

            button_navigate_up.Text = Properties.Resource.navigate_up;
            text_title_pre_filters.Text = Properties.Resource.pre_filters;

            text_bank_head.Text = Properties.Resource.bank;

            text_bank_value.Items.Add(Properties.Resource.bank_uii);
            text_bank_value.Items.Add(Properties.Resource.bank_tid);
            text_bank_value.Items.Add(Properties.Resource.bank_user);

            text_offset_head.Text = Properties.Resource.offset_bit;

            text_pattern_head.Text = Properties.Resource.pattern;

            button_filter_set.Text = Properties.Resource.set;
            button_filter_clear.Text = Properties.Resource.clear;
            button_filter_load.Text = Properties.Resource.load;
        }

        private void FormPreFiltersPage_Load(object sender, EventArgs e)
        {
            mFormDialogResult = DialogResult.OK;

            m_hCommonBase.OnScannerClosedDelegateStackPush(OnScannerClosedFormPage);

            OnAppearing();
        }
        private void OnAppearing()
        {
            scannerConnectedOnCreate = m_hCommonBase.IsScannerConnected();

            // SP1が見つからなかったときはエラーメッセージ表示
            if (!scannerConnectedOnCreate)
            {
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_NO_CONNECTION);
            }

            // 前回SetFilterに成功したフィルタを表示する
            LoadFilterFromTemp();

            // ボタンの状態を更新する
            RefreshSetFilterButton();
        }

        public void OnScannerClosedFormPage()
        {
            //切断
            mFormDialogResult = DialogResult.Cancel;

            this.Invoke(new Action(() => this.Close()));
        }

        private void FormPreFiltersPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            NavigateUp();

            m_hCommonBase.OnScannerClosedDelegateStackPop();

            this.DialogResult = mFormDialogResult;
        }

        /**
         * 画面遷移でいう上の階層に移動する
         */
        private void NavigateUp()
        {

        }
        private void button_navigate_up_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void text_bank_value_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (text_bank_value.SelectedIndex < 0)
            {
                SetFilterBank(null);
            }
            else
            {
                SetFilterBank(FilterBank.GetFilterBank((string?)text_bank_value.SelectedItem));
            }
        }

        private void text_offset_value_Completed(object sender, EventArgs e)
        {
            if (text_offset_value.Text == "")
            {
                _filterOffset = null;
                SetFilterOffset(_filterOffset);
                return;
            }

            // 数値がない場合は空に設定する
            int number = 0;

            bool bResult = int.TryParse(text_offset_value.Text, out number);

            if (bResult == false)
            {
                SetFilterOffset(_filterOffset);
                return;
            }

            // 数値を検証する
            FilterOffset filterOffset;
            try
            {
                filterOffset = FilterOffset.ValueOf(number);
            }
            catch (OutOfRangeException)
            {
                SetFilterOffset(_filterOffset);
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_FILTER_OUT_OF_RANGE_OFFSET);
                return;
            }

            // 数値が正常値なので設定する
            SetFilterOffset(filterOffset);
        }

        private void text_pattern_value_Completed(object sender, EventArgs e)
        {
            if (text_pattern_value.Text == "")
            {
                _filterPattern = null;
                SetFilterPattern(_filterPattern);
                return;
            }

            // 空文字列の場合は空に設定する
            string strInput = text_pattern_value.Text;
            if (strInput == "" || strInput == null)
            {
                SetFilterPattern(_filterPattern);
                return;
            }

            // 数値を検証する
            FilterPattern filterPattern;
            try
            {
                filterPattern = FilterPattern.ValueOf(strInput);
            }
            catch (NotCapitalException)
            {
                SetFilterPattern(_filterPattern);
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_FILTER_NOT_CAPITAL);
                //小文字の場合Setボタンを無効にする。
                RefreshSetFilterButton();
                return;
            }
            catch (NotHexException)
            {
                SetFilterPattern(_filterPattern);
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_FILTER_INVALID_PATTERN);
                return;
            }
            catch (OverflowBitException)
            {
                SetFilterPattern(_filterPattern);
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_FILTER_OUT_OF_RANGE_PATTERN);
                return;
            }

            // 数値が正常値なので設定する
            SetFilterPattern(filterPattern);
        }

        private void button_filter_set_Clicked(object sender, EventArgs e)
        {
            SetFilterToScanner();
        }

        private void button_filter_clear_Clicked(object sender, EventArgs e)
        {
            ClearFilterFromScanner();
        }

        private void button_filter_load_Clicked(object sender, EventArgs e)
        {
            LoadFilterFromPref();
        }
        private void Text_offset_value_TextChanged(object sender, EventArgs e)
        {
            DeleteLineFeed((TextBox)sender, e);
        }
        private void Text_pattern_value_TextChanged(object sender, EventArgs e)
        {
            DeleteLineFeed((TextBox)sender, e);
        }
        private void text_offset_value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // Enterキーが押された場合
            {
                text_offset_value_Completed(sender, e);
            }
        }
        private void text_pattern_value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // Enterキーが押された場合
            {
                text_pattern_value_Completed(sender, e);
            }
        }
        static void DeleteLineFeed(TextBox pTextBox, EventArgs e)
        {

            //文字列の最後に改行文字があったら削除します。
            if (pTextBox != null)
            {
                string strNewValue = pTextBox.Text;

                while (true)
                {
                    int iIndex = strNewValue.LastIndexOf("\n");

                    if (iIndex < 0)
                    {
                        break;
                    }

                    strNewValue = strNewValue.Remove(iIndex, 1);
                }

                pTextBox.Text = strNewValue;
            }
        }

        /**
        * スキャナにフィルタを送信する
        * Bank・Offset・Patternがnullのときはこのメソッドが呼ばれないようにする
        */
        private void SetFilterToScanner()
        {
            if (!scannerConnectedOnCreate)
            {
                return;
            }

            // フィルタ作成
            RFIDScannerFilter filter = new RFIDScannerFilter();
            filter.mBank = GetFilterBank().GetBank();
            filter.mBitOffset = GetFilterOffset().GetNumber();
            filter.mBitLength = GetFilterPattern().GetBitLength();
            filter.mFilterData = GetFilterPattern().GetBytes();

            // bankがUIIの時はオフセット値に32を追加
            if (filter.mBank == RFIDScannerFilter.Bank.UII)
            {
                filter.mBitOffset += 32;
            }

            RFIDScannerFilter[] filters = { filter };
            RFIDScannerFilter.RFIDLogicalOpe logicalOpe = RFIDScannerFilter.RFIDLogicalOpe.AND;

            // フィルタ設定
            try
            {
                m_hCommonBase.GetCommScanner().GetRFIDScanner().SetFilter(filters, logicalOpe);

                // スキャナへのフィルタ設定が成功したときのみ、PreferenceとTempに値を保存する。
                SaveFilterToTemp();
                SaveFilterToPref();

                AutoMessageBox.ShowMessage(Properties.Resource.I_MSG_SET_FILTER);
            }
            catch (RFIDException e)
            {
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_SET_FILTER);
            }
        }

        /**
        * スキャナのフィルタをクリアする
        */
        private void ClearFilterFromScanner()
        {
            if (!scannerConnectedOnCreate)
            {
                return;
            }

            try
            {
                m_hCommonBase.GetCommScanner().GetRFIDScanner().ClearFilter();

                // スキャナから設定をクリアしたときのみフィルタのクリアを反映する
                ClearFilterFromTemp();
                ClearFilterFromView();

                AutoMessageBox.ShowMessage(Properties.Resource.I_MSG_CLEAR_FILTER);
            }
            catch (RFIDException e)
            {
                AutoMessageBox.ShowMessage(Properties.Resource.E_MSG_COMMUNICATION);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }
        /**
         * アプリ実行中の間のみ保持するフィルタを読みこむ
         */
        private void LoadFilterFromTemp()
        {
            SetFilterBank(m_hCommonBase.tempFilterBank);
            SetFilterOffset(m_hCommonBase.tempFilterOffset != null ? (FilterOffset)m_hCommonBase.tempFilterOffset.Clone() : null);
            SetFilterPattern(m_hCommonBase.tempFilterPattern != null ? (FilterPattern)m_hCommonBase.tempFilterPattern.Clone() : null);
        }

        /**
         * フィルタをSharedPreferencesから読み込み
         * Loadボタン押下時に使用
         */
        private void LoadFilterFromPref()
        {
            string sharedFilterBank = m_pSaveSettingsWrapper.GetString(Properties.Resource.pref_filter_bank, null);
            long sharedFilterOffset = m_pSaveSettingsWrapper.GetLong(Properties.Resource.pref_filter_offset, -1);
            string sharedFilterPattern = m_pSaveSettingsWrapper.GetString(Properties.Resource.pref_filter_pattern, null);

            if (sharedFilterBank != null && sharedFilterOffset >= 0 && sharedFilterPattern != null)
            {
                try
                {
                    // FilterBank設定
                    SetFilterBank(FilterBank.GetFilterBank(sharedFilterBank));
                    // FilterOffset設定
                    SetFilterOffset(FilterOffset.ValueOf(sharedFilterOffset));
                    // FilterPattern設定
                    SetFilterPattern(FilterPattern.ValueOf(sharedFilterPattern));
                }
                catch (Exception e)
                {
                    if (e is NotHexException || e is OverflowBitException || e is OutOfRangeException)
                    {
                        System.Diagnostics.Debug.WriteLine(e.StackTrace);
                    }
                }
            }
        }

        /**
         * フィルタをアプリ実行中の間のみ保持するデータとして保存する
         */
        private void SaveFilterToTemp()
        {
            // フィルタがアプリ実行中の間のみ保持しているものと変わらない場合は何もしない
            if (FilterEqualTemp())
            {
                return;
            }

            // フィルタをアプリ実行中の間のみ保持するデータとして保存する
            FilterBank filterBank = GetFilterBank();
            FilterOffset filterOffset = GetFilterOffset();
            FilterPattern filterPattern = GetFilterPattern();
            m_hCommonBase.tempFilterBank = filterBank;
            m_hCommonBase.tempFilterOffset = filterOffset != null ? (FilterOffset)filterOffset.Clone() : null;
            m_hCommonBase.tempFilterPattern = filterPattern != null ? (FilterPattern)filterPattern.Clone() : null;
        }

        /**
         * フィルタをSharedPreferencesに保存する
         * 基本的にはSet成功後に実行
         */
        private void SaveFilterToPref()
        {
            m_pSaveSettingsWrapper.SaveSettings(new SaveSettingsParam(SaveSettingsParam.SaveTypes.STRING, Properties.Resource.pref_filter_bank, GetFilterBank().GetShowName()),
                                                                new SaveSettingsParam(SaveSettingsParam.SaveTypes.LONG, Properties.Resource.pref_filter_offset, GetFilterOffset().GetNumber()),
                                                                new SaveSettingsParam(SaveSettingsParam.SaveTypes.STRING, Properties.Resource.pref_filter_pattern, GetFilterPattern().GetHexString()));
        }

        /**
         * アプリ実行中の間のみ保持するフィルタを破棄する
         */
        private void ClearFilterFromTemp()
        {
            m_hCommonBase.tempFilterBank = null;
            m_hCommonBase.tempFilterOffset = null;
            m_hCommonBase.tempFilterPattern = null;
        }

        /**
         * アクティビティが持つフィルタがアプリ実行中の間のみ保持するフィルタと同じかどうか
         * @return アクティビティが持つフィルタがアプリ実行中の間のみ保持するフィルタと同じであればtrue、そうでなければfalseを返す
         */
        private bool FilterEqualTemp()
        {
            FilterOffset filterOffset = GetFilterOffset();
            FilterPattern filterPattern = GetFilterPattern();
            return GetFilterBank() == m_hCommonBase.tempFilterBank &&
                    (filterOffset == null && m_hCommonBase.tempFilterOffset == null ||
                            filterOffset != null && filterOffset.Equals(m_hCommonBase.tempFilterOffset)) &&
                    (filterPattern == null && m_hCommonBase.tempFilterPattern == null ||
                            filterPattern != null && filterPattern.Equals(m_hCommonBase.tempFilterPattern));
        }

        /**
         * フィルタのBankを設定する
         * 該当するTextView及びButtonに反映する
         * @param filterBank フィルタのBank nullを指定した場合空として保持する
         */
        private void SetFilterBank(FilterBank? filterBank)
        {
            _filterBank = filterBank;

            text_bank_value.SelectedItem = (filterBank != null ? filterBank.GetShowName() : null);

            RefreshSetFilterButton();
        }

        /**
         * フィルタのBankを取得する
         * @return フィルタのBank 値がない場合はnullを返す
         */
        private FilterBank GetFilterBank()
        {
            return _filterBank;
        }

        /**
         * フィルタのOffsetを設定する
         * 該当するTextView及びButtonに反映する
         * @param filterOffset フィルタのOffset nullを指定した場合空として保持する
         */
        private void SetFilterOffset(FilterOffset? filterOffset)
        {
            _filterOffset = filterOffset;

            text_offset_value.Text = (filterOffset != null ? filterOffset.GetNumber().ToString() : "");

            RefreshSetFilterButton();
        }

        /**
         * フィルタのOffsetを取得する
         * @return フィルタのOffset 値がない場合はnullを返す
         */
        private FilterOffset GetFilterOffset()
        {
            return _filterOffset;
        }

        /**
         * フィルタのPatternを設定する
         * 該当するTextView及びButtonに反映する
         * @param filterPattern フィルタのPattern nullを指定した場合空として保持する
         */
        private void SetFilterPattern(FilterPattern? filterPattern)
        {
            _filterPattern = filterPattern;

            text_pattern_value.Text = (filterPattern != null ? filterPattern.GetHexString() : null);

            RefreshSetFilterButton();
        }

        /**
         * フィルタのPatternを取得する
         * @return フィルタのPattern 値がない場合はnullを返す
         */
        private FilterPattern GetFilterPattern()
        {
            return _filterPattern;
        }

        /**
         * 画面に表示されているフィルタを破棄する
         */
        private void ClearFilterFromView()
        {
            SetFilterBank(null);
            SetFilterOffset(null);
            SetFilterPattern(null);
        }

        /**
        * Setボタンの状態を更新する
        */
        private void RefreshSetFilterButton()
        {
            bool enabled = GetFilterBank() != null && GetFilterOffset() != null && GetFilterPattern() != null;

            if (text_pattern_value.Text != null)
            {
                enabled &= !(text_pattern_value.Text.Any(char.IsLower));
            }

            button_filter_set.Enabled = enabled;

            if (enabled == true)
            {
                button_filter_set.BackColor = Color.FromArgb(159, 191, 190);
            }
            else
            {
                button_filter_set.BackColor = SystemColors.Control;
            }
            button_filter_set.UseVisualStyleBackColor = true;
        }

        /**
         * フィルタのBankを表すクラス
         * 表示名への変換を分かりやすくするため、ラップして表現する
         */
        public class FilterBank
        {
            public enum FilterBankType
            {
                UII = 0,
                TID,
                USER
            }

            static readonly Dictionary<FilterBankType, FilterBank> m_DicFilterBanks = new Dictionary<FilterBankType, FilterBank>()
            {
                { FilterBankType.UII, new FilterBank("UII") },
                { FilterBankType.TID, new FilterBank("TID") },
                { FilterBankType.USER, new FilterBank("USER") }
            };

            public FilterBankType m_FilterBankType { get; set; }

            private readonly RFIDScannerFilter.Bank? bank = null;
            private readonly string showName;

            /**
             * 表示名をもとにフィルタのBankを返す
             * @param showName 表示名
             * @return 表示名をもとにしたフィルタのBank
             * @throws IllegalArgumentException 表示名に該当するフィルタのBankが存在しない場合
             */
            public static FilterBank GetFilterBank(String showName)
            {
                foreach (KeyValuePair<FilterBankType, FilterBank> pEntry in m_DicFilterBanks)
                {
                    FilterBank filterBank = pEntry.Value;
                    if (filterBank.showName.Equals(showName))
                    {
                        return filterBank;
                    }
                }

                throw new ArgumentException();
            }

            /**
             * 表示名から初期化する
             * @param showName 表示名
             */
            FilterBank(String showName)
            {
                switch (showName)
                {
                    case "UII":
                        bank = RFIDScannerFilter.Bank.UII;
                        break;
                    case "TID":
                        bank = RFIDScannerFilter.Bank.TID;
                        break;
                    case "USER":
                        bank = RFIDScannerFilter.Bank.USER;
                        break;
                    default:
                        bank = null;
                        break;
                }
                this.showName = showName;
            }

            /**
             * 表示名を取得する
             * @return 表示名
             */
            public String GetShowName()
            {
                return showName;
            }

            /**
             * APIのBankを取得する
             * @return APIのBank
             */
            public RFIDScannerFilter.Bank GetBank()
            {
                return (RFIDScannerFilter.Bank)bank;
            }
        }

        /**
         * フィルタのOffsetを表すクラス
         * Offsetには範囲の制約があるため、ラップして表現する
         */
        public class FilterOffset : ICloneable
        {

            private long number;

            /**
             * 数値をもとにフィルタのOffsetを返す
             * @param number 数値
             * @return 指定した数値をもとにしたフィルタのOffset
             * @throws OutOfRangeException 指定した数値が規定の範囲でない場合
             */
            public static FilterOffset ValueOf(long number)
            {
                if (number < 0 || number > 0x7FFFF)
                {

                    throw new OutOfRangeException(0, 0x7FFFF);
                }
                return new FilterOffset(number);
            }

            /**
             * 数値から初期化する
             * @param value 数値
             */
            private FilterOffset(long value)
            {
                this.number = value;
            }

            public override int GetHashCode()
            {
                return number.ToString().GetHashCode();
            }

            public override bool Equals(Object obj)
            {
                if (this == obj)
                {
                    return true;
                }

                if (obj == null || this.GetType() != obj.GetType())
                {
                    return false;
                }

                FilterOffset other = (FilterOffset)obj;
                return number == other.number;
            }

            public virtual object Clone()
            {
                FilterOffset cloneInstance;

                cloneInstance = new FilterOffset(number);

                return cloneInstance;
            }

            /**
             * 数値として取得する
             * @return 数値
             */
            public long GetNumber()
            {
                return number;
            }
        }

        /**
         * フィルタのPatternを表すクラス
         * Patternには範囲および16進数表記の制約があるため、ラップして表現する
         */
        public class FilterPattern : ICloneable
        {

            private static char[] hexCharacters =
                    {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

            // 16進数のPattern値
            // Patternは256ビット分( (0or1) * 256 )に及ぶため、文字列で指定する
            private String hexString;

            // バイトリストのPattern値
            // 最高位バイトからリストに入れていく
            // 例えば文字列が"ABC"の場合、{0xA, 0xBC} になる
            private byte[] bytes;

            // Pattern値のビット長
            // ビット長は16進数の文字列の長さに即する
            // 例えば文字列が"ABC"の場合、16進数1文字につき4ビットのため長さは12になる
            private short bitLength;

            /**
             * 16進数の文字列をもとにフィルタのPatternを返す
             * @param hexString 16進数の文字列
             * @return 指定した16進数の文字列をもとにしたフィルタのPattern
             * @throws NotHexException 指定した文字列の形式が16進数でない場合
             * @throws OverflowBitException 指定した文字列のビット数が規定のビット数を超えている場合
             */
            public static FilterPattern ValueOf(String hexString)
            {
                if (hexString.Any(char.IsLower))
                {
                    throw new NotCapitalException();
                }

                if (!CheckHexString(hexString))
                {
                    throw new NotHexException();
                }

                if (hexString.Length > 64 /* 256ビットまで入力可能、16進数の64桁分は256ビット分にあたる */)
                {
                    throw new OverflowBitException(256);
                }

                return new FilterPattern(hexString);
            }

            /**
             * 16進数の文字列から初期化する
             * @param hexString 16進数の文字列
             */
            private FilterPattern(String hexString)
            {
                this.hexString = hexString;

                // バイトのリストを求める
                bytes = HexStringToBytes(hexString);

                // ビット長を求める
                bitLength = CalcBitLength(bytes);
            }

            public override int GetHashCode()
            {
                return hexString.GetHashCode();
            }


            public override bool Equals(Object obj)
            {
                if (this == obj)
                {
                    return true;
                }
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                // 文字列が同じであれば、他のパラメータもすべて同じ
                FilterPattern other = (FilterPattern)obj;
                return hexString.Equals(other.hexString);
            }

            public virtual object Clone()
            {
                FilterPattern cloneInstance;

                cloneInstance = new FilterPattern(hexString);
                Array.Copy(bytes, cloneInstance.bytes, bytes.Length);
                cloneInstance.bitLength = bitLength;
                return cloneInstance;
            }

            /**
             * バイトのリストとして取得する
             * @return バイトのリスト
             */
            public byte[] GetBytes()
            {
                return bytes;
            }

            /**
            * 16進数の文字列として取得する
            * @return 16進数の文字列
            */
            public String GetHexString()
            {
                return hexString;
            }

            /**
             * ビット長を取得する
             * @return ビット長
             */
            public short GetBitLength()
            {
                return bitLength;
            }

            /**
             * 16進数の文字列であるか検証する
             * @param string 対象となる文字列
             * @return 16進数の文字列であればtrue、そうでなければfalseを返す
             */
            private static bool CheckHexString(string IN_string)
            {
                for (int i = 0; i < IN_string.Length; i++)
                {
                    char character = IN_string[i];
                    if (!CheckHexCharacter(character))
                    {
                        return false;
                    }
                }
                return true;
            }

            /**
             * 16進数の文字であるか検証する
             * @param character 対象となる文字
             * @return 16進数の文字であればtrue、そうでなければfalseを返す
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

            /**
             * 16進数の文字列からバイトのリストに変換する
             * @param hexString 16進数の文字列
             * @return 16進数の文字列をもとにしたバイトのリスト
             */
            private static byte[] HexStringToBytes(string hexString)
            {
                // 空文字の場合は要素0
                if (hexString.Length == 0)
                {
                    return new byte[0];
                }

                // 16進数の文字列をバイト単位で切り出してリストに格納する
                // 1バイトは16進数2文字分なので2文字ずつ切り出す
                // 文字列長にかかわらず2文字ずつ切り出せるようにするため、文字列が奇数長の場合は0を先頭に追加する
                string workHexString = hexString.Length % 2 == 0 ? hexString : "0" + hexString;
                byte[] bytes = new byte[workHexString.Length / 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    // そのままByte.parseByteすると0x80以上のときにオーバーフローしてしまう
                    // 一旦大きい型にパースしてからbyteにキャストすることで、0x80以上の値を負の値として入れ込むことができる
                    string hex2Characters = CommandBuilder.JavaStyleSubstring(workHexString, i * 2, i * 2 + 2);
                    short number = short.Parse(hex2Characters, NumberStyles.HexNumber);
                    bytes[i] = (byte)number;
                }
                return bytes;
            }

            /**
             * バイトのリストからビット長を求める
             * @param bytes バイトのリスト
             * @return バイトのリストのビット長
             */
            private static short CalcBitLength(byte[] bytes)
            {
                // 1バイトは8ビットにあたる
                return (short)(bytes.Length * 8);
            }
        }

        /**
 * 数値が範囲外である場合にこの例外がスローされる
 */
        private class OutOfRangeException : Exception
        {
            /**
             * 最小値と最大値をもとに初期化する
             * @param minValue 最小値
             * @param maxValue 最大値
             */
            public OutOfRangeException(int minValue, int maxValue) : base(String.Format(CultureInfo.CurrentCulture, "指定できる値は %d から %d までです。", minValue, maxValue))
            {

            }
        }

        /**
         * ビット数がオーバーフローしている場合にこの例外がスローされる
         */
        private class OverflowBitException : Exception
        {

            /**
             * ビット数から初期化する
             * @param bitNumber ビット数
             */
            public OverflowBitException(int bitNumber) : base(String.Format(CultureInfo.CurrentCulture, "指定できる値は %d bitまでです。", bitNumber))
            {

            }
        }

        /**
         * 数値が16進数でない場合にこの例外がスローされる
         */
        private class NotHexException : Exception
        {
            /**
             * 初期化する
             */
            public NotHexException() : base("指定できる値は16進数でなければいけません。")
            {

            }
        }

        /**
         * 小文字エラー
         */
        private class NotCapitalException : Exception
        {
            /**
             * 初期化する
             */
            public NotCapitalException() : base("指定できる値は大文字でなければいけません。")
            {

            }
        }
    }
}
