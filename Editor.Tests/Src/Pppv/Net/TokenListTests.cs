/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 30.09.2010
 * Time: 17:24
 */

namespace Pppv.Net
{
   using System;
   using NUnit.Framework;
   using NUnit.Framework.Constraints;

   using Pppv.Net;

   [TestFixture]
   public class TokensListTests
   {
      private const int TokenCount = 10;

      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "tokenList", Justification = "В тестах не важно")]
      public void TestOfCreation()
      {
         TokensList tokenList = new TokensList();
      }

      [Test]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "В тестах не важно")]
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
   }
}
