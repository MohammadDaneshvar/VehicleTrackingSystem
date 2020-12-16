using Framework.Domain.Aggregate;
using Framework.Domain.Events;

namespace Framework.Application
{
    public class  EventDispatcher : IEventDispatcher
    {
        private readonly IEventHandlerFactory _factory;

        public EventDispatcher(IEventHandlerFactory factory)
        {
            _factory = factory;
        }
        public void Dispatch<T>(T @event) where T : IDomainEvent
        {
            _factory.CreateHandler<T>().ForEach(e => e.Handle(@event));
        }
    }
}