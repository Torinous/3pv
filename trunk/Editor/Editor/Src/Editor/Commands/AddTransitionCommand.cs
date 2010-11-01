namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;

	using Pppv.Net;

	public class AddTransitionCommand : NetEditorInterfaceCommand
	{
		private Point position;

		public AddTransitionCommand(PetriNetGraphical net, Point position)
		{
			this.Net = net;
			this.Position = position;
			this.Name = "Добавить Переход к сети";
			this.Description = "Команда добавляющая к заданной сети Переход по заданным координатам";
		}

		public Point Position
		{
			get { return this.position; }
			set { this.position = value; }
		}

		public override void Execute()
		{
			Net.AddElement(new Transition(new Point(this.Position.X, this.Position.Y)));
		}

		public override void Unexecute()
		{
		}
	}
}