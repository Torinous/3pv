/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 17.10.2010
 * Time: 5:43
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
	using Pppv.ApplicationFramework.Utils;
	using Pppv.Net;

	public class PlotReachabilityGraphImageCommand : Command
	{
		private static string id = "Регенерация";
		
		public PlotReachabilityGraphImageCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Перестроить изображение графа достижимости";
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.StateSpace.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}

