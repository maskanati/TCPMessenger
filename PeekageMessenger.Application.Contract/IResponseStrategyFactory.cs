using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Application.Contract
{
    public interface IResponseStrategyFactory
    {
        IResponseStrategy Create(IResponseSender responseSender ,string message);

    }
}