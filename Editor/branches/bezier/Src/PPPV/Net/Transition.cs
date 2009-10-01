using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PPPv.Utils;

namespace PPPv.Net {
   public class Transition : NetElement {
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

      public Transition(int x_, int y_):base(x_,y_,20,50,true) {
         _ID++;
         Name = "T"+_ID;
         //UpdateHitRegion();
         guardFunction = "x=y";
      }

      public override Point Center{
         get{
            return new Point(X + (int)(Width/2), Y + (int)(Height/2));
         }
      }

      public override void Draw(object sender, PaintEventArgs e) {

         base.Draw(sender,e);

         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;
         Pen blackPen = new Pen(Color.FromArgb(255,0,0,0));
         Pen RedPen = new Pen(Color.FromArgb(255,255,0,0));
         /*Кисти*/
         SolidBrush grayBrush = new SolidBrush(Color.FromArgb(200,100,100,100));
         SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200,0,0,0));
         /*Шрифт*/
         FontFamily fF_Arial = new FontFamily("Arial");
         Font font1 = new Font(fF_Arial,16,FontStyle.Regular,GraphicsUnit.Pixel);

         Region fillRegion = new Region(new Rectangle( X, Y, Width, Height));
         dc.FillRegion(grayBrush, fillRegion);
         dc.DrawRectangle(blackPen, X, Y, Width, Height);
         dc.DrawString(Name+"\n"+guardFunction,font1,blackBrush,X+20,Y-17);
      }

      protected override void UpdateHitRegion(){
         using(PreciseTimer pr = new PreciseTimer("Transition.UpdateRegion")){
            HitRegion.MakeEmpty();
            HitRegion.Union(new Rectangle( X, Y, Width, Height));
         }
      }

      protected override void MouseClickHandler(object sender, MouseEventArgs args){
      }

      protected override void MouseMoveHandler(object sender, MouseEventArgs args){
         base.MouseMoveHandler(sender,args);
      }

      protected override void MouseDownHandler(object sender, MouseEventArgs args){
         base.MouseDownHandler(sender,args);
         if(args.Button == MouseButtons.Left){
            switch(args.currentTool){
               case Editor.ToolEnum.Pointer:
                  break;
               case Editor.ToolEnum.Place:
                  break;
               case Editor.ToolEnum.Transition:
                  break;
               case Editor.ToolEnum.Arc:
                  break;
               default:
                  break;
            }
         }
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
