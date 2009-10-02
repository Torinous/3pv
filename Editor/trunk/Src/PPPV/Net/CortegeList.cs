﻿using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace PPPv.Net {

   [Serializable()]
   [XmlRoot("cortege")]
   public class CortegeList:ArrayList, IXmlSerializable{

      /*Конструкторы*/
      public CortegeList(int a):base(a){
      }

      /*Акцессоры доступа*/
      public string Text{
         get{
            string t = "";
            foreach(string predicate in this){
               if(t != "")
                  t = t + "+";
               t = t + "<" + predicate + ">";
            }
            return t;
         }
      }

      public event EventHandler Change;

      /*Методы*/

      protected void OnChange(EventArgs args){
         if(Change != null){
            Change(this, args);
         }
      }

      public override int Add(object obj){
         int val = base.Add(obj);
         OnChange(new EventArgs());
         return val;
      }

      public void WriteXml (XmlWriter writer)
      {
         foreach(string predicate in this){
            writer.WriteStartElement("predicate");
            writer.WriteStartElement("value");
            writer.WriteString(predicate);
            writer.WriteEndElement(); // value
            writer.WriteEndElement(); // predicate
         }
         
      }

      public void ReadXml (XmlReader reader)
      {
         reader.Read();
         if(reader.Name == "cortege" && reader.NodeType == XmlNodeType.Element ){
            if(!reader.IsEmptyElement){
               reader.ReadToDescendant("predicate");
               reader.ReadToDescendant("value");
               this.Add(reader.ReadString());
               reader.ReadEndElement(); // value
               reader.ReadEndElement(); // predicate
               while(reader.Name == "predicate" && reader.NodeType == XmlNodeType.Element){
                  reader.ReadToDescendant("value");
                  this.Add(reader.ReadString());
                  reader.ReadEndElement(); // value
                  reader.ReadEndElement(); // predicate
               }
               reader.ReadEndElement(); // cortege
            }else{
               reader.Skip(); // cortege
            }
         }else{
            throw new Exception("Невозможно десереализовать СortegeList. Не верен тип узла xml.");
         }
      }

      public XmlSchema GetSchema()
      {
         return(null);
      }
   } // CortegeList
} // namespace
