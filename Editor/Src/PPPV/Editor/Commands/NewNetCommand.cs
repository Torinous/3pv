namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;

   public class NewNetCommand : Command
   {
      //Данные

      //Конструктор
      public NewNetCommand()
      {
         Name = "Создать";
         Description = "Создать новую сеть";
         ShortcutKeys = Keys.Control | Keys.N;
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.New.png"), true);
      }
      //Методы
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
