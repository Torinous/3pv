using System;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using PPPV.Net;

namespace PPPV.Editor.Tools
{
  public class PointerTool : Tool
  {
    /*Данные*/
    private Point lastMouseDownPoint;
    private bool isActive = false;
    private Rectangle selectedRectangle;
    private SelectedNetObjectsList selectedObjects;
    private Point selectFrom;
    private static PointerTool instance;

    /*Акцессоры доступа*/
    public static PointerTool Instance
    {
      get
      {
         if (instance == null)
         {
            instance = new PointerTool();
         }
         return instance;
      }
    }

    public Rectangle SelectedRectangle
    {
      get
      {
        return selectedRectangle;
      }
      private set
      {
        selectedRectangle = value;
      }
    }
    //cons
    private PointerTool()
    {
      Name = "Указатель";
      Description = "Инструмент выбора и перемещения элементов сети";
      ShortcutKeys = Keys.Control|Keys.Shift|Keys.M;
      SelectedRectangle = new Rectangle( new Point(0,0), new Size(0,0));
      selectedObjects = new SelectedNetObjectsList(20, this);
    }
    
    /*Методы*/
    public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      lastMouseDownPoint = new Point(args.X, args.Y);
      if(args.Button == MouseButtons.Left)
      {
        NetElement tmp = ((NetCanvas)sender).Net.NetElementUnder(new Point(args.X, args.Y));
        if(tmp!=null)
        {
           if(!selectedObjects.Contains(tmp))
           {
              selectedObjects.Clear();
              selectedObjects.Add(tmp);
           }
        }
        else
        {
           selectedObjects.Clear();
           isActive = true;
           selectFrom = new Point(args.X, args.Y);
        }
        ((NetCanvas)sender).Invalidate();
      }
      ((NetCanvas)sender).Paint += DrawSelectionRegion;
      base.HandleMouseDown(sender, args);
    }

    public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      if(args.Button == MouseButtons.Left)
      {
        if(isActive)
        {
           Point startPoint = new Point(selectFrom.X, selectFrom.Y);
           if(args.X < selectFrom.X)
              startPoint.X = args.X;
           if(args.Y < selectFrom.Y)
              startPoint.Y = args.Y;
           selectedRectangle.Location = startPoint;
           selectedRectangle.Size = new Size(Math.Abs(args.X-selectFrom.X), System.Math.Abs(args.Y-selectFrom.Y));
           selectedObjects.Clear();
           selectedObjects.AddRange(((NetCanvas)sender).Net.NetElementUnder(SelectedRectangle));
           ((NetCanvas)sender).Invalidate();
        }
        else
        {
           Net.NetElement tmpEl;
           Point delta = new Point(args.X - lastMouseDownPoint.X, args.Y - lastMouseDownPoint.Y);
        
           for(int i=0;i<selectedObjects.Count;++i) {
              ((NetElement)selectedObjects[i]).MoveBy(delta);
           }
           ((NetCanvas)sender).Invalidate();
           lastMouseDownPoint.X = args.X;
           lastMouseDownPoint.Y = args.Y;
        }
      }
      base.HandleMouseMove(sender, args);
    }
    
    public override void HandleMouseUp(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      selectedRectangle.Size = new Size(0, 0);
      ((NetCanvas)sender).Paint -= DrawSelectionRegion;
      isActive = false;
      base.HandleMouseUp(sender, args);
    }

    public override void HandleMouseClick(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      base.HandleMouseClick(sender, args);
    }
    
    public override void HandleKeyDown( object sender, KeyEventArgs args )
    {
      base.HandleKeyDown(sender, args);
    }

    public void DrawSelectionRegion(object sender, PaintEventArgs e)
    {
      if(isActive)
      {
        Pen RedPen = new Pen(Color.Red, 1);
        Graphics dc = e.Graphics;
        dc.SmoothingMode = SmoothingMode.HighQuality;
        dc.DrawRectangle(RedPen, SelectedRectangle);
      }
    }

    public void DrawSelectionMarker(object sender, PaintEventArgs e)
    {
      Pen RedPen = new Pen(Color.Red, 1);
      Graphics dc = e.Graphics;
      RectangleF tmp = ((NetElement)sender).HitRegion.GetBounds(dc);
      dc.DrawRectangle(RedPen, new Rectangle((int)tmp.X, (int)tmp.Y, (int)tmp.Width, (int)tmp.Height) );
    }
    
    public override Image GetPictogram()
    {
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pointer.png"), true);      
    }
  }
}
