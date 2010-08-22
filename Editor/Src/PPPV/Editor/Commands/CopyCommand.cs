namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Diagnostics;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

  public class CopyCommand : Command
  {
    //Данные

    //Конструктор
    public CopyCommand()
    {
      Name = "Копировать";
      Description = "Копировать выделенный элемент сети";
      ShortcutKeys = Keys.Control | Keys.C;
      Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Copy.png"), true);
    }

    //Методы
    public override void Execute()
    {
      
    }

    public override void Unexecute()
    {
      
    }
  }
}
