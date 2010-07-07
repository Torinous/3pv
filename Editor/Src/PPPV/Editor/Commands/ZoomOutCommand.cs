using System;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;

namespace PPPV.Editor.Commands
{
  public class ZoomOutCommand : Command
  {
    //Данные

    //Конструктор
    public ZoomOutCommand()
    {
      Name = "Уменьшить";
      Description = "Уменьшение";
      ShortcutKeys = Keys.Control | Keys.Down;
      Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PPPV.Resources.Zoom out.png"), true);
    }

    //Методы
    public override void Execute()
    {
      PetriNet p = EditorApplication.Instance.ActiveNet;
      if(p != null)
      {
        if(p.Canvas.ScaleAmount > 0.11F)
          p.Canvas.ScaleAmount -= 0.1F;
        p.Canvas.Refresh();
      }
    }

    public override void UnExecute()
    {
      
    }
  }
}
