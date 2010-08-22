namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;

   using Pppv.Net;

   public class NullCommand : Command
   {
      //Данные

      //Конструктор
      public NullCommand()
      {
         Name = "Null команда";
         Description = "Просто заглушка, ничего не делающая команда";
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
