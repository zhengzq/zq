using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zq.Redis;

namespace Zq.Test
{
    [TestClass]
    public class CacheTest
    {
        [TestMethod]
        public void Redis_Test()
        {
            var watch = new Stopwatch();
            watch.Start();
            var cache = new RedisCacheManager();
            for (var i = 0; i < 10000; i++)
            {
                cache.Set(i.ToString(), i, 10);
            }
            watch.Stop();
            Debug.WriteLine(watch.ElapsedMilliseconds);
        }
        [TestMethod]
        public void Redis_Test1()
        {
            var watch = new Stopwatch();
            watch.Start();
            var cache = new RedisCacheManager();
            var queue = new Queue<int>();
            var thread1HandleCount = 0;
            var thread2HandleCount = 0;
            var thread3HandleCount = 0;
            for (var i = 0; i < 10000; i++)
            {
                queue.Enqueue(i);
            }
            ThreadPool.QueueUserWorkItem(x =>
            {
                while (queue.Count != 0)
                {
                    var i = queue.Dequeue();
                    cache.Set(i.ToString(), i, 10);
                    thread1HandleCount++;
                }
            });
            ThreadPool.QueueUserWorkItem(x =>
            {
                while (queue.Count != 0)
                {
                    var i = queue.Dequeue();
                    cache.Set(i.ToString(), i, 10);
                    thread2HandleCount++;
                }
            });
            ThreadPool.QueueUserWorkItem(x =>
            {
                while (queue.Count != 0)
                {
                    var i = queue.Dequeue();
                    cache.Set(i.ToString(), i, 10);
                    thread3HandleCount++;
                }
            });
            while (queue.Count != 0) { }
            watch.Stop();
            Debug.WriteLine("{0}  {1}  {2}", thread1HandleCount, thread2HandleCount, thread3HandleCount);
            Debug.WriteLine(watch.ElapsedMilliseconds);
        }

    }

}
