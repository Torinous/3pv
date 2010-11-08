/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 29.10.2010
 * Time: 19:08
 *
 *
 */

namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;
	
	using Pppv.Commands;

	public class AdditionalCodeEditCommand : Command
	{
		private static string id = "Дополнительный код";
		
		public AdditionalCodeEditCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Редактировать дополнительный Prolog код сети";
			this.ShortcutKeys = Keys.Control | Keys.C;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.AdditionalCode.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}
