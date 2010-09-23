namespace Pppv.Editor
{
   using System;
   using System.Collections;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   public class NetCanvas : UserControl
   {
      private PetriNetWrapper net;
      private int gridStep;
      private Matrix scaleMatrix;
      private float scaleAmount;

      public NetCanvas()
      {
         this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
         this.Name = "NetCanvas";
         this.gridStep = 30;
         this.BackColor = Color.FromArgb(0, 50, 50, 50);
         this.BorderStyle = BorderStyle.FixedSingle;
         this.ScaleMatrix = new Matrix();
         this.ScaleAmount = 1.0F;
         this.ScaleMatrix.Scale(this.ScaleAmount, this.ScaleAmount);
         this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
         this.ParentChanged    += this.ParentChangedHandler;
      }

      public event EventHandler<CanvasMouseEventArgs> CanvasMouseClick;

      public event EventHandler<CanvasMouseEventArgs> CanvasMouseMove;

      public event EventHandler<CanvasMouseEventArgs> CanvasMouseDown;

      public event EventHandler<CanvasMouseEventArgs> CanvasMouseUp;

      public event EventHandler<SaveNetEventArgs> LinkedNetSave;

      public event EventHandler LinkedNetChange;

      public Matrix ScaleMatrix
      {
         get { return this.scaleMatrix; }
         set { this.scaleMatrix = value; }
      }

      public float ScaleAmount
      {
         get 
         {
            return this.scaleAmount; 
         }

         set
         {
            this.scaleAmount = value;
            this.ScaleMatrix = new Matrix();
            this.ScaleMatrix.Scale(this.ScaleAmount, this.ScaleAmount);
         }
      }

      public PetriNetWrapper Net
      {
         get
         {
            return this.net;
         }

         private set
         {
            if (this.net != null)
            {
               this.net.Save -= this.LinkedNetSaveHandler;
               this.net.Change -= this.LinkedNetChangeHandler;
            }

            this.net = value;
            if (this.net != null)
            {
               this.net.Save += this.LinkedNetSaveHandler;
               this.net.Change += this.LinkedNetChangeHandler;
            }
         }
      }

      public void OnCanvasMouseClick(System.Windows.Forms.MouseEventArgs arg)
      {
         if (this.CanvasMouseClick != null)
         {
            Point[] p = { arg.Location };
            Matrix mi = (Matrix)this.ScaleMatrix.Clone();
            mi.Invert();
            mi.TransformPoints(p);
            CanvasMouseEventArgs canvasArg = new CanvasMouseEventArgs(arg, p[0]);
            this.CanvasMouseClick(this, canvasArg);
         }
      }

      public void OnCanvasMouseMove(System.Windows.Forms.MouseEventArgs arg)
      {
         if (this.CanvasMouseMove != null)
         {
            Point[] p = { arg.Location };
            Matrix mi = (Matrix)this.ScaleMatrix.Clone();
            mi.Invert();
            mi.TransformPoints(p);
            CanvasMouseEventArgs canvasArg = new CanvasMouseEventArgs(arg, p[0]);
            this.CanvasMouseMove(this, canvasArg);
         }
      }

      public void OnCanvasMouseDown(System.Windows.Forms.MouseEventArgs arg)
      {
         if (this.CanvasMouseDown != null)
         {
            Point[] p = { arg.Location };
            Matrix mi = (Matrix)this.ScaleMatrix.Clone();
            mi.Invert();
            mi.TransformPoints(p);
            CanvasMouseEventArgs canvasArg = new CanvasMouseEventArgs(arg, p[0]);
            this.CanvasMouseDown(this, canvasArg);
         }
      }

      public void OnCanvasMouseUp(System.Windows.Forms.MouseEventArgs arg)
      {
         if (this.CanvasMouseUp != null)
         {
            Point[] p = { arg.Location };
            Matrix mi = (Matrix)this.ScaleMatrix.Clone();
            mi.Invert();
            mi.TransformPoints(p);
            CanvasMouseEventArgs canvasArg = new CanvasMouseEventArgs(arg, p[0]);
            this.CanvasMouseUp(this, canvasArg);
         }
      }

      public override void Refresh()
      {
         this.SetSize();
         base.Refresh();
      }

      public void PutNetOnCanvas(PetriNetWrapper net)
      {
         this.Net = net;
         this.Net.Canvas = this;
         this.SetSize();
      }

      protected override void OnLoad(EventArgs e)
      {
         base.OnLoad(e);
         this.SetSize();
      }

      protected override void OnPaintBackground(PaintEventArgs e)
      {
         using (PreciseTimer pr = new PreciseTimer("NetCanvas.OnPaintBackground"))
         {
            Point[] p = 
            {
               new Point(e.ClipRectangle.Left, e.ClipRectangle.Top),
               new Point(e.ClipRectangle.Right, e.ClipRectangle.Bottom)
            };
            Matrix mi = (Matrix)this.ScaleMatrix.Clone();
            mi.Invert();
            mi.TransformPoints(p);
            Rectangle newClipRectangle = new Rectangle(p[0].X, p[0].Y, p[1].X - p[0].X, p[1].Y - p[0].Y);
            Graphics dc = e.Graphics;
            e.Graphics.Transform = this.ScaleMatrix;
            Pen grayPen = new Pen(Color.FromArgb(255, 170, 170, 170), 1);
            dc.SmoothingMode = SmoothingMode.HighQuality;
            dc.Clear(Color.White);
            int x, y;
            y = (this.gridStep * ((newClipRectangle.Top - 1) / this.gridStep)) + this.gridStep;
            x = (this.gridStep * ((newClipRectangle.Left - 1) / this.gridStep)) + this.gridStep;
            for (; y <= newClipRectangle.Bottom; y += this.gridStep)
            {
               dc.DrawLine(grayPen, newClipRectangle.Left, y, newClipRectangle.Right, y);
            }

            for (; x <= newClipRectangle.Right; x += this.gridStep)
            {
               dc.DrawLine(grayPen, x, newClipRectangle.Top, x, newClipRectangle.Bottom);
            }
         }
      }

      protected override void OnPaint(PaintEventArgs e)
      {
         e.Graphics.Transform = this.ScaleMatrix;
         base.OnPaint(e);
      }

      protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
      {
         this.OnCanvasMouseClick(e);
         base.OnMouseClick(e);
      }

      protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
      {
         this.OnCanvasMouseMove(e);
         base.OnMouseMove(e);
      }

      protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
      {
         this.OnCanvasMouseDown(e);
         base.OnMouseDown(e);
      }

      protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
      {
         this.OnCanvasMouseUp(e);
         base.OnMouseUp(e);
      }

      protected override void OnParentChanged(EventArgs e)
      {
         base.OnParentChanged(e);
         if (this.Parent != null)
         {
            this.Parent.Resize += this.ParentResizeHandler;
         }

         this.SetSize();
      }

      protected void SetSize()
      {
         int width = 0, height = 0;
         Point[] p = { new Point(0, 0) };
         
         if (this.Net != null)
         {
            p[0].X = this.Net.Width;
            p[0].Y = this.Net.Height;
         }

         Matrix mi = (Matrix)this.ScaleMatrix.Clone();
         mi.TransformPoints(p);

         if (p[0].X >= Parent.ClientRectangle.Width)
         {
            width = p[0].X;
         }
         else
         {
            width = Parent.ClientRectangle.Width;
         }
         
         if (p[0].Y >= Parent.ClientRectangle.Height)
         {
            height = p[0].Y;
         }
         else
         {
            height = Parent.ClientRectangle.Height;
         }

         this.Size = new Size(width, height);
      }

      private void ParentChangedHandler(object sender, EventArgs arg)
      {
         Form f = this.FindForm();
         if (f != null)
         {
            f.KeyDown += this.CanvasKeyDownHandler;
         }
      }

      private void OnLinkedNetSave(SaveNetEventArgs args)
      {
         if (this.LinkedNetSave != null)
         {
            this.LinkedNetSave(this, args);
         }
      }

      private void OnLinkedNetChange(EventArgs args)
      {
         if (this.LinkedNetChange != null)
         {
            this.LinkedNetChange(this, args);
         }
      }

      private void CanvasKeyDownHandler(object sender, KeyEventArgs arg)
      {
         OnKeyDown(arg);
      }

      private void LinkedNetSaveHandler(object sender, Net.SaveNetEventArgs args)
      {
         this.OnLinkedNetSave(args);
      }

      private void LinkedNetChangeHandler(object sender, EventArgs args)
      {
         this.SetSize();
         this.OnLinkedNetChange(args);
      }

      private void ParentResizeHandler(object sender, EventArgs args)
      {
         this.SetSize();
      }
   }
}