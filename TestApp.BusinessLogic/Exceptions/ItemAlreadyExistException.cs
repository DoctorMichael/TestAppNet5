using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.BusinessLogic.Exceptions
{
    [Serializable]
    public class ItemAlreadyExistException : Exception
    {
        public ItemAlreadyExistException() { }
        public ItemAlreadyExistException(string message) : base(message) { }
        public ItemAlreadyExistException(string message, Exception inner) : base(message, inner) { }
        protected ItemAlreadyExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
