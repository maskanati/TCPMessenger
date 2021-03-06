﻿using System;
using System.Collections.Concurrent;
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
using PeekageMessenger.Framework.Core;
using PeekageMessenger.Framework.Core.Exceptions;
using PeekageMessenger.Framework.Core.Extensions;
using PeekageMessenger.Framework.Core.FluentProgramming;



namespace PeekageMessenger.Infrastructure.TCP
{
    public class ClientModel : IClient
    {
        private readonly IConnectionClient _tcpClient;
        private readonly IResponseMessageFactory _responseMessageFactory;
        ConcurrentDictionary<Guid, string> _messageDictionary;

        public ClientModel(IConnectionClient tcpClient, IResponseMessageFactory responseMessageFactory)
        {
            _tcpClient = tcpClient;
            _responseMessageFactory = responseMessageFactory;
            _messageDictionary = new ConcurrentDictionary<Guid, string>();

            Task.Run(async () => await ReadNewResponse());

        }

        private async Task ReadNewResponse()
        {
            var responseMessage = await _tcpClient.ReadMessageAsync();
            var response= MessageFactory.Create(responseMessage);
            _messageDictionary[response.MessageId] = response.Text;

            await ReadNewResponse();


        }

        public async Task<IResponseMessage> SendAsync(IRequestMessage requestMessage)
        {
            return await Send(requestMessage); 

        }

        public async Task<TResponseMessage> SendAsync<TResponseMessage>(IRequestMessage requestMessage) where TResponseMessage : IResponseMessage
        {
            var response = await Send(requestMessage);
            return (TResponseMessage)response;
        }

        //This method violates Single Responsibility Principle but its a limitation in code challenge
        private async Task<IResponseMessage> Send(IRequestMessage requestMessage)
        {
            var message = new Message(Guid.NewGuid(), requestMessage.Message);

            await _tcpClient.WriteMessageAsync(message.ToString());
            _messageDictionary.TryAdd(message.MessageId,null);

            string responseMessage = null;

            responseMessage =await Task.Run(()=>GetResponseMessage(responseMessage, message));


            if (responseMessage == null)
                throw new ClientIsNotConnectException();

            var response = _responseMessageFactory.Create(responseMessage);
            return response;
        }

        private string GetResponseMessage(string responseMessage, Message message)
        {
            while (responseMessage == null)
            {
                FluentHelper.WaitFor(100).Milliseconds();
                _messageDictionary.TryRemove(message.MessageId, out responseMessage);
            }


            return responseMessage;
        }
    }

}