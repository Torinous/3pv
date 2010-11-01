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

	public class QuitCommand : InterfaceCommand
	{
		private Form formToClose;

		public QuitCommand()
		{
			this.Name = "Выход";
			this.Description = "Завершение работы приложения 3PV:Verificator";
			this.ShortcutKeys = Keys.Control | Keys.Q;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Exit.png"), true);
		}
		
		public QuitCommand(VerificatorForm form) : this ()
		{
			this.formToClose = form;
		}

		public Form FormToClose
		{
			get { return this.formToClose; }
			set { this.formToClose = value; }
		}

		public override void Execute()
		{
			this.FormToClose.Close();
		}

		public override void Unexecute()
		{
		}
	}
}