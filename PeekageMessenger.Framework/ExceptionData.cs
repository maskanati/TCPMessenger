using System;

namespace PeekageMessenger.Framework
{
    public class ExceptionData
    {
        public long Code { get; private set; }

        public string Message { get; private set; }

        public ExceptionData(long code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }
}
