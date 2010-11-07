/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 08.10.2010
 * Time: 3:44
 */
namespace Pppv.Editor.Commands
{
	using System;
	using NUnit.Framework;

	using Pppv.Editor;
	using Pppv.Editor.Commands;
	using Pppv.Net;

	[TestFixture]
	public class DeleteCommandTests
	{
		/*[Test]
		public void TestSimpleDeletion()
		{
			PetriNet net = new PetriNet();
			PetriNetGraphical graphicalNet = new PetriNetGraphical(net);
			AddShapeCommand command = new AddShapeCommand();
			command.Net = graphicalNet;
			command.Shape = graphicalNet.CreateShapeForNetElement(new Place());
			command.Execute();
			Assert.That(net.Places.Count, Is.EqualTo(1), "Комманда не добавила место в сеть");
			DeleteCommand command2 = new DeleteCommand();
			command2.Net = graphicalNet;
			command2.Shape = graphicalNet.FindShapeForElement(graphicalNet.Places[0] as Place);
			command2.Execute();
			Assert.That(net.Places.Count, Is.EqualTo(0), "Комманда не удалила место из сети");
		}
		*/
	}
}
