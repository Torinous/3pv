using System;
using System.Drawing;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class AddTransitionCommand : NetCommand
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
    public AddTransitionCommand(PetriNet net, Point position)
    {
      Net = net;
      Position = position;
      Name = "Добавить Переход к сети";
      Description = "Команда добавляющая к заданной сети Переход по заданным координатам";
    }
    
    //Методы
    public override void Execute()
    {
      Net.ElementPortal = new Transition(new Point(Position.X, Position.Y));
    }

    public override void Unexecute()
    {
    }
  }
}
