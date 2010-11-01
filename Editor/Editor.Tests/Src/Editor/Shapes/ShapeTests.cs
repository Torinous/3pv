/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 30.09.2010
 * Time: 15:46
 */

namespace Pppv.Editor.Shapes
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using NUnit.Framework;
   using NUnit.Framework.Constraints;

   using Pppv.Net;

   [TestFixture]
   public class ShapeTests
   {
      private const int XFixed = 256, yFixed = 652;
      private const string IdFixed = "id_tttt";
      private const string TokenFixed = "token_fff";
      private const int IdNumberFixed = 666;
      private const string DecoratorAndDecoratedIsNotEqual = "Данные декоратора и декорируемого объекта не одинаковы";

      [Test]
      public void TestOfEqualutyOfPlaceAndPlaceShape()
      {
         Place place = new Place(new Point(XFixed, yFixed));
         place.Name = IdFixed;
         place.Tokens.Add(new Token(TokenFixed));
         place.SetId(IdNumberFixed);
         PlaceShape placeShape = new PlaceShape(place, new PetriNetGraphical());
         Assert.That(placeShape.X, Is.EqualTo(place.X), DecoratorAndDecoratedIsNotEqual);
         Assert.That(placeShape.Y, Is.EqualTo(place.Y), DecoratorAndDecoratedIsNotEqual);
         Assert.That(placeShape.Name, Is.EqualTo(place.Name), DecoratorAndDecoratedIsNotEqual);
         Assert.That(placeShape.Id, Is.EqualTo(place.Id), DecoratorAndDecoratedIsNotEqual);
         Assert.That(placeShape.Tokens[0], Is.SameAs(place.Tokens[0]), DecoratorAndDecoratedIsNotEqual);
         Assert.That(placeShape.Tokens, Is.SameAs(place.Tokens), DecoratorAndDecoratedIsNotEqual);
      }
   }
}