using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                logger.Log(LogLevel.Error, "INFO 信息");
                throw new Exception("自定义错误");
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex);
            }
        }

        [TestMethod]
        public void Error_Name()
        {
            var logger = new Log4NetLogger("logtest");
            try
            {
                logger.Log(LogLevel.Error, "Test INFO 信息");
                throw new Exception("Test 自定义错误");
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex);
            }
        }
    }
}
