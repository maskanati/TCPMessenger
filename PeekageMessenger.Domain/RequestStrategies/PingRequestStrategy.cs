using System.Threading.Tasks;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Domain.Response.Messages;

namespace PeekageMessenger.Domain.RequestStrategies
{
    public class PingRequestStrategy : IRequestMessage
    {
        private IClient _client;

        public void SetClient(IClient client)
        {
            _client = client;
        }

        public string Message => "Ping";

        public async Task<IResponseMessage> Send()
        {
            return await _client.SendAsync<PongResponseMessage>(this);
        }
    }
}