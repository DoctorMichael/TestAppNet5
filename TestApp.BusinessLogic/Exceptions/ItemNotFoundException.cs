using System;

namespace TestApp.BusinessLogic.Exceptions
{
    [Serializable]
    public class ItemNotFoundException : Exception, IStatusCode
    {
        const int statusCode = 410; // 410 - Gone.
        public ItemNotFoundException() { }
        public ItemNotFoundException(string message) : base(message) { }
        public ItemNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ItemNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public int StatusCode { get => statusCode; }
    }
}
