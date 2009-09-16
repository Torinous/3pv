using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PPPv.Net {
   public abstract class GraphicalElement: IDrawable {

      protected Point location;
      protected Region _hitRegion; //регион где проверяется клик в объект
      protected string _name;
      protected bool selected;
      protected Point dragPoint;
      protected Pilon sizeController;
      protected bool sizeable;

      /*Аксессоры доступа*/
      protected bool Selected{
         get{
            return selected;
         }
         set{
            selected = value;
            OnSelectionChange(new SelectionChangeEventArgs(selected));
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

      public virtual int Height{
         get{
            return sizeController.Y - this.Y;
         }
      }

      public virtual int Width{
         get{
            return sizeController.X - this.X;
         }
      }

      /*События*/
      public virtual event MoveEventHandler            Move;
      public virtual event SelectionChangeEventHandler SelectionChange;

      public virtual event MouseEventHandler           MouseMove;
      public virtual event MouseEventHandler           MouseDown;
      public virtual event PaintEventHandler           Paint;
      public virtual event ResizeEventHandler          Resize;

      /*Конструкторы*/

      public GraphicalElement(int x_, int y_, int width_, int height_, bool sizeable_) {
         location = new Point(0,0);
         dragPoint = new Point(0,0);
         HitRegion = new Region();

         if(sizeable = sizeable_){
            sizeController = new Pilon( X + width_, Y + height_, this);
            sizeController.Move += MoveSizeControllerHandler;
         }

         Location = new Point(x_-(int)(width_/2), (y_-(int)(height_/2)));
      }

      /*Методы*/

      /*Перемещение на*/
      public void MoveBy(Point p){
         /* Входной параметр это радиус-вектор перемещения */
         Point old = new Point(location.X,location.Y);
         Location = new Point(X + p.X,Y + p.Y);
      }

      public void MoveBy(){
         /* Входной параметр это радиус-вектор перемещения */
         Point old = new Point(location.X,location.Y);
         Location = new Point(X,Y);
      }

      /* Абстрактые методы класса */

      protected virtual void OnMouseMove(MouseEventArgs e){
         if(MouseMove != null){
            MouseMove(this,e);
         }
      }

      protected virtual void OnMouseDown(MouseEventArgs e){
         if(MouseDown != null){
            MouseDown(this,e);
         }
      }

      private void OnPaint(PaintEventArgs e){
         if(Paint != null){
            Paint(this,e);
         }
      }

      public virtual bool IsIntersectWith(Point _point){
         return HitRegion.IsVisible(_point);
      }

      public virtual bool IsIntersectWith(Rectangle _rectangle){
         return HitRegion.IsVisible(_rectangle);
      }

      public virtual bool IsIntersectWith(Region _region){
         /*Region tmp = new Region(HitRegion);
         tmp.Intersect(_region);
         return tmp.IsEmpty;*/
         return false;
      }

      protected virtual void MouseClickHandler(object sender, MouseEventArgs args){
      }

      protected virtual void MouseMoveHandler(object sender, MouseEventArgs args){
         if(args.Button == MouseButtons.Left){
            switch(args.currentTool){
               case Editor.ToolEnum.Pointer:
                  if(Selected && !args.alreadyPerform){
                     this.MoveBy(new Point(args.Location.X - Location.X - dragPoint.X, args.Location.Y - Location.Y - dragPoint.Y));
                     //(sender as PetriNet).Canvas.Invalidate();
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
                     //(sender as PetriNet).Canvas.Invalidate();
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

      protected virtual void MouseUpHandler(object sender, MouseEventArgs args){
      }

      protected virtual void RegionSelectionStartHandler(object sender, RegionSelectionEventArgs args){
      }

      protected virtual void RegionSelectionUpdateHandler(object sender, RegionSelectionEventArgs args){
      }

      protected virtual void RegionSelectionEndHandler(object sender, RegionSelectionEventArgs args){
      }

      protected virtual void KeyDownHandler(object sender, KeyEventArgs arg){
      }

      protected abstract void UpdateHitRegion();

      protected virtual void ShowSelectionMarker(Graphics dc){
         dc.SmoothingMode = SmoothingMode.HighQuality;
         Pen RedPen = new Pen(Color.FromArgb(255,255,0,0));
         RectangleF tmp = HitRegion.GetBounds(dc);
         dc.DrawRectangle(RedPen, Rectangle.Inflate(Rectangle.Ceiling(HitRegion.GetBounds(dc)),2,2));
      }

      public virtual void Draw(object sender, PaintEventArgs e){

         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;

         /*Кисти*/
         SolidBrush blueBrush = new SolidBrush(Color.Blue);

         //dc.FillRegion(blueBrush,HitRegion);

         if(Selected)
            ShowSelectionMarker(e.Graphics);
      }

      public virtual Point GetPilon(Point from,Graphics on){
         //Graphics g = this.ParentNet.Canvas.CreateGraphics();
         Region reg = new Region();
         reg = HitRegion.Clone();
         Pen greenPen = new Pen(Color.Black, 1);
         GraphicsPath gp = new GraphicsPath();
         Rectangle rect = new Rectangle();
         Point pilon = new Point();

         /*Если не посчитается, просто вернём центр*/
         pilon.X = Center.X;
         pilon.Y = Center.Y;

         if(from != Center){
            gp.AddLine(from,Center);
            gp.Widen(greenPen);
            reg.Intersect(gp);
            RectangleF bounds = reg.GetBounds(on);
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
            on.Dispose();
         }
         return pilon;
      }

      protected void PaintRetranslator(object sender, PaintEventArgs args){
         OnPaint(args);
      }

      protected void MouseMoveRetranslator(object sender, MouseEventArgs args){
         OnMouseMove(args);
      }

      protected void MouseDownRetranslator(object sender, MouseEventArgs args){
         OnMouseDown(args);
      }

      protected void OnMove(MoveEventArgs args){
         UpdateHitRegion();
         if(Move != null){
            Move(this,args);
         }
      }

      protected void OnResize(ResizeEventArgs args){
         UpdateHitRegion();
         if(Resize != null){
            Resize(this,args);
         }
      }

      protected void OnSelectionChange(SelectionChangeEventArgs args){
         if(SelectionChange != null){
            SelectionChange(this,args);
         }
      }

      protected virtual void MoveSizeControllerHandler(object sender, MoveEventArgs arg){
         ResizeEventArgs arg2 = new ResizeEventArgs(new Point( arg.from.X, arg.from.Y ), new Point(arg.to.X,arg.to.Y));
         OnResize(arg2);
      }

      /*Вся внутренняя подготовка перед удалением элемента сети*/
      public virtual void PrepareToDeletion(){
         /*Отпишемся от всех событый*/
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
