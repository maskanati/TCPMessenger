using PeekageMessenger.Framework.Core.Exceptions.Resources;

namespace PeekageMessenger.Framework.Core.Exceptions
{
    public class InvalidRequestException :BusinessException
    {
        public InvalidRequestException()
        :base(PeekageMessengerExceptions.InvalidRequestException,
            PeekageMessengerExceptionMessages.InvalidRequest)
        {
        }
        
    }
}