using System;
using System.Collections.Generic;
using System.Net.Sockets;
using PeekageMessenger.Domain;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.RequestStrategies;
using IRequestMessage = PeekageMessenger.Domain.Contract.Requests.IRequestMessage;

namespace PeekageMessenger.Application
{
    public static class MessageFactory
    {
        public static Message Create(string message)
        {
            var messageArray = message.Split("~");
            return new Message(Guid.Parse(messageArray[0]), messageArray[1]);
        }

    }
}