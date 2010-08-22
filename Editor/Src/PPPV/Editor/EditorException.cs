/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.08.2010
 * Time: 13:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Pppv.Editor
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class EditorException : Exception
	{
		public EditorException():base()
		{
		}

		public EditorException(string message):base(message)
		{
		}

		public EditorException(string message, Exception exception):base(message, exception)
		{
		}

		protected EditorException(SerializationInfo info, StreamingContext context):base(info, context)
		{
		}
	}
}
