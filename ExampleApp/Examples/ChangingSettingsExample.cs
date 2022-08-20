using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogSharper;

namespace ExampleApp.Examples
{
    public static class ChangingSettingsExample
    {
        public static void Main()
        {
            LoggerSettings.UseColorLoggingSettingChange(true, true, Logger.LogTypes.Sucess);
            LoggerSettings.UseTimeDateOnLoggingSettingChange(true, true, Logger.LogTypes.Info);
            LoggerSettings.LogToFileSettingChange(false, true, Logger.LogTypes.Info);
            LoggerSettings.LogFileSettingChange(null, true, Logger.LogTypes.Sucess);
            LogSharper.LogSharper.Setup(true);
        }
    }
}
