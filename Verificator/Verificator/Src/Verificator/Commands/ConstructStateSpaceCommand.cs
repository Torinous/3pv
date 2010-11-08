/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 17.10.2010
 * Time: 5:43
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
	using Pppv.ApplicationFramework.Graphviz;
	using Pppv.Net;
	
	using SbsSW.SwiPlCs;

	public class BuildStateSpaceCommand : Command
	{
		private static string id = "Построить граф достижимости";
		
		public BuildStateSpaceCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Построить граф достижимости сети";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.StateSpace.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}
