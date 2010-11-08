namespace Pppv.Editor.Commands
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;

	public class CopyCommand : Command
	{
		private static string id = "&Копировать";
		
		public CopyCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Копировать выделенный элемент сети";
			this.ShortcutKeys = Keys.Control | Keys.C;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Copy.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}