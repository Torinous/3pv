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
	using System.Windows.Forms;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	
	public class ArcPointPilonShape : Shape
	{
		private int index;
		
		public ArcPointPilonShape(int index)
		{
			this.Index = index;
			this.Size = new Size(7, 7);
			this.ParentShapeChanged += ParentShapeChangedHandler;
			this.Move += MoveHandler;
		}
		
		public int Index
		{
			get { return index; }
			private set { index = value; }
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

		private void UpdateRelativePosition()
		{
			if (this.ParentShape != null)
			{
				ArcShape arc = this.ParentShape as ArcShape;
				this.X = arc.Points[index].X - 3;
				this.Y = arc.Points[index].Y - 3;
				this.UpdateHitRegion();
			}
		}

		public override void UpdateHitRegion()
		{
			this.HitRegion.MakeEmpty();
			this.HitRegion.Union(new Rectangle(this.X, this.Y, Size.Width, Size.Height));
		}
		
		private void ParentShapeChangedHandler(object sender, ParentShapeChangedEventArgs args)
		{
			/*if (args.OldParentShape != null)
			{
				args.OldParentShape.Move -= this.ParentShapeMoveHandler;
			}
			
			if (args.NewParentShape != null)
			{
				args.NewParentShape.Move += this.ParentShapeMoveHandler;
			}*/
			
			this.UpdateRelativePosition();
		}
		
		/*private void ParentShapeMoveHandler(object sender, MoveEventArgs args)
		{
			this.UpdateRelativePosition();
		}*/
		
		private void MoveHandler(object sender, MoveEventArgs args)
		{
			ArcShape arc = this.ParentShape as ArcShape;
			arc.Points[index] = new Point(this.X + 3, this.Y + 3);
		}
	}
}

