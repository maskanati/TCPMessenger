using System.Threading.Tasks;
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

        public async Task<IResponseMessage> Send()
        {
            return await _client.SendAsync<ByeResponseMessage>(this);
        }
    }
}