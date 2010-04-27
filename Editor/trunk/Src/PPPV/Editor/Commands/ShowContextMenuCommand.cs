using System;
using System.Drawing;
using System.Windows.Forms;

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
    public ShowContextMenuCommand()
    {
      Name = "Контекстное меню";
      Description = "Команда вызывающая контекстное меню для элемента сети";
      Pictogram = null;
    }

    public ShowContextMenuCommand(NetCanvas c, Point pos):this()
    {
      Canvas = c;
      Position = pos;
    }

    //Методы
    public override void Execute()
    {
      PetriNet n = Canvas.Net;
      NetElement contextMenuTarget = n.NetElementUnder(Position);
      ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
      contextMenuStrip.Items.Add( new EditorContextToolStripMenuItem( new EditNetElementCommand(n, contextMenuTarget) ) );
      contextMenuStrip.Items.Add( new EditorContextToolStripMenuItem( new DeleteCommand(n, contextMenuTarget) ) );
      contextMenuStrip.Show(Canvas.PointToScreen(Position));
      
    }

    public override void UnExecute()
    {
      
    }
  }
}
