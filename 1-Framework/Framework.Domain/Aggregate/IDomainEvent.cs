using System;

namespace Framework.Domain.Aggregate
{
    public interface IDomainEvent
    {
        string AggregateRootId { get; set; }
        
        DateTime CreateDate { get; set; }

        string UserName { get; set; }

        string ApplicationSource { get; set; }
    }
}