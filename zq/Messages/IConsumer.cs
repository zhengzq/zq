namespace Zq.Messages
{
    public interface IConsumer
    {
        void Handle<T>(T message) where T : class, IMessage;
    }


}
