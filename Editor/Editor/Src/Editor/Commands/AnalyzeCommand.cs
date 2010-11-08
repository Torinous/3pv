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

	using Pppv.Commands;
	using Pppv.Net;
	using Pppv.Verificator;

	public class AnalyzeCommand : Command
	{
		private static string id = "Анализ";
		
		public AnalyzeCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Запуск средства анализа над текущей сетью";
			this.ShortcutKeys = Keys.Control | Keys.A;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Run.png"), true);
		}

		public static string Id
		{
			get { return id; }
		}
	}
}