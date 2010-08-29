/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 26.08.2010
 * Time: 22:27
 */

namespace Pppv.Net
{
   using System.Collections;

   public class NetElementEnumerator : IEnumerator
   {
      private IEnumerator innerNetElementEnumerator;

      public NetElementEnumerator(IEnumerator enumerator)
      {
         this.innerNetElementEnumerator = enumerator;
      }

      private NetElementEnumerator()
      {
      }

      object IEnumerator.Current
      {
         get
         {
            return this.innerNetElementEnumerator.Current;
         }
      }

      public NetElement Current
      {
         get
         {
            return (NetElement) this.innerNetElementEnumerator.Current;
         }
      }

      public bool MoveNext()
      {
         return this.innerNetElementEnumerator.MoveNext();
      }

      public void Reset()
      {
         this.innerNetElementEnumerator.Reset();
      }
   }
}