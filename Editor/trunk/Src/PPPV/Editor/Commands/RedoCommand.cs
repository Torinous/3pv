using System;
using System.Drawing;
using System.Reflection;
using System.Reflection;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class RedoCommand : Command
  {
    //Данные

    //Конструктор
    public RedoCommand()
    {
      Name = "Повтор";
      Description = "Повтор последнего отменённого действия";
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
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Redo.png"), true);
    }
  }
}
