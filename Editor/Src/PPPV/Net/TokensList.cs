using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace PPPV.Net {

   [Serializable()]
   [XmlRoot("initialMarking")]
   public class TokensList:ArrayList, IXmlSerializable{

      /*Конструкторы*/
      public TokensList(int a):base(a){
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
         foreach(Token token in this){
            token.WriteXml(writer);
         }
      }

      public void ReadXml (XmlReader reader){
         XmlReader subTreeReader;
         reader.Read();
         if(reader.Name == "initialMarking" && reader.NodeType == XmlNodeType.Element ){
            if(!reader.IsEmptyElement){
               reader.ReadToDescendant("token");
               while(reader.Name == "token" && reader.NodeType == XmlNodeType.Element){
                  subTreeReader = reader.ReadSubtree();
                  this.Add(new Token(subTreeReader));
                  subTreeReader.Close();
                  reader.Skip();
               }
               reader.ReadEndElement(); // initialMarking
            }else{
               reader.Skip(); // initialMarking
            }
         }else{
            throw new Exception("Невозможно десереализовать TokensList. Не верен тип узла xml.");
         }
      }

      public XmlSchema GetSchema()
      {
         return(null);
      }
   } // CortegeList
} // namespace

