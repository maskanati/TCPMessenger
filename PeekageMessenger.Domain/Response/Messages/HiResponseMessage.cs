using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Domain.Response.Messages
{
    public class HiResponseMessage : IResponseMessage
    {
        public string Message => "Hi";
    }
}