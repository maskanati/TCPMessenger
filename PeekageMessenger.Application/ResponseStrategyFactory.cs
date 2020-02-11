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
        private IResponseSender _responseSender;

        private static Dictionary<string, IResponseStrategy> _strategyDictionary = new Dictionary<string, IResponseStrategy>();

        public ResponseStrategyFactory(IResponseSender responseSender)
        {
            _responseSender = responseSender;

            StrategyInitializer();
        }

        private void StrategyInitializer()
        {
            if(_strategyDictionary.Count>0)
                return;

            _strategyDictionary.Add("Hello", new HiResponseStrategy());
            _strategyDictionary.Add("Bye", new ByeResponseStrategy());
            _strategyDictionary.Add("Ping", new PongResponseStrategy());
        }
        

        public IResponseStrategy Create(string requestMessage)
        {
            IResponseStrategy strategy= new InvalidResponseStrategy();
            _strategyDictionary.TryGetValue(requestMessage, out strategy);

            strategy.SetResponseSender(_responseSender);

            return strategy;

        }

    }
}