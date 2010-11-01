/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 08.10.2010
 * Time: 2:31
 */

namespace Pppv.Editor.Commands
{
	using System;
	using NUnit.Framework;

	using Pppv.Editor;
	using Pppv.Editor.Commands;
	using Pppv.Net;

	[TestFixture]
	public class AddNetElementCommandTests
	{
		[Test]
		public void TestAddingPlace()
		{
			PetriNet net = new PetriNet();
			PetriNetGraphical graphicalNet = new PetriNetGraphical(net);
			AddShapeCommand command = new AddShapeCommand();
			command.Net = graphicalNet;
			command.Shape = graphicalNet.CreateShapeForNetElement(new Place());
			command.Execute();
			Assert.That(net.Places.Count, Is.EqualTo(1), "Комманда не добавила место в сеть");
		}
	}
}
