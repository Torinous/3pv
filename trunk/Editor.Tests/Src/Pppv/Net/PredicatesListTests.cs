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
      public void TestOfEmptyConstructor()
      {
         PredicatesList predicatesList = new PredicatesList();
      }

      [Test]
      public void TestOfSerealizationWithData()
      {
         PredicatesList predicatesList = new PredicatesList();
         SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(predicatesList, "Pppv.Resources.PredicatesListExample1.pnml");
         serealizationHelper.Perform();
      }

      [Test]
      public void TestOfSerealizationWithoutData()
      {
         PredicatesList predicatesList = new PredicatesList();
         SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(predicatesList, "Pppv.Resources.PredicatesListExample2.pnml");
         serealizationHelper.Perform();
      }
   }
}