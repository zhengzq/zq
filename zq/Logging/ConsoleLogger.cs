using System;

namespace Zq.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Debug(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, args));
        }

        public void Debug(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Debug(Exception exception, string message)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Debug(Exception exception, string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Error(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, args));
        }

        public void Error(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Error(Exception exception, string message)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Error(Exception exception, string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Fatal(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Fatal(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, args));
        }

        public void Fatal(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Fatal(Exception exception, string message)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Fatal(Exception exception, string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Info(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, args));
        }

        public void Info(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Info(Exception exception, string message)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Info(Exception exception, string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Warn(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Warn(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, args));
        }

        public void Warn(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Warn(Exception exception, string message)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }

        public void Warn(Exception exception, string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(exception.ToString());
        }
    }
}
