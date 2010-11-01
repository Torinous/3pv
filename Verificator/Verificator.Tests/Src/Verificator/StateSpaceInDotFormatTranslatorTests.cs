/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 12.10.2010
 * Time: 23:15
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

	using Pppv.Net;
	using Pppv.Verificator.Commands;
	using Pppv.ApplicationFramework.Commands;

	[TestFixture]
	public class StateSpaceInDotFormatTranslatorTests
	{
		[Test]
		public void TestTranslationOfEmptyNet()
		{
			PetriNet net = new PetriNet();
			new LoadNetCommand(net).Execute();
			
			string stateSpaceInDot = StateSpaceInDotFormatTranslator.Create();
			Assert.That(stateSpaceInDot, Is.StringContaining("digraph"), "Пространство состояний в формате dot не имеет глобального тега");
		}

		[Test]
		public void TestTranslationOfNetWithData()
		{
			PetriNet net = new PetriNet();
			XmlSerializer serializer = new XmlSerializer(typeof(PetriNet));
			net = serializer.Deserialize(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.PetriNetExample1.pnml")) as PetriNet;
			new LoadNetCommand(net).Execute();
			new ConstructStateSpaceCommand().Execute();

			string stateSpaceInDot = StateSpaceInDotFormatTranslator.Create();
			Assert.That(stateSpaceInDot, Is.StringContaining("S0 -> S1"), "Пространство состояний в формате dot не имеет перехода из стартового состояния");
		}
	}
}
