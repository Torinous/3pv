namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.ApplicationFramework.Commands;
   using Pppv.Net;

   public class SaveAsCommand : EditorInterfaceCommand
   {
      public SaveAsCommand()
      {
         this.Name = "Сохранить как...";
         this.Description = "Сохранить сеть в файл с заданным именем";
         this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Save as.png"), true);
      }

      public override void Execute()
      {
         MainForm mainForm = MainForm.Instance;
         if (mainForm.ActiveNet != null)
         {
            mainForm.ActiveNet.SaveNetAs();
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