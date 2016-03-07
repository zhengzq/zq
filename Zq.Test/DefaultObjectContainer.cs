using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zq.Ioc;

namespace Zq.Test
{
    public class DefaultObjectContainer : IObjectContainer
    {
        private Dictionary<Type, object> _dic;
        public T Resovle<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ResovleAll<T>()
        {
            throw new NotImplementedException();
        }

        public object Resovle(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
