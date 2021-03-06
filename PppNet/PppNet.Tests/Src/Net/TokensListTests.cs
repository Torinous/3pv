﻿/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 30.09.2010
 * Time: 17:24
 */

namespace Pppv.Net
{
	using System;
	using System.Globalization;
	using NUnit.Framework;
	using NUnit.Framework.Constraints;

	using Pppv.Net;
	using Pppv.Utils;

	[TestFixture]
	public class TokensListTests
	{
		private const int TokenCount = 3;

		[Test]
		public void TestOfCreation()
		{
			TokensList tokenList = new TokensList();
		}

		[Test]
		public void TestOfAddingRemoving()
		{
			TokensList tokensList = new TokensList();
			int i = 0;
			while (i < TokenCount)
			{
				tokensList.Add(new Token("ttt" + i));
				i++;
			}

			Assert.That(tokensList.Count, Is.EqualTo(TokenCount), "Количество Меток в контейнере не равно {0}", TokenCount);
			i = 0;
			while (i < TokenCount)
			{
				tokensList.RemoveAt(0);
				i++;
			}
		}

		[Test]
		public void TestOfSerealizationWithData()
		{
			TokensList tokensList = new TokensList();
			SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(tokensList, "Pppv.Resources.TokensListExample1.pnml");
			serealizationHelper.Perform();
		}

		[Test]
		public void TestOfSerealizationWithoutData()
		{
			TokensList tokensList = new TokensList();
			SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(tokensList, "Pppv.Resources.TokensListExample2.pnml");
			serealizationHelper.Perform();
		}
	}
}