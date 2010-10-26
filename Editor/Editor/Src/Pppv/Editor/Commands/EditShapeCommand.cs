namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public class EditShapeCommand : NetElementInterfaceCommand
	{
		public EditShapeCommand(IShape shape)
		{
			this.Shape = shape;
			this.Name = "Редактировать элемент сети";
			this.Description = "Команда редактирования свойств елемента сети";
		}

		public override void Execute()
		{
			MainForm mainForm = this.Shape.ParentNetGraphical.Canvas.FindForm() as MainForm;

			if (Shape is ArcShape)
			{
				Form f = new ArcEditForm((IArc)Shape);
				f.ShowDialog(mainForm);
				f.Dispose();
			}

			if (Shape is TransitionShape)
			{
				Form f = new TransitionEditForm((ITransition)Shape);
				f.ShowDialog(mainForm);
				f.Dispose();
			}

			if (Shape is PlaceShape)
			{
				Form f = new PlaceEditForm((IPlace)Shape);
				f.ShowDialog(mainForm);
				f.Dispose();
			}

			Shape.ParentNetGraphical.Canvas.Invalidate();
		}

		public override void Unexecute()
		{
		}
	}
}