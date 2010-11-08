namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;

	public class RedoCommand : Command
	{
		private static string id = "Повтор";
		
		public RedoCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Повтор последнего отменённого действия";
			this.ShortcutKeys = Keys.Control | Keys.Y;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Redo.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}