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
   public class TransitionTests
   {
      [Test]
      public void TestOfConstractions()
      {
         Transition transition = new Transition();
         Assert.That(transition.X, Is.EqualTo(0), "Неверны кординаты Перехода");
         Assert.That(transition.Y, Is.EqualTo(0), "Неверны кординаты Перехода");
         transition = new Transition(new Point(0, 0));
         Assert.That(transition.X, Is.EqualTo(0), "Неверны кординаты Перехода");
         Assert.That(transition.Y, Is.EqualTo(0), "Неверны кординаты Перехода");
         transition = new Transition(new Point(10, 10));
         Assert.That(transition.X, Is.EqualTo(10), "Неверны кординаты Перехода");
         Assert.That(transition.Y, Is.EqualTo(10), "Неверны кординаты Перехода");
         transition = new Transition(new Point(-10, -10));
         Assert.That(transition.X, Is.EqualTo(-10), "Неверны кординаты Перехода");
         Assert.That(transition.Y, Is.EqualTo(-10), "Неверны кординаты Перехода");
         transition = new Transition(new Point(-10, 10));
         Assert.That(transition.X, Is.EqualTo(-10), "Неверны кординаты Перехода");
         Assert.That(transition.Y, Is.EqualTo(10), "Неверны кординаты Перехода");
         transition = new Transition(new Point(10, -10));
         Assert.That(transition.X, Is.EqualTo(10), "Неверны кординаты Перехода");
         Assert.That(transition.Y, Is.EqualTo(-10), "Неверны кординаты Перехода");
      }

      [Test]
      public void TestOfSerealizationWithoutData()
      {
         Transition transition = new Transition();
         SerealizationTestHelper serealizationHelper = new SerealizationTestHelper(transition, "Pppv.Resources.TransitionExample1.pnml");
         serealizationHelper.Perform();
      }
   }
}
