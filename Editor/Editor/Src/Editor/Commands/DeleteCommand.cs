namespace Pppv.Editor.Commands
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public class DeleteCommand : Command
	{
		private static string id = "Удалить";
		
		public DeleteCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Удалить выделенные элементы сети";
			this.ShortcutKeys = Keys.Delete;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Delete.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}