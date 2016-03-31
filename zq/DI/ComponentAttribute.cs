using System;

namespace Zq.DI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public Type Type { get;private set; }
        public LifeTime LifeTime { get; private set; }
        public ComponentAttribute(Type type, LifeTime lifeTime)
        {
            this.Type = type;
            this.LifeTime = lifeTime;
        }
    }
}
