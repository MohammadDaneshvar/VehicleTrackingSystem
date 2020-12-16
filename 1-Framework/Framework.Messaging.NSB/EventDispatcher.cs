using Framework.Domain.Aggregate;
using Framework.Domain.Events;
using NServiceBus;

namespace Framework.Messaging.NSB
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IEndpointInstance bus;

        public EventDispatcher(IEndpointInstance bus)
        {
            this.bus = bus;
        }

        public void Dispatch<T>(T @event) where T : IDomainEvent
        {
            _ = bus.Publish(@event);
        }
    }
}
