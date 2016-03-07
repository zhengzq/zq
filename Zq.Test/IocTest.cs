using System;
using System.Threading;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zq.Autofac;
using Zq.Configurations;
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
            Configuration.Instance.UseAutofac();
            Configuration.Instance
                 .Container
                 .Register<Worker, IWorker>();

            for (var i = 0; i < 10; i++)
            {
                var w = ObjectLocator.Resolve<IWorker>();
                Console.WriteLine(w.GetHashCode().ToString());
            }
        }
        [TestMethod]
        public void Autofac_Hierarchical()
        {
            Configuration.Instance.UseAutofac();
            Configuration.Instance
                 .Container
                 .Register<Worker, IWorker>(LifeTime.Hierarchical)
                 .Register<Tool, ITool>();

            for (var i = 0; i < 10; i++)
            {
                var w = ObjectLocator.Resolve<IWorker>();
                Console.WriteLine(w.GetHashCode().ToString());
            }
        }
        [TestMethod]
        public void Autofac_Thread_Hierarchical()
        {
            //Configuration.Instance.UseAutofac();
            //Configuration.Instance
            //     .Container
            //     .Register<Worker, IWorker>(LifeTime.Hierarchical)
            //     .Register<Tool, ITool>(LifeTime.Hierarchical);

            var builder = new ContainerBuilder();
             builder.RegisterType<Worker>().As<IWorker>().InstancePerLifetimeScope();
            var container = builder.Build();

            ThreadPool.QueueUserWorkItem(obj =>
            {
              
                for (var i = 0; i < 10; i++)
                {
                    
                    var w = container.BeginLifetimeScope().Resolve<IWorker>();

                    Console.WriteLine(w.Number.ToString());
                }
            });
          
            ThreadPool.QueueUserWorkItem(obj =>
            {
                for (var i = 0; i < 10; i++)
                {
                    var w = container.BeginLifetimeScope().Resolve<IWorker>();
                    Console.WriteLine(w.Number.ToString());
                }
            });
            Thread.Sleep(1000 * 5);
        }
        [TestMethod]
        public void Unity_Single()
        {
            Configuration.Instance.UseUnity();
            Configuration.Instance
                 .Container
                 .Register<Worker, IWorker>();

            for (var i = 0; i < 10; i++)
            {
                var w = ObjectLocator.Resolve<IWorker>();
                Console.WriteLine(w.GetHashCode().ToString());
            }
        }
        [TestMethod]
        public void Unity_Hierarchical()
        {
            Configuration.Instance.UseUnity();
            Configuration.Instance
                 .Container
                 .Register<Worker, IWorker>(LifeTime.Hierarchical)
                 .Register<Tool, ITool>(LifeTime.Hierarchical);

            for (var i = 0; i < 10; i++)
            {
                var w = ObjectLocator.Resolve<IWorker>();
                Console.WriteLine(w.GetHashCode().ToString());
            }
        }
        [TestMethod]
        public void Unity_Thread_Hierarchical()
        {
            Configuration.Instance.UseUnity();
            Configuration.Instance
                 .Container
                 .Register<Worker, IWorker>(LifeTime.Hierarchical)
                 .Register<Tool, ITool>(LifeTime.Hierarchical);

            ThreadPool.QueueUserWorkItem(obj =>
            {

                for (var i = 0; i < 10; i++)
                {

                    var w = ObjectLocator.Resolve<IWorker>();

                    Console.WriteLine(w.Number.ToString());
                }
            });

            ThreadPool.QueueUserWorkItem(obj =>
            {
                for (var i = 0; i < 10; i++)
                {
                    var w = ObjectLocator.Resolve<IWorker>();
                    Console.WriteLine(w.Number.ToString());

                }
            });
            Thread.Sleep(1000 * 5);
        }
        [TestMethod]
        public void Unity_Transient()
        {
            Configuration.Instance.UseUnity();
            Configuration.Instance
                 .Container
                 .Register<Worker, IWorker>(LifeTime.Transient)
                 .Register<Tool, ITool>();

            for (var i = 0; i < 10; i++)
            {
                var w = ObjectLocator.Resolve<IWorker>();
                Console.WriteLine(w.GetHashCode().ToString());
            }
        }
    }

   

}
