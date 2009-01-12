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

         GraphicsPath tmpPath = new GraphicsPath();
         tmpPath.AddEllipse(X, Y, _radius, _radius);
         Region fillRegion = new Region(tmpPath);
         dc.FillRegion(grayBrush, fillRegion);
         dc.DrawEllipse(blackPen, X, Y, _radius, _radius);
         dc.DrawString(Name,font1,blackBrush,X+_radius,Y-10);

         if(Selected){
            RectangleF tmp = HitRegion.GetBounds(dc);
            dc.DrawRectangle(RedPen, new Rectangle((int)tmp.X, (int)tmp.Y, (int)tmp.Width, (int)tmp.Height) );
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
         if(args.Button == MouseButtons.Left){
            switch(args.currentTool){
               case Editor.ToolEnum.Pointer:
                  if(Selected){
                     this.MoveBy(new Point(args.Location.X - Location.X - dragPoint.X, args.Location.Y - Location.Y - dragPoint.Y));
                     (sender as PetriNet).Canvas.Invalidate();
                  }
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
         if(args.Button == MouseButtons.Left){
            switch(args.currentTool){
               case Editor.ToolEnum.Pointer:
                  if(Selected = this.IsIntersectWith(new Point(args.X,args.Y)))
                  {
                     dragPoint.X = args.X - Location.X;
                     dragPoint.Y = args.Y - Location.Y;
                  }
                  (sender as PetriNet).Canvas.Invalidate();
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
