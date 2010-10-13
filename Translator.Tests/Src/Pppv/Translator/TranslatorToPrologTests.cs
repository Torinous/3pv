/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 09.10.2010
 * Time: 19:41
 */
namespace Pppv.Translator
{
   using System;
   using NUnit.Framework;

   [TestFixture]
   public class TranslatorToPrologTests
   {
      [Test]
      public void TestOfCreation()
      {
         TranslatorToProlog netTranslator = new TranslatorToProlog();
      }
   }
}
