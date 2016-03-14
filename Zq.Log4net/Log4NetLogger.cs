using System;
using System.IO;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using Zq.Logging;

namespace Zq.Log4net
{

    public class Log4NetLogger : ILogger
    {
        private readonly ILog _logger;
        public Log4NetLogger(string name= "logerror", string configFile = "log4net.config")
        {
            var file = new FileInfo(configFile);
            if (!file.Exists)
            {
                file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile));
            }

            if (file.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(file);
            }
            else
            {
                BasicConfigurator.Configure(new TraceAppender { Layout = new PatternLayout() });
            }
            _logger = LogManager.GetLogger(name);
        }

        public void Log(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _logger.Debug(message);
                    break;
                case LogLevel.Warn:
                    _logger.Warn(message);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(message);
                    break;
                case LogLevel.Error:
                    _logger.Error(message);
                    break;
                case LogLevel.Info:
                default:
                    _logger.Info(message);
                    break;
            }
        }

        public void Log(LogLevel level, string format, params object[] args)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _logger.Debug(string.Format(format, args));
                    break;
                case LogLevel.Warn:
                    _logger.Warn(string.Format(format, args));
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(string.Format(format, args));
                    break;
                case LogLevel.Error:
                    _logger.Error(string.Format(format, args));
                    break;
                case LogLevel.Info:
                default:
                    _logger.Info(string.Format(format, args));
                    break;
            }
        }

        public void Log(LogLevel level, Exception exception)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _logger.Debug(exception);
                    break;
                case LogLevel.Warn:
                    _logger.Warn(exception);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(exception);
                    break;
                case LogLevel.Error:
                    _logger.Error(exception);
                    break;
                case LogLevel.Info:
                default:
                    _logger.Info(exception);
                    break;
            }
        }

        public void Log(LogLevel level, Exception exception, string message)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _logger.Debug(message, exception);
                    break;
                case LogLevel.Warn:
                    _logger.Warn(message, exception);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(message, exception);
                    break;
                case LogLevel.Error:
                    _logger.Error(message, exception);
                    break;
                case LogLevel.Info:
                default:
                    _logger.Info(message, exception);
                    break;
            }
        }

        public void Log(LogLevel level, Exception exception, string format, params object[] args)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _logger.Debug(string.Format(format, args), exception);
                    break;
                case LogLevel.Warn:
                    _logger.Warn(string.Format(format, args), exception);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(string.Format(format, args), exception);
                    break;
                case LogLevel.Error:
                    _logger.Error(string.Format(format, args), exception);
                    break;
                case LogLevel.Info:
                default:
                    _logger.Info(string.Format(format, args), exception);
                    break;
            }
        }
    }
}
