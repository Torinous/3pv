/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.09.2010
 * Time: 2:41
 */

namespace Pppv.Editor.Commands
{
   using System;
   using NUnit.Framework;

   [TestFixture]
   public class NullCommandTests
   {
      [Test]
      public void TestOfEmptyConstructor()
      {
         Command c = new NullCommand();
         c.Execute();
      }
   }
}