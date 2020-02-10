

namespace PeekageMessenger.Domain.Contract.Responses
{
    public interface IResponseStrategy
    {
        string Message { get; }
        void Reply();
    }
}