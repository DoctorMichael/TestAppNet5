using System;

namespace TestApp.BusinessLogic.Exceptions
{
    [Serializable]
    public class IncorrectDataException : Exception, IStatusCode
    {
        const int statusCode = 400; // 400 - Bad Request.
        public IncorrectDataException() { }
        public IncorrectDataException(string message) : base(message) { }
        public IncorrectDataException(string message, Exception inner) : base(message, inner) { }
        protected IncorrectDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public int StatusCode { get => statusCode; }
    }
}
