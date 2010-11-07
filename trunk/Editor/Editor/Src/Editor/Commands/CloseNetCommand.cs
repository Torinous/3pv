﻿namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;
	using Pppv.Utils;

	public class CloseNetCommand : Command
	{
		public static string id = "&Закрыть сеть";
		private bool canceled;
		
		public CloseNetCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Закрыть сеть";
			this.ShortcutKeys = Keys.Control | Keys.W;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Close.png"), true);
		}
		
		public bool Canceled
		{
			get { return canceled; }
			set { canceled = value; }
		}
		
		public override void Execute()
		{
			this.Canceled = false;
			base.Execute();
		}
	}
}