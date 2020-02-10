using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Domain.Response.Messages
{
    public class PongResponseMessage:IResponseMessage
    {
        public string Message => "Pong";
    }
}