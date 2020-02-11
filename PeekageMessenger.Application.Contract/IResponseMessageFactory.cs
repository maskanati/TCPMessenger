using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Application.Contract
{
    public interface IResponseMessageFactory
    {
        IResponseMessage Create(string requestMessage);

    }
}