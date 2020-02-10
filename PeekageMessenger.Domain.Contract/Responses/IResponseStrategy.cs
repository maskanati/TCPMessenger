using System.Threading.Tasks;

namespace PeekageMessenger.Domain.Contract.Responses
{
    public interface IResponseStrategy
    {
        string Message { get; }
        Task Reply();
    }
}