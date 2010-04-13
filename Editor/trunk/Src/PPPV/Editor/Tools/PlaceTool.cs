using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Editor.Commands;

namespace PPPV.Editor.Tools
{
  public class PlaceTool : Tool
  {
    /*Данные*/
    private static PlaceTool instance;

    /*Акцессоры доступа*/
    public static PlaceTool Instance
    {
      get
      {
         if (instance == null)
         {
            instance = new PlaceTool();
         }
         return instance;
      }
    }
    
    //cons
    private PlaceTool()
    {
      Name = "Позиция";
      Description = "Инструмент созадния позиций сети";
      ShortcutKeys = Keys.Control|Keys.Shift|Keys.P;
    }
    
    /*Методы*/
    public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      if(args.Button == MouseButtons.Left)
      {
        Command c = new AddPlaceCommand((sender as Editor.NetCanvas).Net, new Point(args.X, args.Y));
        c.Execute();
        (sender as Editor.NetCanvas).Invalidate();
      }
      base.HandleMouseDown(sender, args);
    }

    public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      if(args.Button == MouseButtons.Left)
      {
        //if(IsActive)
        {
           /*Point startPoint = new Point(selectFrom.X, selectFrom.Y);
           if(arg.X < selectFrom.X)
              startPoint.X = arg.X;
           if(arg.Y < selectFrom.Y)
              startPoint.Y = arg.Y;
           selectedRectangle.Location = startPoint;
           selectedRectangle.Size = new Size(Math.Abs(arg.X-selectFrom.X),Math.Abs(arg.Y-selectFrom.Y));
           ((NetCanvas)sender).Invalidate();
           selectedObjects = ((NetCanvas)sender).Net.NetElementUnder(SelectedRectangle);*/
        }
        //else
        {
           Net.NetElement tmpEl;
           /*Point delta = new Point(arg.X - lastMouseDownPoint.X,arg.Y - lastMouseDownPoint.Y);
        
           for(int i=0;i<selectedObjects.Count;++i) {
              ((NetElement)selectedObjects[i]).MoveBy(delta);
           }
           ((NetCanvas)sender).Invalidate();
           lastMouseDownPoint.X = arg.X;
           lastMouseDownPoint.Y = arg.Y;*/
        }
      }
      base.HandleMouseMove(sender, args);
    }
    
    public override void HandleMouseUp(object sender, System.Windows.Forms.MouseEventArgs args)
    {
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

    public override Image GetPictogram()
    {
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Place.png"), true);      
    }
  }
}
