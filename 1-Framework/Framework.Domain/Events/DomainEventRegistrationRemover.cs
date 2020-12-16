using System;

namespace Framework.Domain.Events
{
    public sealed class DomainEventRegistrationRemover : IDisposable
    {
        private readonly Action callOnDispose;

        public DomainEventRegistrationRemover(Action toCall)
        {
            callOnDispose = toCall;
        }

        public void Dispose()
        {
            callOnDispose.DynamicInvoke();
        }
    }
}