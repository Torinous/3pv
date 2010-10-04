/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 04.10.2010
 * Time: 23:11
 */
 
namespace Pppv.Net
{
   using System;
   using NUnit.Framework;

   using Pppv.Net;
   using Pppv.Utils;

   [TestFixture]
   public class PredicateTests
   {
      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "predicate", Justification = "В тестах не важно")]
      public void TestOfEmptyConstructor()
      {
         Predicate predicate = new Predicate();
      }

      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      public void TestOfSerealizationWithData()
      {
         Predicate predicate = new Predicate();
         SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(predicate, "Pppv.Resources.PredicateExample2.pnml");
         serealizationHelper.Perform();
      }
   }
}