﻿namespace PPPV.Net {
	using System;
	using System.Collections;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using System.Windows.Forms;

	 [Serializable()]
	 [XmlRoot("cortege")]
	 public class PredicateList : ArrayList, IXmlSerializable
	 {
			public PredicateList(int size):base(size){
			}
			
			public string Text{
				 get{
						string t = "";
						foreach(Predicate predicate in this){
							if(!String.IsNullOrEmpty(t))
									t = t + "+";
							 t = t + "<" + predicate + ">";
						}
						return t;
				 }
			}

			public event EventHandler Change;
			
			protected void OnChange(EventArgs args){
				 if(Change != null){
						Change(this, args);
				 }
			}

			public override int Add(object value)
			{
				 int val = base.Add(value);
				 OnChange(new EventArgs());
				 return val;
			}
			
			public void Remove(Predicate value) 
      {
         ((IList)this).Remove((object) value);
      }

			public void WriteXml (XmlWriter writer)
			{
				 foreach(Predicate predicate in this){
						predicate.WriteXml(writer);
				 }
			}

			public void ReadXml (XmlReader reader){
				 XmlReader subTreeReader;
				 reader.Read();
				 if(reader.Name == "cortege" && reader.NodeType == XmlNodeType.Element ){
						if(!reader.IsEmptyElement){
							 reader.ReadToDescendant("predicate");
							 while(reader.Name == "predicate" && reader.NodeType == XmlNodeType.Element){
									subTreeReader = reader.ReadSubtree();
									this.Add(new Predicate(subTreeReader));
									subTreeReader.Close();
									reader.Skip();
							 }
							 reader.ReadEndElement(); // initialMarking
						}else{
							 reader.Skip(); // initialMarking
						}
				 }else{
						throw new NetException("Невозможно десереализовать PredicateList. Не верен тип узла xml.");
				 }
			}

			public XmlSchema GetSchema()
			{
				 return(null);
			}
	 } // PredicateList
} // namespace

