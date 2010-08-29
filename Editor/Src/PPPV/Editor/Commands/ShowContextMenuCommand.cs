namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor;
   using Pppv.Net;

   public class ShowContextMenuCommand : Command
   {
      private NetCanvas canvas;
      private Point position;

      public ShowContextMenuCommand()
      {
         this.Name = "Контекстное меню";
         this.Description = "Команда вызывающая контекстное меню для элемента сети";
         this.Pictogram = null;
      }

      public ShowContextMenuCommand(NetCanvas canvas, Point position) : this()
      {
         this.Canvas = canvas;
         this.Position = position;
      }

      public Point Position
      {
         get { return this.position; }
         set { this.position = value; }
      }

      public NetCanvas Canvas
      {
         get { return this.canvas; }
         set { this.canvas = value; }
      }

      public override void Execute()
      {
         PetriNet n = this.Canvas.Net;
         NetElement contextMenuTarget = n.NetElementUnder(this.Position);
         ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
         EditorContextToolStripMenuItem item = new EditorContextToolStripMenuItem(new EditNetElementCommand(n, contextMenuTarget));
         contextMenuStrip.Items.Add(item);
         item.CheckEnabled();
         item = new EditorContextToolStripMenuItem(new DeleteCommand(n, contextMenuTarget));
         contextMenuStrip.Items.Add(item);
         item.CheckEnabled();
         contextMenuStrip.Show(this.Canvas.PointToScreen(this.Position));
      }

      public override void Unexecute()
      {
      }
   }
}