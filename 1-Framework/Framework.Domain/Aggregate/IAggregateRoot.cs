using System.Collections.Generic;

namespace Framework.Domain.Aggregate
{
    public interface IAggregateRoot
    {
        //int RowVersion { get; set; }
        object Id { get; }
        IList<IDomainEvent> GetUnPublishedEvents();
        void ClearEvents();
    }
}