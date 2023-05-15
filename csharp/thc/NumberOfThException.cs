using System;
namespace thc
{
    [Serializable] public class NumberOfThException : Exception
    {
        public NumberOfThException() { }
        public NumberOfThException(string message) : base(message) { }
        public NumberOfThException(string message, Exception inner) : base(message, inner) { }
        protected NumberOfThException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
