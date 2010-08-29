namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;

   public class NewNetCommand : Command
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
         EditorApplication app = EditorApplication.Instance;
         app.NewNet(null, null);
      }

      public override void Unexecute()
      {
      }
   }
}
