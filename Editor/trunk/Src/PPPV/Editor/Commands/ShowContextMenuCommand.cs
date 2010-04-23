using System;
using System.Drawing;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public class ShowContextMenuCommand : Command
  {
    //Данные
    private NetCanvas canvas;
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

    public NetCanvas Canvas
    {
      get
      {
        return canvas;
      }
      set
      {
        canvas = value;
      }
    }

    //Конструктор
    public ShowContextMenuCommand(NetCanvas c, Point pos)
    {
      Name = "Контекстное меню";
      Description = "Команда вызывающая контекстное меню для элемента сети";
      Canvas = c;
      Position = pos;
    }

    //Методы
    public override void Execute()
    {
      ContextMenuController contextMenuController = new ContextMenuController(Canvas);
      PetriNet n = Canvas.Net;
      NetElement contextMenuTarget = n.NetElementUnder(Position);
      contextMenuController.Show( Canvas.PointToScreen(Position), contextMenuTarget, n);
    }

    public override void UnExecute()
    {
      
    }
  }
}
