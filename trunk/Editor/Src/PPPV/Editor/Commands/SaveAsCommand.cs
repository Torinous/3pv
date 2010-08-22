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
         Name = "Сохранить как...";
         Description = "Сохранить сеть в файл с заданным именем";
         ShortcutKeys = Keys.Control| Keys.Shift | Keys.S;
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Save as.png"), true);
      }

      public override void Execute()
      {
         EditorApplication app = EditorApplication.Instance;
         if(app.ActiveNet != null)
            app.ActiveNet.SaveNetAs();
      }

      public override void Unexecute()
      {
      }
   }
}
