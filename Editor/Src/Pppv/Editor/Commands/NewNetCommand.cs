namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.ApplicationFramework.Commands;

   public class NewNetCommand : InterfaceCommand
   {
      public NewNetCommand()
      {
         this.Name = "Создать";
         this.Description = "Создать новую сеть";
         this.ShortcutKeys = Keys.Control | Keys.N;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.New.png"), true);
      }

      public override void Execute()
      {
         MainForm mainForm = MainForm.Instance;
         mainForm.NewNet();
      }

      public override void Unexecute()
      {
      }

      public override bool CheckEnabled()
      {
         return true;
      }
   }
}
