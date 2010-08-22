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
         commands = new ArrayList(5);
      }

      public void Add(Command command)
      {
         commands.Add(command);
      }

      public void Remove(Command command)
      {
         commands.Remove(command);
      }

      public override void Execute()
      {
         foreach(Command c in commands)
            c.Execute();
      }

      public override void Unexecute()
      {
         for(int i = commands.Count-1;i>=0;i--)
           (commands[i] as Command).Unexecute();
      }
   }
}
