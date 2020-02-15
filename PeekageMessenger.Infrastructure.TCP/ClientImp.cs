using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using PeekageMessenger.Application;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core.Exceptions;

namespace PeekageMessenger.Infrastructure.TCP
{
    public class ClientImp : IClient
    {
        private readonly TcpClient _tcpClient;
        private readonly IResponseMessageFactory _responseMessageFactory;

        public ClientImp(TcpClient tcpClient, IResponseMessageFactory responseMessageFactory)
        {
            _tcpClient = tcpClient;
            _responseMessageFactory = responseMessageFactory;
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
                throw new ClientIsNotConnectException();

            var response = _responseMessageFactory.Create(message);
            return response;
        }
    }
}