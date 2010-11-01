namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;

	public class SaveCommand : NetEditorInterfaceCommand
	{
		public SaveCommand()
		{
			this.Name = "Сохранить";
			this.Description = "Сохранить сеть в файл";
			this.ShortcutKeys = Keys.Control | Keys.S;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Save.png"), true);
		}

		public SaveCommand(PetriNetGraphical net) : this()
		{
			this.Net = net;
		}
			
		public override void Execute()
		{	
			if (this.Net != null)
			{
				this.Net.SaveNet();
			}
			else
			{
				MainForm mainForm = MainForm.Instance;
				if (mainForm.ActiveNet != null)
				{
					mainForm.ActiveNet.SaveNet();
				}
			}
		}

		public override void Unexecute()
		{
		}

		public override bool CheckEnabled()
		{
			return CheckFormAndActiveNet();
		}
	}
}