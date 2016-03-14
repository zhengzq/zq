using System;

namespace Zq.Logging
{
    public interface ILogger
    {
        void Log(LogLevel level, string message);
        void Log(LogLevel level, string format, params object[] args);
        void Log(LogLevel level, Exception exception);
        void Log(LogLevel level, Exception exception, string message);
        void Log(LogLevel level, Exception exception, string format, params object[] args);
    }
}
