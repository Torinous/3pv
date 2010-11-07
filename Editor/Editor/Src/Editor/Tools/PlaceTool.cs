namespace Pppv.Editor.Tools
{
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Editor.Commands;
	using Pppv.Net;

	public class PlaceTool : Tool
	{
		protected override void HandleMouseDown(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			if (args.Button == MouseButtons.Left)
			{
				AddShapeCommand command = new AddShapeCommand(canvas.Net);
				command.Shape = canvas.Net.CreateShapeForNetElement(new Place(args.Location));
				command.Execute();
			}

			base.HandleMouseDown(canvas, args);
		}

		protected override void HandleMouseMove(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			base.HandleMouseMove(canvas, args);
		}
		
		protected override void HandleMouseUp(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			base.HandleMouseUp(canvas, args);
		}
		
		protected override void HandleMouseClick(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			base.HandleMouseClick(canvas, args);
		}

		protected override void HandleKeyDown(NetCanvas canvas, KeyEventArgs args)
		{
			base.HandleKeyDown(canvas, args);
		}
	}
}