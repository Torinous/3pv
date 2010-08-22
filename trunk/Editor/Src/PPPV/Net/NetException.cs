/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.08.2010
 * Time: 0:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Pppv.Net
{
   using System;
   using System.Runtime.Serialization;
   
   [Serializable]
   public class NetException : Exception
   {
      public NetException():base()
      {
      }
      
      public NetException(string message):base(message)
      {
      }

      public NetException(string message, Exception exception):base(message, exception)
      {
      }

      protected NetException(SerializationInfo info, StreamingContext context):base(info, context)
      {
      }
   }
}
