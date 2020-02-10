using PeekageMessenger.Domain.Contract.Requests;

namespace PeekageMessenger.Application.Contract
{
    public interface IRequestFactory
    {
        IRequestMessage Create(string message);
    }
}