using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Application.Contract
{
    public interface IResponseFactory
    {
        IResponseMessage Create(string body);

    }
}