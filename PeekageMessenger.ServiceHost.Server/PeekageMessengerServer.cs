using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PeekageMessenger.Application;
using PeekageMessenger.Framework;
using PeekageMessenger.Tools.Notification;

namespace PeekageMessenger.ServiceHost.Server
{
    public class PeekageMessengerServer 
    {
        private readonly TcpListener _tcpListener;
        private readonly INotification _notification;

        public PeekageMessengerServer(string ipAddress, int port)
        {
            _notification=new ConsoleNotification();
            _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
        }
        public async Task Run()
        {
            _tcpListener.Start();
            _notification.Info($"Peekage", "Server started!");
            _notification.Notify($"Server", "Waiting for a connection...");
            do
            {
                var tcpClient = await _tcpListener.AcceptTcpClientAsync().ConfigureAwait(false);
                _notification.Warning($"Server", $"Client{tcpClient.GetId()} connected!");

                new Thread(async () => { await ReplyToClientRequests(tcpClient); }).Start();
            } while (true);
        }

        private async Task ReplyToClientRequests(TcpClient tcpClient)
        {
            do
            {
                var message = await tcpClient.ReadMessageAsync();
                if (message == null)
                    break;
                new Thread(async () =>
                {
                    _notification.Info($"Client {tcpClient.GetId()} said", message);
                    var strategy = new ResponseFactory(tcpClient).GetStrategy(message);
                    await strategy.Reply();
                    _notification.Success("Server Replied", strategy.Message);
                }).Start();
              
            } while (true);
            _notification.Error($"Server", "Client{tcpClient.GetId()} dropped out");

        }

    }
    
}