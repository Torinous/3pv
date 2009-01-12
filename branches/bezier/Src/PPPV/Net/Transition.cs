using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PPPv.Utils;

namespace PPPv.Net {
   public class Transition : BaseNetElement {
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

      public override void Draw(object sender, PaintEventArgs e) {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;
         Pen blackPen = new Pen(Color.Black, 1);
         Pen RedPen = new Pen(Color.Red, 1);
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

         if(Selected){
            RectangleF tmp = HitRegion.GetBounds(dc);
            dc.DrawRectangle(RedPen, new Rectangle((int)tmp.X-1, (int)tmp.Y-1, (int)tmp.Width+2, (int)tmp.Height+2) );
         }
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

      protected override void MouseClickHandler(object sender, MouseEventArgs args){
      }

      protected override void MouseMoveHandler(object sender, MouseEventArgs args){
      }

      protected override void MouseDownHandler(object sender, MouseEventArgs args){
      }

      protected override void MouseUpHandler(object sender, MouseEventArgs args){
      }

      protected override void RegionSelectionStartHandler(object sender, RegionSelectionEventArgs args){
      }

      protected override void RegionSelectionUpdateHandler(object sender, RegionSelectionEventArgs args){
      }

      protected override void RegionSelectionEndHandler(object sender, RegionSelectionEventArgs args){
      }

      protected override void KeyDownHandler(object sender, KeyEventArgs arg){
      }
   }
}
