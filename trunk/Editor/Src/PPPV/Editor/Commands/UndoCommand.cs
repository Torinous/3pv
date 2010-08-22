namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;

   public class UndoCommand : Command
   {
      //Данные

      //Конструктор
      public UndoCommand()
      {
         Name = "Отмена";
         Description = "Отмена последнего выполненного действия";
         Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Undo.png"), true);
      }
      //Методы
      public override void Execute()
      {

      }

      public override void Unexecute()
      {
         
      }
   }
}
