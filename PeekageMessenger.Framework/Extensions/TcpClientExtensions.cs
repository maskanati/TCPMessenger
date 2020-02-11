using System;
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
            var reader = new StreamReader(networkStream);
            return await reader.ReadLineAsync();
        }
        public static async Task<bool> WriteMessageAsync(this TcpClient tcpClient, string message)
        {
            if (!tcpClient.Connected)
                return false;
            NetworkStream networkStream = tcpClient.GetStream();
            var writer = new StreamWriter(networkStream);
            await writer.WriteLineAsync(message);
            writer.Flush();

            return true;
        }

    }
}
