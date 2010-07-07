using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class NewNetCommand : Command
  {
    //Данные

    //Конструктор
    public NewNetCommand()
    {
      Name = "Создать";
      Description = "Создать новую сеть";
      ShortcutKeys = Keys.Control | Keys.N;
      Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PPPV.Resources.New.png"), true);
    }
    //Методы
    public override void Execute()
    {
      EditorApplication app = EditorApplication.Instance;
      app.NewNet(null, null);
    }

    public override void UnExecute()
    {
    }
  }
}
