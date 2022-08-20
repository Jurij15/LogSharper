using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.Common;

namespace LogSharper
{
    /// <summary>
    /// Basic info about LogSharper
    /// </summary>
    public class LogSharper
    {
        private static string Version = "1.0";
        public static string GetVersion()
        {
            return Version;
        }

        public static void LogLoggerInfo()
        {
            Logger.Info("LogSharper, (An attempt at making) A simple Single-File logger solution in c#, made by Jurij15, Version " + GetVersion());
        }

        public static void Setup(bool showSucessMessage)
        {
            if (showSucessMessage)
            {
                Logger.Info("Logger Initialized!");
            }
        }
    }
    /// <summary>
    /// Logger Settings
    /// 
    /// You can set these at any time, and it will apply immidiately
    /// If you don't change any of these, default values will be used
    /// </summary>
    public static class LoggerSettings
    {
        public static void UseColorLoggingSettingChange(bool newValue, bool logChange, Logger.LogTypes LogType)
        {
            LoggerProperties.UseColorLogging = newValue;
            if (logChange)
            {
                LoggerInternal.Log_SettingChanged("UseColorLogging", LogType);
            }
        }
        public static void UseTimeDateOnLoggingSettingChange(bool newValue, bool logChange, Logger.LogTypes LogType)
        {
            LoggerProperties.UseTimeAndDateOnLogging = newValue;
            if (logChange)
            {
                LoggerInternal.Log_SettingChanged("UseTimeAndDateOnLogging", LogType);
            }
        }
        public static void LogToFileSettingChange(bool newValue, bool logChange, Logger.LogTypes LogType)
        {
            LoggerProperties.WriteToFile = newValue;
            if (logChange)
            {
                LoggerInternal.Log_SettingChanged("LogToFile", LogType);
            }
        }
        public static void LogFileSettingChanged(string newValue, bool logChange, Logger.LogTypes LogType)
        {
            LoggerProperties.LogFile = newValue;
            if (logChange)
            {
                LoggerInternal.Log_SettingChanged("LogFile", LogType);
            }
        }
    }

    /// <summary>
    /// Properties, for internal use
    /// </summary>
    public static class LoggerProperties
    {
        public static bool UseColorLogging = true;
        public static bool UseTimeAndDateOnLogging = true;
        public static bool WriteToFile = false;
        public static string LogFile = null;
    }

    /// <summary>
    /// internal stuff tbd
    /// </summary>
    public static class LoggerInternal
    {
        public static string GetCurrentTime(bool ForLog)
        {
            if (ForLog)
            {
                return "["+DateTime.Now.ToString("yyyy-dd-MM-HH-mm-ss")+"]";
            }
            else if (!ForLog)
            {
                return DateTime.Now.ToString();
            }

            return null; //NoT AlL CoDe PaThS ReTuRn A vAlUe
        }
        public static void Log_SettingChanged(string SettingName, Logger.LogTypes LogType)
        {
            Logger.Universal(SettingName, LogType);
        }
    }

    /// <summary>
    /// Logger File Class
    /// 
    /// Contains methods and functions to write text to files
    /// </summary>
    public static class LoggerFile
    {
        public static void WriteToFile(string text)
        {
            if (LoggerProperties.WriteToFile)
            {
                if (LoggerProperties.LogFile != null)
                {
                    if (File.Exists(LoggerProperties.LogFile))
                    {
                        using (StreamWriter sw = new StreamWriter(LoggerProperties.LogFile))
                        {
                            sw.WriteLine(text);
                            sw.Close();
                        }
                    }
                    else if (!File.Exists(LoggerProperties.LogFile))
                    {
                        using (StreamWriter sw = File.CreateText(LoggerProperties.LogFile))
                        {
                            sw.WriteLine(text);
                            sw.Close();
                        }
                    }
                }
            }
        }
    }

    public static class Logger
    {
        public enum LogTypes
        {
            Error,
            Warning,
            Info,
            Sucess
        }
        
        public static void Info(string text)
        {
            string LogText = "";

            if (LoggerProperties.UseTimeAndDateOnLogging)
            {
                LogText = LoggerInternal.GetCurrentTime(true) + "[Info]" + text;
            }
            else if (!LoggerProperties.UseTimeAndDateOnLogging)
            {
                LogText = "[Info]"+text;
            }
            LoggerFile.WriteToFile(LogText);
            Console.WriteLine(LogText);
        }

        public static void Warning(string text)
        {
            string LogText = "";
            if (LoggerProperties.UseTimeAndDateOnLogging)
            {
                LogText = LoggerInternal.GetCurrentTime(true)+ "[Warning]" + text;
            }
            else if (!LoggerProperties.UseTimeAndDateOnLogging)
            {
                LogText = text;
            }

            if (LoggerProperties.UseColorLogging)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            LoggerFile.WriteToFile(LogText);
            Console.WriteLine(LogText);
            Console.ResetColor();
        }

        public static void Success(string text)
        {
            string LogText = "";
            if (LoggerProperties.UseTimeAndDateOnLogging)
            {
                LogText = LoggerInternal.GetCurrentTime(true) + "[Success]" + text;
            }
            else if (!LoggerProperties.UseColorLogging)
            {
                LogText = "[Success]" + text;
            }

            if (LoggerProperties.UseColorLogging)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            LoggerFile.WriteToFile(LogText);
            Console.WriteLine(LogText);
            Console.ResetColor();
        }

        public static void Error(string text)
        {
            string LogText = "";
            if (LoggerProperties.UseTimeAndDateOnLogging)
            {
                LogText = LoggerInternal.GetCurrentTime(true) + "[Error]" + text;
            }
            else if (!LoggerProperties.UseTimeAndDateOnLogging)
            {
                LogText = LoggerInternal.GetCurrentTime(true) + "[Error]" + text;
            }

            if (LoggerProperties.UseColorLogging)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            LoggerFile.WriteToFile(LogText);
            Console.WriteLine(LogText);
            Console.ResetColor();
        }

        public static void Universal(string text, LogTypes LogType)
        {
            switch (LogType)
            {
                case ((LogTypes)(int)LogTypes.Info):
                    Info(text); 
                    break;
                case ((LogTypes)(int)LogTypes.Warning):
                    Warning(text);
                    break;
                case ((LogTypes)(int)LogTypes.Sucess):
                    Success(text);
                    break;
                case ((LogTypes)(int)LogTypes.Error):
                    Error(text);
                    break;
            }
        }
    }
}
