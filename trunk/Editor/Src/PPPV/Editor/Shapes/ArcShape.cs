/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.09.2010
 * Time: 21:38
 */

namespace Pppv.Editor.Shapes
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   public class ArcShape : Arc, IShape
   {
      private Point location;
      private Size size;
      private Region hitRegion;
      private Arc baseArc;
      private Point sourceConnectPoint, targetConnectPoint;
      private PetriNetGraphical parentNet;

      public ArcShape(Arc arc, PetriNetGraphical parentNet) : base(ArcType.BaseArc)
      {
         this.baseArc = arc;
         this.HitRegion = new Region();
         this.ParentNet = parentNet;
      }

      public event EventHandler<MoveEventArgs> Move;

      public event PaintEventHandler Paint;

      public event EventHandler Change;

      public Point Location
      {
         get { return this.location; }
         set { this.location = value; }
      }

      public Size Size
      {
         get { return this.size; }
         set { this.size = value; }
      }

      public Region HitRegion
      {
         get { return this.hitRegion; }
         set { this.hitRegion = value; }
      }

      public NetElement BaseElement
      {
         get { return this.baseArc; }
      }

      public new PetriNetGraphical ParentNet
      {
         get { return this.parentNet; }
         set { this.parentNet = value; }
      }

      public new NetElement Target
      {
         get { return this.baseArc.Target; }
         set { this.baseArc.Target = value; }
      }

      public new NetElement Source
      {
         get { return this.baseArc.Source; }
         set { this.baseArc.Source = value; }
      }

      public new ArcType ArcType
      {
         get { return this.baseArc.ArcType; }
         set { this.baseArc.ArcType = value; }
      }

      public Point Center
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

      public void MoveBy(Point radiusVector)
      {
         Point old = new Point(this.location.X, this.location.Y);
         this.Location = new Point(this.X + radiusVector.X, this.Y + radiusVector.Y);
         this.OnMove(new MoveEventArgs(old, this.Location));
         this.OnChange(new EventArgs());
      }

      public bool Intersect(Point point)
      {
         return this.HitRegion.IsVisible(point);
      }

      public bool Intersect(Rectangle rectangle)
      {
         return this.HitRegion.IsVisible(rectangle);
      }

      public bool Intersect(Region region)
      {
         /*Region tmp = new Region(HitRegion);
         tmp.Intersect(_region);
         return tmp.IsEmpty;*/
         return false;
      }

      /*Чисто фиктивно, просто чтобы реализовать абстрактный член*/
      public Point GetConnectPoint(Point from)
      {
         return this.Center;
      }

      public void Draw(PaintEventArgs e)
      {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;
         this.UpdateConnectPoints();

         // Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0));
         SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
         Font font1 = new Font(new FontFamily("Arial"), 16, FontStyle.Regular, GraphicsUnit.Pixel);

         if (this.Points.Count == 0)
         {
            dc.DrawLine(this.PenFactory(this.DeterminePenCapPlace()), this.SourceConnectPoint, this.TargetConnectPoint);
         }
         else
         {
            /*dc.DrawLine(blackPen, this.sourceConnectPoint, (this.Points[0] as Pilon).Location);
            for (int i = 1; i < this.Points.Count; ++i)
            {
               dc.DrawLine(blackPen, (this.Points[i - 1] as Pilon).Location, (this.Points[i] as Pilon).Location);
            }

            dc.DrawLine(this.PenFactory(this.DeterminePenCapPlace()), (this.Points[this.Points.Count - 1] as Pilon).Location, this.targetConnectPoint);*/
         }

         dc.DrawString(this.Cortege.Text, font1, blackBrush, this.Center.X, this.Center.Y - 15);
         this.OnPaint(new PaintEventArgs(e.Graphics, e.ClipRectangle));
      }

      public Point GetConnectPoint(Point from, NetCanvas onCanvas)
      {
         Graphics g;
         Point pilon = new Point();
         if (onCanvas != null)
         {
            g = onCanvas.CreateGraphics();
            Region reg = new Region();
            reg = this.HitRegion.Clone();
            Pen greenPen = new Pen(Color.Black, 1);
            GraphicsPath gp = new GraphicsPath();
            Rectangle rect = new Rectangle();

            /*Если не посчитается, просто вернём центр*/
            pilon.X = this.Center.X;
            pilon.Y = this.Center.Y;

            if (from != this.Center)
            {
               gp.AddLine(from, this.Center);
               gp.Widen(greenPen);
               reg.Intersect(gp);
               RectangleF bounds = reg.GetBounds(g);
               rect = Rectangle.Ceiling(bounds);
               if (from.X <= this.Center.X)
               {
                  if (from.Y <= this.Center.Y)
                  {
                     pilon.X = rect.Left;
                     pilon.Y = rect.Top;
                  }
                  else
                  {
                     pilon.X = rect.Left;
                     pilon.Y = rect.Bottom;
                  }
               }
               else
               {
                  if (from.Y <= this.Center.Y)
                  {
                     pilon.X = rect.Right;
                     pilon.Y = rect.Top;
                  }
                  else
                  {
                     pilon.X = rect.Right;
                     pilon.Y = rect.Bottom;
                  }
               }

               g.Dispose();
            }
         }
         else
         {
            pilon.X = this.Center.X;
            pilon.Y = this.Center.Y;
         }

         return pilon;
      }

      public void UpdateConnectPoints()
      {
         Point mousePositionOnCanvas = this.ParentNet.Canvas.PointToClient(System.Windows.Forms.Control.MousePosition);
         IShape sourceShape = this.ParentNet.GetShapeForElement(this.Source);
         IShape targetShape = this.ParentNet.GetShapeForElement(this.Target);
         
         if (this.Points.Count == 0)
         {
            if (this.Target != null)
            {
               this.SourceConnectPoint = sourceShape.GetConnectPoint(targetShape.Center);
            }
            else
            {
               this.SourceConnectPoint = sourceShape.GetConnectPoint(mousePositionOnCanvas);
            }
         }
         else
         {
            this.SourceConnectPoint = sourceShape.GetConnectPoint((this.Points[0] as Pilon).Center);
         }

         if (this.Points.Count == 0)
         {
            if (this.Target != null)
            {
               this.TargetConnectPoint = targetShape.GetConnectPoint(sourceShape.Center);
            }
            else
            {
               this.TargetConnectPoint = mousePositionOnCanvas;
            }
         }
         else
         {
            this.TargetConnectPoint = targetShape.GetConnectPoint((this.Points[this.Points.Count - 1] as Pilon).Center);
         }

         this.UpdateHitRegion();
      }

      /*protected override void OnParentNetChange(ParentNetChangeEventArgs e)
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
      }*/

      public void UpdateHitRegion()
      {
         using (PreciseTimer pr = new PreciseTimer("Arc.UpdateRegion"))
         {
            this.HitRegion.MakeEmpty();
            GraphicsPath tmpPath = new GraphicsPath();

            Point lastPoint = this.SourceConnectPoint;
            /*if (this.Points != null)
            {
               foreach (Pilon p in this.Points)
               {
                  tmpPath.AddLine(lastPoint.X, lastPoint.Y, p.X, p.Y);
                  lastPoint = p.Location;
               }
            }*/

            if (lastPoint != this.TargetConnectPoint)
            {
               tmpPath.AddLine(lastPoint.X, lastPoint.Y, this.TargetConnectPoint.X, this.TargetConnectPoint.Y);

               tmpPath.Widen(new Pen(Color.Red, 4));
            }

            this.HitRegion.Union(tmpPath);
         }
      }

      public void ParentNetDrawHandler(object sender, PaintEventArgs e)
      {
         this.Draw(e);
      }

      protected static CustomLineCap ArrowCapFabric()
      {
         GraphicsPath capPath = new GraphicsPath();
         capPath.AddLine(new Point(4, -7), new Point(0, 0));
         capPath.AddLine(new Point(-4, -7), new Point(0, 0));
         CustomLineCap arrowCap = new CustomLineCap(null, capPath);
         return arrowCap;
      }

      protected static CustomLineCap RoundCapFabric()
      {
         GraphicsPath capPath = new GraphicsPath();
         capPath.AddEllipse(-4, -8, 8, 8);
         GraphicsPath capPath2 = new GraphicsPath();
         capPath2.AddLine(new Point(0, -8), new Point(0, 0));
         CustomLineCap roundCap = new CustomLineCap(null, capPath);
         return roundCap;
      }

      protected static PositionOnArc DeterminePenCapPlaceForBaseArc()
      {
         return PositionOnArc.End;
      }

      protected void OnPaint(PaintEventArgs e)
      {
         if (this.Paint != null)
         {
            this.Paint(this, e);
         }
      }

      protected void OnMove(MoveEventArgs args)
      {
         this.UpdateHitRegion();
         if (this.Move != null)
         {
            this.Move(this, args);
         }

         this.OnChange(new EventArgs());
      }

      protected void OnChange(EventArgs args)
      {
         if (this.Change != null)
         {
            this.Change(this, args);
         }
      }

      protected Pen PenFactory(PositionOnArc penCapPlace)
      {
         Pen p = new Pen(Color.Black, 1);
         if (this.ArcType == ArcType.BaseArc)
         {
            CustomLineCap arrowCap = ArrowCapFabric();
            arrowCap.SetStrokeCaps(LineCap.Triangle, LineCap.Triangle);
            p.CustomEndCap = arrowCap;
         }
         else
         {
            CustomLineCap roundCap = RoundCapFabric();
            if (penCapPlace == PositionOnArc.End)
            {
               p.CustomEndCap = roundCap;
            }
            else
            {
               p.CustomStartCap = roundCap;
            }
         }

         return p;
      }

      protected PositionOnArc DeterminePenCapPlace()
      {
         if (this.ArcType == ArcType.BaseArc)
         {
            return DeterminePenCapPlaceForBaseArc();
         }
         else
         {
            return this.DeterminePenCapPlaceForInhibitorArc();
         }
      }

      protected PositionOnArc DeterminePenCapPlaceForInhibitorArc()
      {
         if (this.Source is Transition)
         {
            return PositionOnArc.Start;
         }
         else
         {
            return PositionOnArc.End;
         }
      }
   }
}