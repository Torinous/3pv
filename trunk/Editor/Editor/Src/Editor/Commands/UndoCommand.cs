namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;

	public class UndoCommand : Command
	{
		private static string id = "Отмена";
		
		public UndoCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Отмена последнего выполненного действия";
			this.ShortcutKeys = Keys.Control | Keys.Z;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Undo.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}