using System;

using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework.Core.Exceptions;

namespace PeekageMessenger.Domain.RequestStrategies
{
    public class InvalidRequestStrategy : IRequestMessage
    {
        private IClient _client;

        public InvalidRequestStrategy(IClient client)
        {
            _client = client;
        }
        public string Message => "Invalid";

        public  IResponseMessage Send() { 
            throw new InvalidRequestException();
        }

    }
}