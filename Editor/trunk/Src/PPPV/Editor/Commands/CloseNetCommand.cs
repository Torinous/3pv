using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class CloseNetCommand : Command
  {
    //Данные

    //Конструктор
    public CloseNetCommand()
    {
      Name = "Закрыть";
      Description = "Закрыть сеть";
      ShortcutKeys = Keys.Control | Keys.W;
    }
    //Методы
    public override void Execute()
    {
      EditorApplication app = EditorApplication.Instance;
      app.CloseNet(app.ActiveNet);
    }

    public override void UnExecute()
    {
    }
    
    public override Image GetPictogram()
    {
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Close.png"), true);
    }
  }
}
