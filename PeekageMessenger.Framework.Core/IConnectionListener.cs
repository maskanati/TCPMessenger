using System;
using System.IO;
using System.Threading.Tasks;

namespace PeekageMessenger.Framework.Core
{
    public interface IConnectionListener
    {
        void Start();
//        void BeginAcceptTcpClient(Action<IAsyncResult> handleNewClient, IConnectionListener tcpListener);
        IConnectionClient EndAcceptTcpClient(IAsyncResult asyncResult);
        Task<IConnectionClient> AcceptTcpClientAsync();
    }
}
