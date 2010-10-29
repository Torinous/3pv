/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 25.10.2010
 * Time: 2:25
 *
 *
 */

namespace Pppv.Editor.Shapes
{
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	
	public class SizePilonShape : Shape
	{
		public SizePilonShape()
		{
			this.Size = new Size(7, 7);
			this.ParentShapeChanged += this.ParentShapeChangedHandler;
			this.Move += this.MoveHandler;
		}
		
		public override void Draw(PaintEventArgs e)
		{
			Graphics dc = e.Graphics;
			dc.SmoothingMode = SmoothingMode.HighQuality;
			Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0));
			SolidBrush yellowBrush = new SolidBrush(Color.FromArgb(255, 200, 200, 100));
			SolidBrush blackBrush = new SolidBrush(Color.FromArgb(255, 100, 100, 100));
			Region fillRegion = new Region(new Rectangle(this.X, this.Y, Size.Width, Size.Height));
			dc.FillRegion(yellowBrush, fillRegion);
			dc.DrawRectangle(blackPen, this.X, this.Y, Size.Width, Size.Height);
			this.OnPaint(new PaintEventArgs(e.Graphics, e.ClipRectangle));
		}

		public override void UpdateHitRegion()
		{
			this.HitRegion.MakeEmpty();
			this.HitRegion.Union(new Rectangle(this.X, this.Y, Size.Width, Size.Height));
		}

		private void UpdateRelativePosition()
		{
			if (this.ParentShape != null)
			{
				this.X = this.ParentShape.X + this.ParentShape.Size.Width - 3;
				this.Y = this.ParentShape.Y + this.ParentShape.Size.Height - 3;
				this.UpdateHitRegion();
			}
		}
		
		private void ParentShapeChangedHandler(object sender, ParentShapeChangedEventArgs args)
		{
			this.UpdateRelativePosition();
		}
		
		private void MoveHandler(object sender, MoveEventArgs args)
		{
			if (this.X < this.ParentShape.X)
			{
				this.X = this.ParentShape.X;
			}
			
			if (this.Y < this.ParentShape.Y)
			{
				this.Y = this.ParentShape.Y;
			}
			
			this.ParentShape.Size = new Size(this.X - this.ParentShape.X + 3, this.Y - this.ParentShape.Y + 3);
		}
	}
}
