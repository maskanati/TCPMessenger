using PeekageMessenger.Domain.Contract.Requests;
using System;

namespace PeekageMessenger.Domain.Contract.Responses
{
    public interface IResponseMessage
    {
        string Message { get; }
    }
}