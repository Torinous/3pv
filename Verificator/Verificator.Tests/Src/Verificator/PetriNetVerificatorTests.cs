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
	using System.Reflection;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using NUnit.Framework;
	using NUnit.Framework.Constraints;

	using Pppv.Net;
	using SbsSW.SwiPlCs;
	using SbsSW.SwiPlCs.Exceptions;

	[TestFixture]
	public class PetriNetVerificatorTests
	{
		[Test]
		public void TestOfSimpeCreation()
		{
			PetriNet net = new PetriNet();
			PetriNetVerificator verificator = PetriNetVerificator.Instance;
			verificator.LoadNetToPrologEngine(net);
		}

		[Test]
		public void TestOfMultipleCreation()
		{
			PetriNet net = new PetriNet();
			PetriNetVerificator verificator = PetriNetVerificator.Instance;
			PetriNetVerificator verificator2 = PetriNetVerificator.Instance;
			verificator.LoadNetToPrologEngine(net);
			verificator2.LoadNetToPrologEngine(net);
		}

		[Test]
		public void TestOfStateSpaceCreation()
		{
			PetriNet net = new PetriNet();
			XmlSerializer serializer = new XmlSerializer(typeof(PetriNet));
			net = serializer.Deserialize(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.PetriNetExample1.pnml")) as PetriNet;
			PetriNetVerificator verificator = PetriNetVerificator.Instance;
			verificator.LoadNetToPrologEngine(net);
			try
			{
				Assert.That(PlQuery.PlCall("statespace:rstate(N,X)."), Is.False, "Предложения пространства состояний присутствуют до его построения");
			}
			catch (PlException e)
			{
				Console.WriteLine(e.Message);
			}

			verificator.CalculateStateSpace();
			Assert.That(PlQuery.PlCall("statespace:rstate(N,X)."), Is.True, "Предложения пространства состояний отсутствуют после его построения");
		}
	}
}
