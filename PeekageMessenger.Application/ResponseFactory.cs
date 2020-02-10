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
    public class ResponseFactory: IResponseFactory
    {
        private TcpClient _tcpClient;

        private static Dictionary<string, IResponseStrategy> _strategyDictionary = new Dictionary<string, IResponseStrategy>();
        private static Dictionary<string, IResponseMessage> _messageDictionary = new Dictionary<string, IResponseMessage>();

        public ResponseFactory(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;

            StrategyInitializer();
            MessageInitializer();
        }

        private void StrategyInitializer()
        {
            if(_strategyDictionary.Count>0)
                return;

            _strategyDictionary.Add("Hello", new HiResponseStrategy(_tcpClient));
            _strategyDictionary.Add("Bye", new ByeResponseStrategy(_tcpClient));
            _strategyDictionary.Add("Ping", new PongResponseStrategy(_tcpClient));
        }
        

        public IResponseStrategy GetStrategy(string requestMessage)
        {
            var result = _strategyDictionary.TryGetValue(requestMessage, out var strategy);
 
            if(!result)
                    return new InvalidResponseStrategy(_tcpClient);

            return strategy;

        }

        private void MessageInitializer()
        {

            if (_messageDictionary.Count > 0)
                return;

            _messageDictionary.Add("Hi", new HiResponseMessage());
            _messageDictionary.Add("Bye", new ByeResponseMessage());
            _messageDictionary.Add("Pong", new PongResponseMessage());
        }
        public IResponseMessage Create(string requestMessage)
        {
            var result = _messageDictionary.TryGetValue(requestMessage, out var responseMessage);

            if (!result)
                return new InvalidResponseMessage();

            return responseMessage;
        }
    }
}