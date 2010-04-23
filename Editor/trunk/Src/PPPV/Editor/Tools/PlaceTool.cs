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
      Description = "Инструмент создания позиций сети";
      ShortcutKeys = Keys.Control|Keys.Shift|Keys.P;
    }
    
    /*Методы*/
    public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
    {
      if(args.Button == MouseButtons.Left)
      {
        AddNetElementCommand c = new AddNetElementCommand((sender as Editor.NetCanvas).Net);
        c.Element = new Place(new Point(args.X, args.Y));
        c.Execute();
        (sender as Editor.NetCanvas).Invalidate();
      }
      base.HandleMouseDown(sender, args);
    }

    public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
    {
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
