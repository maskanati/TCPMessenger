using System;
using System.Threading;
using System.Threading.Tasks;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core.Logic;

namespace PeekageMessenger.Domain.Response.Strategies
{
    public class HiResponseStrategy : IResponseStrategy
    {

        private IResponseSender _responseSender { set; get; }
        public void SetResponseSender(IResponseSender responseSender)
        {
            _responseSender = responseSender;
        }
        public string Message => "Hi";

        public async Task<ReplyResult> Reply()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            await _responseSender.SendAsync(this.Message);
            return ReplyResult.StillConnected;
        }
    }
}