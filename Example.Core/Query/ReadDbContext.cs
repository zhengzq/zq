using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Zq.DI;
using Zq.Logging;

namespace Example.Core.Query
{
    public class ReadDbContext : Database
    {
        public ReadDbContext()
            : base("BlogDB")
        {
            // if (Mapper == null) Mapper = new MyMapper();
        }

        public override void OnExecutingCommand(IDbCommand cmd)
        {
            var sb = new StringBuilder();
            var sql = cmd.CommandText;
            sb.AppendLine(string.Format("[{0}]", DateTime.Now.ToString()));
            sb.AppendLine("[SQL]");
            sb.AppendLine(string.Format("{0}", sql));
            sb.AppendLine("[PARAMETERS]");
            foreach (var parameter in cmd.Parameters)
            {
                var p = parameter as SqlParameter;
                if (p != null) sb.Append(string.Format(@"{0}:{1} | ", p.ParameterName, p.Value));
            }
            var msg = sb.ToString();
            if (!string.IsNullOrWhiteSpace(msg))
            {
                var logger = Ioc.Resolve<ILogger>(new Tuple<string, object>("name", "SQL"));
                logger.Log(LogLevel.Info, msg);
            }

            base.OnExecutedCommand(cmd);
        }

        public override void OnExecutedCommand(IDbCommand cmd)
        {

        }


    }

}
