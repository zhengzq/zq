using System;

namespace Zq.Logging
{
    public class NullLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            
        }

        public void Log(LogLevel level, string format, params object[] args)
        {
            
        }

        public void Log(LogLevel level, Exception exception)
        {
            
        }

        public void Log(LogLevel level, Exception exception, string message)
        {
            
        }

        public void Log(LogLevel level, Exception exception, string format, params object[] args)
        {
            
        }
    }
}
