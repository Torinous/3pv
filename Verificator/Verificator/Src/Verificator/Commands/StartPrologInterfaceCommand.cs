/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 17.10.2010
 * Time: 18:55
 *
 *
 */

namespace Pppv.Verificator.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;
	using Pppv.ApplicationFramework.Utils;

	public class StartPrologInterfaceCommand : Command
	{
		private static string id = "Prolog интерпретатор";
		
		public StartPrologInterfaceCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Запустить Prolog интерпретатор";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.swiprolog.ico"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}

