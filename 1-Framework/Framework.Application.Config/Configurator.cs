using Framework.Application.Common.GoogleMapApi;
using Framework.Domain.Events;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Framework.Application.Config
{
    public class FrameworkConfigurator
    {

        public static void WireUp(Container container, bool runAsClient, Assembly application, Assembly contracts)
        {
            container.Register<IServiceProvider>(() => container, Lifestyle.Singleton);
            container.Register<IEventDispatcher, EventDispatcher>(Lifestyle.Singleton);
            container.Register<ICommandHandlerFactory, CommandHandlerFactory>(Lifestyle.Singleton);
            container.Register<ICommandBus, CommandBus>(Lifestyle.Singleton);
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionalCommandHandler<>));
            container.Register(typeof(ICommandHandler<>), new List<Assembly> { application });
            container.Register(typeof(ICommandValidator<>), new List<Assembly> { contracts });
            container.Register<IMapAPI, GoogleMapApi>(Lifestyle.Singleton);
            container.Register<ISignatureProvider, SignatureServiceProvider>(Lifestyle.Singleton);
            //container.Register<IKeyGenerator, KeyGenerator>(Lifestyle.Singleton);



            




            //container.RegisterDecorator(typeof(ICommandHandler<>), typeof(AccessValidatorCommandHandler<>), t => typeof(IRestrictedCommand).IsAssignableFrom(t.ServiceType.GetGenericArguments().First()));

            //container.RegisterDecorator(typeof(ICommandHandler<>), typeof(ValidatorCommandHandler<>));
            //container.Register<ISession>(() =>
            //                SessionFactoryBuilder.Create("Core", typeof(OrderMapper).Assembly).OpenSession()
            //            , Lifestyle.Scoped);
            //container.Register<IValidator, Validator>();
            container.Register<IEventHandlerFactory, EventHandlerFactory>(Lifestyle.Singleton);
            container.Collection.Register(typeof(IEventHandler<>), new List<Assembly> { application });
        }
    }
}
