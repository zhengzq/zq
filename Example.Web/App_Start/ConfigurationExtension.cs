using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Example.Core.Data;
using Example.Core.Web.Authentication;
using Zq;
using Zq.Autofac;
using Zq.Configurations;
using Zq.Domain;
using Zq.UnitOfWork;

namespace Example.Web
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
                throw new InvalidOperationException("container hasn't initialized");

            var assemblies = new Assembly[]
            {
                 Assembly.Load("Example.Web"),
                 Assembly.Load("Example.Core"),
            };

            configuration.Container.CustomRegisterComponents(obj =>
            {
                var container = obj as IContainer;
                var builder = new ContainerBuilder();

                //HttpContext注入
                builder.Register(c => (new HttpContextWrapper(HttpContext.Current) as HttpContextBase)).As<HttpContextBase>().InstancePerLifetimeScope();

                //验证注入
                builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>()
                .InstancePerLifetimeScope();

                //DbContext注入
                builder.RegisterType<EfDbContext>().As<IDbContext>().InstancePerLifetimeScope();

                //UnitOfWork注入
                builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

                //数据层注入
                builder.RegisterAssemblyTypes(assemblies)
                .Where(t =>
                {
                    var b = !t.IsInterface && t.Name.EndsWith("Repository");
                    return b;
                }).AsImplementedInterfaces().InstancePerDependency();

                //应用层注入
                builder.RegisterAssemblyTypes(assemblies)
                .Where(t =>
                {
                    var b = !t.IsInterface && typeof(IApplicationService).IsAssignableFrom(t);
                    return b;
                }).AsImplementedInterfaces().InstancePerDependency();

                //查询层注入
                builder.RegisterAssemblyTypes(assemblies)
                .Where(t =>
                {
                    var b = !t.IsInterface && typeof(IQueryService).IsAssignableFrom(t);
                    return b;
                }).AsImplementedInterfaces().InstancePerDependency();


                builder.RegisterControllers(assemblies);
                builder.Update(container);
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            });
            return configuration;
        }
    }
}