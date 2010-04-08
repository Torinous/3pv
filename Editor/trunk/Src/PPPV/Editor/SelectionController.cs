using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using PPPV.Net;

namespace PPPV.Editor
{
  public class SelectionController
  {
    private Rectangle selectedRectangle;
    private ArrayList selectedObjects;
    private Point lastMouseDownPoint;
    private bool isActive = false;
    private Point selectFrom;

    public bool IsActive
    {
      get
      {
        return isActive;
      }
      set
      {
        isActive = value;
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

    //Конструктор
    public SelectionController()
    {
      SelectedRectangle = new Rectangle( new Point(0,0), new Size(0,0));
      selectedObjects = new ArrayList(20);
    }

    public void Draw(object sender, PaintEventArgs e)
    {
      Pen RedPen = new Pen(Color.Red, 1);
      Graphics dc = e.Graphics;
      if(IsActive)
      {
        dc.SmoothingMode = SmoothingMode.HighQuality;
        dc.DrawRectangle(RedPen, SelectedRectangle);
      }
      else
      {
        /*int i;
        for(i=0;i<selectedObjects.Count;++i)
        {
           RectangleF tmp = ((NetElement)selectedObjects[i]).HitRegion.GetBounds(dc);
           dc.DrawRectangle(RedPen, new Rectangle((int)tmp.X, (int)tmp.Y, (int)tmp.Width, (int)tmp.Height) );
        }
        */
      }
    }

    public void CanvasMouseMoveHandler(object sender, CanvasMouseEventArgs arg)
    {
      ToolController tc = ToolController.Instance;
      if(tc.CurrentTool != null)
        tc.CurrentTool.HandleMouseMove(sender, arg);
    }

    public void CanvasMouseDownHandler(object sender, CanvasMouseEventArgs arg)
    {
      ToolController tc = ToolController.Instance;
      if(tc.CurrentTool != null)
        tc.CurrentTool.HandleMouseDown(sender, arg);
    }

    public void CanvasMouseUpHandler(object sender, CanvasMouseEventArgs arg)
    {
      ToolController tc = ToolController.Instance;
      if(tc.CurrentTool != null)
        tc.CurrentTool.HandleMouseUp(sender, arg);
    }
  }
}

