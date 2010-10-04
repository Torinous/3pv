/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 23.09.2010
 * Time: 20:08
 */

namespace Pppv.Net
{
   using System;
   using NUnit.Framework;

   using Pppv.Utils;

   [TestFixture]
   public class PetriNetTests
   {
      private const int NetElementCount = 500;

      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Pppv.Net.PetriNet", Justification = "В тестах не важно")]
      public void TestOfEmptyConstructor()
      {
         new PetriNet();
      }

      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Pppv.Net.PetriNet", Justification = "В тестах не важно")]
      public void TestOfManyDeletion()
      {
         PetriNet net = new PetriNet();
         int netElementIndex = 0;

         while (netElementIndex < NetElementCount)
         {
            net.AddElement(new Place());
            net.AddElement(new Transition());
            net.AddElement(new Arc(ArcType.NormalArc));
            net.AddElement(new Arc(ArcType.InhibitorArc));
            netElementIndex++;
         }

         Assert.That(net.Places.Count, Is.EqualTo(NetElementCount));
         Assert.That(net.Transitions.Count, Is.EqualTo(NetElementCount));
         Assert.That(net.Arcs.Count, Is.EqualTo(NetElementCount * 2));

         netElementIndex = 0;
         while (netElementIndex < NetElementCount)
         {
            net.DeleteElement((Place)net.Places[0]);
            net.DeleteElement((Transition)net.Transitions[0]);
            net.DeleteElement((Arc)net.Arcs[0]);
            net.DeleteElement((Arc)net.Arcs[0]);
            netElementIndex++;
         }

         Assert.That(net.Places.Count, Is.EqualTo(0));
         Assert.That(net.Transitions.Count, Is.EqualTo(0));
         Assert.That(net.Arcs.Count, Is.EqualTo(0));
      }

      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Pppv.Net.PetriNet", Justification = "В тестах не важно")]
      public void TestOfSerealizationWithData()
      {
         PetriNet petriNet = new PetriNet();
         SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(petriNet, "Pppv.Resources.PetriNetExample1.pnml");
         serealizationHelper.Perform();
      }

   }
}