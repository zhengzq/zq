using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using Zq.Common;
using Zq.Logging;

namespace Zq.Repository.Ef.Interceptors
{
    public class LogCommandInterceptor : DbCommandInterceptor
    {
        private readonly ILogger _logger;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public LogCommandInterceptor(ILogger logger)
        {
            _logger = logger;
        }

        [ThreadStatic]
        public static bool LogEntityStatsToDatabase = true;

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Ensure.IsNotNull(command, "command");
            Ensure.IsNotNull(interceptionContext, "interceptionContext");
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Ensure.IsNotNull(command, "command");
            Ensure.IsNotNull(interceptionContext, "interceptionContext");
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                Trace.TraceInformation("Error executing command: {0}", command.CommandText);
                if (LogEntityStatsToDatabase)
                {
                    _logger.Log(LogLevel.Error, $"Error executing command: {command.CommandText}", interceptionContext.Exception);
                }
            }
            else
            {
                Trace.TraceInformation("ScalarExecuted: {0}, Elapsed: {1}", command.CommandText, _stopwatch.Elapsed);
                if (LogEntityStatsToDatabase)
                {
                    _logger.Log(LogLevel.Error, "ScalarExecuted.  Elapsed: {0}, Command: {1}", _stopwatch.Elapsed, command.CommandText);
                }
            }
            base.ScalarExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Ensure.IsNotNull(command, "command");
            Ensure.IsNotNull(interceptionContext, "interceptionContext");
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Ensure.IsNotNull(command, "command");
            Ensure.IsNotNull(interceptionContext, "interceptionContext");
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                Trace.TraceInformation("Error executing command: {0}", command.CommandText);
                if (LogEntityStatsToDatabase)
                {
                    _logger.Log(LogLevel.Error, interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
                }
            }
            else
            {
                Trace.TraceInformation("NonQueryExecuted: {0}, Elapsed: {1}", command.CommandText, _stopwatch.Elapsed);
                if (LogEntityStatsToDatabase)
                {
                    _logger.Log(LogLevel.Error, "NonQueryExecuted.  Elapsed: {0}, Command: {1}", _stopwatch.Elapsed, command.CommandText);
                }
            }
            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Ensure.IsNotNull(command, "command");
            Ensure.IsNotNull(interceptionContext, "interceptionContext");
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Ensure.IsNotNull(command, "command");
            Ensure.IsNotNull(interceptionContext, "interceptionContext");
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                Trace.TraceInformation("Error executing command: {0}", command.CommandText);
                if (LogEntityStatsToDatabase)
                {
                    _logger.Log(LogLevel.Error, interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
                }
            }
            else
            {
                Trace.TraceInformation("ReaderExecuted: {0}, Elapsed: {1}", command.CommandText, _stopwatch.Elapsed);
                if (LogEntityStatsToDatabase)
                {
                    _logger.Log(LogLevel.Error, "ReaderExecuted.  Elapsed: {0}, Command: {1}", _stopwatch.Elapsed, command.CommandText);
                }
            }
            base.ReaderExecuted(command, interceptionContext);
        }
    }
}