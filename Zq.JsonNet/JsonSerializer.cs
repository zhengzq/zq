using Zq.Serializers;

namespace Zq.JsonNet
{
    public class JsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string json) 
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
