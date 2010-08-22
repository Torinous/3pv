namespace PPPV.Net
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

   using PPPV.Editor;
   using PPPV.Utils;


   public class Arc : NetElement, IXmlSerializable
   {
      private NetElement source, target;
      private Point sourcePilon, targetPilon;
      private PredicateList cortege;
      private ArrayList points;

      public Arc(NetElement startElement):base(new Point(0,0))
      {
         source = startElement;
         if(source != null)
            targetPilon = source.Center;
         points = new ArrayList(20);
         cortege = new PredicateList(10);
         cortege.Change += CortegeChangeHandler;
      }

      public Arc(XmlReader reader, PetriNet net):this((NetElement)null)
      {
         ParentNet = net;
         this.ReadXml(reader);
      }

      public override string Id
      {
         get
         {
            string source_="", target_="";
            if(Source != null)
               source_ = Source.Id;
            if(Target != null)
               target_ = Target.Id;
            return source_+" to "+target_;
         }
      }

      public ArrayList Points{
         get{
            return points;
         }
      }

      public PredicateList Cortege {
         get { return cortege; }
      }

      private Point SourcePilon {
         get {
            return sourcePilon;
         }
         /*set{
            sourcePilon = value;
            UpdateHitRegion();
            OnChange(new EventArgs());
         }*/
      }

      public Point TargetPilon{
         get{
            return targetPilon;
         }
         set{
            if(Target == null)
            {
               targetPilon = value;
               UpdateHitRegion();
               OnChange(new EventArgs());
            }
         }
      }

      public NetElement Target
      {
         get{
            return target;
         }
         set
         {
            if(target != null)
            {
               target.Move -= MoveHandler;
               target.Resize -= ResizeLinkedElementsHandler;
            }
            target = value;
            if(target != null)
            {
               UpdatePosition();
               target.Move += MoveHandler;
               target.Resize += ResizeLinkedElementsHandler;
            }
            OnChange(new EventArgs());
         }
      }

      public NetElement Source{
         get{
            return source;
         }
         set{
            if(source != null)
            {
               source.Move -= MoveHandler;
               source.Resize -= ResizeLinkedElementsHandler;
            }
            source = value;
            if(source != null)
            {
               sourcePilon = source.Center;
               source.Move += MoveHandler;
               source.Resize += ResizeLinkedElementsHandler;
            }
            OnChange(new EventArgs());
         }
      }

      public bool Unfinished{
         get{
            return target == null;
         }
      }

      public override Point Center{
         get{
            if(Points.Count == 0)
            {
               return new Point((sourcePilon.X+targetPilon.X)/2,(sourcePilon.Y+targetPilon.Y)/2);
            }
            else
            {
               if(Points.Count%2 == 1)
               {
                  Pilon p1 = (Pilon)Points[( (Points.Count+1)/2)-1 ];
                  return new Point(p1.X, p1.Y);
               }
               else
               {
                  Pilon p1,p2;
                  p1 = (Pilon)Points[( (Points.Count)/2)-1 ];
                  p2 = (Pilon)Points[( (Points.Count)/2+1)-1 ];
                  return new Point((p1.X + p2.X)/2, (p1.Y + p2.Y)/2);
               }
            }
         }
      }

      public override Size Size
      {
         get
         {
            return new Size( Math.Abs(sourcePilon.X - targetPilon.X), Math.Abs(sourcePilon.Y - targetPilon.Y));
         }
      }

      public override void Draw(object sender, PaintEventArgs e)
      {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;

         Pen blackPen = new Pen(Color.FromArgb(255,0,0,0));
         SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200,0,0,0));
         Font font1 = new Font(new FontFamily("Arial"), 16, FontStyle.Regular, GraphicsUnit.Pixel);

         if( Points.Count == 0 )
         {
            dc.DrawLine(PenFactory(), sourcePilon, targetPilon);
         }
         else
         {
            dc.DrawLine(blackPen, sourcePilon,(Points[0] as Pilon).Location);
            for(int i = 1;i < Points.Count; ++i)
            {
               dc.DrawLine(blackPen, (Points[i-1] as Pilon).Location, (Points[i] as Pilon).Location);
            }
            dc.DrawLine(PenFactory(), (Points[Points.Count-1] as Pilon).Location, targetPilon);
         }
         dc.DrawString(Cortege.Text, font1, blackBrush, Center.X, Center.Y-15);
      }

      private void MoveHandler(object sender, MoveEventArgs args)
      {
         UpdatePosition();
         OnChange(new EventArgs());
      }

      private void OneOfPointMoveHandler(object sender, MoveEventArgs args)
      {
         UpdatePosition();
         OnChange(new EventArgs());
      }

      private void ResizeLinkedElementsHandler(object sender, ResizeEventArgs args)
      {
         UpdatePosition();
         OnChange(new EventArgs());
      }

      private void UpdatePosition(){
         UpdateHitRegion();
         if(Points.Count == 0)
            sourcePilon = source.GetPilon(target.Center);
         else
            sourcePilon = source.GetPilon((Points[0] as Pilon).Center);

         if(Points.Count == 0)
            targetPilon = target.GetPilon(source.Center);
         else
            targetPilon = target.GetPilon((Points[Points.Count-1] as Pilon).Center);
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

      protected override void UpdateHitRegion()
      {
         using(PreciseTimer pr = new PreciseTimer("Arc.UpdateRegion"))
         {
            if(!Unfinished)
            {
               HitRegion.MakeEmpty();
               GraphicsPath tmpPath = new GraphicsPath();

               Point lastPoint = this.SourcePilon;
               foreach(Pilon p in Points){
                  tmpPath.AddLine(lastPoint.X, lastPoint.Y, p.X, p.Y);
                  lastPoint = p.Location;
               }
               tmpPath.AddLine(lastPoint.X, lastPoint.Y, TargetPilon.X, TargetPilon.Y);

               tmpPath.Widen(new Pen(Color.Red, 4));

               HitRegion.Union(tmpPath);
            }
         }
      }

      /*Чисто фиктивно, просто чтобы реализовать абстрактный член*/
      public override Point GetPilon(Point from)
      {
         return Center;
      }

      public override void PrepareToDeletion()
      {
         Source = null;
         Target = null;
         base.PrepareToDeletion();
      }

      private void CortegeChangeHandler(object sender, EventArgs args)
      {
         OnChange(args);
      }

      public void WriteXml (XmlWriter writer)
      {
         int i = 0;
         writer.WriteAttributeString("id",this.Id);
         writer.WriteAttributeString("source", this.Source.Name);
         writer.WriteAttributeString("target", this.Target.Name);
         foreach(Pilon p in Points){
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

      public void ReadXml (XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.Read();
         reader.MoveToAttribute("id");
         reader.MoveToAttribute("source");
         this.Source = Parent.GetElementById(reader.Value);
         reader.MoveToAttribute("target");
         this.Target = Parent.GetElementById(reader.Value);
         reader.ReadStartElement("arc");
         while(reader.NodeType != XmlNodeType.EndElement)
         {
            switch(reader.Name)
            {
               case "cortege":
                  subTreeReader = reader.ReadSubtree();
                  Cortege.ReadXml(subTreeReader);
                  subTreeReader.Close();
                  reader.Skip();
                  break;
               case "arcpath":
                  Points.Add(new Pilon( new Point(int.Parse(reader.GetAttribute("x")),int.Parse(reader.GetAttribute("y"),CultureInfo.InvariantCulture))));
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
         return(null);
      }

      protected virtual Pen PenFactory()
      {
         Pen p = new Pen(Color.Black,1);
         GraphicsPath hPath = new GraphicsPath();
         hPath.AddLine(new Point(4, -7), new Point(0, 0));
         hPath.AddLine(new Point(-4, -7), new Point(0, 0));
         CustomLineCap ArrowCap = new CustomLineCap(null, hPath);
         ArrowCap.SetStrokeCaps(LineCap.Triangle, LineCap.Triangle);
         p.CustomEndCap = ArrowCap;
         return p;
      }
   } // Arc
} // namespace
