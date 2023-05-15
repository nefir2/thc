using System; //Exception
using System.Runtime.Serialization; //Serializable
namespace thc
{
	/// <summary> NaN of touhou exception. </summary>
	[Serializable] public class NumberOfThException : Exception
	{
		public NumberOfThException() { }
		public NumberOfThException(string message) : base(message) { }
		public NumberOfThException(string message, Exception inner) : base(message, inner) { }
		protected NumberOfThException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
