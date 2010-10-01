namespace Pppv.Net
{
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;

   public class Pilon : NetElement
   {
      public Pilon(Point point) : base(point)
      {
         // Size = new Size(7, 7);
      }

      public Point Center
      {
         get
         {
            return new Point(X + 3, Y + 3);
         }
      }

      public void Draw(PaintEventArgs e)
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
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "from", Justification = "Ну, не нужно")]
      public Point GetConnectPoint(Point from)
      {
         return this.Center;
      }

      public override void SetId(int number)
      {
      }

      /*protected void UpdateHitRegion()
      {
         HitRegion.MakeEmpty();
         HitRegion.Union(new Rectangle(X - 2, Y - 2, 7, 7));
      }*/

      /*protected override void MoveHandler(object sender, MoveEventArgs args)
      {
         UpdateHitRegion();
         MoveBy(new Point(args.to.X-args.from.X,args.to.Y-args.from.Y));
      }*/
   }
}