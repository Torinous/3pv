using System;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;

namespace PPPV.Editor.Commands
{
  public class PasteCommand : Command
  {
    //Данные

    //Конструктор
    public PasteCommand()
    {
      Name = "Вставить";
      Description = "Вставить элемент сети из буфера обмена";
      ShortcutKeys = Keys.Control | Keys.P;
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
      return Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Paste.png"), true);
    }
  }
}
