/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 12.10.2010
 * Time: 6:01
 *
 */

namespace Pppv.Verificator
{
   using System;
   using NUnit.Framework;
   using NUnit.Framework.Constraints;

   using Pppv.Net;
   using SbsSW.SwiPlCs;

   [TestFixture]
   public class PetriNetVerificatorTests
   {
      [Test]
      public void TestOfSimpeCreation()
      {
         PetriNet net = new PetriNet();
         PetriNetVerificator verificator = PetriNetVerificator.Instance;
         verificator.LoadNetToPrologEngine(net);
         verificator.Cleanup();
      }

      [Test]
      public void TestOfMultipleCreation()
      {
         PetriNet net = new PetriNet();
         PetriNetVerificator verificator = PetriNetVerificator.Instance;
         PetriNetVerificator verificator2 = PetriNetVerificator.Instance;
         verificator.LoadNetToPrologEngine(net);
         verificator2.LoadNetToPrologEngine(net);
         verificator.Cleanup();
         verificator2.Cleanup();
      }

      [Test]
      public void TestOfIsInitializedOnCleanup()
      {
         PetriNet net = new PetriNet();
         PetriNetVerificator verificator = PetriNetVerificator.Instance;
         verificator.LoadNetToPrologEngine(net);
         verificator.Cleanup();
         Assert.That(PlEngine.IsInitialized, Is.EqualTo(false), "После Cleanup`а ядро пролога осталось инициализированным");
      }
   }
}
