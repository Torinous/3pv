namespace Pppv.Net 
{
   using System;
   using System.Collections;
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
      
      public Token(XmlReader reader)
      {
         this.ReadXml(reader);
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
         writer.WriteStartElement("token");
         writer.WriteStartElement("value");
         writer.WriteString(this.Text);
         writer.WriteEndElement(); // value
         writer.WriteEndElement(); // token
      }

      public void ReadXml(XmlReader reader)
      {
         reader.Read();
         if (reader.Name == "token" && reader.NodeType == XmlNodeType.Element)
         {
            reader.ReadToDescendant("value");
            this.Text = reader.ReadString();
            reader.ReadEndElement(); // value
            reader.ReadEndElement(); // token
         }
         else
         {
            throw new NetException("Невозможно десереализовать Token. Не верен тип узла xml.");
         }
      }

      public XmlSchema GetSchema()
      {
         return null;
      }
   }
}