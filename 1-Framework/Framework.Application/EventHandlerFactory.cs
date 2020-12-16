using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace Framework.Application
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        private readonly Container _container;

        public EventHandlerFactory(Container container)
        {
            _container = container;
        }
        public List<IEventHandler<T>> CreateHandler<T>()
        {
            return _container.GetAllInstances<IEventHandler<T>>().ToList();
        }
    }
}
