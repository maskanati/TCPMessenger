using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using System.Threading.Tasks;

namespace PeekageMessenger.Domain.Contract
{
    public interface IResponseSender
    {
        Task SendAsync(string message);
        void Close();
    }
}