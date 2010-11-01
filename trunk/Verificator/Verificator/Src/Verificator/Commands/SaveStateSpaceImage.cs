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
		private VerificatorForm mainForm;
		
		public SaveStateSpaceImageCommand()
		{
			this.Name = "Сохранить пространство состояний";
			this.Description = "Сохранить изображение пространства состояний";
			//this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Export.png"), true);
		}
		
		public SaveStateSpaceImageCommand(VerificatorForm form) : this()
		{
			this.MainForm = form;
		}
		
		public VerificatorForm MainForm
		{
			get { return this.mainForm; }
			set { this.mainForm = value; }
		}
			
		public override void Execute()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			
			Image image = this.MainForm.StateSpaceImage;
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
