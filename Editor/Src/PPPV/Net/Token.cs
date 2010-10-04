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
   [XmlRoot("token")]
   public class Token : IXmlSerializable
   {
      private string text;

      public Token()
      {
      }

      public Token(string text)
      {
         this.text = text;
      }

      public string Text
      {
         get { return this.text; }
         set { this.text = value; }
      }

      public override string ToString()
      {
         return this.Text;
      }

      public void WriteXml(XmlWriter writer)
      {
         writer.WriteStartElement("value");
         writer.WriteString(this.Text);
         writer.WriteEndElement(); // value
      }

      public void ReadXml(XmlReader reader)
      {
         if (reader.Name == "token")
         {
            if (!reader.IsEmptyElement)
            {
               reader.Read();
               if (reader.Name == "value")
               {
                  this.Text = reader.ReadString();
                  reader.ReadEndElement(); // token
               }
               else
               {
                  throw new NetException(String.Format(CultureInfo.InvariantCulture, "Невозможно десереализовать элемент token. Получен узел {0}, ожидался value", reader.Name));
               }
            }
         }
         else
         {
            throw new NetException(String.Format(CultureInfo.InvariantCulture, "Невозможно десереализовать элемент token. Получен узел {0}", reader.Name));
         }
      }

      public XmlSchema GetSchema()
      {
         return null;
      }
   }
}