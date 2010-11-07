namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;

	public class SaveNetCommand : Command
	{
		public static string id = "Сохранить";
		
		public SaveNetCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Сохранить сеть в файл";
			this.ShortcutKeys = Keys.Control | Keys.S;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Save.png"), true);
		}
			
		/*public override void Execute()
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
		}*/
	}
}