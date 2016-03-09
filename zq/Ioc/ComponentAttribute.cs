using System;

namespace Zq.Ioc
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public Type Type { get;private set; }
        public LifeTime LifeTime { get; private set; }
        public ComponentAttribute(Type type, LifeTime lifeTime = LifeTime.Single)
        {
            this.Type = type;
            this.LifeTime = lifeTime;
        }
    }
}
