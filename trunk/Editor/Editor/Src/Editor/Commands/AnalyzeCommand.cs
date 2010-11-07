/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 12.10.2010
 * Time: 3:42
 *
 */

namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Net;
	using Pppv.Commands;
	using Pppv.Verificator;

	public class AnalyzeCommand : Command
	{
		public static string id = "Анализ";
		
		public AnalyzeCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Запуск средства анализа над текущей сетью";
			this.ShortcutKeys = Keys.Control | Keys.A;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Save.png"), true);
		}
	}
}