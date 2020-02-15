using System;
using System.Collections.Generic;
using System.Net.Sockets;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Domain.Response.Messages;
using PeekageMessenger.Domain.Response.Strategies;

namespace PeekageMessenger.Application
{
    public class ResponseStrategyFactory: IResponseStrategyFactory
    {
        private static Dictionary<string, IResponseStrategy> _strategyDictionary = new Dictionary<string, IResponseStrategy>();

        public ResponseStrategyFactory()
        {
            StrategyInitializer();
        }

        private void StrategyInitializer()
        {
            _strategyDictionary.Add("Hello", new HiResponseStrategy());
            _strategyDictionary.Add("Bye", new ByeResponseStrategy());
            _strategyDictionary.Add("Ping", new PongResponseStrategy());
        }
        

        public IResponseStrategy Create(IResponseSender responseSender, string requestMessage)
        {
            var result = _strategyDictionary.TryGetValue(requestMessage, out var strategy);
            
            if (!result)
                strategy = new InvalidResponseStrategy();

            strategy.SetResponseSender(responseSender);

            return strategy;

        }

    }
}