
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PPPv.Net {
   public abstract class Pilon: NetElementElement {

      public Pilon(){
         
      }

      public virtual void Draw(object sender, PaintEventArgs e){

         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;

         Pen blackPen = new Pen(Color.FromArgb(255,0,0,0));
         Pen RedPen = new Pen(Color.FromArgb(255,255,0,0));

         /*Кисти*/
         SolidBrush whiteBrush = new SolidBrush(Color.FromArgb(255,255,255,0));

         Region fillRegion = new Region(new Rectangle( X-2, Y-2, 5, 5));
         dc.FillRegion(whiteBrush, fillRegion);
         dc.DrawRectangle(blackPen, X-3, Y-3, 7, 7);
      }
   }

}
