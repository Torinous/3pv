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
      private Point sourceConnectPoint, targetConnectPoint;
      private PredicateList cortege;
      private ArrayList points;

      public Arc() : base(new Point(0, 0))
      {
         this.points = new ArrayList(20);
         this.cortege = new PredicateList();
         this.cortege.Change += this.CortegeChangeHandler;
      }
      
      public Arc(NetElement startElement) : this()
      {
         this.Source = startElement;
         if (this.source != null)
         {
            this.targetConnectPoint = this.source.Center;
         }

         if (startElement != null)
         {
            this.ParentNet = startElement.ParentNet;
         }
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Не смертельно")]
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
               // this.UpdatePosition();
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
               this.sourceConnectPoint = this.source.Center;
               this.source.Move += this.MoveHandler;
               this.source.Resize += this.ResizeLinkedElementsHandler;
            }

            this.Id = this.MakeId();
            OnChange(new EventArgs());
         }
      }

      public bool Unfinished
      {
         get { return this.Target == null; }
      }

      public override Point Center
      {
         get
         {
            if (this.Points.Count == 0)
            {
               return new Point((this.sourceConnectPoint.X + this.targetConnectPoint.X) / 2, (this.sourceConnectPoint.Y + this.targetConnectPoint.Y) / 2);
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

      public virtual string ArcType
      {
         get { return "arc"; }
      }

      private Point SourceConnectPoint
      {
         get { return this.sourceConnectPoint; }
         set { this.sourceConnectPoint = value; }
      }

      private Point TargetConnectPoint
      {
         get { return this.targetConnectPoint; }
         set { this.targetConnectPoint = value; }
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
            dc.DrawLine(this.PenFactory(this.DeterminePenCapPlace()), this.SourceConnectPoint, this.TargetConnectPoint);
         }
         else
         {
            dc.DrawLine(blackPen, this.sourceConnectPoint, (this.Points[0] as Pilon).Location);
            for (int i = 1; i < this.Points.Count; ++i)
            {
               dc.DrawLine(blackPen, (this.Points[i - 1] as Pilon).Location, (this.Points[i] as Pilon).Location);
            }

            dc.DrawLine(this.PenFactory(this.DeterminePenCapPlace()), (this.Points[this.Points.Count - 1] as Pilon).Location, this.targetConnectPoint);
         }

         dc.DrawString(this.Cortege.Text, font1, blackBrush, this.Center.X, this.Center.Y - 15);
         base.Draw(e);
      }

      /*Чисто фиктивно, просто чтобы реализовать абстрактный член*/
      public override Point GetConnectPoint(Point from)
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
         reader.ReadStartElement(this.ArcType);
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

      public void UpdateConnectPoints()
      {
         Point mousePositionOnCanvas = this.ParentNet.Canvas.PointToClient(System.Windows.Forms.Control.MousePosition);
         
         if (this.Points.Count == 0)
         {
            if (this.Target != null)
            {
               this.SourceConnectPoint = this.source.GetConnectPoint(this.target.Center);
            }
            else
            {
               this.SourceConnectPoint = this.source.GetConnectPoint(mousePositionOnCanvas);
            }
         }
         else
         {
            this.SourceConnectPoint = this.source.GetConnectPoint((this.Points[0] as Pilon).Center);
         }

         if (this.Points.Count == 0)
         {
            if (this.Target != null)
            {
               this.TargetConnectPoint = this.target.GetConnectPoint(this.source.Center);
            }
            else
            {
               this.TargetConnectPoint = mousePositionOnCanvas;
            }
         }
         else
         {
            this.TargetConnectPoint = this.target.GetConnectPoint((this.Points[this.Points.Count - 1] as Pilon).Center);
         }

         this.UpdateHitRegion();
      }

      protected virtual Pen PenFactory(PenCapPlace penCapPlace)
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

               Point lastPoint = this.SourceConnectPoint;
               foreach (Pilon p in this.Points)
               {
                  tmpPath.AddLine(lastPoint.X, lastPoint.Y, p.X, p.Y);
                  lastPoint = p.Location;
               }

               tmpPath.AddLine(lastPoint.X, lastPoint.Y, this.TargetConnectPoint.X, this.TargetConnectPoint.Y);

               tmpPath.Widen(new Pen(Color.Red, 4));

               HitRegion.Union(tmpPath);
            }
         }
      }

      protected virtual PenCapPlace DeterminePenCapPlace()
      {
         return PenCapPlace.End;
      }

      protected override void OnParentNetChange(ParentNetChangeEventArgs e)
      {
         if (e.OldParentNet != null)
         {
            e.OldParentNet.CanvasChange -= this.NetCanvasChangeHandler;
         }

         if (e.NewParentNet != null)
         {
            e.NewParentNet.CanvasChange += this.NetCanvasChangeHandler;
         }

         base.OnParentNetChange(e);
      }

      private void MoveHandler(object sender, MoveEventArgs args)
      {
         this.UpdateConnectPoints();
         OnChange(new EventArgs());
      }

      /*private void OneOfPointMoveHandler(object sender, MoveEventArgs args)
      {
         this.UpdatePosition();
         OnChange(new EventArgs());
      }*/

      private void ResizeLinkedElementsHandler(object sender, ResizeEventArgs args)
      {
         this.UpdateConnectPoints();
         OnChange(new EventArgs());
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

      private void NetCanvasChangeHandler(object sender, EventArgs args)
      {
         this.UpdateConnectPoints();
      }
   }
}