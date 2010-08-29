namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;

   public class CloseNetCommand : Command
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
         EditorApplication app = EditorApplication.Instance;
         app.CloseNet(app.ActiveNet);
      }

      public override void Unexecute()
      {
      }
   }
}