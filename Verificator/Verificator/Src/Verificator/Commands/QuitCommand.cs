/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 14.10.2010
 * Time: 12:52
 *
 *
 */

namespace Pppv.Verificator.Commands
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;

	public class QuitCommand : Command
	{
		private static string id = "Выход";
		
		public QuitCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Завершение работы приложения 3PV:Verificator";
			this.ShortcutKeys = Keys.Control | Keys.Q;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Exit.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}