using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

//using PPPv.Editor;
using PPPv.Utils;

namespace PPPv.Net {
   public class Place : BaseNetElement {
      private int _radius;
      private static int _ID = 0;

      /*Конструктор*/

      public Place(int x, int y) {
         _ID++;
         Name = "P"+_ID;
         _radius = 50;
         X = x - (int)_radius/2;
         Y = y - (int)_radius/2;
      }

      public override Point Center{
         get{
            return new Point(X + (int)_radius/2, Y + (int)_radius/2);
         }
      }

      public override void Draw(object sender, PaintEventArgs e){

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

         GraphicsPath tmpPath = new GraphicsPath();
         tmpPath.AddEllipse(X, Y, _radius, _radius);
         Region fillRegion = new Region(tmpPath);
         dc.FillRegion(grayBrush, fillRegion);
         dc.DrawEllipse(blackPen, X, Y, _radius, _radius);
         dc.DrawString(Name,font1,blackBrush,X+_radius,Y-10);

         if(Selected){
            dc.DrawRectangle(RedPen, Rectangle.Inflate( Rectangle.Ceiling(HitRegion.GetBounds(dc)),2,2));
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
         return tmp.IsEmpty();*/
         return false;
      }

      protected override void UpdateHitRegion(){
         using(PreciseTimer pr = new PreciseTimer("Place.UpdateRegion")){
           HitRegion.MakeEmpty();
           GraphicsPath tmpPath = new GraphicsPath();
           tmpPath.AddEllipse(X, Y, _radius, _radius);
           HitRegion.Union(tmpPath);
         }
      }

      protected override void MouseClickHandler(object sender, MouseEventArgs args){
      }

      protected override void MouseMoveHandler(object sender, MouseEventArgs args){
         base.MouseMoveHandler(sender,args);
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
