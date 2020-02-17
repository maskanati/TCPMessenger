using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using PeekageMessenger.Framework.Core;

namespace PeekageMessenger.Infrastructure.TCP
{
    public static class TcpFactory 
    {
        const int PORT_NO = 5000;
        const string SERVER_IP = "127.0.0.1";

        public static TcpListener CreateListener()
        {
            return new TcpListener(IPAddress.Parse(SERVER_IP), PORT_NO);
        }
        public static TcpClient CreateClient()
        {
            return new TcpClient(SERVER_IP, PORT_NO);
        }
    }

    public class AppTcpClient : IConnectionClient
    {
        private readonly TcpClient _tcpClient;


        public AppTcpClient(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public bool Connected => _tcpClient.Connected;

        public NetworkStream GetStream()
        {
           return _tcpClient.GetStream();
        }

        public void Close()
        {
            _tcpClient.GetStream();
        }

    }
    public class AppTcpListener : IConnectionListener
    {
        private readonly TcpListener _tcpClient;


        public AppTcpListener(TcpListener tcpClient)
        {
            _tcpClient = tcpClient;
        }


        public void Start()
        {
            _tcpClient.Start();
        }

//        public void BeginAcceptTcpClient(Action<IAsyncResult> handleNewClient, IConnectionListener tcpListener)
//        {
//            _tcpClient.BeginAcceptTcpClient(handleNewClient., tcpListener);
//        }

        public IConnectionClient EndAcceptTcpClient(IAsyncResult asyncResult)
        {
            var a = _tcpClient.EndAcceptTcpClient(asyncResult);
            return new AppTcpClient(a);
        }

        public async Task<IConnectionClient> AcceptTcpClientAsync()
        {
            var a= await _tcpClient.AcceptTcpClientAsync();
            return new AppTcpClient(a);
        }
    }

}