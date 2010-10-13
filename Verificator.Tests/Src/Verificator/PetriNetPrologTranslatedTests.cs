/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 09.10.2010
 * Time: 1:51
 *
 *
 */
namespace Pppv.Verificator
{
   using System;
   using System.Reflection;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using NUnit.Framework;
   using NUnit.Framework.Constraints;

   using Pppv.Net;

   [TestFixture]
   public class PetriNetPrologTranslatedTests
   {
      [Test]
      public void TestOfTranslationToProlog()
      {
         PetriNet net = new PetriNet();
         XmlSerializer serializer = new XmlSerializer(typeof(PetriNet));
         net = serializer.Deserialize(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.PetriNetExample1.pnml")) as PetriNet;
         PetriNetPrologTranslated netTranslator = new PetriNetPrologTranslated(net);
         Assert.That(netTranslator.PlacesList(), Is.StringContaining("place"));
         Assert.That(netTranslator.Precondition(netTranslator.Net.Transitions[0] as Transition), Is.StringContaining("p"));
         Assert.That(netTranslator.Postcondition(netTranslator.Net.Transitions[0] as Transition), Is.StringContaining("p"));
         Assert.That(netTranslator.TransitionsList(), Is.StringContaining("transition"));
         Assert.That(netTranslator.ToProlog(), Is.StringContaining("transition"));
      }

      [Test]
      public void TestOfKernelCodeExistance()
      {
         Assert.That(PetriNetPrologTranslated.KernelCode, Is.StringContaining("transition"));
      }
   }
}
