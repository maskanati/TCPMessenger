using System.Threading.Tasks;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core.Logic;

namespace PeekageMessenger.Domain.Response.Strategies
{
    public class PongResponseStrategy: IResponseStrategy
    {
        private IResponseSender _responseSender { set; get; }
        public void SetResponseSender(IResponseSender responseSender)
        {
            _responseSender = responseSender;
        }
        public string Message => "Pong";
        
        public async Task<ReplyResult> Reply()
        {
            await _responseSender.SendAsync(this.Message);
            return ReplyResult.StillConnected;
        }

    }
}