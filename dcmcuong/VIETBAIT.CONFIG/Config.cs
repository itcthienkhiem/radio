using System;
using System.Configuration;
using System.IO;
using VIETBAIT.CONFIG.Properties;

namespace VIETBAIT.CONFIG
{
    public class Config
    {
        private const string StrAppSettings = "appSettings";

        #region Attributies

        private readonly Configuration _config;
        private readonly string _configFileName = "App.config";

        #endregion

        #region Contructor

        /// <summary>
        /// Khởi tạo không truyền vào tham số. Tên file config mặc định là "App.config"
        /// </summary>
        public Config()
        {
            string defaultContent = Resources.InitConfigContent;  
            var configFileMap = new ExeConfigurationFileMap();
            if (!File.Exists(_configFileName))
            {
                File.WriteAllText(_configFileName, defaultContent);
            }
            configFileMap.ExeConfigFilename = _configFileName;

            try
            {
                _config =
                        ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            }
            catch (Exception)
            {
                
                File.WriteAllText(_configFileName, defaultContent);
                _config =
                        ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            }
        }

        /// <summary>
        /// Khởi tạo truyền vào tham số.
        /// </summary>
        /// <param name="pConfigFileName">Tên file config</param>
        public Config(string pConfigFileName)
        {
            _configFileName = pConfigFileName;
            string defaultContent = Resources.InitConfigContent;
            var configFileMap = new ExeConfigurationFileMap();
            if (!File.Exists(_configFileName))
            {
                File.WriteAllText(_configFileName, defaultContent);
            }
            configFileMap.ExeConfigFilename = _configFileName;

            _config =
                ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
        }

        #endregion

        #region Private Method

        private static string GetValueFromKey(string pKey, Configuration pConfig)
        {
            string result = string.Empty;
            try
            {
                result = pConfig.AppSettings.Settings[pKey].Value;
                ConfigurationManager.RefreshSection(StrAppSettings);
            }
            catch
            {
                SetValueForKey(pKey, "", pConfig);
                result = string.Empty;
            }
            return result;
        }

        private static bool SetValueForKey(string pkey, string pvalue, Configuration pconfig)
        {
            try
            {
                pconfig.AppSettings.Settings.Remove(pkey.Trim().ToLower());
                pconfig.AppSettings.Settings.Add(pkey.Trim().ToLower(), pvalue.Trim());
                ConfigurationManager.RefreshSection(StrAppSettings);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Public Method

        public string GetValueFromKey(string pKey)
        {
            return GetValueFromKey(pKey.ToLower(), _config);
        }

        public bool SetValueForKey(string pkey, string pvalue)
        {
            return SetValueForKey(pkey, pvalue, _config);
        }

        public bool SaveConfig()
        {
            bool result;
            try
            {
                _config.Save(ConfigurationSaveMode.Modified);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        #endregion
    }
}