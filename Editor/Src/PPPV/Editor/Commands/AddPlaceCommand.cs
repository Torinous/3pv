using System;
using System.Drawing;
using System.Reflection;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class AddPlaceCommand : NetCommand
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
    public AddPlaceCommand(PetriNet n, Point p)
    {
      Net = n;
      Position = p;
      Name = "Добавить Позицию к сети";
      Description = "Команда добавляющая к заданной сети Позицию по заданным координатам";
    }
    //Методы
    public override void Execute()
    {
      Net.ElementPortal = new Place(new Point(Position.X, Position.Y));
    }

    public override void UnExecute()
    {
    }
  }
}
