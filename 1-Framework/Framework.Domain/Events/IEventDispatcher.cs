using Framework.Domain.Aggregate;

namespace Framework.Domain.Events
{
    public interface IEventDispatcher
    {
        void Dispatch<T>(T @event) where T : IDomainEvent;
    }
}