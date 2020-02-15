using System;
using System.Collections.Generic;
using System.Net.Sockets;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Domain.Response.Messages;

namespace PeekageMessenger.Infrastructure.TCP
{
    public class ResponseMessageFactory : IResponseMessageFactory
    {
        private static Dictionary<string, IResponseMessage> _messageDictionary = new Dictionary<string, IResponseMessage>();

        public ResponseMessageFactory()
        {
            MessageInitializer();
        }

        private void MessageInitializer()
        {
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