using PeekageMessenger.Domain.Contract.Responses;
using System;


namespace PeekageMessenger.Domain.Contract.Requests
{
    public interface IRequestMessage
    {
        string Message { get; }
        IResponseMessage Send();
    }
}

