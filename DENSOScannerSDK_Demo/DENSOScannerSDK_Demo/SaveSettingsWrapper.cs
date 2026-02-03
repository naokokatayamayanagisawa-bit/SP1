using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DENSOScannerSDK_Demo
{
    public class SaveSettingsParam
    {
        public enum SaveTypes
        {
            STRING = 0,
            LONG,
            FLOAT
        }

        public SaveTypes m_SaveTypes;
        public string m_strKey;
        public object m_pObj;

        public SaveSettingsParam(SaveTypes IN_SaveTypes, string IN_strKey, object IN_pObj)
        {
            m_SaveTypes = IN_SaveTypes;
            m_strKey = IN_strKey;
            m_pObj = IN_pObj;
        }
    }

    public class SaveSettingsWrapper
    {
        static SaveSettingsWrapper? m_hSaveSettings = null;

        const string settinsFile = "DENSOScannerSDK_DemoSettings";

        public static SaveSettingsWrapper GetInstance()
        {
            if (m_hSaveSettings == null)
            {
                m_hSaveSettings = new SaveSettingsWrapper();
            }
            m_hSaveSettings.LoadSettings(settinsFile);

            return m_hSaveSettings;
        }

        public void InitSharedPreferences()
        {
        }

        public bool SaveSettings(params SaveSettingsParam[] IN_varrpParams)
        {
            bool bSave = false;
            string? value;
            foreach (SaveSettingsParam pParam in IN_varrpParams)
            {
                switch (pParam.m_SaveTypes)
                {
                    case SaveSettingsParam.SaveTypes.STRING:
                        value = (string?)pParam.m_pObj;
                        bSave = m_hSaveSettings.SetSetting(pParam.m_strKey, value);
                        break;
                    case SaveSettingsParam.SaveTypes.LONG:
                        value = (string?)pParam.m_pObj.ToString();
                        bSave = m_hSaveSettings.SetSetting(pParam.m_strKey, value);
                        break;
                    case SaveSettingsParam.SaveTypes.FLOAT:
                        value = (string?)pParam.m_pObj.ToString();
                        bSave = m_hSaveSettings.SetSetting(pParam.m_strKey, value);
                        break;
                    default:
                        break;
                }
            }

            if (bSave == true)
            {
                bSave = m_hSaveSettings.SaveSettings(settinsFile);
            }
            return bSave;
        }

        public bool LoadSettings()
        {
            bool bLoad = false;

            if (m_hSaveSettings != null)
            {
               bLoad = m_hSaveSettings.LoadSettings(settinsFile);
            }

            return bLoad;
        }

        public string GetString(string IN_strKey, string IN_strDefValue)
        {
            try
            {
                string value = "";
                value = m_hSaveSettings.GetSetting(IN_strKey, IN_strDefValue);
                return value;
            }
            catch (Exception e)
            {
                return IN_strDefValue;
            }
        }

        public long GetLong(string IN_strKey, long IN_lDefValue)
        {
            try
            {
                string value = "";
                value = m_hSaveSettings.GetSetting(IN_strKey, "");
                if (value != "")
                {
                    return long.Parse(value);
                }
                else 
                {
                    return IN_lDefValue;
                }
            }
            catch (Exception e)
            {
                return IN_lDefValue;
            }
        }

        public float GetFloat(string IN_strKey, float IN_fDefValue)
        {
            try
            {
                string value = "";
                value = m_hSaveSettings.GetSetting(IN_strKey, "");
                if (value != "")
                {
                    return float.Parse(value);
                }
                else
                {
                    return IN_fDefValue;
                }
            }
            catch (Exception e)
            {
                return IN_fDefValue;
            }
        }

        [DataContract]
        public class ScannerSettingParam
        {
            [DataMember]
            public string property;
            [DataMember]
            public string value;

            public ScannerSettingParam(string property, string value)
            {
                this.property = property;
                this.value = value;
            }
        }

        Dictionary<string, string> settingDictionary = new Dictionary<string, string>();
        string m_settingFolderPath = ".\\";

        public bool SaveSettings(string fileName, string extension = ".json")
        {
            try
            {
                using (var fs = new FileStream(m_settingFolderPath + fileName + extension, FileMode.Create))
                using (var jrwf = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, true, true))
                {
                    // JSONシリアライザーを作成
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<ScannerSettingParam>));

                    // 現在の設定をリスト形式に変換
                    List<ScannerSettingParam> paramList = new List<ScannerSettingParam>();
                    foreach (var item in settingDictionary)
                    {
                        paramList.Add(new ScannerSettingParam(item.Key, item.Value));
                    }

                    // データを書き込む
                    serializer.WriteObject(jrwf, paramList);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool LoadSettings(string fileName, string extension = ".json")
        {
            try
            {
                using (var fs = new FileStream(m_settingFolderPath + fileName + extension, FileMode.Open))
                {
                    // JSONシリアライザーを作成
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<ScannerSettingParam>));
                    // データを読み込む
                    List<ScannerSettingParam>? paramList = (List<ScannerSettingParam>?)serializer.ReadObject(fs);

                    // Dictionaryに変換
                    this.settingDictionary.Clear();
                    paramList.ForEach((param) => {
                        this.settingDictionary.Add(param.property, param.value);
                    });

                    return true;
                }
            }
            catch (Exception e)
            {
                this.settingDictionary.Clear();
                return false;
            }
        }

        public bool SetSetting(string property, string value)
        {
            try
            {
                if (this.settingDictionary.ContainsKey(property) == true)
                {
                    this.settingDictionary[property] = value;
                }
                else
                {
                    this.settingDictionary.Add(property, value);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string GetSetting(string property, string defaultValue)
        {
            try
            {
                if (this.settingDictionary.ContainsKey(property) == true)
                {
                    return this.settingDictionary[property];
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception e)
            {
                return defaultValue;
            }
        }
    }
}
