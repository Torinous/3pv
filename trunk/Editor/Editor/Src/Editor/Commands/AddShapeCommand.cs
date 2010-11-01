namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;

	using Pppv.ApplicationFramework;
	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public class AddShapeCommand : NetCommand
	{
		private IShape shape;

		public AddShapeCommand()
		{
			Name = "Добавить элемент к сети";
		}

		public AddShapeCommand(PetriNetGraphical net) : this()
		{
			this.Net = net;
		}

		public AddShapeCommand(IShape shape) : this()
		{
			this.Shape = shape;
		}

		public AddShapeCommand(PetriNetGraphical net, IShape shape) : this()
		{
			this.Shape = shape;
			Net = net;
		}

		public IShape Shape
		{
			get { return this.shape; }
			set { this.shape = value; }
		}

		public override void Execute()
		{
			try
			{
				Net.AddElement(this.Shape);

				if (Net.Canvas != null)
				{
					Net.Canvas.Invalidate();
				}
			}
			catch (Exception e)
			{
				throw new PppvException("Внутреннее исключение в команде AddNetElementCommand", e);
			}
		}

		public override void Unexecute()
		{
		}
	}
}