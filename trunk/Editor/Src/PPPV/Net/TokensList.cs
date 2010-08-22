namespace PPPV.Net 
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;
   using System.Windows.Forms;

   [Serializable()]
   [XmlRoot("initialMarking")]
   public class TokensList : IXmlSerializable
   {
      List<Token> list;
      
      public List<Token> List {
         get { return list; }
      }

      public TokensList(int size)
      {
         list = new List<Token>(size);
      }

      public event EventHandler Change;

      protected void OnChange(EventArgs args){
         if(Change != null){
            Change(this, args);
         }
      }

      public int Add(Token value){
         int val =((IList)this).Add((object) value);
         OnChange(new EventArgs());
         return val;
      }

      public void WriteXml (XmlWriter writer)
      {
         foreach(Token token in list){
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
            throw new NetException("Невозможно десереализовать TokensList. Не верен тип узла xml.");
         }
      }

      public XmlSchema GetSchema()
      {
         return(null);
      }
   } // CortegeList
} // namespace

