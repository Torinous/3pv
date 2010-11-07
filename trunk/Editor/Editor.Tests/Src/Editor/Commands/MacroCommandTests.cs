/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 08.10.2010
 * Time: 4:07
 */

namespace Pppv.Editor.Commands
{
	using System;
	using NUnit.Framework;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Editor;
	using Pppv.Editor.Commands;
	using Pppv.Net;

	[TestFixture]
	public class MacroCommandTests
	{
		/*[Test]
		public void TestMacroCommand()
		{
			PetriNet net = new PetriNet();
			PetriNetGraphical graphicalNet = new PetriNetGraphical(net);

			MacroCommand macroCommand = new MacroCommand();
			macroCommand.Add(AddNetElementCommandFabric(graphicalNet));
			macroCommand.Add(AddNetElementCommandFabric(graphicalNet));
			macroCommand.Execute();
			Assert.That(net.Places.Count, Is.EqualTo(2), "Комманда не добавила места в сеть");

			MacroCommand macroCommand2 = new MacroCommand();
			macroCommand2.Add(DeleteCommand(graphicalNet, graphicalNet.Places[0] as Place));
			macroCommand2.Add(DeleteCommand(graphicalNet, graphicalNet.Places[1] as Place));
			macroCommand2.Execute();
			Assert.That(net.Places.Count, Is.EqualTo(0), "Комманда не удалила места из сети");
		}*/

		/*private static DeleteCommand DeleteCommand(PetriNetGraphical graphicalNet, INetElement element)
		{
			DeleteCommand command = new DeleteCommand();
			command.Net = graphicalNet;
			command.Shape = graphicalNet.FindShapeForElement(element);
			return command;
		}*/

		private static AddShapeCommand AddNetElementCommandFabric(PetriNetGraphical graphicalNet)
		{
			return new AddShapeCommand(graphicalNet, graphicalNet.CreateShapeForNetElement(new Place()));
		}
	}
}
