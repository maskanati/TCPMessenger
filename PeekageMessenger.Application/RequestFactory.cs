using System;
using System.Net.Sockets;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.RequestStrategies;
using IRequestMessage = PeekageMessenger.Domain.Contract.Requests.IRequestMessage;

namespace PeekageMessenger.Application
{
    public class RequestFactory: IRequestFactory
    {
        private readonly IClient _client;

        public RequestFactory(TcpClient tcpClient)
        {
            _client = new Client(tcpClient);
        }
        public IRequestMessage Create(string message)
        {

            switch (message)
            {
                case "Hello":
                    return new HelloRequestStrategy(_client);
                case "Bye":
                    return new ByeRequestStrategy(_client);
                case "Ping":
                    return new PingRequestStrategy(_client);
                default:
                    return new InvalidRequestStrategy(_client);
            }
        }

  
    }
}