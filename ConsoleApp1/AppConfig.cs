using System.Configuration;

namespace ConsoleApp1
{
    public class AppConfig
    {
        #region -- Config Keys --
        private const string URI_CONFIG_KEY = "URI";
        private const string FILENAME_CONFIG_KEY = "FILENAME";
        private const string FILEPATH_CONFIG_KEY = "FILEPATH";
        private const string PROCESS_FULL_NAME_CONFIG_KEY = "PROCESS_FULL_NAME";
        private const string DELIMITER_CONFIG_KEY = "DELIMITER";
        private const string LOGIN_CONFIG_KEY = "LOGIN";
        private const string DOMAIN_CONFIG_KEY = "DOMAIN";
        private const string PASSWORD_CONFIG_KEY = "PASSWORD";
        private const string APPLICATION_KEY_CONFIG_KEY = "APPLICATION_KEY";
        #endregion

        #region -- Properties --
        public string Uri => ConfigurationManager.AppSettings[URI_CONFIG_KEY];
        public string FileName => ConfigurationManager.AppSettings[FILENAME_CONFIG_KEY];
        public string FilePath => ConfigurationManager.AppSettings[FILEPATH_CONFIG_KEY];
        public string ProcessFullName => ConfigurationManager.AppSettings[PROCESS_FULL_NAME_CONFIG_KEY];
        public string Delimiter => ConfigurationManager.AppSettings[DELIMITER_CONFIG_KEY];
        public string Login => ConfigurationManager.AppSettings[LOGIN_CONFIG_KEY];
        public string Domain => ConfigurationManager.AppSettings[DOMAIN_CONFIG_KEY];
        public string Password => ConfigurationManager.AppSettings[PASSWORD_CONFIG_KEY];
        public string ApplicationKey => ConfigurationManager.AppSettings[APPLICATION_KEY_CONFIG_KEY];
        #endregion
    }
}
