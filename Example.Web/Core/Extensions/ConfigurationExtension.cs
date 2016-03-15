using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Zq.Autofac;
using Zq.Configurations;
using Zq.Domain;

namespace Example.Web.Core.Extensions
{
    public static class ConfigurationExtension
    {
        public static Configuration UseIoc(this Configuration configuration)
        {
            configuration.UseAutofac(() =>
            {
                if (HttpContext.Current != null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;
                return null;
            });

            if (configuration.Container == null)
                throw new InvalidOperationException("container isn't initialized");

            var assemblies = new Assembly[]
            {
                 Assembly.Load("Example.Web"),
            };

            configuration.Container.RegisterComponentFromAssemblys(assemblies);

            configuration.Container.CustomRegisterComponents(obj =>
            {
                var container = obj as IContainer;
                var builder = new ContainerBuilder();
                builder.Register(c => (new HttpContextWrapper(HttpContext.Current) as HttpContextBase)).As<HttpContextBase>().InstancePerLifetimeScope();
               
                builder.RegisterControllers(assemblies);
                builder.Update(container);
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            });
            return configuration;
        }
    }
}