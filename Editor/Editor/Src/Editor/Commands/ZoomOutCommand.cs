namespace Pppv.Editor.Commands
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;

	public class ZoomOutCommand : Command
	{
		private static string id = "Уменьшить";
		
		public ZoomOutCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Уменьшение";
			this.ShortcutKeys = Keys.Control | Keys.Down;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Zoom out.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}