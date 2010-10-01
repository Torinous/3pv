/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 25.09.2010
 * Time: 2:50
 */

namespace Pppv.Editor.Shapes
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Globalization;
   using System.Windows.Forms;

   using Pppv.Editor;
   using Pppv.Net;
   using Pppv.Utils;

   public class PlaceShape : Place, IShape
   {
      private Size size;
      private Region hitRegion;
      private Place basePlace;
      private PetriNetGraphical parentNet;

      public PlaceShape(Place place, PetriNetGraphical parentNet)
      {
         this.basePlace = place;
         this.ParentNet = parentNet;
         this.HitRegion = new Region();
         this.Size = new Size(50, 50);
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

      public Point Center
      {
         get { return new Point(this.X + ((int)Size.Width / 2), this.Y + ((int)Size.Height / 2)); }
      }

      public new PetriNetGraphical ParentNet
      {
         get { return this.parentNet; }
         set { this.parentNet = value; }
      }

      public NetElement BaseElement
      {
         get { return this.basePlace; }
      }

      public new TokensList Tokens
      {
         get { return this.basePlace.Tokens; }
      }

      public new string Id
      {
         get { return this.BaseElement.Id; }
      }

      public new string Name
      {
         get { return this.BaseElement.Name; }
         set { this.BaseElement.Name = value; }
      }

      public void MoveBy(Point radiusVector)
      {
         Point old = new Point(this.X, this.Y);
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

         GraphicsPath tmpPath = new GraphicsPath();
         tmpPath.AddEllipse(this.X, this.Y, Size.Width, Size.Height);
         Region fillRegion = new Region(tmpPath);
         dc.FillRegion(grayBrush, fillRegion);
         dc.DrawEllipse(blackPen, this.X, this.Y, Size.Width, Size.Height);
         dc.DrawString(this.Name, font1, blackBrush, this.X + ((int)Size.Width / 2) + 5, this.Y - 5);
         dc.DrawString(this.Tokens.Count.ToString(CultureInfo.CurrentCulture), font1, blackBrush, this.X + ((int)Size.Width / 2) - 10, this.Y + ((int)Size.Height / 2) - 10);
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
         using (PreciseTimer pr = new PreciseTimer("Place.UpdateRegion"))
         {
            this.HitRegion.MakeEmpty();
            GraphicsPath tmpPath = new GraphicsPath();
            tmpPath.AddEllipse(this.X, this.Y, Size.Width, Size.Height);
            this.HitRegion.Union(tmpPath);
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

      protected virtual void OnPaint(PaintEventArgs e)
      {
         if (this.Paint != null)
         {
            this.Paint(this, e);
         }
      }
   }
}
