using PeekageMessenger.Domain.Contract.Responses;
using System;
using System.Threading.Tasks;

namespace PeekageMessenger.Domain.Contract.Requests
{
    public interface IRequestMessage
    {
        string Message { get; }
        Task<IResponseMessage> Send();
    }
}

