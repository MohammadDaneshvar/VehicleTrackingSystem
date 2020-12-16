using System;

namespace Framework.Application
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider serviceProvider;

        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommandHandler<T> CreateHandler<T>()
        {
            return (ICommandHandler<T>) serviceProvider.GetService(typeof(ICommandHandler<T>));
        }
    }
}