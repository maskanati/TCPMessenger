﻿using System.Net.Sockets;

using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;

namespace PeekageMessenger.Domain.Response.Strategies
{
    public class InvalidResponseStrategy : IResponseStrategy
    {
        public string Message => "Invalid";
        private TcpClient _tcpClient;

        public InvalidResponseStrategy(TcpClient tcpClient)
        {
            this._tcpClient = tcpClient;
        }

        public void  Reply()
        {
             _tcpClient.WriteMessage(this.Message);
        }

    }
}