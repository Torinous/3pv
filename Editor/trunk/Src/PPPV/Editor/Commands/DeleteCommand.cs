using System;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;

namespace PPPV.Editor.Commands
{
  public class DeleteCommand : Command
  {
    //Данные

    //Конструктор
    public DeleteCommand()
    {
      Name = "Удалить";
      Description = "Удалить выделенный элемент сети";
      ShortcutKeys = Keys.Delete;
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
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Delete.png"), true);
    }
  }
}
