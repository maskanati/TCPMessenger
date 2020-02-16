using System;

namespace PeekageMessenger.Framework.Handlers
{
    public class EndOfChainHandler<T> : IHandler<T>
    {
        public void Handle(T request)
        {
            throw new Exception("No other handler has processed the request");
        }

        public void SetNext(IHandler<T> handler)
        {
            throw new NotSupportedException();
        }
    }
}
