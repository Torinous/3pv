
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PPPV.Net {
   public class Pilon: NetElementElement {

      public Pilon(int x_, int y_, GraphicalElement parent_):base(x_, y_, 0, 0, false)
      {
         ParentNetElement = parent_;
      }

      public override void Draw(object sender, PaintEventArgs e){

         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;

         Pen blackPen = new Pen(Color.FromArgb(255,0,0,0));
         Pen RedPen = new Pen(Color.FromArgb(255,255,0,0));

         /*╨Ъ╨╕╤Б╤В╨╕*/
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

      protected override void UpdateHitRegion(){
         HitRegion.MakeEmpty();
         HitRegion.Union(new Rectangle( X-2, Y-2, 7, 7));
      }

      protected override void MoveHandler(object sender, MoveEventArgs args){
         UpdateHitRegion();
         MoveBy(new Point(args.to.X-args.from.X,args.to.Y-args.from.Y));
      }
   }
}
