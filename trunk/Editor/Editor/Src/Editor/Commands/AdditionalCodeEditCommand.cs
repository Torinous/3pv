/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 29.10.2010
 * Time: 19:08
 *
 *
 */

namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	public class AdditionalCodeEditCommand : NetEditorInterfaceCommand
	{
		public AdditionalCodeEditCommand()
		{
			this.Name = "Дополнительный код";
			this.Description = "Редактировать дополнительный Prolog код сети";
			this.ShortcutKeys = Keys.Control | Keys.C;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.AdditionalCode.png"), true);
		}

		public AdditionalCodeEditCommand(PetriNetGraphical net) : this()
		{
			this.Net = net;
		}

		public override void Execute()
		{
			this.SetUpTargetNet();
			MainForm mainForm = MainForm.Instance;
			if (mainForm != null)
			{
				Form f = new AdditionalCodeEditForm(this.Net);
				f.ShowDialog(mainForm);
				f.Dispose();
			}
		}
		
		public override void Unexecute()
		{
		}
	}
}
