using System;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PPPV.Net {

   [Serializable()]
   [XmlRoot("token")]
   public class Token: IXmlSerializable{
      private string text;

      /*Конструктор*/
      public Token(){
      }

      public Token(string text_){
         text = text_;
      }
      
      public Token(XmlReader reader){
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

      public void WriteXml (XmlWriter writer)
      {
         writer.WriteStartElement("token");
         writer.WriteStartElement("value");
         writer.WriteString(this.Text);
         writer.WriteEndElement(); // value
         writer.WriteEndElement(); // token
      }

      public void ReadXml (XmlReader reader)
      {
         reader.Read();
         if(reader.Name == "token" && reader.NodeType == XmlNodeType.Element ){
            reader.ReadToDescendant("value");
            this.Text = reader.ReadString();
            reader.ReadEndElement(); // value
            reader.ReadEndElement(); // token
         }else{
            throw new Exception("Невозможно десереализовать Token. Не верен тип узла xml.");
         }
      }

      public XmlSchema GetSchema()
      {
         return(null);
      }
   } // Token
} // namespace

