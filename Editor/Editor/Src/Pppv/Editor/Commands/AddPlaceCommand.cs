namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;

	using Pppv.Net;

	public class AddPlaceCommand : NetEditorInterfaceCommand
	{
		private Point position;

		public AddPlaceCommand(PetriNetGraphical net, Point place)
		{
			this.Net = net;
			this.Position = place;
			this.Name = "Добавить Позицию к сети";
			this.Description = "Команда добавляющая к заданной сети Позицию по заданным координатам";
		}

		public Point Position
		{
			get { return this.position; }
			set { this.position = value; }
		}

		public override void Execute()
		{
			Net.AddElement(new Place(new Point(this.Position.X, this.Position.Y)));
		}

		public override void Unexecute()
		{
		}
	}
}
