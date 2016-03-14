using System;

namespace Zq.Logging
{
    public class ConsoleLogger : ILogger
    {
        
        public void Log(LogLevel level,string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
        public void Log(LogLevel level,string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, args));
        }
        public void Log(LogLevel level,Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }
        public void Log(LogLevel level, Exception exception, string message)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }
        public void Log(LogLevel level, Exception exception, string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }
    }
}
