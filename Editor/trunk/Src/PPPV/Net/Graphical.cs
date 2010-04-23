using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

using PPPV.Editor;
using PPPV.Utils;

namespace PPPV.Net
{
  [Serializable()]
  public abstract class Graphical
  {
    /*Поля*/
    protected Point location;
    protected Size size;
    protected Region _hitRegion; //регион где проверяется клик в объект
    protected string _name;

    /*Конструкторы*/
    public Graphical(Point p)
    {
      //location = new Point(0,0);
      HitRegion = new Region();
      //Location = new Point(x_-(int)(width_/2), (y_-(int)(height_/2)));
      Location = p;
    }

    public Point Location
    {
      get
      {
        return location;
      }
      set
      {
        if(value.X < 0) value.X = 0;
        if(value.Y < 0) value.Y = 0;
        MoveEventArgs args = new MoveEventArgs(location, value);
        location = value;
        OnMove(args);
      }
    }

    public int X
    {
      get
      {
        return location.X;
      }
      set
      {
        Point old = new Point(location.X,location.Y);
        location.X = value;
        if(location.X < 0) location.X = 0;
        MoveEventArgs args = new MoveEventArgs(old, location);
        OnMove(args);
      }
    }

    public int Y
    {
      get
      {
        return location.Y;
      }
      set
      {
        Point old = new Point(location.X,location.Y);
        location.Y = value;
        if(location.Y<0) location.Y =0;
        MoveEventArgs args = new MoveEventArgs(old,location);
        OnMove(args);
      }
    }

    public string Name
    {
      get
      {
        return _name;
      }
      set
      {
        _name = value;
        OnChange(new EventArgs());
      }
    }

    public Region HitRegion
    {
      get
      {
        return _hitRegion;
      }
      protected set
      {
        _hitRegion = value;
      }
    }

    /*Центр объекта*/
    public abstract Point Center
    {
      get;
    }

    public virtual Size Size
    {
      get
      {
        return size;
      }
      protected set
      {
        Size oldSize = size;
        size = value;
        OnResize(new ResizeEventArgs(oldSize, size));
      }
    }

    /*События*/
    public virtual event MoveEventHandler            Move;
    public virtual event PaintEventHandler           Paint;
    public virtual event ResizeEventHandler          Resize;

    public virtual event EventHandler                Change;

    protected virtual void OnChange(EventArgs args)
    {
      if(Change != null)
      {
        Change(this, args);
      }
    }

    /*Методы*/
    /*Перемещение на*/
    public void MoveBy(Point p)
    {
      /* Входной параметр это радиус-вектор перемещения */
      Point old = new Point(location.X,location.Y);
      Location = new Point(X + p.X,Y + p.Y);
    }

    public void MoveBy()
    {
      /* Входной параметр это радиус-вектор перемещения */
      Point old = new Point(location.X,location.Y);
      Location = new Point(X,Y);
    }

    /* Абстрактые методы класса */

    private void OnPaint(PaintEventArgs e){
      if(Paint != null)
      {
        Paint(this,e);
      }
    }

    public virtual bool Intersect(Point _point)
    {
      return HitRegion.IsVisible(_point);
    }

    public virtual bool Intersect(Rectangle _rectangle)
    {
      return HitRegion.IsVisible(_rectangle);
    }

    public virtual bool Intersect(Region _region)
    {
      /*Region tmp = new Region(HitRegion);
      tmp.Intersect(_region);
      return tmp.IsEmpty;*/
      return false;
    }

    protected abstract void UpdateHitRegion();

    public abstract void Draw(object sender, PaintEventArgs e);

    public abstract Point GetPilon(Point from);
    
    protected virtual Point GetPilon(Point from, NetCanvas on)
    {
      Graphics g;
      Point pilon = new Point();
      if (on != null)
      {
        g = on.CreateGraphics();
        Region reg = new Region();
        reg = HitRegion.Clone();
        Pen greenPen = new Pen(Color.Black, 1);
        GraphicsPath gp = new GraphicsPath();
        Rectangle rect = new Rectangle();

        /*Если не посчитается, просто вернём центр*/
        pilon.X = Center.X;
        pilon.Y = Center.Y;

        if(from != Center)
        {
          gp.AddLine(from,Center);
          gp.Widen(greenPen);
          reg.Intersect(gp);
          RectangleF bounds = reg.GetBounds(g);
          rect = Rectangle.Ceiling(bounds);
          if(from.X <= Center.X)
          {
            if(from.Y <= Center.Y)
            {
              pilon.X = rect.Left;
              pilon.Y = rect.Top;
            }
            else
            {
              pilon.X = rect.Left;
              pilon.Y = rect.Bottom;
            }
          }
          else
          {
            if(from.Y <= Center.Y)
            {
              pilon.X = rect.Right;
              pilon.Y = rect.Top;
            }
            else
            {
              pilon.X = rect.Right;
              pilon.Y = rect.Bottom;
            }
          }
          g.Dispose();
        }
      }
      else
      {
        pilon.X = Center.X;
        pilon.Y = Center.Y;
      }
      return pilon;
    }

    protected void PaintRetranslator(object sender, PaintEventArgs args)
    {
      OnPaint(args);
    }

    protected void OnMove(MoveEventArgs args)
    {
      UpdateHitRegion();
      if(Move != null)
      {
        Move(this,args);
      }
      OnChange(new EventArgs());
    }

    protected void OnResize(ResizeEventArgs args)
    {
      UpdateHitRegion();
      if(Resize != null)
      {
        Resize(this,args);
      }
      OnChange(new EventArgs());
    }

    /*Вся внутренняя подготовка перед удалением элемента сети*/
    public virtual void PrepareToDeletion()
    {
      /*Отпишемся от всех событый*/
    }

    protected static Pen ArrowedBlackPenFactory()
    {
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
