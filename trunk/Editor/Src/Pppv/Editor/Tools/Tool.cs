namespace Pppv.Editor.Tools
{
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor;
   using Pppv.Editor.Commands;
   using Pppv.Net;

   public abstract class Tool
   {
      private PetriNetGraphical eventSourceNet;

      protected Tool(PetriNetGraphical net)
      {
         this.EventSourceNet = net;
      }

      public PetriNetGraphical EventSourceNet
      {
         get { return this.eventSourceNet; }
         set { this.eventSourceNet = value; }
      }

      public virtual string Name { get; set; }

      public virtual string Description { get; set; }

      public virtual Keys ShortcutKeys { get; set; }

      public virtual Image Pictogram { get; set; }

      public void ConnectEvents()
      {
         this.EventSourceNet.Canvas.CanvasMouseClick += this.CanvasMouseClickHandler;
         this.EventSourceNet.Canvas.CanvasMouseMove += this.CanvasMouseMoveHandler;
         this.EventSourceNet.Canvas.CanvasMouseDown += this.CanvasMouseDownHandler;
         this.EventSourceNet.Canvas.CanvasMouseUp += this.CanvasMouseUpHandler;
         this.EventSourceNet.Canvas.KeyDown += this.CanvasKeyDownHandler;
      }
      
      public void DisconnectEvents()
      {
         this.EventSourceNet.Canvas.CanvasMouseClick -= this.CanvasMouseClickHandler;
         this.EventSourceNet.Canvas.CanvasMouseMove -= this.CanvasMouseMoveHandler;
         this.EventSourceNet.Canvas.CanvasMouseDown -= this.CanvasMouseDownHandler;
         this.EventSourceNet.Canvas.CanvasMouseUp -= this.CanvasMouseUpHandler;
         this.EventSourceNet.Canvas.KeyDown -= this.CanvasKeyDownHandler;
      }

      protected virtual void HandleMouseDown(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
      }

      protected virtual void HandleMouseMove(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
      }

      protected virtual void HandleMouseUp(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
      }

      protected virtual void HandleMouseClick(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
      {
         /*Контекстное меню по умолчанию показывают все инструменты*/
         if (args.Button == MouseButtons.Right)
         {
            ShowContextMenuCommand c = new ShowContextMenuCommand(args.Location);
            c.Net = canvas.Net;
            c.Execute();
         }
      }

      protected virtual void HandleKeyDown(NetCanvas canvas, KeyEventArgs args)
      {
      }

      private void CanvasMouseDownHandler(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         this.HandleMouseDown(sender as NetCanvas, args);
      }

      private void CanvasMouseMoveHandler(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         this.HandleMouseMove(sender as NetCanvas, args);
      }

      private void CanvasMouseUpHandler(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         this.HandleMouseUp(sender as NetCanvas, args);
      }

      private void CanvasMouseClickHandler(object sender, System.Windows.Forms.MouseEventArgs args)
      {
         this.HandleMouseClick(sender as NetCanvas, args);
      }

      private void CanvasKeyDownHandler(object sender, KeyEventArgs args)
      {
         this.HandleKeyDown(sender as NetCanvas, args);
      }
   }
}