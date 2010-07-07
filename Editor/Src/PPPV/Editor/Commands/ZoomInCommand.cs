using System;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;

namespace PPPV.Editor.Commands
{
  public class ZoomInCommand : Command
  {
    //Данные

    //Конструктор
    public ZoomInCommand()
    {
      Name = "Увеличить";
      Description = "Увеличить";
      ShortcutKeys = Keys.Control | Keys.Up;
      Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PPPV.Resources.Zoom in.png"), true);
    }

    //Методы
    public override void Execute()
    {
      PetriNet p = EditorApplication.Instance.ActiveNet;
      if(p != null)
      {
        if(p.Canvas.ScaleAmount < 10.0F)
          p.Canvas.ScaleAmount += 0.1F;
        p.Canvas.Refresh();
      }
    }

    public override void UnExecute()
    {
      
    }
  }
}
