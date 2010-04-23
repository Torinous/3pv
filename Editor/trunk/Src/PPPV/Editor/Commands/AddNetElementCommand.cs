using System;
using System.Drawing;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class AddNetElementCommand : Command
  {
    //Данные
    private NetElement ne;
    
    public NetElement Element
    {
      get
      {
        return ne;
      }
      set
      {
        ne = value;
      }
    }

    //Конструктор
    public AddNetElementCommand(PetriNet n, NetElement _ne)
    {
      Net = n;
      Element = _ne;
      Name = "Добавить элемент к сети";
      Description = "Команда добавляющая к заданной сети элемент";
    }

    public AddNetElementCommand(PetriNet n)
    {
      Net = n;
      Name = "Добавить элемент к сети";
      Description = "Команда добавляющая к заданной сети элемент";
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
