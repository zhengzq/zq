using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zq.Autofac;
using Zq.Configurations;
using Zq.Domain;
using Zq.Ioc;
using Zq.Logging;

namespace Zq.Test
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void AddTest()
        {
            Configuration.Instance
                .UseAutofac()
                .UseConsoleLog()
                .InitComponent();

            var repository = ObjectLocator.Resolve<IRepository<Worker>>();
            repository.Add(new Worker() { });
        }
    }
    
    public static class ConfigurationExtension
    {
        public static Configuration InitComponent(this Configuration configuration)
        {
            //configuration.Container.RegisterComponents(obj =>
            //{
            //    var container = obj as IContainer;
            //    var builder = new ContainerBuilder();


            //    //builder.RegisterAssemblyTypes(Assembly.Load("Zq.Test"))
            //    //.Where(t =>
            //    //{
            //    //    var b = !t.IsInterface && (t.Name.EndsWith("Repository"));
            //    //    return b;
            //    //}).AsImplementedInterfaces().InstancePerDependency();
            //    builder.RegisterGeneric(typeof(FakeRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //    builder.Update(container);
            //});
            return configuration;
        }
    }

    public interface IWorkerRepository : IRepository<Worker>
    {

    }

    public class WorkerRepository : FakeRepository<Worker>, IWorkerRepository
    {

        public override void Add(Worker aggregateRoot)
        {
            Console.WriteLine("Add");
        }
        public override void Delete(Worker aggregateRoot)
        {
            Console.WriteLine("Delete");
        }
        public override Worker Get(object id)
        {
            return new Worker(new Tool())
            {
                Id = id.ToString(),
                Name = "zzq"
            };
        }
        public override void Update(Worker aggregateRoot)
        {
            Console.WriteLine("Update");
        }
    }

}
