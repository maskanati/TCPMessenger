using System.Net.Sockets;
using System.Threading.Tasks;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;

namespace PeekageMessenger.Domain.Response.Strategies
{
    public class ByeResponseStrategy : IResponseStrategy
    {
        public string Message => "Bye";

        private TcpClient _tcpClient;

        public ByeResponseStrategy(TcpClient tcpClient)
        {
            this._tcpClient = tcpClient;
        }

        public async Task Reply()
        {
            await _tcpClient.WriteMessageAsync(this.Message);
            _tcpClient.Client.Close();
            _tcpClient.Close();
            _tcpClient = null;
        }
     
    }
}