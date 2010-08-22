namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Diagnostics;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Utils;

   public class PasteCommand : Command
   {
      //Данные

      //Конструктор
      public PasteCommand()
      {
         Name = "Вставить";
         Description = "Вставить элемент сети из буфера обмена";
         ShortcutKeys = Keys.Control | Keys.P;
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Paste.png"), true);
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
