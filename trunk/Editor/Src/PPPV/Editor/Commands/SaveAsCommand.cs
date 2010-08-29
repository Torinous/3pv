namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;

   public class SaveAsCommand : Command
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
         EditorApplication app = EditorApplication.Instance;
         if (app.ActiveNet != null)
         {
            app.ActiveNet.SaveNetAs();
         }
      }

      public override void Unexecute()
      {
      }
   }
}