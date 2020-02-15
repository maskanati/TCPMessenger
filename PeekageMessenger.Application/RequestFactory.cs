using System;
using System.Collections.Generic;
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
        private static Dictionary<string, IRequestMessage> _strategyDictionary = new Dictionary<string, IRequestMessage>();

        public RequestFactory()
        {
            StrategyInitializer();
        }
        private void StrategyInitializer()
        {
            _strategyDictionary.Add("Hello", new HelloRequestStrategy());
            _strategyDictionary.Add("Bye", new ByeRequestStrategy());
            _strategyDictionary.Add("Ping", new PingRequestStrategy());
        }
        public IRequestMessage Create(IClient client,string message)
        {
            var result =_strategyDictionary.TryGetValue(message, out var strategy);
            if(!result)
                strategy = new InvalidRequestStrategy();

            strategy.SetClient(client);

            return strategy;

        }
    }
}