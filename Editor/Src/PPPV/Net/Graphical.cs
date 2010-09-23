namespace Pppv.Net
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;
   using System.Xml.Serialization;

   using Pppv.Editor;
   using Pppv.Utils;

   [Serializable()]
   public abstract class Graphical
   {
      private Point location;
      private Size size;
      [NonSerializedAttribute]
      private Region hitRegion;

      [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Не смертельно")]
      protected Graphical(Point place)
      {
         this.HitRegion = new Region();
         this.Location = place;
      }

      public virtual event EventHandler<MoveEventArgs> Move;

      public virtual event PaintEventHandler Paint;

      public virtual event EventHandler<ResizeEventArgs> Resize;

      public event EventHandler Change;

      public Point Location
      {
         get
         {
            return this.location;
         }

         private set
         {
            this.location = value;
            this.UpdateHitRegion();
         }
      }

      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "етить")]
      public int X
      {
         get
         {
            return this.location.X;
         }

         set
         {
            Point old = new Point(this.location.X, this.location.Y);
            this.location.X = value;
            if (this.location.X < 0)
            {
               this.location.X = 0;
            }

            MoveEventArgs args = new MoveEventArgs(old, this.location);
            this.OnMove(args);
         }
      }

      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "етить")]
      public int Y
      {
         get
         {
            return this.location.Y;
         }

         set
         {
            Point old = new Point(this.location.X, this.location.Y);
            this.location.Y = value;
            if (this.location.Y < 0)
            {
               this.location.Y = 0;
            }

            MoveEventArgs args = new MoveEventArgs(old, this.location);
            this.OnMove(args);
         }
      }

      public Region HitRegion
      {
         get { return this.hitRegion; }
         protected set { this.hitRegion = value; }
      }

      public abstract Point Center { get; }

      public Size Size
      {
         get
         {
            return this.size;
         }

         protected set
         {
            Size oldSize = this.size;
            this.size = value;
            this.OnResize(new ResizeEventArgs(oldSize, this.size));
         }
      }

      public void MoveBy(Point radiusVector)
      {
         Point old = new Point(this.location.X, this.location.Y);
         this.Location = new Point(this.X + radiusVector.X, this.Y + radiusVector.Y);
         this.OnMove(new MoveEventArgs(old, this.Location));
         this.OnChange(new EventArgs());
      }

      public virtual bool Intersect(Point point)
      {
         return this.HitRegion.IsVisible(point);
      }

      public virtual bool Intersect(Rectangle rectangle)
      {
         return this.HitRegion.IsVisible(rectangle);
      }

      public virtual bool Intersect(Region region)
      {
         /*Region tmp = new Region(HitRegion);
         tmp.Intersect(_region);
         return tmp.IsEmpty;*/
         return false;
      }

      public virtual void Draw(PaintEventArgs e)
      {
         this.OnPaint(e);
      }

      public abstract Point GetConnectPoint(Point from);

      protected void OnChange(EventArgs args)
      {
         if (this.Change != null)
         {
            this.Change(this, args);
         }
      }

      protected abstract void UpdateHitRegion();

      protected virtual Point GetConnectPoint(Point from, NetCanvas onCanvas)
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

      protected void OnMove(MoveEventArgs args)
      {
         if (this.Move != null)
         {
            this.Move(this, args);
         }
      }

      protected void OnResize(ResizeEventArgs args)
      {
         if (this.Resize != null)
         {
            this.Resize(this, args);
         }

         this.OnChange(new EventArgs());
      }

      private void OnPaint(PaintEventArgs e)
      {
         if (this.Paint != null)
         {
            this.Paint(this, e);
         }
      }
   }
}