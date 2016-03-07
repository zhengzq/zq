using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zq.Serializers
{
    public class NullJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            throw new NotImplementedException();
        }

        public string Serialize(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
