/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 05.11.2010
 * Time: 2:26
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

	public class SelectAnnotationToolCommand : Command
	{
		private static string id = "Аннотация";

		public SelectAnnotationToolCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Инструмент создания аннотация";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.O;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Annotation.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}
