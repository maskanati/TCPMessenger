
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Domain.Response.Messages;

namespace PeekageMessenger.Domain.RequestStrategies
{
    public class HelloRequestStrategy : IRequestMessage
    {
        private IClient _client;

        public HelloRequestStrategy(IClient client)
        {
            _client = client;
        }

        public string Message => "Hello";

        public  IResponseMessage Send()
        {
           return  _client.Send<HiResponseMessage>(this);
        }

    }
}
