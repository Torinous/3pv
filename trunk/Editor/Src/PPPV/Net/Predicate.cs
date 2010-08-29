namespace Pppv.Net
{
   using System;
   using System.Collections;
   using System.Globalization;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.Utils;

   [Serializable()]
   [XmlRoot("predicate")]
   public class Predicate : IXmlSerializable
   {
      private string text;

      public Predicate()
      {
      }

      public Predicate(string text)
      {
         this.text = Upperfy(text);
      }

      public Predicate(XmlReader reader)
      {
         this.ReadXml(reader);
      }

      public string Text
      {
         get { return this.text; }
         set { this.text = Upperfy(value); }
      }

      public override string ToString()
      {
         return this.Text;
      }

      public void WriteXml(XmlWriter writer)
      {
         writer.WriteStartElement("predicate");
         writer.WriteStartElement("value");
         writer.WriteString(this.Text);
         writer.WriteEndElement(); // value
         writer.WriteEndElement(); // token
      }

      public void ReadXml(XmlReader reader)
      {
         reader.Read();
         if (reader.Name == "predicate" && reader.NodeType == XmlNodeType.Element)
         {
            reader.ReadToDescendant("value");
            this.Text = reader.ReadString();
            reader.ReadEndElement(); // value
            reader.ReadEndElement(); // token
         }
         else
         {
            throw new NetException("Невозможно десереализовать Predicate. Не верен тип узла xml.");
         }
      }

      public XmlSchema GetSchema()
      {
         return null;
      }

      private static string Upperfy(string t)
      {
         string txt;
         if (t.Length > 1)
         {
            txt = t.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + t.Substring(1);
         }
         else
         {
            if (t.Length == 1)
            {
               txt = t.ToUpper(CultureInfo.CurrentCulture);
            }
            else
            {
               txt = t;
            }
         }

         return txt;
      }
   }
}