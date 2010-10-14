namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.ApplicationFramework.Commands;

   public class CloseNetCommand : EditorInterfaceCommand
   {
      public CloseNetCommand()
      {
         this.Name = "Закрыть";
         this.Description = "Закрыть сеть";
         this.ShortcutKeys = Keys.Control | Keys.W;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Close.png"), true);
      }

      public override void Execute()
      {
         MainForm mainForm = MainForm.Instance;
         if (mainForm != null)
         {
            mainForm.CloseNet(mainForm.ActiveNet);
         }
      }

      public override void Unexecute()
      {
      }

      public override bool CheckEnabled()
      {
         return CheckFormAndActiveNet();
      }
   }
}