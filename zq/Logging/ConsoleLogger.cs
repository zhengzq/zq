using System;

namespace Zq.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public void Debug(string format, params object[] args)
        {
            Console.WriteLine(string.Format(format, args));
        }

        public void Debug(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Debug(Exception exception, string message)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Debug(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string format, params object[] args)
        {
            Console.WriteLine(string.Format(format, args));
        }

        public void Error(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Error(Exception exception, string message)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Error(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Fatal(string message)
        {
            Console.WriteLine(message);
        }

        public void Fatal(string format, params object[] args)
        {
            Console.WriteLine(string.Format(format, args));
        }

        public void Fatal(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Fatal(Exception exception, string message)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Fatal(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Info(string format, params object[] args)
        {
            Console.WriteLine(string.Format(format, args));
        }

        public void Info(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Info(Exception exception, string message)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Info(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Warn(string message)
        {
            Console.WriteLine(message);
        }

        public void Warn(string format, params object[] args)
        {
            Console.WriteLine(string.Format(format, args));
        }

        public void Warn(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Warn(Exception exception, string message)
        {
            Console.WriteLine(exception.ToString());
        }

        public void Warn(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}
