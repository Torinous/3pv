namespace Pppv.Net
{
   using System;
   using System.Collections;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Globalization;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.Editor;
   using Pppv.Utils;

   [Serializable()]
   [XmlRoot("arc")]
   public class Arc : NetElement, IArc
   {
      private string sourceId, targetId;
      private ArcType arcType;
      private PredicatesList cortege;
      private ArrayList points;

      public Arc(ArcType type) : base(new Point(0, 0))
      {
         this.ArcType = type;
         this.points = new ArrayList(20);
         this.cortege = new PredicatesList();
      }

      public Arc() : this(ArcType.NormalArc)
      {
      }

      public Arc(string startElementId, ArcType type) : this(type)
      {
         this.SourceId = startElementId;
      }

      public Arc(INetElement startElement, ArcType type) : this(startElement.Id, type)
      {
      }

      public ArcType ArcType
      {
         get { return this.arcType; }
         set { this.arcType = value; }
      }

      public ArrayList Points
      {
         get { return this.points; }
      }

      public PredicatesList Cortege
      {
         get { return this.cortege; }
         protected set { this.cortege = value; }
      }

      public string TargetId
      {
         get
         {
            return this.targetId;
         }

         set
         {
            this.targetId = value;
            this.Id = this.MakeId();
         }
      }

      public string SourceId
      {
         get
         {
            return this.sourceId;
         }

         set
         {
            this.sourceId = value;
            this.Id = this.MakeId();
         }
      }

      public bool Unfinished
      {
         get { return String.IsNullOrEmpty(this.TargetId); }
      }

      public string ArcTypeName
      {
         get
         {
            if (this.ArcType == ArcType.NormalArc)
            {
               return "normal";
            }
            else
            {
               return "inhibitor";
            }
         }
      }

      public override void WriteXml(XmlWriter writer)
      {
         writer.WriteAttributeString("id", this.Id);
         writer.WriteAttributeString("source", this.SourceId);
         writer.WriteAttributeString("target", this.TargetId);
         /*foreach (Pilon p in this.Points) {
            writer.WriteStartElement("arcpath");
            writer.WriteAttributeString("id", String.Format(CultureInfo.CurrentCulture, "{0:000}", i));
            writer.WriteAttributeString("x", p.X.ToString(CultureInfo.CurrentCulture));
            writer.WriteAttributeString("y", p.Y.ToString(CultureInfo.CurrentCulture));
            writer.WriteAttributeString("curvePoint", "false");
            writer.WriteEndElement();
            // arcpath
            i++;
         }*/

         XmlSerializer cortegeSerealizer = new XmlSerializer(typeof(PredicatesList));
         cortegeSerealizer.Serialize(writer, this.Cortege);

         writer.WriteStartElement("type");
         writer.WriteAttributeString("value", this.ArcTypeName);
         writer.WriteEndElement(); // type
      }

      public override void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.MoveToAttribute("id");
         reader.MoveToAttribute("source");
         this.SourceId = reader.Value;
         reader.MoveToAttribute("target");
         this.TargetId = reader.Value;
         reader.ReadStartElement("arc");
         while (reader.NodeType != XmlNodeType.EndElement)
         {
            switch (reader.Name)
            {
               case "cortege":
                  subTreeReader = reader.ReadSubtree();
                  XmlSerializer serealizer = new XmlSerializer(typeof(PredicatesList));
                  this.Cortege = new PredicatesList();
                  this.Cortege = serealizer.Deserialize(subTreeReader) as PredicatesList;
                  subTreeReader.Close();
                  reader.Skip();
                  break;
               /*case "arcpath":
                  this.Points.Add(new Pilon(new Point(int.Parse(reader.GetAttribute("x"), CultureInfo.InvariantCulture), int.Parse(reader.GetAttribute("y"), CultureInfo.InvariantCulture))));
                  reader.MoveToAttribute("curvePoint");
                  reader.MoveToElement();
                  reader.Skip();
                  break;*/
                case "type":
                  reader.MoveToAttribute("value");
                  if (reader.Value == "normal")
                  {
                     this.ArcType = ArcType.NormalArc;
                  }
                  else
                  {
                     this.ArcType = ArcType.InhibitorArc;
                  }
                  reader.MoveToElement();
                  reader.Skip();
                  break;
               default:
                  reader.Read();
                  break;
            }
         }
      }

      public override XmlSchema GetSchema()
      {
         return null;
      }

      public override void SetId(int number)
      {
         if (String.IsNullOrEmpty(this.Id))
         {
            this.Id = this.MakeId();
         }
      }

      /*private void AddPoint(Pilon p)
      {
         Points.Add(p);
         p.Move += OneOfPointMoveHandler;
         OnChange(new EventArgs());
      }

      private void DeletePoint(Pilon p)
      {
         Points.Remove(p);
         p.Move -= OneOfPointMoveHandler;
         OnChange(new EventArgs());
      }*/

      private string MakeId()
      {
         return this.SourceId + " to " + this.TargetId;
      }
   }
}
