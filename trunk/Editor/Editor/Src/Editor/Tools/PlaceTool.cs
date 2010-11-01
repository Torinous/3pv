namespace Pppv.Editor.Tools
{
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Editor.Commands;
	using Pppv.Net;

	public class PlaceTool : Tool
	{
		private static string name = "Позиция";
		private static string description = "Инструмент создания позиций сети";
		private static Keys shortcutKeys = Keys.Control | Keys.Shift | Keys.P;
		private static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Place.png"), true);

		public PlaceTool(PetriNetGraphical net) : base(net)
		{
		}

		public override string Name
		{
			get { return name; }
			set { name = value; }
		}

		public override string Description
		{
			get { return description; }
			set { description = value; }
		}

		public override Keys ShortcutKeys
		{
			get { return shortcutKeys; }
			set { shortcutKeys = value; }
		}
		
		public override Image Pictogram
		{
			get { return pictogram; }
			set { pictogram = value; }
		}

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