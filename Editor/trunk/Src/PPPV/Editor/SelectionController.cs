using System.Drawing;
using System.Windows.Forms;


using PPPV.Net;

namespace PPPV.Editor
{
  public class SelectionController
  {




    //Конструктор
    public SelectionController()
    {

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

