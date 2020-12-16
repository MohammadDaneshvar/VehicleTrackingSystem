using System.Collections.Generic;

namespace Framework.Application
{
    public interface IEventHandlerFactory
    {
        List<IEventHandler<T>> CreateHandler<T>();
    }
}