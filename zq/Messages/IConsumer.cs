namespace Zq.Messages
{
    public interface IConsumer
    {
        void Handle(IMessage message);
    }
}
