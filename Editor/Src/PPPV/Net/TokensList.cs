namespace Pppv.Net 
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Collections.ObjectModel;
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
            token.WriteXml(writer);
         }
      }

      public void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.Read();
         if (reader.Name == "initialMarking" && reader.NodeType == XmlNodeType.Element)
         {
            if (!reader.IsEmptyElement)
            {
               reader.ReadToDescendant("token");
               while (reader.Name == "token" && reader.NodeType == XmlNodeType.Element)
               {
                  subTreeReader = reader.ReadSubtree();
                  this.Add(new Token(subTreeReader));
                  subTreeReader.Close();
                  reader.Skip();
               }

               reader.ReadEndElement(); // initialMarking
            }
            else
            {
               reader.Skip(); // initialMarking
            }
         }
         else
         {
            throw new NetException("Невозможно десереализовать TokensList. Не верен тип узла xml.");
         }
      }

      public XmlSchema GetSchema()
      {
         return null;
      }
   }
}