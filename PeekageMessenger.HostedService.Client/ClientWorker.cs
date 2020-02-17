using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.RequestStrategies;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core.Exceptions;
using PeekageMessenger.Infrastructure.TCP;
using PeekageMessenger.Tools.Notification;

namespace PeekageMessenger.HostedService.Client
{
    public class ClientWorker : BackgroundService
    {
        private readonly INotification _notification;
        private readonly IRequestFactory _requestFactory;
        private readonly IClient _client;

        public ClientWorker(INotification notification, IClient client, IRequestFactory requestFactory)
        {
            _notification = notification;
            _client = client;
            _requestFactory = requestFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _notification.Info("Peekage", "Welcome To PeekageMessenger!");
            _notification.Warning("Note", "This messenger actually treats messages in a case-sensitive manner!");
            _notification.Warning("Valid messages", "'Hello', 'Bye', 'Ping'");
            _notification.Notify("Peekage", "Please enter a command...");

            AcceptNewMessage();
        }

        private void AcceptNewMessage()
        {
            string message = Console.ReadLine();
            var strategy = _requestFactory.Create(_client, message);
            _ = Task.Run(async () => await HandleMessage(strategy, message));
            AcceptNewMessage();
        }

        private async Task HandleMessage(IRequestMessage strategy, string message)
        {
            try
            {
                await ResponseToRequest(strategy, message);
            }
            catch (ClientIsNotConnectException exception)
            {
                _notification.Error("Disconnect", exception.ToString());
            }
            catch (IOException exception)
            {
                _notification.Error("Disconnect", new ClientIsNotConnectException().ToString());
            }
            catch (InvalidRequestException exception)
            {
                _notification.Error("Not Allowed", exception.ToString());
            }
            catch (Exception exception)
            {
                HandelException(exception);
            }
        }

        private async Task ResponseToRequest(IRequestMessage strategy, string message)
        {
            NotifyRequest(strategy, message);
            var response = await strategy.Send();
            _notification.Success("Server Replied", response.Message);
        }

        private void HandelException(Exception exception)
        {
            if (exception is BusinessException)
                _notification.Error("Exception", exception.ToString());
            else if (exception is KeyNotFoundException){}
            else
                _notification.Error("Exception", exception.Message);
        }

        private void NotifyRequest(IRequestMessage request, string message)
        {
            if (!(request is InvalidRequestStrategy))
                _notification.Info("Client is trying to say", message);
        }
    }
}
