using System;
using System.Collections;


using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class MacroCommand : Command
  {
    //Данные
    private ArrayList commands;
    
    
    //Конструктор
    public MacroCommand()
    {
      commands = new ArrayList(5);
    }

    //Методы
    public void Add(Command c)
    {
      commands.Add(c);
    }

    public void Remove(Command c)
    {
      commands.Remove(c);
    }

    public override void Execute()
    {
      foreach(Command c in commands)
        c.Execute();
    }

    public override void UnExecute()
    {
      for(int i = commands.Count-1;i>=0;i--)
        (commands[i] as Command).UnExecute();
    }
  }
}
