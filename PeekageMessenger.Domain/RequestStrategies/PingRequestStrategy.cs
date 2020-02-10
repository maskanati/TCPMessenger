
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Domain.Response.Messages;

namespace PeekageMessenger.Domain.RequestStrategies
{
    public class PingRequestStrategy : IRequestMessage
    {
        private IClient _client;

        public PingRequestStrategy(IClient client)
        {
            _client = client;
        }

        public string Message => "Ping";

        public  IResponseMessage Send()
        {
            return  _client.Send<PongResponseMessage>(this);
        }
    }
}