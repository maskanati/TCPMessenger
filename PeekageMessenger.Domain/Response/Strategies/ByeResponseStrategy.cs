﻿using System;
using System.Threading.Tasks;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core.Logic;

namespace PeekageMessenger.Domain.Response.Strategies
{
    public class ByeResponseStrategy : IResponseStrategy
    {

        private IResponseSender _responseSender { set; get; }
        public void SetResponseSender(IResponseSender responseSender)
        {
            _responseSender = responseSender;
        }
        public string Message => "Bye";

        public async Task<ReplyResult> Reply(Guid messageId)
        {
            var message = new Message(messageId,this.Message);
            await _responseSender.SendAsync(message);
            _responseSender.Close();
            return ReplyResult.Disconnected;
        }
    }
}