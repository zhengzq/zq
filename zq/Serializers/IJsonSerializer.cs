using System;

namespace Zq.Serializers
{
    public partial interface IJsonSerializer
    {
        T Deserialize<T>(string json) ;

        string Serialize(object obj);

        object Deserialize(string json, Type type);
    }
}
