using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PeekageMessenger.Framework
{
    public static class TcpClientExtensions
    {
        public static async Task<string> ReadMessageAsync(this TcpClient tcpClient)
        {
            if (!tcpClient.Connected)
                return null;
            var networkStream = tcpClient.GetStream();
            byte[] bytesToRead = new byte[tcpClient.ReceiveBufferSize];
            int bytesRead = await networkStream.ReadAsync(bytesToRead, 0, tcpClient.ReceiveBufferSize);
            return Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
        }
        public static async Task<bool> WriteMessageAsync(this TcpClient tcpClient, string message)
        {
            if (!tcpClient.Connected)
                return false;
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytesToSend = Encoding.ASCII.GetBytes(message);
            await networkStream.WriteAsync(bytesToSend, 0, bytesToSend.Length);
            //var writer = new StreamWriter(networkStream);
            //await writer.WriteLineAsync(message);
            return true;
        }

    }
}
