/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.10.2010
 * Time: 15:44
 *
 *
 */
namespace Pppv.Editor
{
	partial class ArcEditForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.okButton = new System.Windows.Forms.Button();
			this.arcEditControl = new Pppv.Editor.ArcEditControl();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(376, 325);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// arcEditControl
			// 
			this.arcEditControl.Arc = null;
			this.arcEditControl.Location = new System.Drawing.Point(2, 4);
			this.arcEditControl.Name = "arcEditControl";
			this.arcEditControl.Size = new System.Drawing.Size(449, 315);
			this.arcEditControl.TabIndex = 1;
			// 
			// ArcEditForm2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(456, 358);
			this.Controls.Add(this.arcEditControl);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ArcEditForm2";
			this.Text = "Форма редактирования дуги";
			this.ResumeLayout(false);
		}
		private Pppv.Editor.ArcEditControl arcEditControl;
		private System.Windows.Forms.Button okButton;
	}
}
