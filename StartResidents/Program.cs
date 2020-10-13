using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WZS.Common;

namespace StartResidents
{
    class Program
    {
        private static AppConfig _appConfig = new AppConfig();


        static void Main(string[] args)
        {
            try
            {
                Task.WaitAll(Task.Run(async () =>
                {
                    var resHelper = new ResidentHelper(_appConfig);
                    await resHelper.Login();
                    await resHelper.GetResidents();
                    await OpenResidentsProcess(resHelper.ResidentFileName);
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            NLog.LogManager.Shutdown();
        }

        private static async Task OpenResidentsProcess(string residentFileName)
        {
            await Task.Run(() => { Process.Start(_appConfig.ProcessFullName, residentFileName); });
        }

    }
}
