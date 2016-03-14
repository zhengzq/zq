using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zq.Autofac;
using Zq.Configurations;
using Zq.Ioc;
using Zq.Redis;

namespace Zq.Test
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void Configuration_Container_Test()
        {
            Configuration.Instance.UseAutofac();

            Assert.AreEqual(true, Configuration.Instance.Container != null);

        }
        [TestMethod]
        public void Configuration_ObjectLocator_Test()
        {
            Configuration.Instance.UseAutofac();

            Configuration.Instance.Container.Register<Worker, IWorker>();

            Assert.AreEqual(true, ObjectLocator.Resolve<IWorker>() != null);
        }
    }

}
