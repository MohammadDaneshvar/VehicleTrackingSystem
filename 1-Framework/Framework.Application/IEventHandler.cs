namespace Framework.Application
{
    public interface IEventHandler<in T>
    {
        void Handle(T @event);
    }
}