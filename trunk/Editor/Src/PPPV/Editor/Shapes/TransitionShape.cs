/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 25.09.2010
 * Time: 2:30
 */

namespace Pppv.Editor.Shapes
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   public class TransitionShape : Transition, IShape
   {
      private Point location;
      private Size size;
      private Region hitRegion;
      private Transition baseTransition;
      private PetriNetGraphical parentNet;

      public TransitionShape(Transition transition, PetriNetGraphical parentNet)
      {
         this.baseTransition = transition;
         this.ParentNet = parentNet;
         this.HitRegion = new Region();
         this.Size = new Size(20, 50);
         this.UpdateHitRegion();
      }

      public event EventHandler<MoveEventArgs> Move;

      public event PaintEventHandler Paint;

      public event EventHandler Change;

      public Point Location
      {
         get { return new Point(this.BaseElement.X, this.BaseElement.Y); }
      }

      public new int X
      {
         get { return this.BaseElement.X; }
         set { this.BaseElement.X = value; }
      }

      public new int Y
      {
         get { return this.BaseElement.Y; }
         set { this.BaseElement.Y = value; }
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

      public new PetriNetGraphical ParentNet
      {
         get { return this.parentNet; }
         set { this.parentNet = value; }
      }

      public NetElement BaseElement
      {
         get { return this.baseTransition; }
      }

      public new string Name
      {
         get { return this.BaseElement.Name; }
         set { this.BaseElement.Name = value; }
      }

      public Point Center
      {
         get
         {
            return new Point(this.X + (int)(Size.Width / 2), this.Y + (int)(Size.Height / 2));
         }
      }

      public void MoveBy(Point radiusVector)
      {
         Point old = new Point(this.location.X, this.location.Y);
         this.X = this.X + radiusVector.X;
         this.Y = this.Y + radiusVector.Y;
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

      public void Draw(PaintEventArgs e)
      {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;
         Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0));
         SolidBrush grayBrush = new SolidBrush(Color.FromArgb(200, 100, 100, 100));
         SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
         Font font1 = new Font(new FontFamily("Arial"), 16, FontStyle.Regular, GraphicsUnit.Pixel);

         Region fillRegion = new Region(new Rectangle(this.X, this.Y, Size.Width, Size.Height));
         dc.FillRegion(grayBrush, fillRegion);
         dc.DrawRectangle(blackPen, this.X, this.Y, Size.Width, Size.Height);
         dc.DrawString(this.Name + "\n" + this.GuardFunction, font1, blackBrush, this.X + 20, this.Y - 17);
         this.OnPaint(new PaintEventArgs(e.Graphics, e.ClipRectangle));
      }

      public Point GetConnectPoint(Point from)
      {
         return this.GetConnectPoint(from, this.ParentNet.Canvas);
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

      public void UpdateHitRegion()
      {
         using (PreciseTimer pr = new PreciseTimer("Transition.UpdateRegion"))
         {
            this.HitRegion.MakeEmpty();
            this.HitRegion.Union(new Rectangle(this.X, this.Y, Size.Width, Size.Height));
         }
      }

      public void ParentNetDrawHandler(object sender, PaintEventArgs e)
      {
         this.Draw(e);
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

      protected void OnPaint(PaintEventArgs e)
      {
         if (this.Paint != null)
         {
            this.Paint(this, e);
         }
      }
   }
}
