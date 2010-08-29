namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;

   public class SaveCommand : Command
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
         EditorApplication app = EditorApplication.Instance;
         if (app.ActiveNet != null)
         {
            app.ActiveNet.SaveNet();
         }
      }

      public override void Unexecute()
      {
      }
   }
}