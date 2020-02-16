using System;
using PeekageMessenger.Framework.Core.Logic;
using System.Threading.Tasks;

namespace PeekageMessenger.Domain.Contract.Responses
{
    public interface IResponseStrategy
    {
        void SetResponseSender(IResponseSender responseSender);
        string Message { get; }
        Task<ReplyResult> Reply(Guid messageId);
    }
}