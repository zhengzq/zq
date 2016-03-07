using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using Zq.Logging;

namespace Zq.Log4net
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _logdebug;
        private readonly ILog _logwarn;
        private readonly ILog _loginfo;
        private readonly ILog _logerror;
        private readonly ILog _logfatal;
        public Log4NetLogger(string configFile = "log4net.config")
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

            _logdebug = LogManager.GetLogger("logdebug");
            _logwarn = LogManager.GetLogger("logwarn");
            _loginfo = LogManager.GetLogger("loginfo");
            _logerror = LogManager.GetLogger("logerror");
            _logfatal = LogManager.GetLogger("logfatal");
        }
        public void Debug(string message)
        {
            EnsureLogEnabled(_logdebug, "_logdebug.IsDebugEnabled is false");

            _logdebug.Debug(message);
        }

        public void Debug(string format, params object[] args)
        {
            EnsureLogEnabled(_logdebug, "_logdebug.IsDebugEnabled is false");

            _logdebug.Debug(string.Format(format, args));
        }

        public void Debug(Exception exception)
        {
            EnsureLogEnabled(_logdebug, "_logdebug.IsDebugEnabled is false");
          
            _logdebug.Debug(exception.Message, exception);
        }

        private void EnsureLogEnabled(ILog log, string message)
        {
            if (!log.IsDebugEnabled)
                throw new Exception(message);
        }

        public void Debug(Exception exception, string message)
        {
            EnsureLogEnabled(_logdebug, "_logdebug.IsDebugEnabled is false");
        }

        public void Debug(Exception exception, string format, params object[] args)
        {
            EnsureLogEnabled(_logdebug, "_logdebug.IsDebugEnabled is false");
        }

        public void Error(string format, params object[] args)
        {
            EnsureLogEnabled(_logerror, "_logerror.IsDebugEnabled is false");
        }

        public void Error(Exception exception)
        {
            EnsureLogEnabled(_logerror, "_logerror.IsDebugEnabled is false");


            if (!_logerror.IsErrorEnabled) return;

            var info = "【附加信息】:";

            info = info + "<br/>具体信息：" + ErrorDetails(exception);
            _logerror.Error(info);
        }

        public void Error(Exception exception, string message)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        private string ErrorDetails(Exception ex)
        {
            var sb = new StringBuilder();
            var count = 0;
            var appString = "";
            while (ex != null)
            {
                if (count > 0)
                {
                    appString += "　";
                }
                sb.AppendLine(appString + " <br>异常消息：" + ex.Message);
                sb.AppendLine(appString + " <br>异常类型：" + ex.GetType().FullName);
                sb.AppendLine(appString + " <br>异常方法：" + ex.TargetSite?.Name);
                sb.AppendLine(appString + " <br>异常源：" + ex.Source);
                if (ex.StackTrace != null)
                {
                    sb.AppendLine(appString + "<br>异常堆栈：" + ex.StackTrace);
                }
                if (ex.InnerException != null)
                {
                    sb.AppendLine(appString + "<br>内部异常：");
                    count++;
                }
                ex = ex.InnerException;
            }
            return sb.ToString().Replace("位置:", "<br>位置");
        }
        public void Error(string msg)
        {
            if (!_logerror.IsErrorEnabled) return;

            var info = "【附加信息】:";
            info = info + "<br/>具体信息：" + msg;
            _logerror.Error(info);
        }

        public void Fatal(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string msg)
        {
            if (!_loginfo.IsInfoEnabled) return;

            var info = "【附加信息】:";
            info = info + "<br/>具体信息：" + msg;
            _loginfo.Info(info);
        }

        public void Info(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(Exception exception, string message)
        {
            throw new NotImplementedException();
        }

        public void Info(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string msg)
        {
            if (!_logfatal.IsFatalEnabled) return;

            var info = "【附加信息】:";
            info = info + "<br/>具体信息：" + msg;
            _logfatal.Fatal(info);
        }

        public void Fatal(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception, string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string msg)
        {
            if (!_logwarn.IsWarnEnabled) return;

            var info = "【附加信息】:";
            info = info + "<br/>具体信息：" + msg;
            _logwarn.Warn(info);
        }

        public void Warn(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Warn(Exception exception, string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
