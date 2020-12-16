using System;
using Framework.Domain.Aggregate;

namespace Framework.Domain.Events
{
    public class Event : AggregateRoot<long>
    {
        protected Event()
        {
        }

        public Event(string name, string originator, string aggregateId, string content, string eventName)
        {
            createDate = DateTime.Now;
            SetProperties(name, originator, aggregateId, content, eventName);
        }

        #region Properties

        private long aggregateId;
        private DateTime createDate;
        private string content;
        private string errorMessage;
        private string eventName;
        private long id;

        private string name;
        private string originator;
        private bool processed;

        public override long Id
        {
            get { return id; }
        }

        public virtual string Name
        {
            get { return name; }
        }

        public virtual DateTime CreateDate
        {
            get { return createDate; }
        }

        public virtual string Originator
        {
            get { return originator; }
        }

        public virtual long AggregateId
        {
            get { return aggregateId; }
        }

        public virtual string Content
        {
            get { return content; }
        }

        public virtual string EventName
        {
            get { return eventName; }
        }

        public virtual string ErrorMessage
        {
            get { return errorMessage; }
        }

        public virtual bool Processed
        {
            get { return processed; }
        }

        #endregion

        #region Methods

        protected void SetProperties(string name, string originator, string aggregateId, string content, string eventName,
            bool processed = false)
        {
            this.name = name;
            this.originator = originator;
            this.aggregateId = long.Parse(aggregateId);
            this.content = content;
            this.processed = processed;
            this.eventName = eventName;
        }

        public virtual void Complete()
        {
            processed = true;
        }

        public virtual void SetError(Exception exception)
        {
            errorMessage = exception.ToString();
            processed = false;
        }

        #endregion
    }
}