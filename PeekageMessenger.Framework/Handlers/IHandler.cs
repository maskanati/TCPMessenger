namespace PeekageMessenger.Framework.Handlers
{
    public interface IHandler<T>
    {
        void Handle(T request);
        void SetNext(IHandler<T> handler);
    }
}
