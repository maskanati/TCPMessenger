using System.Net.Sockets;
using System.Threading.Tasks;
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

        public async Task Reply()
        {
            await _tcpClient.WriteMessageAsync(this.Message);
        }

    }
}