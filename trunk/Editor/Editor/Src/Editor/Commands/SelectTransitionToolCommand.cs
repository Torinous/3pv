/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 05.11.2010
 * Time: 0:10
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

	public class SelectTransitionToolCommand : Command
	{
		public static string id = "Переход";

		public SelectTransitionToolCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Инструмент создания переходов сети";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.T;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Transition.png"), true);
		}
	}
}
