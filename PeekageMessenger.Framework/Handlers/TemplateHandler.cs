namespace PeekageMessenger.Framework.Handlers
{
    public abstract class TemplateHandler<T> : BaseHandler<T>
    {
        public override void Handle(T request)
        {
            if (CanHandleRequest(request))
            {
                HandleRequest(request);
                return;
            }
            else
            {
                CallNext(request);
            }
        }
        protected abstract bool CanHandleRequest(T request);
        protected abstract void HandleRequest(T request);
    }
}
