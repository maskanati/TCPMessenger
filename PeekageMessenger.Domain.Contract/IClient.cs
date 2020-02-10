using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using System.Threading.Tasks;

namespace PeekageMessenger.Domain.Contract
{
    public interface IClient
    {
        Task<IResponseMessage> SendAsync(IRequestMessage requestMessage);

        Task<TResponseMessage> SendAsync<TResponseMessage>(IRequestMessage requestMessage)
            where TResponseMessage : IResponseMessage;
    }
}