namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;

	public class NewNetCommand : Command
	{
		public static string id = "Создать";

		public NewNetCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Создать новую сеть";
			this.ShortcutKeys = Keys.Control | Keys.N;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.New.png"), true);
		}
	}
}
