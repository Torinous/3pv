namespace Pppv.Net
{
   using System;
   using System.Collections;
   using System.Globalization;
   using System.IO;
   using System.Text;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   [Serializable()]
   [XmlRoot("pnml")]
   public class PetriNet : IXmlSerializable
   {
      [NonSerializedAttribute]
      private string id;
      [NonSerializedAttribute]
      private string type;
      [NonSerializedAttribute]
      private ArrayList places;
      [NonSerializedAttribute]
      private ArrayList transitions;
      [NonSerializedAttribute]
      private ArrayList arcs;
      [NonSerializedAttribute]
      private string additionalCode;

      public PetriNet()
      {
         this.Id = String.Empty;
         this.NetType = "PPr/T net";
         this.places = new ArrayList(30);
         this.transitions = new ArrayList(30);
         this.arcs = new ArrayList(60);
         this.additionalCode = String.Empty;
      }

      public string Id
      {
         get { return this.id; }
         set { this.id = value; }
      }

      public string NetType
      {
         get { return this.type; }
         private set { this.type = value; }
      }

      public ArrayList Places
      {
         get { return this.places; }
      }

      public ArrayList Transitions
      {
         get { return this.transitions; }
      }

      public ArrayList Arcs
      {
         get { return this.arcs; }
      }

      public string AdditionalCode
      {
         get { return this.additionalCode; }
         set { this.additionalCode = value; }
      }

      public void AddElement(INetElement element)
      {
         if (element is Place)
         {
            element.SetId(this.Places.Count);
            this.Places.Add(element);
         }

         if (element is Transition)
         {
            element.SetId(this.Transitions.Count);
            this.Transitions.Add(element);
         }

         if (element is Arc)
         {
            element.SetId(this.Arcs.Count);
            this.Arcs.Add(element);
         }

         element.ParentNet = this;
      }

      public void DeleteElement(INetElement element)
      {
         element.ParentNet = null;
         if (element is Place)
         {
            this.Places.Remove(element);
         }

         if (element is Transition)
         {
            this.Transitions.Remove(element);
         }

         if (element is Arc)
         {
            this.Arcs.Remove(element);
         }
      }

      public bool HaveArcBetween(INetElement startElement, INetElement endElement)
      {
         for (int i = 0; i < this.Arcs.Count; ++i)
         {
            if ((this.Arcs[i] as Arc).SourceId == startElement.Id && (this.Arcs[i] as Arc).TargetId == endElement.Id)
            {
               return true;
            }
         }

         return false;
      }

      public NetElement GetElementById(string searchingId)
      {
         if (String.IsNullOrEmpty(searchingId))
         {
            return null;
         }

         foreach (Place place in this.Places)
         {
            if (place.Id == searchingId)
            {
               return place;
            }
         }

         foreach (Transition transition in this.Transitions)
         {
            if (transition.Id == searchingId)
            {
               return transition;
            }
         }

         foreach (Arc arc in this.Arcs)
         {
            if (arc.Id == searchingId)
            {
               return arc;
            }
         }

         return null;
      }

      public void WriteXml(XmlWriter writer)
      {
         XmlSerializer placeSerealizer = new XmlSerializer(typeof(Place));
         XmlSerializer transitionSerealizer = new XmlSerializer(typeof(Transition));
         XmlSerializer arcSerealizer = new XmlSerializer(typeof(Arc));
         writer.WriteStartElement("net");
         writer.WriteAttributeString("id", this.Id);
         writer.WriteAttributeString("type", this.NetType);
         foreach (Place place in this.Places)
         {
            placeSerealizer.Serialize(writer, place);
         }

         foreach (Transition transition in this.Transitions)
         {
            transitionSerealizer.Serialize(writer, transition);
         }

         foreach (Arc arc in this.Arcs)
         {
            arcSerealizer.Serialize(writer, arc);
         }

         writer.WriteStartElement("additionalCode");
         writer.WriteString(this.AdditionalCode);
         writer.WriteEndElement(); // additionalCode
         writer.WriteEndElement(); // net
      }

      public void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.ReadStartElement("pnml");
         this.Id = reader.GetAttribute("id");
         this.NetType = reader.GetAttribute("type");

         if (!reader.IsEmptyElement)
         {
            reader.ReadStartElement("net");
            while (reader.NodeType != XmlNodeType.EndElement)
            {
               switch (reader.Name)
               {
                  case "place":
                     subTreeReader = reader.ReadSubtree();
                     Place place = new Place();
                     XmlSerializer placeSerealizer = new XmlSerializer(place.GetType());
                     place = placeSerealizer.Deserialize(subTreeReader) as Place;
                     this.AddElement(place);
                     subTreeReader.Close();
                     reader.Skip();
                     break;
                  case "transition":
                     subTreeReader = reader.ReadSubtree();
                     Transition transition = new Transition();
                     XmlSerializer transitionSerealizer = new XmlSerializer(transition.GetType());
                     transition = transitionSerealizer.Deserialize(subTreeReader) as Transition;
                     this.AddElement(transition);
                     subTreeReader.Close();
                     reader.Skip();
                     break;
                  case "arc":
                     subTreeReader = reader.ReadSubtree();
                     Arc arc = new Arc();
                     XmlSerializer arcSerealizer = new XmlSerializer(typeof(Arc));
                     arc = arcSerealizer.Deserialize(subTreeReader) as Arc;
                     this.AddElement(arc);
                     subTreeReader.Close();
                     reader.Skip();
                     break;
                  case "additionalCode":
                     if (!reader.IsEmptyElement)
                     {
                        reader.ReadStartElement("additionalCode");
                        /*Причину Replace см. Issue 27*/
                        this.AdditionalCode = reader.ReadString().Replace("\n", System.Environment.NewLine);
                        reader.ReadEndElement(); // additionalCode
                     }
                     else
                     {
                        reader.Skip();
                     }

                     break;
                  default:
                     reader.Read();
                     break;
               }
            }

            reader.ReadEndElement();
            reader.ReadEndElement();
         }
         else
         {
            reader.Skip();
            reader.ReadEndElement();
         }
      }

      public XmlSchema GetSchema()
      {
         return null;
      }
   }
}