using System;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;

namespace PPPV.Editor.Commands
{
  public class QuitCommand : Command
  {
    //Данные

    //Конструктор
    public QuitCommand()
    {
      Name = "Выход";
      Description = "Завершение работы приложения 3PV:Editor";
      ShortcutKeys = Keys.Control | Keys.Q;
    }
    
    //Методы
    public override void Execute()
    {
      EditorApplication.Instance.Quit();
    }

    public override void UnExecute()
    {
      
    }
    
    public override Image GetPictogram()
    {
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Exit.png"), true);
    }
  }
}
