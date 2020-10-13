using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZS.Common;

namespace ConsoleApp1
{
    public class ResidentHelper
    {
        #region -- Variables --
        private readonly AppConfig _appConfig;
        private readonly WZSServiceClient _client;
        private readonly NLog.Logger _logger;

        private LoginInfo _loginInfo;
        private List<Resident> _residents;
        #endregion

        #region -- Constructor --
        public ResidentHelper(AppConfig appConfig)
        {
            _appConfig = appConfig;
            _client = new WZSServiceClient(_appConfig.Uri);
            _logger = NLog.LogManager.GetCurrentClassLogger();

            ResidentFileName = BuildCompleteFileName();
        }
        #endregion

        #region -- Properties --
        public string ResidentFileName { get; }
        private string _sessionID => _loginInfo.sessionID;
        #endregion

        #region -- Public Methods --
        /// <summary>
        /// Build the <see cref="LoginParams"/> object and call the <see cref="WZSServiceClient.Login(LoginParams)"/> method
        /// </summary>
        /// <returns></returns>
        public async Task Login()
        {
            await Task.Run(() =>
            {
                _logger.Debug("Login ongoing");
                LoginParams loginParams = new LoginParams()
                {
                    aUserID = $"{_appConfig.Login}@{_appConfig.Domain}",
                    aPassword = _appConfig.Password,
                    aApplicationKey = _appConfig.ApplicationKey
                };
                _loginInfo = _client.Login(loginParams);
                _logger.Debug("Login Done with session ID {0}", _sessionID);
            });
        }

        /// <summary>
        /// Build the <see cref="GetResidentsParameters"/> with the Active filter = true then call the <see cref="WZSServiceClient.GetResidents(GetResidentsParameters)"/> method
        /// Once the residents are retrieved, then log it to a csv file
        /// </summary>
        /// <returns></returns>
        public async Task GetResidents()
        {
            await Task.Run(() =>
            {
                _logger.Debug("Get Residents ongoing");
                GetResidentsParameters residentParams = new GetResidentsParameters()
                {
                    // TODO: filter on what you want (I.E. Active = true)
                    Active = true,
                    SessionID = _sessionID
                };
                _residents = _client.GetResidents(residentParams).OrderBy(r => r.ID).ToList();
                _logger.Debug("Get Resident done. Received {0} residents", _residents.Count);
            });
            if (_residents.Any())
            {
                await LogResidents();
            }
        }
        #endregion

        #region -- Private Methods --

        private async Task LogResidents()
        {
            _logger.Info($"Logging residents");
            using (var writer = new StreamWriter(BuildCompleteFileName()))
            using (var csv = new CsvWriter(writer, BuildCsvConfiguration()))
            {
                await csv.WriteRecordsAsync(_residents);
            }
            _logger.Info("Residents logged");
        }

        private string ConvertResidentToString(Resident resident)
        {
            StringBuilder sbResident = new StringBuilder();
            foreach (var property in resident.GetType().GetProperties())
            {
                string propertyName = property.Name;
                string propertyValue = property.GetValue(resident)?.ToString() ?? "N/A";
                if (string.IsNullOrEmpty(propertyValue))
                {
                    propertyValue = "N/A";
                }
                sbResident.AppendLine($"{propertyName}: {propertyValue}");
            }
            return sbResident.ToString();
        }

        private CsvConfiguration BuildCsvConfiguration()
        {
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.Delimiter = _appConfig.Delimiter;
            config.ShouldQuote = ShouldQuote;
            config.TrimOptions = TrimOptions.Trim;
            return config;
        }

        private bool ShouldQuote(string field, object context)
        {
            return !string.IsNullOrEmpty(field);
        }

        private string BuildCompleteFileName()
        {
            string filePath = _appConfig.FilePath;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return Path.Combine(filePath, _appConfig.FileName);
        }

        #endregion
    }
}
