
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Domain.Response.Messages;

namespace PeekageMessenger.Domain.RequestStrategies
{
    public class ByeRequestStrategy : IRequestMessage
    {
        private IClient _client;

        public ByeRequestStrategy(IClient client)
        {
            _client = client;
        }

        public string Message => "Bye";

        public  IResponseMessage Send()
        {
            return  _client.Send<ByeResponseMessage>(this);
        }
    }
}