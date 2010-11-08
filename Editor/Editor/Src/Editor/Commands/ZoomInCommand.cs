namespace Pppv.Editor.Commands
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;

	public class ZoomInCommand : Command
	{
		private static string id = "Увеличить";
				
		public ZoomInCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Увеличить";
			this.ShortcutKeys = Keys.Control | Keys.Up;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Zoom in.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}