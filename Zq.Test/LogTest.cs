using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zq.Autofac;
using Zq.Configurations;
using Zq.Ioc;
using Zq.Log4net;
using Zq.Logging;

namespace Zq.Test
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void Error()
        {
            var logger = new Log4NetLogger();
            try
            {
                logger.Info("INFO 信息");
                throw new Exception("自定义错误");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}
