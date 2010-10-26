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

	public class TransitionShape : NetElementShape, ITransition
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Не смертельно")]
		public TransitionShape(ITransition transition, PetriNetGraphical parentNet)
		{
			this.BaseElement = transition;
			this.ParentNetGraphical = parentNet;
			this.Size = new Size(20, 50);
			this.UpdateHitRegion();
			this.DependentShapes.Add(new SizePilonShape());
		}

		/*public Point Center
      {
         get
         {
            return new Point(this.X + (int)(Size.Width / 2), this.Y + (int)(Size.Height / 2));
         }
      }*/

		public string GuardFunction
		{
			get { return (this.BaseElement as ITransition).GuardFunction; }
			set { (this.BaseElement as ITransition).GuardFunction = value; }
		}

		public override void Draw(PaintEventArgs e)
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

		public override void UpdateHitRegion()
		{
			using (PreciseTimer pr = new PreciseTimer("Transition.UpdateRegion"))
			{
				this.HitRegion.MakeEmpty();
				this.HitRegion.Union(new Rectangle(this.X, this.Y, Size.Width, Size.Height));
			}
		}
	}
}
