using System;
using System.Drawing;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class AddNetElementCommand : ElementCommand
  {
    //Данные

    //Конструктор
    public AddNetElementCommand()
    {
      Name = "Добавить элемент к сети";
      Description = "Команда добавляющая к заданной сети элемент";
      Pictogram = null;
    }

    public AddNetElementCommand(PetriNet n):this()
    {
      Net = n;
    }
    
    public AddNetElementCommand(NetElement _ne):this(_ne.ParentNet)
    {
      Element = _ne;
    }
    
    //Методы
    public override void Execute()
    {
      Net.ElementPortal = Element;
    }

    public override void UnExecute()
    {
    }
  }
}
