using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PeekageMessenger.Framework.Core
{
    public interface IConnectionClient
    {
        bool Connected { get; }
        NetworkStream GetStream();

        void Close();

        Task<string> ReadMessageAsync();

        Task<bool> WriteMessageAsync(string message);

    }
}
