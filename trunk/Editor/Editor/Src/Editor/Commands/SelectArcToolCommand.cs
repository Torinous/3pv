/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 05.11.2010
 * Time: 0:12
 *
 *
 */

namespace Pppv.Editor.Commands
{
	using System;
	using System.Windows.Forms;
	using System.Drawing;
	using System.Reflection;
	
	using Pppv.Commands;

	public class SelectArcToolCommand : Command
	{
		public static string id = "Дуга";

		public SelectArcToolCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Инструмент создание дуг сети";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.T;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Arc.png"), true);
		}
	}
}
