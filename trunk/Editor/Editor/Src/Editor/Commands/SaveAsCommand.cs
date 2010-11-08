namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;

	public class SaveAsNetCommand : Command
	{
		private static string id = "Сохранить как...";
		
		public SaveAsNetCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Сохранить сеть в файл с заданным именем";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Save as.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}