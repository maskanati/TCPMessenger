using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PeekageMessenger.Application;
using PeekageMessenger.Domain;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core;
using PeekageMessenger.Framework.Core.Exceptions;

namespace PeekageMessenger.Infrastructure.TCP
{
    public class ResponseSender : IResponseSender
    {
        private readonly IConnectionClient _tcpClient;
        private readonly StreamWriter _streamWriter;
        private readonly StreamReader _streamReader;

        public ResponseSender(IConnectionClient tcpClient)
        {
            _tcpClient = tcpClient;
            if (!tcpClient.Connected)
                throw new ClientIsNotConnectException();
            Connected = true;
            var networkStream = tcpClient.GetStream();
            _streamWriter = new StreamWriter(networkStream, Encoding.ASCII);
            _streamReader = new StreamReader(networkStream, Encoding.ASCII);
        }

        public void Close()
        {
            _tcpClient.GetStream().Close();
            _tcpClient.Close();
            Connected = false;
        }

        public async Task SendAsync(Message message)
        {
            await _streamWriter.WriteLineAsync(message.ToString());
            _streamWriter.Flush();
        }
        public bool Connected;
    }
}