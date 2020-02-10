using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

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
        public  IResponseMessage Send(IRequestMessage requestMessage)
        {
            return  SendAndGetResponse(requestMessage); 

        }

        public  TResponseMessage Send<TResponseMessage>(IRequestMessage requestMessage) where TResponseMessage : IResponseMessage
        {
            var response =  SendAndGetResponse(requestMessage);
            return (TResponseMessage)response;
        }

        private  IResponseMessage SendAndGetResponse(IRequestMessage requestMessage)
        {
             _tcpClient.WriteMessage(requestMessage.Message);
            
            var message =  _tcpClient.ReadMessage();
            
            if (message == null)
                throw new ClientIsNotConecteException();
            var _notification = new ConsoleNotification();
            _notification.Warning("test",message);

            var response = new ResponseFactory(_tcpClient).Create(message);
            return response;
        }
    }
}