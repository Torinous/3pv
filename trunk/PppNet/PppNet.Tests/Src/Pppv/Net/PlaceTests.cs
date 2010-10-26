/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.09.2010
 * Time: 2:48
 */

namespace Pppv.Net
{
	using System;
	using System.Drawing;
	using NUnit.Framework;
	using NUnit.Framework.Constraints;

	using Pppv.Utils;

	[TestFixture]
	public class PlaceTests
	{
		[Test]
		public void TestOfConstractions()
		{
			Place place = new Place();
			Assert.That(place.X, Is.EqualTo(0), "Неверны кординаты Места");
			Assert.That(place.Y, Is.EqualTo(0), "Неверны кординаты Места");
			place = new Place(new Point(0, 0));
			Assert.That(place.X, Is.EqualTo(0), "Неверны кординаты Места");
			Assert.That(place.Y, Is.EqualTo(0), "Неверны кординаты Места");
			place = new Place(new Point(10, 10));
			Assert.That(place.X, Is.EqualTo(10), "Неверны кординаты Места");
			Assert.That(place.Y, Is.EqualTo(10), "Неверны кординаты Места");
			place = new Place(new Point(-10, -10));
			Assert.That(place.X, Is.EqualTo(-10), "Неверны кординаты Места");
			Assert.That(place.Y, Is.EqualTo(-10), "Неверны кординаты Места");
			place = new Place(new Point(-10, 10));
			Assert.That(place.X, Is.EqualTo(-10), "Неверны кординаты Места");
			Assert.That(place.Y, Is.EqualTo(10), "Неверны кординаты Места");
			place = new Place(new Point(10, -10));
			Assert.That(place.X, Is.EqualTo(10), "Неверны кординаты Места");
			Assert.That(place.Y, Is.EqualTo(-10), "Неверны кординаты Места");
		}

		[Test]
		public void TestOfSerealization()
		{
			Place place = new Place();
			SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(place, "Pppv.Resources.PlaceExample1.pnml");
			serealizationHelper.Perform();
		}
	}
}