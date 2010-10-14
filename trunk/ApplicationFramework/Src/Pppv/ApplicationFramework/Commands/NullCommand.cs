namespace Pppv.ApplicationFramework.Commands
{
   using System;
   using System.Drawing;

   public class NullCommand : InterfaceCommand
   {
      public NullCommand()
      {
         this.Name = "Null команда";
         this.Description = "Просто заглушка, ничего не делающая команда";
      }

      public override void Execute()
      {
      }

      public override void Unexecute()
      {
      }
   }
}