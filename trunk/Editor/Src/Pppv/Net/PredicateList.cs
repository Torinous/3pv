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
   [XmlRoot("cortege")]
   public class PredicatesList : IXmlSerializable
   {
      private Collection<Predicate> list;

      public PredicatesList()
      {
         this.list = new Collection<Predicate>();
      }

      public Collection<Predicate> List
      {
         get { return this.list; }
      }

      public string Text
      {
         get
         {
            string t = String.Empty;
            foreach (Predicate predicate in this.list)
            {
               if (!String.IsNullOrEmpty(t))
               {
                  t = t + "+";
               }

               t = t + "<" + predicate + ">";
            }

            return t;
         }
      }

      public void Add(Predicate value)
      {
         this.list.Add(value);
      }
      
      public void Remove(Predicate value)
      {
         this.list.Remove(value);
      }

      public void WriteXml(XmlWriter writer)
      {
         foreach (Predicate predicate in this.List)
         {
            XmlSerializer serealizer = new XmlSerializer(predicate.GetType());
            serealizer.Serialize(writer, predicate);
         }
      }

      public void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         if (reader.Name == "cortege")
         {
            if (!reader.IsEmptyElement)
            {
               reader.Read();
               while (reader.Name == "predicate" && reader.NodeType == XmlNodeType.Element)
               {
                  subTreeReader = reader.ReadSubtree();
                  Predicate predicate = new Predicate();
                  XmlSerializer serealizer = new XmlSerializer(typeof(Predicate));
                  predicate = serealizer.Deserialize(subTreeReader) as Predicate;
                  this.Add(predicate);
                  subTreeReader.Close();
                  reader.Skip();
               }

               reader.ReadEndElement(); // cortege
            }
         }
         else
         {
            throw new NetException(String.Format(CultureInfo.InvariantCulture, "Невозможно десереализовать элемент cortege. Получен узел {0}", reader.Name));
         }
      }

      public XmlSchema GetSchema()
      {
         return null;
      }
   }
}