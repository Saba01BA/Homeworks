using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace BankingApp.Services
{
    public class LoggerService
    {
        public LoggerService()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("atm_log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void LogInfo(string message)
        {
            Log.Information(message);
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public void LogError(string message)
        {
            Log.Error(message);
        }

        public void Close()
        {
            Log.CloseAndFlush();
        }
    }
}
