using System;
using System.Collections.Generic;

namespace Framework.Domain.Events
{
    public static class EventPublisher
    {

        [ThreadStatic] private static Dictionary<Type, IList<Action<object>>> _eventHandlers;
        internal static Dictionary<Type, IList<Action<object>>> EventHandlers
        {
            get
            {
                return (_eventHandlers ?? (_eventHandlers=new Dictionary<Type, IList<Action<object>>>()));
            }
        }

        public static void Raise<E>(E @event)
        {
            if (EventHandlers.ContainsKey(@event.GetType().GetInterfaces()[0]))
                foreach (var handler in EventHandlers[@event.GetType().GetInterfaces()[0]])
                    handler(@event);
        }

        public static IDisposable Register<E>(Action<E> handler)
        {
            Action<object> callback = @event => handler((E)@event);
            if (!EventHandlers.ContainsKey(typeof(E)))
                EventHandlers[typeof(E)] = new List<Action<object>>();
            EventHandlers[typeof(E)].Add(callback);
            return new DomainEventRegistrationRemover(
                () => { if (EventHandlers[typeof(E)].Contains(callback)) EventHandlers[typeof(E)].Remove(callback); });
        }
    }
}