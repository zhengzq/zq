
using System;

namespace Zq.Messages
{
    public interface IBus
    {
        void Publish<T>(T message) 
            where T : class, IMessage;
        void PublishAsync<T>(T message)
            where T : class, IMessage;
        void Subscribe<T>(string subscriptionId, Action<T> consumer) 
            where T : class, IMessage;
        void SubscribeAsync<T>(string subscriptionId, Action<T> consumer) 
            where T : class, IMessage;
    }
}
