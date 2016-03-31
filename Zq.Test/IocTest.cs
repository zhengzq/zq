using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zq.Autofac;
using Zq.DI;
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
        public void Autofac_Thread_Single()
        {
            var hashCodes = new List<int>();

            var container = new AutofacObjectContainer();
            container.Register<Worker, IWorker>();

            ThreadPool.QueueUserWorkItem(obj =>
            {
                Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                for (var i = 0; i < 2; i++)
                {
                    var w = container.Resolve<IWorker>();
                    hashCodes.Add(w.GetHashCode());
                }
            });

            ThreadPool.QueueUserWorkItem(obj =>
            {
                Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                for (var i = 0; i < 2; i++)
                {
                    var w = container.Resolve<IWorker>();
                    hashCodes.Add(w.GetHashCode());
                }
            });
            Thread.Sleep(1000 * 5);
            Debug.WriteLine(string.Join("\n", hashCodes));
            Assert.AreEqual(true, hashCodes.GroupBy(x => x).Count() == 1);
        }
        [TestMethod]
        public void Autofac_Hierarchical()
        {
            //var hashCodes = new List<int>();

            var container = new AutofacObjectContainer();
            container.Register<Worker, IWorker>(LifeTime.Thread)
                .Register<Employee, IEmployee>(LifeTime.Thread)
                .Register<Tool, ITool>(LifeTime.Single);

            var scope = container.Scope();
            var w = scope.Resolve<IWorker>();
            var e = scope.Resolve<IEmployee>();

            Debug.WriteLine("worker tool:{0}\nemployee tool:{1}", w.Tool.GetHashCode(), e.Tool.GetHashCode());

            Assert.AreEqual(true, w.Tool.GetHashCode() == e.Tool.GetHashCode());

            //Debug.WriteLine(string.Join("\n", hashCodes));
            //Assert.AreEqual(true, hashCodes.GroupBy(x => x).Count() == 1);
        }
        [TestMethod]
        public void Autofac_Thread_Hierarchical()
        {
            var hashCodes = new List<int>();

            var container = new AutofacObjectContainer();
            container.Register<Worker, IWorker>(LifeTime.Thread)
              .Register<Employee, IEmployee>(LifeTime.Thread)
              .Register<Tool, ITool>(LifeTime.Thread);

            ThreadPool.QueueUserWorkItem(obj =>
            {
                Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                var scope = container.Scope();
                for (var i = 0; i < 2; i++)
                {
                    var w = scope.Resolve<IWorker>();
                    var e = scope.Resolve<IEmployee>();

                    Debug.WriteLine("worker tool:{0}\nemployee tool:{1}", w.Tool.GetHashCode(), e.Tool.GetHashCode());

                    hashCodes.Add(w.GetHashCode());
                }
            });

            ThreadPool.QueueUserWorkItem(obj =>
            {
                Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
                var scope = container.Scope();
                for (var i = 0; i < 2; i++)
                {
                    var w = scope.Resolve<IWorker>();
                    var e = scope.Resolve<IEmployee>();

                    Debug.WriteLine("worker tool:{0}\nemployee tool:{1}", w.Tool.GetHashCode(), e.Tool.GetHashCode());
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
            container.Register<Worker, IWorker>(LifeTime.Thread)
                 .Register<Tool, ITool>(LifeTime.Thread);

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
            container.Register<Worker, IWorker>(LifeTime.Thread)
                 .Register<Tool, ITool>(LifeTime.Thread);

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
                 .Register<Tool, ITool>(LifeTime.Thread);

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
