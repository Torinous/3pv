/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 23.09.2010
 * Time: 20:08
 */

namespace Pppv.Net.Tests
{
   using System;
   using NUnit.Framework;

   [TestFixture]
   public class PetriNetTests
   {
      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Pppv.Net.PetriNet", Justification = "В тестах не важно")]
      public void TestOfEmptyConstructor()
      {
         new PetriNet();
      }
   }
}