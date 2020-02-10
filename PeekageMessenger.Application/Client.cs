using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using PeekageMessenger.Application;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core.Exceptions;
using PeekageMessenger.Tools.Notification;

namespace PeekageMessenger.Application
{
    public class Client : IClient
    {
        private readonly TcpClient _tcpClient;

        public Client(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }
        public async Task<IResponseMessage> SendAsync(IRequestMessage requestMessage)
        {
            return await SendAndGetResponse(requestMessage); 

        }

        public async Task<TResponseMessage> SendAsync<TResponseMessage>(IRequestMessage requestMessage) where TResponseMessage : IResponseMessage
        {
            var response = await SendAndGetResponse(requestMessage);
            return (TResponseMessage)response;
        }

        private async Task<IResponseMessage> SendAndGetResponse(IRequestMessage requestMessage)
        {
            await _tcpClient.WriteMessageAsync(requestMessage.Message);
            
            var message = await _tcpClient.ReadMessageAsync();
            
            if (message == null)
                throw new ClientIsNotConecteException();
            var _notification = new ConsoleNotification();
            _notification.Warning("test",message);

            var response = new ResponseFactory(_tcpClient).Create(message);
            return response;
        }
    }
}