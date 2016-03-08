namespace Zq.Caching
{
    public partial class NullCacheManager : ICacheManager
    {
        public T Get<T>(string key, string region = "")
            where T : class
        {
            return default(T);
        }

        public void Set(string key, object data, int cacheTime, string region = "")
        {

        }

        public bool IsSet(string key, string region = "")
        {
            return false;
        }

        public void Remove(string key, string region = "")
        {
           
        }

        public void RemoveByPattern(string pattern)
        {
           
        }

        public void Clear()
        {
             
        }
    }
}
