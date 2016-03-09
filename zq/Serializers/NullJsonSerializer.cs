using System;

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
