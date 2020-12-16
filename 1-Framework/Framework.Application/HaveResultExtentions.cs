namespace Framework.Application
{
    public static class HaveResultExtentions
    {
        public static T As<T>(this IHaveResult<T> result)
        {
            return (T)result.Result;
        }
    }
}