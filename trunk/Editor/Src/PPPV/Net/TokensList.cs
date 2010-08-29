namespace Pppv.Net 
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   [Serializable()]
   [XmlRoot("initialMarking")]
   public class TokensList : IXmlSerializable
   {
      private List<Token> list;

      public TokensList(int size)
      {
         this.list = new List<Token>(size);
      }

      public event EventHandler Change;

      public List<Token> List
      {
         get { return this.list; }
      }

      public void Add(Token value)
      {
         this.List.Add(value);
         this.OnChange(new EventArgs());
      }

      public void WriteXml(XmlWriter writer)
      {
         foreach (Token token in this.list)
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

      protected void OnChange(EventArgs args)
      {
         if (this.Change != null)
         {
            this.Change(this, args);
         }
      }
   }
}