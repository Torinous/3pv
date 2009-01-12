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
            MoveEventArgs args = new MoveEventArgs(location,value);
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
            MoveEventArgs args = new MoveEventArgs(old,location);
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
            MoveEventArgs args = new MoveEventArgs(old,location);
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
      public virtual event MoveEventHandler Move;

      /*Конструкторы*/

      public BaseNetElement() {
         location = new Point(0,0);
         dragPoint = new Point(0,0);
         HitRegion = new Region();
         //UpdateHitRegion();
      }

      /*Методы*/

      /*Перемещение на*/
      public void MoveBy(Point p){
         /* Входной параметр это радиус-вектор перемещения */
         Point old = new Point(location.X,location.Y);
         Location = new Point(X + p.X,Y + p.Y);
         MoveEventArgs args = new MoveEventArgs(old,location);
         OnMove(args);
      }

      /* Абстрактые методы класса */

      public abstract bool IsIntersectWith(Point _point);
      public abstract bool IsIntersectWith(Rectangle _rectangle);
      public abstract bool IsIntersectWith(Region _region);

      protected abstract void MouseClickHandler(object sender, MouseEventArgs args);

      protected virtual void MouseMoveHandler(object sender, MouseEventArgs args){
         if(args.Button == MouseButtons.Left){
            switch(args.currentTool){
               case Editor.ToolEnum.Pointer:
                  if(Selected && !args.alreadyPerform){
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

      protected virtual void MouseDownHandler(object sender, MouseEventArgs args){
         if(args.Button == MouseButtons.Left){
            switch(args.currentTool){
               case Editor.ToolEnum.Pointer:
                  if(Selected = this.IsIntersectWith(new Point(args.X,args.Y)) && !args.alreadyPerform)
                  {
                     dragPoint.X = args.X - Location.X;
                     dragPoint.Y = args.Y - Location.Y;
                     args.alreadyPerform = true;
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

      protected abstract void MouseUpHandler(object sender, MouseEventArgs args);

      protected abstract void RegionSelectionStartHandler(object sender, RegionSelectionEventArgs args);
      protected abstract void RegionSelectionUpdateHandler(object sender, RegionSelectionEventArgs args);
      protected abstract void RegionSelectionEndHandler(object sender, RegionSelectionEventArgs args);

      protected abstract void KeyDownHandler(object sender, KeyEventArgs arg);

      protected abstract void UpdateHitRegion();

      public virtual void Draw(object sender, PaintEventArgs e){

         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;

         /*Кисти*/
         SolidBrush blueBrush = new SolidBrush(Color.Blue);
         dc.FillRegion(blueBrush,HitRegion);
      }

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

      protected void OnMove(MoveEventArgs args){
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
