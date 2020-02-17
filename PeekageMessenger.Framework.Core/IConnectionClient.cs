using System.IO;
using System.Net.Sockets;

namespace PeekageMessenger.Framework.Core
{
    public interface IConnectionClient
    {
        bool Connected { get; }
        NetworkStream GetStream();

        void Close();
    }
}
