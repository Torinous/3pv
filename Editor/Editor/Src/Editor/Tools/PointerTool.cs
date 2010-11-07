namespace Pppv.Editor.Tools
{
	using System;
	using System.Collections;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public class PointerTool : Tool
	{
		private Point lastMouseDownPoint;
		private bool frameIsActive;
		private Rectangle selectedRectangle;
		private Point selectFrom;
		private IShape pickedShape;
		
		public PointerTool() : base()
		{
			this.SelectedRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));
		}

		public Rectangle SelectedRectangle
		{
			get { return this.selectedRectangle; }
			private set { this.selectedRectangle = value; }
		}

		public IShape PickedShape
		{
			get { return this.pickedShape; }
			private set { this.pickedShape = value; }
		}		
		
		protected override void HandleMouseDown(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			PetriNetGraphical net = canvas.Net;
			this.lastMouseDownPoint = new Point(args.X, args.Y);
			if (args.Button == MouseButtons.Left)
			{
				this.PickedShape = net.GetShapeUnder(new Point(args.X, args.Y));
				if (this.PickedShape != null)
				{
					if (!net.SelectedObjects.Contains(this.PickedShape))
					{
						net.SelectedObjects.Clear();
						net.SelectedObjects.Add(this.PickedShape);
					}
				}
				else
				{
					net.SelectedObjects.Clear();
					this.frameIsActive = true;
					this.selectFrom = new Point(args.X, args.Y);
				}

				canvas.Invalidate();
			}

			canvas.Paint += this.DrawSelectionRegion;
			base.HandleMouseDown(canvas, args);
		}

		protected override void HandleMouseMove(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			PetriNetGraphical pnw = canvas.Net;
			if (args.Button == MouseButtons.Left)
			{
				if (this.frameIsActive)
				{
					Point startPoint = new Point(this.selectFrom.X, this.selectFrom.Y);
					if (args.X < this.selectFrom.X)
					{
						startPoint.X = args.X;
					}

					if (args.Y < this.selectFrom.Y)
					{
						startPoint.Y = args.Y;
					}

					this.selectedRectangle.Location = startPoint;
					this.selectedRectangle.Size = new Size(Math.Abs(args.X - this.selectFrom.X), System.Math.Abs(args.Y - this.selectFrom.Y));
					pnw.SelectedObjects.Clear();
					pnw.SelectedObjects.AddRange(canvas.Net.GetShapeUnder(this.SelectedRectangle));
					canvas.Invalidate();
				}
				else
				{
					Point delta = new Point(args.X - this.lastMouseDownPoint.X, args.Y - this.lastMouseDownPoint.Y);
					
					if (pnw.SelectedObjects.Contains(this.PickedShape))
					{
						for (int i = 0; i < pnw.SelectedObjects.Count; ++i)
						{
							pnw.SelectedObjects[i].MoveBy(delta);
						}
					}
					else
					{
						this.PickedShape.MoveBy(delta);
					}

					canvas.Invalidate();
					this.lastMouseDownPoint.X = args.X;
					this.lastMouseDownPoint.Y = args.Y;
				}
			}

			base.HandleMouseMove(canvas, args);
		}

		protected override void HandleMouseUp(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			this.selectedRectangle.Size = new Size(0, 0);
			canvas.Paint -= this.DrawSelectionRegion;
			this.frameIsActive = false;
			base.HandleMouseUp(canvas, args);
			canvas.Invalidate();
		}

		protected override void HandleMouseClick(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			base.HandleMouseClick(canvas, args);
		}

		protected override void HandleKeyDown(NetCanvas canvas, KeyEventArgs args)
		{
			base.HandleKeyDown(canvas, args);
		}

		private void DrawSelectionRegion(object sender, PaintEventArgs e)
		{
			if (this.frameIsActive)
			{
				Pen redPen = new Pen(Color.Red, 1);
				Graphics dc = e.Graphics;
				dc.SmoothingMode = SmoothingMode.HighQuality;
				dc.DrawRectangle(redPen, this.SelectedRectangle);
			}
		}
	}
}