using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zq.Autofac;
using Zq.Ioc;
using Zq.Unity;

namespace Zq.Test
{
    [TestClass]
    public class IocTest
    {
        [TestMethod]
        public void Autofac_Single()
        {
            var hashCodes = new List<int>();

            var container = new AutofacObjectContainer();
            container.Register<Worker, IWorker>();

            for (var i = 0; i < 10; i++)
            {
                var w = container.Resolve<IWorker>();
                hashCodes.Add(w.GetHashCode());
            }
            Debug.WriteLine(string.Join("\n", hashCodes));
            Assert.AreEqual(true, hashCodes.GroupBy(x => x).Count() == 1);
        }
        [TestMethod]
        public void Autofac_Hierarchical()
        {
            var hashCodes = new List<int>();

            var container = new AutofacObjectContainer();
            container.Register<Worker, IWorker>(LifeTime.Hierarchical)
                .Register<Tool, ITool>();

            for (var i = 0; i < 10; i++)
            {
                var w = container.Resolve<IWorker>();
                hashCodes.Add(w.GetHashCode());
            }
            Debug.WriteLine(string.Join("\n", hashCodes));
            Assert.AreEqual(true, hashCodes.GroupBy(x => x).Count() == 1);
        }
        [TestMethod]
        public void Autofac_Thread_Hierarchical()
        {
            var hashCodes = new List<int>();

            var container = new AutofacObjectContainer();
            container.Register<Worker, IWorker>(LifeTime.Hierarchical)
              .Register<Tool, ITool>();

            ThreadPool.QueueUserWorkItem(obj =>
            {
                for (var i = 0; i < 1; i++)
                {
                    var w = container.Resolve<IWorker>();
                    hashCodes.Add(w.GetHashCode());
                }
            });

            ThreadPool.QueueUserWorkItem(obj =>
            {
                for (var i = 0; i < 1; i++)
                {
                    var w = container.Resolve<IWorker>();
                    hashCodes.Add(w.GetHashCode());
                }
            });
            Thread.Sleep(1000 * 5);
            Debug.WriteLine(string.Join("\n", hashCodes));
            Assert.AreEqual(true, hashCodes.GroupBy(x => x).Count() == 2);
        }
        [TestMethod]
        public void Unity_Single()
        {
            var hashCodes = new List<int>();

            var container = new UnityObjectContainer();
                 container.Register<Worker, IWorker>()
                 .Register<Tool, ITool>();

            for (var i = 0; i < 10; i++)
            {
                var w = container.Resolve<IWorker>();
                hashCodes.Add(w.GetHashCode());
            }

            Debug.WriteLine(string.Join("\n", hashCodes));
            Assert.AreEqual(true, hashCodes.GroupBy(x => x).Count() == 1);
        }
        [TestMethod]
        public void Unity_Hierarchical()
        {
            var hashCodes = new List<int>();

            var container = new UnityObjectContainer();
            container.Register<Worker, IWorker>(LifeTime.Hierarchical)
                 .Register<Tool, ITool>(LifeTime.Hierarchical);

            for (var i = 0; i < 10; i++)
            {
                var w = container.Resolve<IWorker>();
                hashCodes.Add(w.GetHashCode());
            }

            Debug.WriteLine(string.Join("\n", hashCodes));
            Assert.AreEqual(true, hashCodes.GroupBy(x => x).Count() == 1);
        }
        [TestMethod]
        public void Unity_Thread_Hierarchical()
        {
            var hashCodes = new List<int>();

            var container = new UnityObjectContainer();
            container.Register<Worker, IWorker>(LifeTime.Hierarchical)
                 .Register<Tool, ITool>(LifeTime.Hierarchical);

            ThreadPool.QueueUserWorkItem(obj =>
            {
                for (var i = 0; i < 1; i++)
                {
                    var w = container.Resolve<IWorker>();
                    hashCodes.Add(w.GetHashCode());
                }
            });

            ThreadPool.QueueUserWorkItem(obj =>
            {
                for (var i = 0; i < 1; i++)
                {
                    var w = container.Resolve<IWorker>();
                    hashCodes.Add(w.GetHashCode());
                }
            });
            Thread.Sleep(1000 * 5);
            Debug.WriteLine(string.Join("\n", hashCodes));
            Assert.AreEqual(true, hashCodes.GroupBy(x => x).Count() == 2);
        }
        [TestMethod]
        public void Unity_Transient()
        {
            var hashCodes = new List<int>();

            var container = new UnityObjectContainer();

            container.Register<Worker, IWorker>(LifeTime.Transient)
                 .Register<Tool, ITool>(LifeTime.Hierarchical);

            for (var i = 0; i < 10; i++)
            {
                var w = container.Resolve<IWorker>();
                hashCodes.Add(w.GetHashCode());
            }
            Debug.WriteLine(string.Join("\n", hashCodes));
            Assert.AreEqual(true, hashCodes.GroupBy(x => x).Count() == 10);
        }
    }



}
