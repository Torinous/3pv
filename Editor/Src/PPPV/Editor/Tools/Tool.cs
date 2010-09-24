namespace Pppv.Editor.Tools
{
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor;
   using Pppv.Editor.Commands;
   using Pppv.Net;

   public abstract class Tool
   {
      private PetriNetWrapper eventSourceNet;

      protected Tool()
      {
      }

      public PetriNetWrapper EventSourceNet
      {
         get
         {
            return this.eventSourceNet;
         }

         set
         {
            if (this.eventSourceNet != null)
            {
               this.eventSourceNet.Canvas.CanvasMouseClick -= this.CanvasMouseClickHandler;
               this.eventSourceNet.Canvas.CanvasMouseMove -= this.CanvasMouseMoveHandler;
               this.eventSourceNet.Canvas.CanvasMouseDown -= this.CanvasMouseDownHandler;
               this.eventSourceNet.Canvas.CanvasMouseUp -= this.CanvasMouseUpHandler;
               this.eventSourceNet.Canvas.KeyDown -= this.CanvasKeyDownHandler;
            }

            this.eventSourceNet = value;
            if (this.eventSourceNet != null)
            {
               this.eventSourceNet.Canvas.CanvasMouseClick += this.CanvasMouseClickHandler;
               this.eventSourceNet.Canvas.CanvasMouseMove += this.CanvasMouseMoveHandler;
               this.eventSourceNet.Canvas.CanvasMouseDown += this.CanvasMouseDownHandler;
               this.eventSourceNet.Canvas.CanvasMouseUp += this.CanvasMouseUpHandler;
               this.eventSourceNet.Canvas.KeyDown += this.CanvasKeyDownHandler;
            }
         }
      }

      public virtual string Name { get; set; }

      public virtual string Description { get; set; }

      public virtual Keys ShortcutKeys { get; set; }

      public virtual Image Pictogram { get; set; }

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