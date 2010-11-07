namespace Pppv.Editor.Commands
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;

	public class QuitCommand : Command
	{
		public static string id = "Выход";
		
		public QuitCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Завершение работы приложения 3PV:Editor";
			this.ShortcutKeys = Keys.Control | Keys.Q;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Exit.png"), true);
		}
	}
}
