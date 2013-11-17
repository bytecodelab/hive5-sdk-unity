using System;
using System.Collections.Generic;
using System.Text;

namespace Hive5
{
	[Serializable()]
	public class InvalidJsonException : Exception
	{
		public InvalidJsonException() : base() { }
		public InvalidJsonException(string message) : base(message) { }
		public InvalidJsonException(string message, System.Exception inner) : base(message, inner) { }
		
		protected InvalidJsonException(System.Runtime.Serialization.SerializationInfo info,
		                               System.Runtime.Serialization.StreamingContext context)
		{ }
	}
}
