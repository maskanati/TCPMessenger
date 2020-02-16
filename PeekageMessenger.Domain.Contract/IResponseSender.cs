using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.Contract.Responses;
using System.Threading.Tasks;

namespace PeekageMessenger.Domain.Contract
{
    public interface IResponseSender
    {
        Task SendAsync(Message message);
        void Close();
    }
}