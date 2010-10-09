namespace Pppv.Editor.Commands
{
   using System;
   using System.Collections;

   using Pppv.Net;

   public class MacroCommand : Command
   {
      private ArrayList commands;

      public MacroCommand()
      {
         this.commands = new ArrayList(5);
      }

      public void Add(Command command)
      {
         this.commands.Add(command);
      }

      public void Remove(Command command)
      {
         this.commands.Remove(command);
      }

      public override void Execute()
      {
         foreach (Command c in this.commands)
         {
            c.Execute();
         }
      }

      public override void Unexecute()
      {
         for (int i = this.commands.Count - 1; i >= 0; i--)
         {
           (this.commands[i] as Command).Unexecute();
         }
      }
   }
}
