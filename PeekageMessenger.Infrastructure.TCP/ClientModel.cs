using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PeekageMessenger.Application;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Domain;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core.Exceptions;
using PeekageMessenger.Framework.Extensions;

namespace PeekageMessenger.Infrastructure.TCP
{
    public class ClientModel : IClient
    {
        private readonly TcpClient _tcpClient;
        private readonly IResponseMessageFactory _responseMessageFactory;
        Dictionary<Guid,IResponseMessage> _messageDictionary;

        public ClientModel(TcpClient tcpClient, IResponseMessageFactory responseMessageFactory)
        {
            _tcpClient = tcpClient;
            _responseMessageFactory = responseMessageFactory;
            _messageDictionary = new Dictionary<Guid, IResponseMessage>();
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
            var message = new Message(Guid.NewGuid(), requestMessage.Message);

            await _tcpClient.WriteMessageAsync(message.ToString());
           
            //wait

            var responseMessage = await _tcpClient.ReadMessageAsync();
            
            if (responseMessage == null)
                throw new ClientIsNotConnectException();

            var response = _responseMessageFactory.Create(responseMessage);
            return response;
        }
    }


    public class Bus
    {

        private IList<IMessageSender> _senders;
        private IList<Response> _responses;
        private readonly TcpClient _tcpClient;

        public Bus(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public void Register(IMessageSender messageSender)
        {
            this._senders.Add(messageSender);
        }

        public async Task Send(IMessageSender messageSender)
        {

            Register(messageSender);
            var message = new Message(messageSender.Id, messageSender.Message);
            await _tcpClient.WriteMessageAsync(message.ToString());

            await ListenToResponses();
           
            //            foreach (var messageSender in _senders)
            //            {
            //                var message = new Message(messageSender.Id, messageSender.Message);
            //                await _tcpClient.WriteMessageAsync(message.ToString());
            //
            //            }


        }

        private async Task ListenToResponses()
        {
            var responseMessage = await _tcpClient.ReadMessageAsync();

        }
    }

    public class Response
    {
        Guid Id { get; set; }
        string Message { get; set; }
    }

    public interface IMessageSender
    {
        Guid Id { get; set; }
        string Message { get; set; }
        eve
    }
}