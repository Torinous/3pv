/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 05.11.2010
 * Time: 0:08
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

	public class SelectPlaceToolCommand : Command
	{
		private static string id = "Позиция";

		public SelectPlaceToolCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Инструмент создания позиций сети";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Place.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}
