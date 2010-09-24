/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.09.2010
 * Time: 2:48
 */

namespace Pppv.Net.Tests
{
   using System;
   using NUnit.Framework;

   [TestFixture]
   public class PlaceTests
   {
      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Pppv.Net.Place", Justification = "В тестах не важно")]
      public void TestOfEmptyConstructor()
      {
         new Place();
      }
   }
}
