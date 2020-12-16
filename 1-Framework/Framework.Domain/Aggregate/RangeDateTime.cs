namespace Framework.Domain.Aggregate
{
    public class RangeDateTime : ValueObject
    {
        public RangeDateTime()
        {

        }
        public RangeDateTime(PersianDateTime fromDate)
        {
            this.fromDate = fromDate;
        }
        public RangeDateTime(PersianDateTime fromDate, PersianDateTime toDate)
        {
            this.fromDate = fromDate;
            this.toDate = toDate;
        }

        private PersianDateTime fromDate;
        public virtual PersianDateTime FromDate
        {
            get { return fromDate; }
        }

        private PersianDateTime toDate;
        public virtual PersianDateTime ToDate
        {
            get { return toDate; }
        }
    }
}