namespace Zq.Messages
{
    public interface IProducer
    {
        void Publish<T>(T message) where T : class, IMessage;
        void PublishAsync<T>(T message) where T : class, IMessage;
    }
}
