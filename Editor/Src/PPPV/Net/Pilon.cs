namespace Pppv.Net
{
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;

   public class Pilon : NetElement
   {
      public Pilon(Point point) : base(point)
      {
         Size = new Size(7, 7);
      }

      public override Point Center
      {
         get
         {
            return new Point(X + 3, Y + 3);
         }
      }

      public override void Draw(PaintEventArgs e)
      {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;

         Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0));

         SolidBrush whiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 0));

         Region fillRegion = new Region(new Rectangle(X - 2, Y - 2, 5, 5));
         dc.FillRegion(whiteBrush, fillRegion);
         dc.DrawRectangle(blackPen, X - 3, Y - 3, 7, 7);
      }

      /*Чисто фиктивно, просто чтобы реализовать абстрактный член*/
      public override Point GetPilon(Point from)
      {
         return this.Center;
      }

      protected override void UpdateHitRegion()
      {
         HitRegion.MakeEmpty();
         HitRegion.Union(new Rectangle(X - 2, Y - 2, 7, 7));
      }

      /*protected override void MoveHandler(object sender, MoveEventArgs args)
      {
         UpdateHitRegion();
         MoveBy(new Point(args.to.X-args.from.X,args.to.Y-args.from.Y));
      }*/
   }
}