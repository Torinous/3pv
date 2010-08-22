namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Diagnostics;
   using System.Windows.Forms;
   using Pppv.Net;
   using Pppv.Utils;

   public class QuitCommand : Command
   {
      //Данные

      //Конструктор
      public QuitCommand()
      {
         Name = "Выход";
         Description = "Завершение работы приложения 3PV:Editor";
         ShortcutKeys = Keys.Control | Keys.Q;
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Exit.png"), true);
      }
      
      //Методы
      public override void Execute()
      {
         EditorApplication.Instance.Quit();
      }

      public override void Unexecute()
      {
         
      }
   }
}
