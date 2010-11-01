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

	using Pppv.Net;
	using Pppv.Verificator;

	public class AnalyzeCommand : NetEditorInterfaceCommand
	{
		private Form mainForm;
		
		public AnalyzeCommand()
		{
			this.Name = "Анализ";
			this.Description = "Запуск средства анализа над текущей сетью";
			this.ShortcutKeys = Keys.Control | Keys.A;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Save.png"), true);
			this.IsHistorical = false;
		}
		
		public AnalyzeCommand(Form form) : this()
		{
			this.MainForm = form;
		}
		
		public Form MainForm
		{
			get { return mainForm; }
			set { mainForm = value; }
		}		
		
		public override void Execute()
		{
			this.SetUpTargetNet();
			this.RunVerificator();
		}

		public override void Unexecute()
		{
		}

		public override bool CheckEnabled()
		{
			return true;
		}
		
		private void RunVerificator()
		{
			Form verificatorForm;
			if (this.Net != null)
			{
				verificatorForm = new VerificatorForm(this.Net.BaseNet);
			}
			else
			{
				verificatorForm = new VerificatorForm();
			}

			verificatorForm.ShowDialog(this.MainForm);			
		}
	}
}