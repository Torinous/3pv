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

   public class Arc : NetElement, IArc
   {
      private string sourceId, targetId;
      private ArcType arcType;
      private PredicateList cortege;
      private ArrayList points;

      public Arc(ArcType type) : base(new Point(0, 0))
      {
         this.ArcType = type;
         this.points = new ArrayList(20);
         this.cortege = new PredicateList();
      }

      public Arc() : this(ArcType.BaseArc)
      {
      }

      public Arc(string startElementId, ArcType type) : this(type)
      {
         this.SourceId = startElementId;
      }

      public Arc(INetElement startElement, ArcType type) : this(startElement.Id, type)
      {
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Не смертельно")]
      public Arc(XmlReader reader, ArcType type) : this(type)
      {
         this.ReadXml(reader);
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

      public PredicateList Cortege
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
            if (this.ArcType == ArcType.BaseArc)
            {
               return "arc";
            }
            else
            {
               return "inhibitorArc";
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

         writer.WriteStartElement("cortege");
         this.cortege.WriteXml(writer);
         writer.WriteEndElement(); // cortege
      }

      public override void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.Read();
         reader.MoveToAttribute("id");
         reader.MoveToAttribute("source");
         this.SourceId = reader.Value;
         reader.MoveToAttribute("target");
         this.TargetId = reader.Value;
         reader.ReadStartElement(this.ArcTypeName);
         while (reader.NodeType != XmlNodeType.EndElement)
         {
            switch (reader.Name)
            {
               case "cortege":
                  subTreeReader = reader.ReadSubtree();
                  this.Cortege = new PredicateList(subTreeReader);
                  subTreeReader.Close();
                  reader.Skip();
                  break;
               /*case "arcpath":
                  this.Points.Add(new Pilon(new Point(int.Parse(reader.GetAttribute("x"), CultureInfo.InvariantCulture), int.Parse(reader.GetAttribute("y"), CultureInfo.InvariantCulture))));
                  reader.MoveToAttribute("curvePoint");
                  reader.MoveToElement();
                  reader.Skip();
                  break;*/
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
