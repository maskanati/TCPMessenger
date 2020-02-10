using System;

namespace PeekageMessenger.Framework
{
    public class BusinessException : Exception
    {
        public ExceptionData ExceptionData { get; private set; }


        public BusinessException(long errorCode, string errorMessage)
        {
            this.ExceptionData = new ExceptionData(errorCode, errorMessage);
        }

        public BusinessException(Enum errorEnumValue, string errorMessage)
        {
            this.ExceptionData = new ExceptionData((long)Convert.ToInt32((object)errorEnumValue), errorMessage);
        }


        public override string ToString()
        {
            if (this.ExceptionData.Code != -1L)
                return $"{(object) this.ExceptionData.Code} - {(object) this.ExceptionData.Message}";
            return this.ExceptionData.Message;
        }
    }
}
