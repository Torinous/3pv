namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;

   using Pppv.Net;

   public class NullCommand : Command
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