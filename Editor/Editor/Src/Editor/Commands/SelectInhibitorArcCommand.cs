/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 05.11.2010
 * Time: 0:24
 *
 *
 */

namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;
	
	using Pppv.ApplicationFramework.Commands;

	public class SelectInhibitorArcToolCommand : Command
	{
		private static string id = "Ингибиторная дуга";

		public SelectInhibitorArcToolCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Инструмент создание ингибиторных дуг сети";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.I;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Inhibitor Arc.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}
