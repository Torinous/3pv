/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.08.2010
 * Time: 13:39
 * 
 */

namespace Pppv.ApplicationFramework
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class PppvException : Exception
	{
		public PppvException() : base()
		{
		}

		public PppvException(string message) : base(message)
		{
		}

		public PppvException(string message, Exception exception) : base(message, exception)
		{
		}

		protected PppvException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}