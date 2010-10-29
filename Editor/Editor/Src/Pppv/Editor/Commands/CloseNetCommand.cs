namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework;
	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;
	using Pppv.Utils;

	public class CloseNetCommand : NetEditorInterfaceCommand
	{
		public CloseNetCommand()
		{
			this.Name = "Закрыть";
			this.Description = "Закрыть сеть";
			this.ShortcutKeys = Keys.Control | Keys.W;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Close.png"), true);
		}
		
		public CloseNetCommand(PetriNetGraphical net) : this()
		{
			this.Net = net;
		}

		public override void Execute()
		{
			bool shouldPerform = true;
			MainForm mainForm = MainForm.Instance;
			if (mainForm != null)
			{
				this.SetUpTargetNet();
				if (!this.Net.NetSaved)
				{
					DialogResult dialogResult = RtlAwareMessageBox.Show(mainForm, "Сохранить сеть перед закрытием?", "Попытка закрыть несохранённую сеть", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, 0);
					switch (dialogResult)
					{
						case DialogResult.Yes:
							Command command = new SaveCommand(mainForm.ActiveNet);
							command.Execute();
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							shouldPerform = false;
							break;
						default:
							throw new PppvException("Invalid value for DialogResult");
					}
				}

				if (shouldPerform)
				{
					mainForm.CloseNet(mainForm.ActiveNet);	
				}
			}
		}
		
		public override void Unexecute()
		{
		}
	}
}