namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.ApplicationFramework.Commands;

   public class SaveCommand : EditorInterfaceCommand
   {
      public SaveCommand()
      {
         this.Name = "Сохранить";
         this.Description = "Сохранить сеть в файл";
         this.ShortcutKeys = Keys.Control | Keys.S;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Save.png"), true);
      }

      public override void Execute()
      {
         MainForm mainForm = MainForm.Instance;
         if (mainForm.ActiveNet != null)
         {
            mainForm.ActiveNet.SaveNet();
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