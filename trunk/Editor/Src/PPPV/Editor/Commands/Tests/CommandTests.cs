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

   using Pppv.Net;

   [TestFixture]
   public class CommonCommandsTests
   {
      private const int NetElementCount = 1000;

      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Pppv.Net.PetriNet", Justification = "В тестах не важно")]
      public void TestOfManyDeletion()
      {
         PetriNet net = new PetriNet();
         int netElementIndex = 0;

         while (netElementIndex < NetElementCount)
         {
            AddNetElementCommand c = new AddNetElementCommand(net);
            c.Element = new Place();
            c.Execute();
            netElementIndex++;
         }

         MacroCommand mc = new MacroCommand();

         foreach (Place place in net.Places)
         {
            mc.Add(new DeleteCommand(place));
         }

         mc.Execute();
      }
   }
}