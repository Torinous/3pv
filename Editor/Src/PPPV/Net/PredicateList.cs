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
   [XmlRoot("cortege")]
   public class PredicateList : IXmlSerializable
   {
      private Collection<Predicate> list;

      public PredicateList()
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
            predicate.WriteXml(writer);
         }
      }

      public void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.Read();
         if (reader.Name == "cortege" && reader.NodeType == XmlNodeType.Element)
         {
            if (!reader.IsEmptyElement)
            {
               reader.ReadToDescendant("predicate");
               while (reader.Name == "predicate" && reader.NodeType == XmlNodeType.Element)
               {
                  subTreeReader = reader.ReadSubtree();
                  this.Add(new Predicate(subTreeReader));
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
            throw new NetException("Невозможно десереализовать PredicateList. Не верен тип узла xml.");
         }
      }

      public XmlSchema GetSchema()
      {
         return null;
      }
   }
}