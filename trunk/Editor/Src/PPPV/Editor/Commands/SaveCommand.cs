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
         Name = "Сохранить";
         Description = "Сохранить сеть в файл";
         ShortcutKeys = Keys.Control | Keys.S;
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Save.png"), true);
      }
      //Методы
      public override void Execute()
      {
         EditorApplication app = EditorApplication.Instance;
         if(app.ActiveNet != null)
            app.ActiveNet.SaveNet();
      }

      public override void Unexecute()
      {
      }
   }
}
