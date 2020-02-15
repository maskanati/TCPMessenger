using System;
using System.Net;
using System.Net.Sockets;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Application.Contract
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

}