namespace Zq.Serializers
{
    public partial interface IJsonSerializer
    {
        T Deserialize<T>(string json) ;

        string Serialize(object obj);
    }
}
