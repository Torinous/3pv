using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class SaveCommand : Command
  {
    //Данные

    //Конструктор
    public SaveCommand()
    {
      Name = "Сохранить";
      Description = "Сохранить сеть в файл";
      ShortcutKeys = Keys.Control | Keys.S;
    }
    //Методы
    public override void Execute()
    {
      EditorApplication app = EditorApplication.Instance;
      if(app.ActiveNet != null)
        app.ActiveNet.SaveNet();
    }

    public override void UnExecute()
    {
    }
    
    public override Image GetPictogram()
    {
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Save.png"), true);
    }
  }
}
