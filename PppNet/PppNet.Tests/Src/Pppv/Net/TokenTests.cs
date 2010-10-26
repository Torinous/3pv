/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 30.09.2010
 * Time: 17:11
 */

namespace Pppv.Net
{
	using System;
	using NUnit.Framework;
	using NUnit.Framework.Constraints;

	using Pppv.Net;
	using Pppv.Utils;

	[TestFixture]
	public class TokenTests
	{
		private const string TokenString = "tokenString_tttt";

		[Test]
		public void TestOfConstraction()
		{
			Token token = new Token();
		}

		[Test]
		public void TestHoldingTest()
		{
			Token token = new Token(TokenString);
			Assert.That(token.Text, Is.EqualTo(TokenString), "Хранимое значение в Token изменилось");
		}

		[Test]
		public void TestOfSerealizationWithData()
		{
			Token token = new Token();
			SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(token, "Pppv.Resources.TokenExample1.pnml");
			serealizationHelper.Perform();
		}
	}
}