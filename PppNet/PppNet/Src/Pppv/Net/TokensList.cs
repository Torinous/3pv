namespace Pppv.Net 
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Collections.ObjectModel;
   using System.Globalization;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   [Serializable()]
   [XmlRoot("initialMarking")]
   public class TokensList : Collection<Token>, IXmlSerializable
   {
      public TokensList() : base()
      {
      }

      public new void Add(Token value)
      {
         base.Add(value);
      }

      public void WriteXml(XmlWriter writer)
      {
         foreach (Token token in this)
         {
            XmlSerializer serealizer = new XmlSerializer(token.GetType());
            serealizer.Serialize(writer, token);
         }
      }

      public void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         if (reader.Name == "initialMarking")
         {
            if (!reader.IsEmptyElement)
            {
               reader.Read();
               while (reader.Name == "token" && reader.NodeType == XmlNodeType.Element)
               {
                  subTreeReader = reader.ReadSubtree();
                  Token token = new Token();
                  XmlSerializer serealizer = new XmlSerializer(token.GetType());
                  token = (Token)serealizer.Deserialize(subTreeReader);
                  this.Add(token);
                  subTreeReader.Close();
                  reader.Skip();
               }
   
               reader.ReadEndElement(); // initialMarking
            }
         }
         else
         {
            throw new NetException(String.Format(CultureInfo.InvariantCulture, "Невозможно десереализовать элемент initialMarking. Получен узел {0}", reader.Name));
         }
      }

      public XmlSchema GetSchema()
      {
         return null;
      }
   }
}