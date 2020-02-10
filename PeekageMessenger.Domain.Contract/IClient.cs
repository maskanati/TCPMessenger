using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Domain.Contract
{
    public interface IClient
    {
        IResponseMessage Send(IRequestMessage requestMessage);

        TResponseMessage Send<TResponseMessage>(IRequestMessage requestMessage)
            where TResponseMessage : IResponseMessage;
    }
}