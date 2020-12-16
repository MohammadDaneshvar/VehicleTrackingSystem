namespace Framework.Domain.Events
{
    public interface IRaiseEventWhenDelete
    {
        void PublishDeleteEvent();
    }
}