/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 05.10.2010
 * Time: 1:00
 */

namespace Pppv.Net
{
   using System;
   using NUnit.Framework;

   using Pppv.Utils;

   [TestFixture]
   public class PredicatesListTests
   {
      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "predicatesList", Justification = "В тестах не важно")]
      public void TestOfEmptyConstructor()
      {
         PredicatesList predicatesList = new PredicatesList();
      }

      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "predicatesList", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      public void TestOfSerealizationWithData()
      {
         PredicatesList predicatesList = new PredicatesList();
         SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(predicatesList, "Pppv.Resources.PredicatesListExample1.pnml");
         serealizationHelper.Perform();
      }

      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "predicatesList", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      public void TestOfSerealizationWithoutData()
      {
         PredicatesList predicatesList = new PredicatesList();
         SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(predicatesList, "Pppv.Resources.PredicatesListExample2.pnml");
         serealizationHelper.Perform();
      }
   }
}