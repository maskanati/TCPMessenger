using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;

namespace PeekageMessenger.Application.Contract
{
    public interface IRequestFactory
    {
        IRequestMessage Create(IClient client, string message);
    }
}