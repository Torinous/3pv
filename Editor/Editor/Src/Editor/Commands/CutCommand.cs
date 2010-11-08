namespace Pppv.Editor.Commands
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;

	public class CutCommand : Command
	{
		private static string id = "Вырезать";
		
		public CutCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Вырезать выделенный элемент сети";
			this.ShortcutKeys = Keys.Control | Keys.X;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Cut.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}