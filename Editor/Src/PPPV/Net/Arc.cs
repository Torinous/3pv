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

   public class Arc : NetElement, IXmlSerializable
   {
      private NetElement source, target;
      private ArcType arcType;
      private PredicateList cortege;
      private ArrayList points;

      public Arc(ArcType type) : base(new Point(0, 0))
      {
         this.ArcType = type;
         this.points = new ArrayList(20);
         this.cortege = new PredicateList();
      }
      
      public Arc(NetElement startElement, ArcType type) : this(type)
      {
         this.Source = startElement;

         if (startElement != null)
         {
            this.ParentNet = startElement.ParentNet;
         }
      }

      public Arc(XmlReader reader, PetriNet net) : this((NetElement)null, ArcType.BaseArc)
      {
         ParentNet = net;
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
      }

      public NetElement Target
      {
         get
         {
            return this.target;
         }

         set
         {
            this.target = value;
            this.Id = this.MakeId();
         }
      }

      public NetElement Source
      {
         get
         {
            return this.source;
         }

         set
         {
            this.source = value;
            this.Id = this.MakeId();
         }
      }

      public bool Unfinished
      {
         get { return this.Target == null; }
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

      public override void PrepareToDeletion()
      {
         this.Source = null;
         this.Target = null;
         base.PrepareToDeletion();
      }

      public void WriteXml(XmlWriter writer)
      {
         int i = 0;
         writer.WriteAttributeString("id", this.Id);
         writer.WriteAttributeString("source", this.Source.Name);
         writer.WriteAttributeString("target", this.Target.Name);
         foreach (Pilon p in this.Points)
         {
            writer.WriteStartElement("arcpath");
            writer.WriteAttributeString("id",  String.Format(CultureInfo.CurrentCulture, "{0:000}", i));
            writer.WriteAttributeString("x", p.X.ToString(CultureInfo.CurrentCulture));
            writer.WriteAttributeString("y", p.Y.ToString(CultureInfo.CurrentCulture));
            writer.WriteAttributeString("curvePoint", "false");
            writer.WriteEndElement(); // arcpath
            i++;
         }

         writer.WriteStartElement("cortege");
         this.cortege.WriteXml(writer);
         writer.WriteEndElement(); // cortege
      }

      public void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.Read();
         reader.MoveToAttribute("id");
         reader.MoveToAttribute("source");
         this.Source = Parent.GetElementById(reader.Value);
         reader.MoveToAttribute("target");
         this.Target = Parent.GetElementById(reader.Value);
         reader.ReadStartElement(this.ArcTypeName);
         while (reader.NodeType != XmlNodeType.EndElement)
         {
            switch (reader.Name)
            {
               case "cortege":
                  subTreeReader = reader.ReadSubtree();
                  this.Cortege.ReadXml(subTreeReader);
                  subTreeReader.Close();
                  reader.Skip();
                  break;
               case "arcpath":
                  this.Points.Add(new Pilon(new Point(int.Parse(reader.GetAttribute("x"), CultureInfo.InvariantCulture), int.Parse(reader.GetAttribute("y"), CultureInfo.InvariantCulture))));
                  reader.MoveToAttribute("curvePoint");
                  reader.MoveToElement();
                  reader.Skip();
                  break;
               default:
                  reader.Read();
                  break;
            }
         }
      }

      public XmlSchema GetSchema()
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
         string source = String.Empty, target = String.Empty;
         if (this.Source != null)
         {
            source = this.Source.Id;
         }

         if (this.Target != null)
         {
            target = this.Target.Id;
         }

         return source + " to " + target;
      }
   }
}