﻿/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 23.09.2010
 * Time: 20:25
 */

namespace Pppv.Net
{
   using System;
   using NUnit.Framework;

   using Pppv.Utils;

   [TestFixture]
   public class ArcTests
   {
      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Pppv.Net.Arc", Justification = "В тестах не важно")]
      public void TestOfEmptyConstructor()
      {
         new Arc(ArcType.NormalArc);
         new Arc(ArcType.InhibitorArc);
      }

      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Pppv.Net.Arc", Justification = "В тестах не важно")]
      public void TestOfSerealizationWithoutData()
      {
         Arc arc = new Arc();
         SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(arc, "Pppv.Resources.ArcExample1.pnml");
         serealizationHelper.Perform();
      }
   }
}
