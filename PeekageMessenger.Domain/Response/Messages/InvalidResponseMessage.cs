using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Domain.Response.Messages
{
    public class InvalidResponseMessage : IResponseMessage
    {
        public string Message => "Invalid";
    }
}