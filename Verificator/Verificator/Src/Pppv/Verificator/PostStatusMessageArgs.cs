/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 16.10.2010
 * Time: 20:34
 *
 *
 */

namespace Pppv.Verificator
{
   using System;

   public class PostStatusMessageArgs : EventArgs
   {
      private string message;

      public PostStatusMessageArgs(string message)
      {
         this.Message = message;
      }

      public string Message
      {
         get { return this.message; }
         private set { this.message = value; }
      }
   }
}
