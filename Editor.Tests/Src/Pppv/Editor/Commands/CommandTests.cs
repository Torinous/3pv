/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.09.2010
 * Time: 2:41
 */

namespace Pppv.Editor.Commands.Tests
{
   using System;
   using NUnit.Framework;

   using Pppv.Editor.Commands;

   [TestFixture]
   public class CommonCommandsTests
   {
      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      public void NullCommandTest()
      {
         Command c = new NullCommand();
         c.Execute();
      }
   }
}