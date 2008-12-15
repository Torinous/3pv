using System.Drawing;

namespace PPPv.Net {
   public abstract class BaseNetElement {
      protected PetriNet parent;
      protected Point location;
      protected Region _hitRegion; //регион где проверяется клик в объект
      protected string _name;

      /*Аксессоры доступа*/

      public PetriNet ParentNet{
         get{
            return parent;
         }
         set{
            parent = value;
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

      public abstract bool IsIntersectWith(Point _point);
      public abstract bool IsIntersectWith(Rectangle _rectangle);
      public abstract bool IsIntersectWith(Region _region);
      public abstract Point GetPilon(Point from);
      protected abstract void UpdateHitRegion();
      protected void OnMove(BaseNetElementMoveEventArgs args){
         UpdateHitRegion();
         if(Move != null){
            Move(this,args);
         }
      }
   }
}
