using System;
using PeekageMessenger.Framework.Core.Exceptions.Resources;

namespace PeekageMessenger.Framework.Core.Exceptions
{
    public class ClientIsNotConnectException :  BusinessException
    {
        public ClientIsNotConnectException()
        :base(PeekageMessengerExceptions.ClientIsNotConnectException,
            PeekageMessengerExceptionMessages.ClientIsNotConnect)
        {
        }
        
    }
}