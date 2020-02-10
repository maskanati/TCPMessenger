﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PeekageMessenger.Application;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Domain.RequestStrategies;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core.Exceptions;
using PeekageMessenger.Tools.Notification;

namespace PeekageMessenger.ServiceHost.Client
{
    public class PeekageMessengerClient
    {
        private readonly TcpClient _tcpClient;
        private readonly INotification _notification;
        

        public PeekageMessengerClient(string ipAddress, int port)
        {
            _notification = new ConsoleNotification();
            _tcpClient = new TcpClient(ipAddress, port);
            _notification.Success("Client", "Successfully connected to server");

        }
        private void clearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.Read(); // read next key, but discard
            }
        }
        public async Task Run()
        {
            _notification.Notify("Peekage", "Please enter a command...");

            do
            {
                var message = Console.ReadLine();
                clearInputBuffer();
                var strategy = new RequestFactory(_tcpClient).Create(message);

                new Thread(async () =>
                {
                    try
                    {
                        NotifyRequest(strategy,message);
                        var response = await strategy.Send();
                        _notification.Success("Server Replied", response.Message);
                    }
                    catch (ClientIsNotConecteException exception)
                    {
                        _notification.Error("Disconnect", exception.ToString());
                    }
                    catch (IOException exception)
                    {
                        _notification.Error("Disconnect", new ClientIsNotConecteException().ToString());
                    }
                    catch (InvalidRequestException exception)
                    {
                        _notification.Error("Not Allowed", exception.ToString());
                    }
                    catch (Exception exception)
                    {
                        HandelException(exception);
                    }
                    
                }).Start();
            } while (true);


        }

        private void HandelException(Exception exception)
        {
            if (exception is BusinessException)
                _notification.Error("Exception", exception.ToString());
            else
                _notification.Error("Exception", exception.Message);
        }

        private void NotifyRequest(IRequestMessage request, string message)
        {
            if (request is InvalidRequestStrategy)
                throw new InvalidRequestException();
            else
                _notification.Info("Client is trying to say", message);
        }
    }
}