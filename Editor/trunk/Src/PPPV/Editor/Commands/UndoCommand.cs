using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class UndoCommand : Command
  {
    //Данные

    //Конструктор
    public UndoCommand()
    {
      Name = "Отмена";
      Description = "Отмена последнего выполненного действия";
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
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Undo.png"), true);
    }
  }
}
