using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PPPv.Utils;
using PPPv.Editor;
using PPPv.Net;

namespace PPPv.Net {
   public class Arc : BaseNetElement, IDrawable {
      private static int _ID = 0;
      protected static Pen ArrowedBlackPen = ArrowedBlackPenFactory();
      private BaseNetElement from,to;
      private Point fromPilon,toPilon;
      private NetCanvas _canvas;
      private string cortege;

      public string Cortege{
         get{
            return cortege;
         }
         set{
            cortege = value;
         }
      }
      /*Конструктор*/
      public Arc(BaseNetElement startElement,NetCanvas netCanvas) {
         _ID++;
         Name = "A"+_ID;
         From = startElement;
         toPilon = From.Center;
         _canvas = netCanvas;
         _canvas.CanvasMouseMove += _arcMouseMoveHandler;
         cortege = "2X";
      }

      public override Point Center{
         get{
            return new Point((fromPilon.X+toPilon.X)/2,(fromPilon.Y+toPilon.Y)/2);
         }
      }

      public void Draw(Graphics dc){
         /*Кисти*/
         SolidBrush grayBrush = new SolidBrush(Color.Gray);
         SolidBrush blackBrush = new SolidBrush(Color.Black);
         /*Шрифт*/
         FontFamily fF_Arial = new FontFamily("Arial");
         Font font1 = new Font(fF_Arial,16,FontStyle.Regular,GraphicsUnit.Pixel);
         dc.DrawLine(ArrowedBlackPen,fromPilon,toPilon);
         dc.DrawString(cortege,font1,blackBrush,Center.X,Center.Y+5);
      }

      public void Draw(object sender, PaintEventArgs e){
         Graphics dc = e.Graphics;
         /*Кисти*/
         SolidBrush grayBrush = new SolidBrush(Color.Gray);
         SolidBrush blackBrush = new SolidBrush(Color.Black);
         /*Шрифт*/
         FontFamily fF_Arial = new FontFamily("Arial");
         Font font1 = new Font(fF_Arial,16,FontStyle.Regular,GraphicsUnit.Pixel);
         dc.DrawLine(ArrowedBlackPen,fromPilon,toPilon);
         dc.DrawString(cortege,font1,blackBrush,Center.X,Center.Y+5);
      }

      private void _arcMouseMoveHandler(object sender, CanvasMouseEventArgs arg){
         toPilon.X = arg.X;
         toPilon.Y = arg.Y;
         ((NetCanvas)sender).Invalidate();
      }

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

      public BaseNetElement To{
         get{
            return to;
         }
         set{
            to = value;
            Unlink();
            to.Move += MoveHandler;
            toPilon = to.GetPilon(From.Center);
         }
      }

      public BaseNetElement From{
         get{
            return from;
         }
         set{
            from = value;
            from.Move += MoveHandler;
            fromPilon = from.Center;
         }
      }

      public void Unlink(){
          _canvas.CanvasMouseMove -= _arcMouseMoveHandler;
      }

      private static Pen ArrowedBlackPenFactory(){
         Pen p = new Pen(Color.Black,1);
         GraphicsPath hPath = new GraphicsPath();
         hPath.AddLine(new Point(4, -7), new Point(0, 0));
         hPath.AddLine(new Point(-4, -7), new Point(0, 0));
         CustomLineCap ArrowCap = new CustomLineCap(null, hPath);
         ArrowCap.SetStrokeCaps(LineCap.Triangle, LineCap.Triangle);
         p.CustomEndCap = ArrowCap;
         return p;
      }

      public override Point GetPilon(Point from){
         return Center;
      }
   }
}
