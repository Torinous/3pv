/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.10.2010
 * Time: 17:18
 */
namespace Pppv.Editor.Shapes
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.Editor;
   using Pppv.Net;

   public abstract class Shape : IShape, INetElement
   {
      private Size size;
      private Region hitRegion;
      private PetriNetGraphical parentNet;
      private INetElement baseElement;

      protected Shape()
      {
      }

      public event EventHandler<MoveEventArgs> Move;

      public event PaintEventHandler Paint;

      public event EventHandler Change;

      public Point Location
      {
         get { return new Point(this.BaseElement.X, this.BaseElement.Y); }
      }

      public int X
      {
         get { return this.BaseElement.X; }
         set { this.BaseElement.X = value; }
      }

      public int Y
      {
         get { return this.BaseElement.Y; }
         set { this.BaseElement.Y = value; }
      }

      public Point Center
      {
         get
         {
            Graphics g = this.ParentNetGraphical.Canvas.CreateGraphics();
            RectangleF rect = this.HitRegion.GetBounds(g);
            float xt, yt;
            xt = rect.X + (rect.Width / 2);
            yt = rect.Y + (rect.Height / 2);
            Point center = new Point();
            center.X = (int)xt;
            center.Y = (int)yt;
            return center;
         }
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

      public PetriNetGraphical ParentNetGraphical
      {
         get { return this.parentNet; }
         set { this.parentNet = value; }
      }

      public INetElement BaseElement
      {
         get { return this.baseElement; }
         protected set { this.baseElement = value; }
      }

      public string Name
      {
         get { return this.BaseElement.Name; }
         set { this.BaseElement.Name = value; }
      }

      public string Id
      {
         get { return this.BaseElement.Id; }
      }

      public PetriNet ParentNet
      {
         get { return this.BaseElement.ParentNet; }
         set { this.BaseElement.ParentNet = value; }
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

      public virtual Point GetConnectPoint(Point from)
      {
         return this.GetConnectPoint(from, this.ParentNetGraphical.Canvas);
      }

      public abstract void UpdateHitRegion();

      public abstract void Draw(PaintEventArgs e);
      
      public void WriteXml(XmlWriter writer)
      {
         this.BaseElement.WriteXml(writer);
      }

      public void ReadXml(XmlReader reader)
      {
         this.BaseElement.ReadXml(reader);
      }

      public XmlSchema GetSchema()
      {
         return this.BaseElement.GetSchema();
      }

      public void SetId(int number)
      {
         this.BaseElement.SetId(number);
      }

      public void DrawHandler(object sender, PaintEventArgs e)
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

      protected Point GetConnectPoint(Point from, Control onCanvas)
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
   }
}