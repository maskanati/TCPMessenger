using System;
using System.Net.Sockets;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Application.Contract
{
    public interface IListenerFactory
    {

        TcpListener Create();
    }

}