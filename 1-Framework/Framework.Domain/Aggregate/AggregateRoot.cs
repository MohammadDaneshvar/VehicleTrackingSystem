using System;
using System.Collections.Generic;
using Framework.Domain.Events;

namespace Framework.Domain.Aggregate
{
    public abstract class AggregateRoot<TId> : EntityBase<AggregateRoot<TId>, TId>, IAggregateRoot
    {
        private readonly IList<IDomainEvent> _events = new List<IDomainEvent>();

        object IAggregateRoot.Id
        {
            get { return Id; }
        }

        IList<IDomainEvent> IAggregateRoot.GetUnPublishedEvents()
        {
            return _events;
        }

        void IAggregateRoot.ClearEvents()
        {
            _events.Clear();
        }

        protected void Publish<T>(T @event) where T : IDomainEvent
        {
            _events.Add(@event);
            EventPublisher.Raise<T>(@event);
        }

        protected void RaiseError(string key, string message)
        {
        }
    }
}