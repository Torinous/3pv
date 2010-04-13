using System;
using System.Drawing;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class AddTransitionCommand : Command
  {
    //Данные
    private Point position;
    
    public Point Position
    {
      get
      {
        return position;
      }
      set
      {
        position = value;
      }
    }

    //Конструктор
    public AddTransitionCommand(PetriNet n, Point p)
    {
      Net = n;
      Position = p;
      Name = "Добавить Переход к сети";
      Description = "Команда добавляющая к заданной сети Переход по заданным координатам";
    }
    
    //Методы
    public override void Execute()
    {
      Net.AddTransition(Position);
    }

    public override void UnExecute()
    {
    }
  }
}
