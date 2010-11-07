namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;

	public class OpenNetCommand : Command
	{
		public static string id = "открыть";
				
		public OpenNetCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Открыть сеть из файла";
			this.ShortcutKeys = Keys.Control | Keys.O;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Open.png"), true);
		}
	}
}