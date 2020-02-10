using System;
using PeekageMessenger.Framework.Core.Exceptions.Resources;

namespace PeekageMessenger.Framework.Core.Exceptions
{
    public class ClientIsNotConecteException :  BusinessException
    {
        public ClientIsNotConecteException()
        :base(PeekageMessengerExceptions.ClientIsNotConecteException,
            PeekageMessengerExceptionMessages.ClientIsNotConecte)
        {
        }
        
    }
}