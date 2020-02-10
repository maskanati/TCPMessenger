using System;
using System.Net.Sockets;
using System.Threading;

using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;

namespace PeekageMessenger.Domain.Response.Strategies
{
    public class HiResponseStrategy : IResponseStrategy
    {
        public string Message => "Hi";

        private TcpClient _tcpClient;
        public HiResponseStrategy(TcpClient tcpClient)
        {
            this._tcpClient = tcpClient;
        }
        public  void Reply()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
             _tcpClient.WriteMessage(this.Message);
        }
    }
}