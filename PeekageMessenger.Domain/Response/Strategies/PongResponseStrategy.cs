using System.Net.Sockets;

using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;

namespace PeekageMessenger.Domain.Response.Strategies
{
    public class PongResponseStrategy: IResponseStrategy
    {
        public string Message => "Pong";
        private TcpClient _tcpClient;

        public PongResponseStrategy(TcpClient tcpClient)
        {
            this._tcpClient = tcpClient;
        }

        public void  Reply()
        {
             _tcpClient.WriteMessage(this.Message);
        }

    }
}