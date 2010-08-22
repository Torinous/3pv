namespace PPPV.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Diagnostics;
   using System.Windows.Forms;
   using PPPV.Net;
   using PPPV.Utils;

   public class QuitCommand : Command
   {
      //Данные

      //Конструктор
      public QuitCommand()
      {
         Name = "Выход";
         Description = "Завершение работы приложения 3PV:Editor";
         ShortcutKeys = Keys.Control | Keys.Q;
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PPPV.Resources.Exit.png"), true);
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
