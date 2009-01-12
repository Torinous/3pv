using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PPPv.Net {
   public abstract class BaseNetElement: IDrawable {

      protected PetriNet parent;
      protected Point location;
      protected Region _hitRegion; //регион где проверяется клик в объект
      protected string _name;
      protected bool selected;
      protected Point dragPoint;

      /*Аксессоры доступа*/
      protected bool Selected{
         get{
            return selected;
         }
         set{
            if(selected && !value)
               ParentNet.Unselect(this);

            if(!selected && value)
               ParentNet.Select(this);

            selected = value;
         }
      }

      public PetriNet ParentNet{
         get{
            return parent;
         }
         set{
            if(parent != null){
               parent.Paint                 -= this.Draw;
               parent.MouseMove             -= this.MouseMoveHandler;
               parent.MouseClick            -= this.MouseClickHandler;
               parent.MouseUp               -= this.MouseUpHandler;
               parent.MouseDown             -= this.MouseDownHandler;
               parent.RegionSelectionStart  -= this.RegionSelectionStartHandler;
               parent.RegionSelectionUpdate -= this.RegionSelectionUpdateHandler;
               parent.RegionSelectionEnd    -= this.RegionSelectionEndHandler;
               parent.KeyDown               -= this.KeyDownHandler;
            }
            parent = value;
            if(parent != null){
               parent.Paint                 += this.Draw;
               parent.MouseMove             += this.MouseMoveHandler;
               parent.MouseClick            += this.MouseClickHandler;
               parent.MouseUp               += this.MouseUpHandler;
               parent.MouseDown             += this.MouseDownHandler;
               parent.RegionSelectionStart  += this.RegionSelectionStartHandler;
               parent.RegionSelectionUpdate += this.RegionSelectionUpdateHandler;
               parent.RegionSelectionEnd    += this.RegionSelectionEndHandler;
               parent.KeyDown               += this.KeyDownHandler;
            }
         }
      }

      public Point Location{
         get{
            return location;
         }
         set{
            BaseNetElementMoveEventArgs args = new BaseNetElementMoveEventArgs(location,value);
            location = value;
            OnMove(args);
         }
      }

      public int X{
         get{
            return location.X;
         }
         set{
            Point old = new Point(location.X,location.Y);
            location.X = value;
            BaseNetElementMoveEventArgs args = new BaseNetElementMoveEventArgs(old,location);
            OnMove(args);
         }
      }

      public int Y{
         get{
            return location.Y;
         }
         set{
            Point old = new Point(location.X,location.Y);
            location.Y = value;
            BaseNetElementMoveEventArgs args = new BaseNetElementMoveEventArgs(old,location);
            OnMove(args);
         }
      }

      public string Name{
         get{
            return _name;
         }
         set{
            _name = value;
         }
      }

      public Region HitRegion{
         get{
            return _hitRegion;
         }
         protected set{
            _hitRegion = value;
         }
      }

      /*Центр объекта*/
      public abstract Point Center{
        get;
      }

      /*События*/
      public virtual event BaseNetElementMoveEventHandler Move;

      /*Конструкторы*/

      public BaseNetElement() {
         location = new Point(0,0);
         dragPoint = new Point(0,0);
         HitRegion = new Region();
      }

      /*Методы*/

      /*Перемещение на*/
      public void MoveBy(Point p){
         /*Входной параметр это радиусвектор перемещения*/
         Point old = new Point(location.X,location.Y);
         Location = new Point(X + p.X,Y + p.Y);
         BaseNetElementMoveEventArgs args = new BaseNetElementMoveEventArgs(old,location);
         OnMove(args);
      }

      /*Абстрактые методы класса*/

      public abstract void Draw(object sender, PaintEventArgs e);
      public abstract bool IsIntersectWith(Point _point);
      public abstract bool IsIntersectWith(Rectangle _rectangle);
      public abstract bool IsIntersectWith(Region _region);

      protected abstract void MouseClickHandler(object sender, MouseEventArgs args);
      protected abstract void MouseMoveHandler(object sender, MouseEventArgs args);
      protected abstract void MouseDownHandler(object sender, MouseEventArgs args);
      protected abstract void MouseUpHandler(object sender, MouseEventArgs args);

      protected abstract void RegionSelectionStartHandler(object sender, RegionSelectionEventArgs args);
      protected abstract void RegionSelectionUpdateHandler(object sender, RegionSelectionEventArgs args);
      protected abstract void RegionSelectionEndHandler(object sender, RegionSelectionEventArgs args);

      protected abstract void KeyDownHandler(object sender, KeyEventArgs arg);

      protected abstract void UpdateHitRegion();

      public virtual Point GetPilon(Point from){
         Graphics g = this.ParentNet.Canvas.CreateGraphics();
         Region reg = new Region();
         reg = HitRegion.Clone();
         Pen greenPen = new Pen(Color.Black, 1);
         GraphicsPath gp = new GraphicsPath();
         Rectangle rect = new Rectangle();
         Point pilon = new Point();

         /*Если не посчитается просто вернём центр*/
         pilon.X = Center.X;
         pilon.Y = Center.Y;

         if(from != Center){
            gp.AddLine(from,Center);
            gp.Widen(greenPen);
            reg.Intersect(gp);
            RectangleF bounds = reg.GetBounds(g);
            rect = Rectangle.Ceiling(bounds);
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
         }
         return pilon;
      }

      protected void OnMove(BaseNetElementMoveEventArgs args){
         UpdateHitRegion();
         if(Move != null){
            Move(this,args);
         }
      }
      /*Вся внутренняя подготовка перед удалением элемента сети*/
      public virtual void PrepareToDeletion(){
         /*Отпишемся от всех событый*/
         ParentNet = null;
      }

      protected static Pen ArrowedBlackPenFactory(){
         Pen p = new Pen(Color.Black,1);
         GraphicsPath hPath = new GraphicsPath();
         hPath.AddLine(new Point(4, -7), new Point(0, 0));
         hPath.AddLine(new Point(-4, -7), new Point(0, 0));
         CustomLineCap ArrowCap = new CustomLineCap(null, hPath);
         ArrowCap.SetStrokeCaps(LineCap.Triangle, LineCap.Triangle);
         p.CustomEndCap = ArrowCap;
         return p;
      }
   }
}
