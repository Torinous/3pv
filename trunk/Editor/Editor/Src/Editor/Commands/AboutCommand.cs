namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;

	public class AboutCommand : Command
	{
		private static string id = "О программе";
		
		public AboutCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			Description = "Вызов формы \"О программе\"";
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}
