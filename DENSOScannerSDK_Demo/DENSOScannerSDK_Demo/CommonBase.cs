using System;
using System.Collections.Generic;
using System.Text;
using DENSOScannerSDK;
using DENSOScannerSDK.Common;
using DENSOScannerSDK.Interface;

namespace DENSOScannerSDK_Demo
{
    public class CommonBase : ScannerStatusListener
    {
        static CommonBase? m_hCommonBase = null;

        public static CommScanner? commScanner;

        public static bool scannerConnected = false;

        public static CommonBase GetInstance()
        {
            if (m_hCommonBase == null)
            {
                m_hCommonBase = new CommonBase();
            }
            return m_hCommonBase;
        }

        /**
         * 接続済みのCommScannerを設定
         * Set up connected CommScanner
         * @param connectedCommScanner 接続済みのCommScanner nullの場合、保持しているCommScannerをnullにする if argument is null, make the holding CommScanner null.
         */
        public void SetConnectedCommScanner(CommScanner IN_pCommScanner)
        {
            if (IN_pCommScanner != null)
            {
                scannerConnected = true;
                IN_pCommScanner.AddStatusListener(this);
            }
            else
            {
                scannerConnected = false;
                if (commScanner != null)
                {
                    commScanner.RemoveStatusListener(this);
                }
            }

            commScanner = IN_pCommScanner;
        }

        /**
         * CommScanner取得
         * 返されるCommScannerがnullでなくてもCommScannerが接続している状態とは限らないので
         * スキャナが接続されていることを確認するならば,IsScannerConnectedを使用すること
         * Get CommScanner
         * Even if the returned CommScanner is not null,CommScanner is not always connected.
         * If you want to confirm that the scanner is connected, use IsScannerConnected.
         * @return
         */
        public CommScanner? GetCommScanner()
        {
            return commScanner;
        }

        /**
         * CommScannerが接続されているか
         * Whether the CommScanner is connected or not
         * @return CommScannerが接続されていたらtrue、切断されていたらfalseを返す true:connected false:not connected
         */
        public bool IsScannerConnected()
        {
            return scannerConnected;
        }

        /**
         * SP1切断
         * disconnect SP1 
         */
        public void DisconnectCommScanner()
        {
            if (commScanner != null)
            {
                try
                {
                    commScanner.Close();
                    commScanner.RemoveStatusListener(this);
                    scannerConnected = false;
                    commScanner = null;
                }
                catch (CommException e)
                {
                }
            }
        }


        public void OnScannerClosedDelegateStackPush(Action _Delegate)
        {
            if (OnScannerClosedDelegate != null)
            {
                OnScannerClosedDelegateStack.Push(OnScannerClosedDelegate);
            }

            OnScannerClosedDelegate = null;
            OnScannerClosedDelegate += _Delegate;
        }
        private Stack<Action> OnScannerClosedDelegateStack = new Stack<Action>();

        public void OnScannerClosedDelegateStackPop()
        {

            OnScannerClosedDelegate = null;

            if (OnScannerClosedDelegateStack.Count > 0)
            {
                OnScannerClosedDelegate += OnScannerClosedDelegateStack.Last();
                OnScannerClosedDelegateStack.Pop();
            }
        }

        public Action OnScannerClosedDelegate;
        public void OnScannerStatusChanged(CommScanner scanner, CommStatusChangedEvent state)
        {
            CommConst.ScannerStatus scannerStatus = state.GetStatus();
            if (scanner == commScanner && scannerStatus.Equals(CommConst.ScannerStatus.CLOSE_WAIT))
            {
                if (scannerConnected)
                {
                    scannerConnected = false;
                    if (OnScannerClosedDelegate != null)
                    {
                        this.OnScannerClosedDelegate();
                    }
                }
            }
        }

        #region PreFilter Temp Values
        // アプリ実行中の間のみ保持するフィルタ
        // フィルタを設定するときにこのアクティビティが持つフィルタと変わりなければフィルタの保存を行わない
        // フィルタの設定が成功した時に値を保存し、画面遷移後に表示する値を保持
        // this filter is used while this app is executing.
        // When setting the filter, do not save the filter unless it is the same as the filter of this activity
        // Save the value when the filter setting is successful
        public FormPreFiltersPage.FilterBank? tempFilterBank = null;
        public FormPreFiltersPage.FilterOffset? tempFilterOffset = null;
        public FormPreFiltersPage.FilterPattern? tempFilterPattern = null;
        #endregion

        #region LocationTag Setting Values
        //LocationTag 画面での設定
        public float fStage2_Max_Read_Power_Level = 0.0f;
        public float fStage3_Max_Read_Power_Level = 0.0f;
        public float fStage4_Max_Read_Power_Level = 0.0f;
        public float fStage5_Max_Read_Power_Level = 0.0f;
        #endregion
    }
}
