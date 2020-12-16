using Framework.Domain.Aggregate;

namespace Vehicle.Domain.PropertyTypes
{
    public class Option : ValueObject
    {
        private string text;
        public virtual string Text { get { return text; } }
        protected Option() { }
        public Option(string text)
        {
            this.text = text;
        }

    }
}