namespace Pppv.Editor.Tools
{
	using System.Drawing;
	using System.Windows.Forms;

	using Pppv.Editor;
	using Pppv.Editor.Commands;
	using Pppv.Net;

	public abstract class Tool
	{
		protected Tool()
		{
		}

		public void ConnectEvents(PetriNetGraphical net)
		{
			net.Canvas.CanvasMouseClick += this.CanvasMouseClickHandler;
			net.Canvas.CanvasMouseMove += this.CanvasMouseMoveHandler;
			net.Canvas.CanvasMouseDown += this.CanvasMouseDownHandler;
			net.Canvas.CanvasMouseUp += this.CanvasMouseUpHandler;
			net.Canvas.KeyDown += this.CanvasKeyDownHandler;
		}
		
		public void DisconnectEvents(PetriNetGraphical net)
		{
			net.Canvas.CanvasMouseClick -= this.CanvasMouseClickHandler;
			net.Canvas.CanvasMouseMove -= this.CanvasMouseMoveHandler;
			net.Canvas.CanvasMouseDown -= this.CanvasMouseDownHandler;
			net.Canvas.CanvasMouseUp -= this.CanvasMouseUpHandler;
			net.Canvas.KeyDown -= this.CanvasKeyDownHandler;
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
				ContextMenuShower contextMenu = new ContextMenuShower(canvas.Net.GetShapeUnder(args.Location));
				contextMenu.Show(canvas.PointToScreen(args.Location));
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