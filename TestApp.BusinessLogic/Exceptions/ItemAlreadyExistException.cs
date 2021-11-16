using System;

namespace TestApp.BusinessLogic.Exceptions
{
    [Serializable]
    public class ItemAlreadyExistException : Exception, IStatusCode
    {
        int statusCode = 422; // 422 - Unprocessable Entity.
        public ItemAlreadyExistException() { }
        public ItemAlreadyExistException(string message) : base(message) { }
        public ItemAlreadyExistException(string message, Exception inner) : base(message, inner) { }
        protected ItemAlreadyExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public ItemAlreadyExistException(string message, int statusCode) : base(message)
        {
            this.statusCode = statusCode;
        }

        public int StatusCode
        {
            get => statusCode;
            set => statusCode = value;
        }
    }
}
