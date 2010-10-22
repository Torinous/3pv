/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.10.2010
 * Time: 16:34
 *
 *
 */

namespace Pppv.Verificator.Commands
{
	using System;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Windows.Forms;
	using System.Reflection;
	
	using Pppv.ApplicationFramework.Commands;

	public class SaveStateSpaceImageCommand : InterfaceCommand
	{
		public SaveStateSpaceImageCommand()
		{
			this.Name = "Сохранить пространство состояний";
			this.Description = "Сохранить изображение пространства состояний";
			//this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Export.png"), true);
		}
		
		public override void Execute()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			PetriNetVerificator verificator = PetriNetVerificator.Instance;
			if (verificator == null)
			{
				return;
			}
			
			VerificatorForm mainForm = verificator.MainForm;
			if (mainForm == null)
			{
				return;
			}
			
			Image image = mainForm.StateSpaceImage;
			if (image == null)
			{
				return;
			}
			
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (image != null)
				{
					image.Save(saveFileDialog.FileName + ".png", ImageFormat.Png);
				}
			}
		}

		public override void Unexecute()
		{
		}
	}
}
