using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PeekageMessenger.Application;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core;
using PeekageMessenger.Framework.Core.Logic;
using PeekageMessenger.Infrastructure.TCP;
using PeekageMessenger.Tools.Notification;

namespace PeekageMessenger.ServiceHost.Server
{
    public class PeekageMessengerServer
    {
        private readonly TcpListener _tcpListener;
        private readonly INotification _notification;

        public PeekageMessengerServer(string ipAddress, int port)
        {
            _notification = new ConsoleNotification();
            _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
        }

        public async Task Run()
        {
            _tcpListener.Start();
            _notification.Info("Peekage", "Server started!");
            _notification.Notify("Server", "Waiting for a connection...");

            await AcceptNewClient();
        }

        private async Task AcceptNewClient()
        {
            var tcpClient = await _tcpListener.AcceptTcpClientAsync().ConfigureAwait(false);
            _notification.Warning("Server", $"Client{tcpClient.GetId()} connected!");

            _ = Task.Run(async () => await HandleClient(tcpClient));

            await AcceptNewClient();
        }

        private async Task HandleClient(TcpClient tcpClient)
        {
            var clientId = tcpClient.GetId();
            
            var responseSender = new ResponseSender(tcpClient);
            
            await HandelNewMessage(tcpClient, responseSender);

            _notification.Error("Server", $"Client{clientId} dropped out");
        }

        private async Task HandelNewMessage(TcpClient tcpClient, ResponseSender responseSender)
        {
            string message = await GetNewMessage(tcpClient);
            if (message == null) return;

            _notification.Info($"Client {tcpClient.GetId()} said", message);

            _ = Task.Run(() => ReplyToMessage(responseSender, message));

            await HandelNewMessage(tcpClient, responseSender);
        }

        private async Task<ReplyResult> ReplyToMessage(ResponseSender responseSender, string message)
        {
            var strategy = new ResponseStrategyFactory(responseSender).Create(message);
            var connectionState = await strategy.Reply();
            _notification.Success("Server Replied", strategy.Message);
            return connectionState;
        }

        private async Task<string> GetNewMessage(TcpClient tcpClient)
        {
            try { 
                return await (Task.Run(() => tcpClient.ReadMessageAsync()));
            }
            catch(IOException ex)
            {
                //When the connected clients console window closing with/without 'Bye' command
            }
            return null;
        }

    }

}