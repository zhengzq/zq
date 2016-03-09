
using System;

namespace Zq.Messages
{
    public interface IBus
    {
        void Publish<T>(T message);
        void PublishAsync<T>(T message));
        void Subscribe<T>(string subscriptionId, Action<T> consumer) where T : class;
        void SubscribeAsync<T>(string subscriptionId, Action<T> consumer) where T : class;
    }
}
