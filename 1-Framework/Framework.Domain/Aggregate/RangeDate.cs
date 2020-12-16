using System;

namespace Framework.Domain.Aggregate
{
    public class RangeDate : ValueObject
    {
        protected RangeDate() { }

        public RangeDate(PersianDate fromDate)
        {
            this.fromDate = fromDate;
        }

        public RangeDate(PersianDate fromDate, PersianDate toDate)
        {
            this.fromDate = fromDate;
            this.toDate = toDate;
        }

        private PersianDate fromDate;
        public virtual PersianDate FromDate
        {
            get { return fromDate; }
        }

        private PersianDate toDate;
        public virtual PersianDate ToDate
        {
            get { return toDate; }
        }

        public virtual bool IsIn(DateTime now)
        {
            var pnow = new PersianDate(now);
            return (pnow >= fromDate && (toDate == null || pnow < toDate));
        }
    }
}