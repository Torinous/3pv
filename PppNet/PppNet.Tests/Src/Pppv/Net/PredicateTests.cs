/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 04.10.2010
 * Time: 23:11
 */

namespace Pppv.Net
{
	using System;
	using NUnit.Framework;

	using Pppv.Net;
	using Pppv.Utils;

	[TestFixture]
	public class PredicateTests
	{
		[Test]
		public void TestOfEmptyConstructor()
		{
			Predicate predicate = new Predicate();
		}

		[Test]
		public void TestOfSerealizationWithData()
		{
			Predicate predicate = new Predicate();
			SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(predicate, "Pppv.Resources.PredicateExample2.pnml");
			serealizationHelper.Perform();
		}
	}
}