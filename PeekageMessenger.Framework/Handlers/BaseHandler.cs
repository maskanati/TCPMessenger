namespace PeekageMessenger.Framework.Handlers
{
    public abstract class BaseHandler<T> : IHandler<T>
    {
        private IHandler<T> _nextHandler;
        public abstract void Handle(T request);
        public void SetNext(IHandler<T> handler)
        {
            this._nextHandler = handler;
        }

        protected void CallNext(T request)
        {
            _nextHandler?.Handle(request);
        }
    }
}
