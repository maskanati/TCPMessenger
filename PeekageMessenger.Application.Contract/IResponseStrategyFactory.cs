using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Application.Contract
{
    public interface IResponseStrategyFactory
    {
        IResponseStrategy Create(string message);

    }
}