using System;
using PeekageMessenger.Domain.Contract;

namespace PeekageMessenger.Domain
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