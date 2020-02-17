using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PeekageMessenger.Application;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Domain;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core;
using PeekageMessenger.Framework.Core.Extensions;
using PeekageMessenger.Framework.Core.Logic;
using PeekageMessenger.Infrastructure.TCP;
using PeekageMessenger.Tools.Notification;

namespace PeekageMessenger.HostedService.Server
{
    public class ServerWorker : BackgroundService
    {
        private readonly INotification _notification;
        private IConnectionListener _listener;

        private readonly IListenerFactory _listenerFactory;
        private readonly IResponseStrategyFactory _responseStrategyFactory;
        public ServerWorker(INotification notification, IResponseStrategyFactory responseStrategyFactory, IConnectionListener listener)
        {
            _notification = notification;
            _responseStrategyFactory = responseStrategyFactory;
            _listener = listener;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_tcpListener = //TcpFactory.CreateListener();
            _listener.Start();
            _notification.Info("Peekage", "Server started!");
            _notification.Info("Server", "Waiting for a connection...");

            await AcceptNewClient();
        }


        private async Task AcceptNewClient()
        {
            var tcpClient = await _listener.AcceptTcpClientAsync();

            _ = Task.Run(async () => await HandleClient(tcpClient));

            await AcceptNewClient();
        }

        private async Task HandleClient(IConnectionClient tcpClient)
        {
            var clientId = tcpClient.GetId();

            _notification.Warning("Server", $"Client{clientId} connected!");

            var responseSender = new ResponseSender(tcpClient);

            await HandelNewMessage(tcpClient, responseSender);

            _notification.Error("Server", $"Client{clientId} dropped out");
        }

        private async Task HandelNewMessage(IConnectionClient tcpClient, ResponseSender responseSender)
        {
            string message = await GetNewMessage(tcpClient);

            if (message == null) return;

            var messageData= MessageFactory.Create(message);

            _notification.Info($"Client {tcpClient.GetId()} said", messageData.Text);

            _ = Task.Run(() => ReplyToMessage(responseSender, messageData));

            await HandelNewMessage(tcpClient, responseSender);
        }

        private async Task<ReplyResult> ReplyToMessage(ResponseSender responseSender, Message message)
        {
            var strategy = _responseStrategyFactory.Create(responseSender, message.Text);
            var connectionState = await strategy.Reply(message.MessageId);
            _notification.Success("Server Replied", strategy.Message);
            return connectionState;
        }

        private async Task<string> GetNewMessage(IConnectionClient tcpClient)
        {
            try
            {
                return await (Task.Run(tcpClient.ReadMessageAsync));
            }
            catch (IOException ex)
            {
                //When the connected clients console window closing with/without 'Bye' command
            }
            return null;
        }
    }
}
