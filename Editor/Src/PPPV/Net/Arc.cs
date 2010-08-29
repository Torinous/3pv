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
      private Point sourcePilon, targetPilon;
      private PredicateList cortege;
      private ArrayList points;

      public Arc(NetElement startElement) : base(new Point(0, 0))
      {
         this.source = startElement;
         if (this.source != null)
         {
            this.targetPilon = this.source.Center;
         }

         this.points = new ArrayList(20);
         this.cortege = new PredicateList();
         this.cortege.Change += this.CortegeChangeHandler;
      }

      public Arc(XmlReader reader, PetriNet net) : this((NetElement)null)
      {
         ParentNet = net;
         this.ReadXml(reader);
      }

      public ArrayList Points
      {
         get { return this.points; }
      }

      public PredicateList Cortege
      {
         get { return this.cortege; }
      }

      public Point TargetPilon
      {
         get
         {
            return this.targetPilon;
         }

         set
         {
            if (this.Target == null)
            {
               this.targetPilon = value;
               this.UpdateHitRegion();
               this.OnChange(new EventArgs());
            }
         }
      }

      public NetElement Target
      {
         get
         {
            return this.target;
         }

         set
         {
            if (this.target != null)
            {
               this.target.Move -= this.MoveHandler;
               this.target.Resize -= this.ResizeLinkedElementsHandler;
            }

            this.target = value;

            if (this.target != null)
            {
               this.UpdatePosition();
               this.target.Move += this.MoveHandler;
               this.target.Resize += this.ResizeLinkedElementsHandler;
            }

            this.Id = this.MakeId();
            OnChange(new EventArgs());
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
            if (this.source != null)
            {
               this.source.Move -= this.MoveHandler;
               this.source.Resize -= this.ResizeLinkedElementsHandler;
            }

            this.source = value;

            if (this.source != null)
            {
               this.sourcePilon = this.source.Center;
               this.source.Move += this.MoveHandler;
               this.source.Resize += this.ResizeLinkedElementsHandler;
            }

            this.Id = this.MakeId();
            OnChange(new EventArgs());
         }
      }

      public bool Unfinished
      {
         get { return this.target == null; }
      }

      public override Point Center
      {
         get
         {
            if (this.Points.Count == 0)
            {
               return new Point((this.sourcePilon.X + this.targetPilon.X) / 2, (this.sourcePilon.Y + this.targetPilon.Y) / 2);
            }
            else
            {
               if (this.Points.Count % 2 == 1)
               {
                  Pilon p1 = (Pilon)this.Points[((this.Points.Count + 1) / 2) - 1];
                  return new Point(p1.X, p1.Y);
               }
               else
               {
                  Pilon p1, p2;
                  p1 = (Pilon)this.Points[(this.Points.Count / 2) - 1];
                  p2 = (Pilon)this.Points[((this.Points.Count / 2) + 1) - 1];
                  return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
               }
            }
         }
      }

      private Point SourcePilon
      {
         get { return this.sourcePilon; }
      }

      public override void Draw(PaintEventArgs e)
      {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;

         Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0));
         SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
         Font font1 = new Font(new FontFamily("Arial"), 16, FontStyle.Regular, GraphicsUnit.Pixel);

         if (this.Points.Count == 0)
         {
            dc.DrawLine(this.PenFactory(), this.sourcePilon, this.targetPilon);
         }
         else
         {
            dc.DrawLine(blackPen, this.sourcePilon, (this.Points[0] as Pilon).Location);
            for (int i = 1; i < this.Points.Count; ++i)
            {
               dc.DrawLine(blackPen, (this.Points[i - 1] as Pilon).Location, (this.Points[i] as Pilon).Location);
            }

            dc.DrawLine(this.PenFactory(), (this.Points[this.Points.Count - 1] as Pilon).Location, this.targetPilon);
         }

         dc.DrawString(this.Cortege.Text, font1, blackBrush, this.Center.X, this.Center.Y - 15);
         base.Draw(e);
      }

      /*Чисто фиктивно, просто чтобы реализовать абстрактный член*/
      public override Point GetPilon(Point from)
      {
         return this.Center;
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
         reader.ReadStartElement("arc");
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

      protected virtual Pen PenFactory()
      {
         Pen p = new Pen(Color.Black, 1);
         GraphicsPath capPath = new GraphicsPath();
         capPath.AddLine(new Point(4, -7), new Point(0, 0));
         capPath.AddLine(new Point(-4, -7), new Point(0, 0));
         CustomLineCap arrowCap = new CustomLineCap(null, capPath);
         arrowCap.SetStrokeCaps(LineCap.Triangle, LineCap.Triangle);
         p.CustomEndCap = arrowCap;
         return p;
      }

      protected override void UpdateHitRegion()
      {
         using (PreciseTimer pr = new PreciseTimer("Arc.UpdateRegion"))
         {
            if (!this.Unfinished)
            {
               HitRegion.MakeEmpty();
               GraphicsPath tmpPath = new GraphicsPath();

               Point lastPoint = this.SourcePilon;
               foreach (Pilon p in this.Points)
               {
                  tmpPath.AddLine(lastPoint.X, lastPoint.Y, p.X, p.Y);
                  lastPoint = p.Location;
               }

               tmpPath.AddLine(lastPoint.X, lastPoint.Y, this.TargetPilon.X, this.TargetPilon.Y);

               tmpPath.Widen(new Pen(Color.Red, 4));

               HitRegion.Union(tmpPath);
            }
         }
      }

      private void MoveHandler(object sender, MoveEventArgs args)
      {
         this.UpdatePosition();
         OnChange(new EventArgs());
      }

      /*private void OneOfPointMoveHandler(object sender, MoveEventArgs args)
      {
         this.UpdatePosition();
         OnChange(new EventArgs());
      }*/

      private void ResizeLinkedElementsHandler(object sender, ResizeEventArgs args)
      {
         this.UpdatePosition();
         OnChange(new EventArgs());
      }

      private void UpdatePosition()
      {
         // this.UpdateHitRegion();
         if (this.Points.Count == 0)
         {
            this.sourcePilon = this.source.GetPilon(this.target.Center);
         }
         else
         {
            this.sourcePilon = this.source.GetPilon((this.Points[0] as Pilon).Center);
         }

         if (this.Points.Count == 0)
         {
            this.targetPilon = this.target.GetPilon(this.source.Center);
         }
         else
         {
            this.targetPilon = this.target.GetPilon((this.Points[this.Points.Count - 1] as Pilon).Center);
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

      private void CortegeChangeHandler(object sender, EventArgs args)
      {
         OnChange(args);
      }

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