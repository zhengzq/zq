namespace Zq.Messages
{
    public interface IBus
    {
        void Publish<T>(T message);
    }
}
