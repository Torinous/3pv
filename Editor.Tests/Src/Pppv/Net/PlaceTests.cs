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
   using System.Drawing.Drawing2D;
   using NUnit.Framework;
   using NUnit.Framework.Constraints;

   [TestFixture]
   public class PlaceTests
   {
      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Pppv.Net.Place", Justification = "В тестах не важно")]
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
   }
}