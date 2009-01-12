using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PPPv.Utils;
//using PPPv.Editor;

namespace PPPv.Net {
   public class Arc : BaseNetElement {
      private static int _ID = 0;
      protected static Pen ArrowedBlackPen = ArrowedBlackPenFactory();
      private BaseNetElement from,to;
      private Point fromPilon,toPilon;
      private string cortege;

      public string Cortege{
         get{
            return cortege;
         }
         set{
            cortege = value;
         }
      }


      public BaseNetElement To{
         get{
            return to;
         }
         set{
            if(to != null)
               to.Move -= MoveHandler;
            to = value;
            if(to != null){
               toPilon = to.GetPilon(From.Center);
               to.Move += MoveHandler;
            }
         }
      }

      public BaseNetElement From{
         get{
            return from;
         }
         set{
            if(from != null)
               from.Move -= MoveHandler;
            from = value;
            if(from != null){
               fromPilon = from.Center;
               from.Move += MoveHandler;
            }
         }
      }

      public bool Unfinished{
         get{
            return To == null;
         }
      }

      /*Конструктор*/
      public Arc(BaseNetElement startElement) {
         _ID++;
         Name = "A"+_ID;
         From = startElement;
         toPilon = From.Center;
         cortege = "2X";
      }

      public override Point Center{
         get{
            return new Point((fromPilon.X+toPilon.X)/2,(fromPilon.Y+toPilon.Y)/2);
         }
      }

      public override void Draw(object sender, PaintEventArgs e){
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;

         Pen RedPen = new Pen(Color.Red, 1);
         /*Кисти*/
         SolidBrush grayBrush = new SolidBrush(Color.Gray);
         SolidBrush blackBrush = new SolidBrush(Color.Black);
         /*Шрифт*/
         FontFamily fF_Arial = new FontFamily("Arial");
         Font font1 = new Font(fF_Arial,16,FontStyle.Regular,GraphicsUnit.Pixel);
         dc.DrawLine(ArrowedBlackPen,fromPilon,toPilon);
         dc.DrawString(cortege,font1,blackBrush,Center.X,Center.Y+5);

         if(Selected){
            RectangleF tmp = HitRegion.GetBounds(dc);
            dc.DrawRectangle(RedPen, new Rectangle((int)tmp.X, (int)tmp.Y, (int)tmp.Width, (int)tmp.Height) );
         }
      }

      /*private void _arcMouseMoveHandler(object sender, CanvasMouseEventArgs arg){
         toPilon.X = arg.X;
         toPilon.Y = arg.Y;
         fromPilon = from.GetPilon(toPilon);
         ((NetCanvas)sender).Invalidate();
      }*/

      public override bool IsIntersectWith(Point _point){
         return HitRegion.IsVisible(_point);
      }

      public override bool IsIntersectWith(Rectangle _rectangle){
         return HitRegion.IsVisible(_rectangle);
      }

      public override bool IsIntersectWith(Region _region){
         /*Region tmp = new Region(HitRegion.GetRegionData());
         tmp.Intersect(_region);
         return tmp.IsEmpty();*/
         return false;
      }

      private void MoveHandler(object sender, BaseNetElementMoveEventArgs args){
         UpdateHitRegion();
         fromPilon = from.GetPilon(to.Center);
         toPilon = to.GetPilon(from.Center);
      }

      protected override void MouseClickHandler(object sender, MouseEventArgs args){
      }

      protected override void MouseMoveHandler(object sender, MouseEventArgs args){
         switch(args.currentTool){
            case Editor.ToolEnum.Pointer:
               break;
            case Editor.ToolEnum.Place:
               break;
            case Editor.ToolEnum.Transition:
               break;
            case Editor.ToolEnum.Arc:
               if(Unfinished){
                  toPilon.X = args.X;
                  toPilon.Y = args.Y;
                  fromPilon = from.GetPilon(toPilon);
                  (sender as PetriNet).Canvas.Invalidate();
               }
               break;
            default:
               break;
         }
      }

      protected override void MouseDownHandler(object sender, MouseEventArgs args){
         if(args.Button == MouseButtons.Left){
            switch(args.currentTool){
               case Editor.ToolEnum.Pointer:
                  break;
               case Editor.ToolEnum.Place:
                  break;
               case Editor.ToolEnum.Transition:
                  break;
               case Editor.ToolEnum.Arc:
                  if(Unfinished){
                     BaseNetElement clicked = parent.NetElementUnder(new Point(args.X,args.Y));
                     clicked = clicked == this ? null : clicked;
                     if(clicked != null){
                        if(From.GetType() != clicked.GetType()){
                           To = clicked;
                        }
                     }else{
                     }
                     (sender as PetriNet).Canvas.Invalidate();
                  }
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
         if(arg.KeyCode == Keys.Escape){
            if(to == null){
               parent.Delete(this);
               (sender as PetriNet).Canvas.Invalidate();//TODO: полный Invalidate это нехорошо!!!
            }
         }
      }

      protected override void UpdateHitRegion(){
         using(PreciseTimer pr = new PreciseTimer("Arc.UpdateRegion")){
            HitRegion.MakeEmpty();
            GraphicsPath tmpPath = new GraphicsPath();

            Point point1 = new Point(from.Center.X,from.Center.Y-1);
            Point point2 = new Point(to.Center.X,to.Center.Y-1);
            tmpPath.AddLine(point1,point2);

            point1 = new Point(to.Center.X,to.Center.Y-1);
            point2 = new Point(to.Center.X,to.Center.Y+1);
            tmpPath.AddLine(point1,point2);

            point1 = new Point(to.Center.X,to.Center.Y+1);
            point2 = new Point(from.Center.X,from.Center.Y+1);
            tmpPath.AddLine(point1,point2);

            point1 = new Point(from.Center.X,from.Center.Y+1);
            point2 = new Point(from.Center.X,from.Center.Y-1);
            tmpPath.AddLine(point1,point2);

            HitRegion.Union(tmpPath);
         }
      }

      /*Чисто фиктивно, просто чтобы реализовать абстрактный член*/
      public override Point GetPilon(Point from){
         return Center;
      }

      public override void PrepareToDeletion(){
         From = null;
         To = null;
         base.PrepareToDeletion();
      }

   }
}
