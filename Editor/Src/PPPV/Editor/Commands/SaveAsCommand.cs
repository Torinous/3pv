using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class SaveAsCommand : Command
  {
    //Данные

    //Конструктор
    public SaveAsCommand()
    {
      Name = "Сохранить как...";
      Description = "Сохранить сеть в файл с заданным именем";
      ShortcutKeys = Keys.Control| Keys.Shift | Keys.S;
      Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Save as.png"), true);
    }
    //Методы
    public override void Execute()
    {
      EditorApplication app = EditorApplication.Instance;
      if(app.ActiveNet != null)
        app.ActiveNet.SaveNetAs();
    }

    public override void UnExecute()
    {
    }
  }
}
