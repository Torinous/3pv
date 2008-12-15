using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using PPPv.Utils;

namespace PPPv.Net {
   public class Place : BaseNetElement, IDrawable {
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

      public void Draw(Graphics dc){
         Pen blackPen = new Pen(Color.Black, 1);
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
      }

      public void Draw(object sender, PaintEventArgs e){
         Graphics dc = e.Graphics;
         Pen blackPen = new Pen(Color.Black, 1);
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
