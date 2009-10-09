using System;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using PPPV.Utils;

namespace PPPV.Net {

   [Serializable()]
   [XmlRoot("predicate")]
   public class Predicate: IXmlSerializable{
      private string text;

      /*Конструктор*/
      public Predicate(){
      }

      public Predicate(string text_){
         text = text_;
      }

      public Predicate(XmlReader reader){
         this.ReadXml(reader);
      }

      /*Акцессоры доступа*/
      public string Text{
         get{
            return text;
         }
         set{
            text = value;
         }
      }
      
      public override string ToString()
      {
         return Text;
      }

      public void WriteXml (XmlWriter writer)
      {
         writer.WriteStartElement("predicate");
         writer.WriteStartElement("value");
         writer.WriteString(this.Text);
         writer.WriteEndElement(); // value
         writer.WriteEndElement(); // token
      }

      public void ReadXml (XmlReader reader)
      {
         reader.Read();
         if(reader.Name == "predicate" && reader.NodeType == XmlNodeType.Element ){
            reader.ReadToDescendant("value");
            this.Text = reader.ReadString();
            reader.ReadEndElement(); // value
            reader.ReadEndElement(); // token
         }else{
            throw new Exception("Невозможно десереализовать Predicate. Не верен тип узла xml.");
         }
      }

      public XmlSchema GetSchema()
      {
         return(null);
      }
   } // Predicate
} // namespace

