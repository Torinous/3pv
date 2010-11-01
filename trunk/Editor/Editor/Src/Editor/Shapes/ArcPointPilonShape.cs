/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 25.10.2010
 * Time: 21:52
 *
 *
 */

namespace Pppv.Editor.Shapes
{
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	
	public class ArcPointPilonShape : Shape
	{
		private int index;
		
		public ArcPointPilonShape(int index)
		{
			this.Index = index;
			this.Size = new Size(7, 7);
			this.ParentShapeChanged += this.ParentShapeChangedHandler;
			this.Move += this.MoveHandler;
		}
		
		public int Index
		{
			get { return this.index; }
			private set { this.index = value; }
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
				ArcShape arc = this.ParentShape as ArcShape;
				this.X = arc.Points[this.index].X - 3;
				this.Y = arc.Points[this.index].Y - 3;
				this.UpdateHitRegion();
			}
		}
		
		private void ParentShapeChangedHandler(object sender, ParentShapeChangedEventArgs args)
		{
			this.UpdateRelativePosition();
		}
	
		private void MoveHandler(object sender, MoveEventArgs args)
		{
			ArcShape arc = this.ParentShape as ArcShape;
			arc.Points[this.index] = new Point(this.X + 3, this.Y + 3);
		}
	}
}

