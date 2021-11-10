using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.BusinessLogic.Exceptions
{
    [Serializable]
    public class IncorrectDataException : Exception
    {
        public IncorrectDataException() { }
        public IncorrectDataException(string message) : base(message) { }
        public IncorrectDataException(string message, Exception inner) : base(message, inner) { }
        protected IncorrectDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
