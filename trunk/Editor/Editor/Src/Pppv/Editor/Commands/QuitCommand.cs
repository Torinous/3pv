namespace Pppv.Editor.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.ApplicationFramework.Commands;
   using Pppv.Net;

   public class QuitCommand : InterfaceCommand
   {
      public QuitCommand()
      {
         this.Name = "Выход";
         this.Description = "Завершение работы приложения 3PV:Editor";
         this.ShortcutKeys = Keys.Control | Keys.Q;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Exit.png"), true);
      }

      public override void Execute()
      {
         MainForm mainForm = MainForm.Instance;
         mainForm.Quit();
      }

      public override void Unexecute()
      {
      }
   }
}
