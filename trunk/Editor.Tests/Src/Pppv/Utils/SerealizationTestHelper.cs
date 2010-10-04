/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 04.10.2010
 * Time: 13:34
 */

namespace Pppv.Utils
{
   using System;
   using System.IO;
   using System.Reflection;
   using System.Text;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;
   using NUnit.Framework;
   using NUnit.Framework.Constraints;

   public class SerealizationTestHelper
   {
      private object target;
      private string sourceResourcePath;

      public SerealizationTestHelper(object target, string sourceResourcePath)
      {
         this.Target = target;
         this.SourceResourcePath = sourceResourcePath;
      }

      public string SourceResourcePath
      {
         get { return this.sourceResourcePath; }
         set { this.sourceResourcePath = value; }
      }

      public object Target
      {
         get { return this.target; }
         set { this.target = value; }
      }

      public void Perform()
      {
         XmlSerializer serealizer = new XmlSerializer(this.Target.GetType());
         this.Target = serealizer.Deserialize(Assembly.GetExecutingAssembly().GetManifestResourceStream(this.SourceResourcePath));
         string tmpFile = Path.GetTempFileName();
         StreamWriter tmpFilestream = new StreamWriter(tmpFile, false, Encoding.GetEncoding(1251));
         serealizer.Serialize(tmpFilestream, this.Target);
         tmpFilestream.Close();
         Console.WriteLine("\t" + tmpFile);

         // TODO:Файлы потом нужно удалять, пока полезно для дебага
         Stream streamResult = new FileStream(tmpFile, FileMode.Open);
         Stream streamEtalon = Assembly.GetExecutingAssembly().GetManifestResourceStream(this.SourceResourcePath);
         Assert.That(streamResult, Is.EqualTo(streamEtalon), "Данные десереализации не равны исходным данным сереализации");
      }
   }
}
