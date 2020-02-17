using System.IO;
using System.Threading.Tasks;

namespace PeekageMessenger.Framework.Core.Extensions
{
    public static class TcpClientExtensions
    {
        public static async Task<string> ReadMessageAsync(this IConnectionClient tcpClient)
        {
            if (!tcpClient.Connected)
                return null;

            var networkStream = tcpClient.GetStream();
            var reader = new StreamReader(networkStream);
            return await reader.ReadLineAsync();
        }
        public static async Task<bool> WriteMessageAsync(this IConnectionClient tcpClient, string message)
        {
            if (!tcpClient.Connected)
                return false;
            var networkStream = tcpClient.GetStream();
            var writer = new StreamWriter(networkStream);
            await writer.WriteLineAsync(message);
            writer.Flush();

            return true;
        }

    }
}
