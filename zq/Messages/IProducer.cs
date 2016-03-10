namespace Zq.Messages
{
    public interface IProducer
    {
        void Publish<T>(T message);
        void PublishAsync<T>(T message);
    }
}
