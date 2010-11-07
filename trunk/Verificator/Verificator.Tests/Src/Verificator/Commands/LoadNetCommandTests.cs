/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 31.10.2010
 * Time: 17:35
 *
 */

namespace Pppv.Verificator.Commands
{
	using System;
	using System.Reflection;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using NUnit.Framework;
	using NUnit.Framework.Constraints;

	using Pppv.Net;
	using Pppv.ApplicationFramework.Commands;
	using SbsSW.SwiPlCs;
	using SbsSW.SwiPlCs.Exceptions;

	[TestFixture]
	public class LoadNetCommandTests
	{
		[TestFixtureSetUp]
		public void Init()
		{
			SWIProlog.InitPrologEngineIfNeed();
		}
		
		/*[Test]
		public void TestOfSimpeCreation()
		{
			PetriNet net = new PetriNet();
			Command command = new LoadNetCommand(net);
			command.Execute();
		}*/

		/*[Test]
		public void TestOfStateSpaceCreation()
		{
			PetriNet net = new PetriNet();
			XmlSerializer serializer = new XmlSerializer(typeof(PetriNet));
			net = serializer.Deserialize(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.PetriNetExample1.pnml")) as PetriNet;
			Command command = new LoadNetCommand(net);
			command.Execute();
			try
			{
				Assert.That(PlQuery.PlCall("statespace:rstate(N,X)."), Is.False, "Предложения пространства состояний присутствуют до его построения");
			}
			catch (PlException e)
			{
				Console.WriteLine(e.Message);
			}

			Command command2 = new ConstructStateSpaceCommand();
			command2.Execute();
			Assert.That(PlQuery.PlCall("statespace:rstate(N,X)."), Is.True, "Предложения пространства состояний отсутствуют после его построения");
		}*/
	}
}

