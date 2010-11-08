/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 05.11.2010
 * Time: 0:02
 *
 */

namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;
	
	using Pppv.ApplicationFramework.Commands;

	public class SelectPointerToolCommand : Command
	{
		private static string id = "Указатель";

		public SelectPointerToolCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Инструмент выбора и перемещения элементов сети";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.M;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Pointer.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}
