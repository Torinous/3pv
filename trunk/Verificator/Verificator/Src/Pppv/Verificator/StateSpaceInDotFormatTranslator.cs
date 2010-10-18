/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 12.10.2010
 * Time: 18:34
 *
 *
 */
namespace Pppv.Verificator
{
   using System;
   using System.Diagnostics;
   using System.IO;
   using System.Text;

   using Pppv.Net;

   using SbsSW.SwiPlCs;
   using SbsSW.SwiPlCs.Exceptions;

   public static class StateSpaceInDotFormatTranslator
   {
      public static string Create()
      {
         PlTerm term = PlQuery.PlCallQuery("stateSpaceToDotFormatTmpFile(FileName)");
         string fileName = term.ToString();
         StreamReader tmpFileStream = new StreamReader(fileName, Encoding.GetEncoding(1251));
         string result = tmpFileStream.ReadToEnd();
         tmpFileStream.Close();
         return result;
      }
   }
}
