namespace Zq.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key, string region = "")
            where T : class;
        void Set(string key, object data, int cacheTime, string region = "");
        bool IsSet(string key, string region = "");
        void Remove(string key, string region = "");
        void Clear();
    }
}
