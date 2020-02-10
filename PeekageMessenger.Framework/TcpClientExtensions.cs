using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace PeekageMessenger.Framework
{
    public static class TcpClientExtensions
    {
        public static  string ReadMessage(this TcpClient tcpClient)
        {
            if (!tcpClient.Connected)
                return null;

            var networkStream = tcpClient.GetStream();
            var reader = new StreamReader(networkStream);
            return  reader.ReadLine();
        }
        public static  bool WriteMessage(this TcpClient tcpClient, string message)
        {
            if (!tcpClient.Connected)
                return false;
            NetworkStream networkStream = tcpClient.GetStream();
            var writer = new StreamWriter(networkStream);
            writer.AutoFlush = true;
             writer.WriteLine(message);
            return true;
        }

    }
}
