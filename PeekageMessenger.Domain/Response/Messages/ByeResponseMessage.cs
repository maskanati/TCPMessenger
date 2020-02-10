using PeekageMessenger.Domain.Contract.Responses;

namespace PeekageMessenger.Domain.Response.Messages
{
    public class ByeResponseMessage : IResponseMessage
    {
        public string Message => "Bye";

     
    }
}