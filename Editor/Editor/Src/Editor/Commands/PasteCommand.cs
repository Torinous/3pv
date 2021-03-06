﻿namespace Pppv.Editor.Commands
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;

	public class PasteCommand : Command
	{
		private static string id = "&Вставить";
		
		public PasteCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Вставить элемент сети из буфера обмена";
			this.ShortcutKeys = Keys.Control | Keys.P;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Paste.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}