/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 12.10.2010
 * Time: 4:24
 *
 *
 */
namespace Pppv.Verificator
{
   using System;
   using System.Runtime.Serialization;

   [Serializable]
   public class VerificatorException : Exception
   {
      public VerificatorException() : base()
      {
      }

      public VerificatorException(string message) : base(message)
      {
      }

      public VerificatorException(string message, Exception exception) : base(message, exception)
      {
      }

      protected VerificatorException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}
