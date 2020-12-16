namespace Framework.Application
{
    public interface IHaveResult<T>
    {
        T Result { get; set; }
    }
}