using System;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;

namespace PPPV.Editor.Commands
{
  public class CopyCommand : Command
  {
    //Данные

    //Конструктор
    public CopyCommand()
    {
      Name = "Копировать";
      Description = "Копировать выделенный элемент сети";
      ShortcutKeys = Keys.Control | Keys.C;
    }

    //Методы
    public override void Execute()
    {
      
    }

    public override void UnExecute()
    {
      
    }
    
    public override Image GetPictogram()
    {
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Copy.png"), true);
    }
  }
}
