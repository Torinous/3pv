namespace Pppv.Editor.Commands
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public class DeleteCommand : NetElementInterfaceCommand
	{
		public DeleteCommand()
		{
			this.Name = "Удалить";
			this.Description = "Удалить элемент сети";
			this.ShortcutKeys = Keys.Delete;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Delete.png"), true);
		}

		public DeleteCommand(IShape netElement) : this()
		{
			this.Shape = netElement;
		}

		public override void Execute()
		{
			this.DeleteCurrentShape();
		}

		public override void Unexecute()
		{
		}

		private void DeleteCurrentShape()
		{
			this.CheckAndDeleteCurrentShape();
		}

		private void CheckAndDeleteCurrentShape()
		{
			if (this.Shape != null)
			{
				PetriNetGraphical net = this.Shape.ParentNetGraphical;
				net.DeleteElement(this.Shape);
				if (net.Canvas != null)
				{
					net.Canvas.Invalidate();
				}
			}
		}
	}
}