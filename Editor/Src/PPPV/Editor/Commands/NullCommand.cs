using System;
using System.Drawing;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  /*Ничего не делающая команда заглушка*/
  public class NullCommand : Command
  {
    //Данные

    //Конструктор
    public NullCommand()
    {
      Name = "Null команда";
      Description = "Просто заглушка, ничего не делающая команда";
    }
    //Методы
    public override void Execute()
    {
      
    }

    public override void UnExecute()
    {
      
    }
  }
}
