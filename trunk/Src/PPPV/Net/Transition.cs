using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PPPv.Utils;

namespace PPPv.Net {
   public class Transition : BaseNetElement, IDrawable {
      private static int _ID = 0;
      private string guardFunction;

      public string GuardFunction{
         get{
            return guardFunction;
         }
         set{
            guardFunction = value;
         }
      }

      public Transition(int x, int y) {
         _ID++;
         Name = "T"+_ID;
         X = x-10;
         Y = y-25;
         UpdateHitRegion();
         guardFunction = "x=y";
      }
      public override Point Center{
         get{
            return new Point(X + 10, Y + 25);
         }
      }
      public void Draw(Graphics dc) {
         Pen blackPen = new Pen(Color.Black, 1);
         /*Кисти*/
         SolidBrush grayBrush = new SolidBrush(Color.Gray);
         SolidBrush blackBrush = new SolidBrush(Color.Black);
         /*Шрифт*/
         FontFamily fF_Arial = new FontFamily("Arial");
         Font font1 = new Font(fF_Arial,16,FontStyle.Regular,GraphicsUnit.Pixel);

         SolidBrush myBrush = new SolidBrush(Color.Gray);
         Region fillRegion = new Region(new Rectangle( X, Y, 20, 50));
         dc.FillRegion(myBrush, fillRegion);
         dc.DrawRectangle(blackPen, X, Y, 20, 50);
         dc.DrawString(Name+"\n"+guardFunction,font1,blackBrush,X+20,Y-17);
      }

      public override bool IsIntersectWith(Point _point){
         return HitRegion.IsVisible(_point);
      }

      public override bool IsIntersectWith(Rectangle _rectangle){
         return HitRegion.IsVisible(_rectangle);
      }

      public override bool IsIntersectWith(Region _region){
         /*Region tmp = new Region(HitRegion);
         tmp.Intersect(_region);
         return tmp.IsEmpty;*/
         return false;
      }

      protected override void UpdateHitRegion(){
         using(PreciseTimer pr = new PreciseTimer("Transition.UpdateRegion")){
            HitRegion.MakeEmpty();
            HitRegion.Union(new Rectangle( X, Y, 20, 50));
         }
      }

      public override Point GetPilon(Point from){
         Region reg = new Region();
         reg = HitRegion.Clone();
         Pen greenPen = new Pen(Color.Black, 1);
         GraphicsPath gp = new GraphicsPath();
         gp.AddLine(from,Center);
         gp.Widen(greenPen);
         reg.Intersect(gp);
         Graphics g = this.ParentNet.Canvas.CreateGraphics();
         if(reg.IsEmpty(g))
            MessageBox.Show("ff");
         RectangleF bounds = reg.GetBounds(g);
         Rectangle rect = new Rectangle();
         rect = Rectangle.Ceiling(bounds);
         Point pilon = new Point();
         if(from.X <= Center.X){
            if(from.Y <= Center.Y){
               pilon.X = rect.Left;
               pilon.Y = rect.Top;
            }else{
               pilon.X = rect.Left;
               pilon.Y = rect.Bottom;
            }
         }else{
            if(from.Y <= Center.Y){
               pilon.X = rect.Right;
               pilon.Y = rect.Top;
            }else{
               pilon.X = rect.Right;
               pilon.Y = rect.Bottom;
            }
         }
         g.Dispose();
         return pilon;
      }
   }
}
