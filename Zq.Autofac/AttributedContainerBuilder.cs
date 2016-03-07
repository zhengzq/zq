using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Zq.Ioc;

namespace Zq.Autofac
{
    public class AttributedContainerBuilder
    {
        public ContainerBuilder ContainerBuilder { get; private set; }

        public AttributedContainerBuilder()
        {
            this.ContainerBuilder = new ContainerBuilder();
        }

        public AttributedContainerBuilder(ContainerBuilder builder)
        {
            this.ContainerBuilder = builder;
        }

        public static ComponentAttribute FindComponentAttribute(Type t)
        {
            var attrs = t.GetCustomAttributes(typeof(ComponentAttribute), false) as ComponentAttribute[];
            if (attrs.Length == 0)
                return null;
            return attrs[0];
        }

        public void RegisterTypeWithAttribute(Type type)
        {
            var attr = FindComponentAttribute(type);
            if (attr == null)
            {
                throw new ArgumentException(string.Format("Specified type {0} does not have ComponentAttribute.", type.FullName));
            }

            var registration = ContainerBuilder.RegisterType(type).As(attr.Type ?? type);

            switch (attr.LifeTime)
            {
                case ComponentScope.InstancePerDependency:
                    registration.InstancePerDependency();
                    break;

                case ComponentScope.SingleInstance:
                    registration.SingleInstance();
                    break;

                case ComponentScope.InstancePerLifetimeScope:
                    registration.InstancePerLifetimeScope();
                    break;

                case ComponentScope.InstancePerMatchingLifetimeScope:
                    registration.InstancePerMatchingLifetimeScope(attr.Tag);
                    break;
            }
        }

        public void RegisterAssemblyTypesWithAttribute(params Assembly[] assemblies)
        {
            foreach (var type in assemblies.Distinct()
                                           .SelectMany(asm => asm.GetTypes())
                                           .Where(t => FindComponentAttribute(t) != null))
            {
                RegisterTypeWithAttribute(type);
            }
        }

        public IContainer Build()
        {
            return ContainerBuilder.Build();
        }

        public void Update(IComponentRegistry componentRegistry)
        {
            ContainerBuilder.Update(componentRegistry);
        }
    }
}
