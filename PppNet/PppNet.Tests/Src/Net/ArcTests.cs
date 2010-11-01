/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 23.09.2010
 * Time: 20:25
 */

namespace Pppv.Net
{
	using System;
	using NUnit.Framework;

	using Pppv.Utils;

	[TestFixture]
	public class ArcTests
	{
		[Test]
		public void TestOfEmptyConstructor()
		{
			new Arc(ArcType.NormalArc);
			new Arc(ArcType.InhibitorArc);
		}

		[Test]
		public void TestOfSerealizationWithoutData()
		{
			Arc arc = new Arc();
			SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(arc, "Pppv.Resources.ArcExample1.pnml");
			serealizationHelper.Perform();
		}
	}
}
