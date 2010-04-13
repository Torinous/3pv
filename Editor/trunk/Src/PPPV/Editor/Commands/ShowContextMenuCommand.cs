using System;
using System.Drawing;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class ShowContextMenuCommand : Command
  {
    //Данные

    //Конструктор
    public ShowContextMenuCommand()
    {
      Name = "Контекстное меню";
      Description = "Команда вызывающая контекстное меню для элемента сети";
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
