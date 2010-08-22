namespace PPPV.Net
{
	using System.Drawing;
	using System.Windows.Forms;
	using System.Drawing.Drawing2D;

	public class Pilon: NetElement
	{
		public Pilon(Point point):base(point)
		{
			Size = new Size(7, 7);
		}
		
		public override void Draw(object sender, PaintEventArgs e)
		{
			Graphics dc = e.Graphics;
			dc.SmoothingMode = SmoothingMode.HighQuality;

			Pen blackPen = new Pen(Color.FromArgb(255,0,0,0));
			Pen RedPen = new Pen(Color.FromArgb(255,255,0,0));

			SolidBrush whiteBrush = new SolidBrush(Color.FromArgb(255,255,255,0));

			Region fillRegion = new Region(new Rectangle( X-2, Y-2, 5, 5));
			dc.FillRegion(whiteBrush, fillRegion);
			dc.DrawRectangle(blackPen, X-3, Y-3, 7, 7);
		}

		public override Point Center
		{
			get{
				return new Point(X+3, Y+3);
			}
		}

		protected override void UpdateHitRegion()
		{
			HitRegion.MakeEmpty();
			HitRegion.Union(new Rectangle( X-2, Y-2, 7, 7));
		}

		/*protected override void MoveHandler(object sender, MoveEventArgs args)
		{
			UpdateHitRegion();
			MoveBy(new Point(args.to.X-args.from.X,args.to.Y-args.from.Y));
		}*/
		
		/*Чисто фиктивно, просто чтобы реализовать абстрактный член*/
		public override Point GetPilon(Point from)
		{
			return Center;
		}
	}
}
