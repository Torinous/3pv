using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Pppv.Net;

namespace Pppv.Editor.Commands
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
      Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Close.png"), true);
    }

    //Методы
    public override void Execute()
    {
      EditorApplication app = EditorApplication.Instance;
      app.CloseNet(app.ActiveNet);
    }

    public override void Unexecute()
    {
    }
  }
}
