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
	using Pppv.Graphviz;
	using Pppv.Net;
	
	using SbsSW.SwiPlCs;

	public class ConstructStateSpaceCommand : InterfaceCommand
	{
		public ConstructStateSpaceCommand()
		{
			this.Name = "Пространство состояний";
			this.Description = "Построить пространство состояний сети";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.StateSpace.png"), true);
		}

		public override void Execute()
		{
			//this.SetStatusMessage("Вычисление пространства состояний сети: " + this.Net.Id);
			DateTime startTime = DateTime.Now;
			PlQuery.PlCall("statespace:createStateSpace.");
			TimeSpan duration = DateTime.Now - startTime;
			//this.SetStatusMessage(String.Format("Пространство состояний сети: {0} построено({1} мс)", this.Net.Id, duration.TotalMilliseconds));
		}

		public override void Unexecute()
		{
		}
	}
}
