/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 29.10.2010
 * Time: 19:03
 *
 *
 */
namespace Pppv.Editor
{
	partial class AdditionalCodeEditForm
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
			this.additionalCodeControl1 = new Pppv.Editor.AdditionalCodeControl();
			this.OkButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// additionalCodeControl1
			// 
			this.additionalCodeControl1.Location = new System.Drawing.Point(12, 12);
			this.additionalCodeControl1.Name = "additionalCodeControl1";
			this.additionalCodeControl1.Net = null;
			this.additionalCodeControl1.Size = new System.Drawing.Size(444, 299);
			this.additionalCodeControl1.TabIndex = 0;
			// 
			// OkButton
			// 
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.Location = new System.Drawing.Point(381, 317);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 1;
			this.OkButton.Text = "OK";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// AdditionalCodeEditForm2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(468, 349);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.additionalCodeControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AdditionalCodeEditForm2";
			this.Text = "AdditionalCodeEditForm2";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button OkButton;
		private Pppv.Editor.AdditionalCodeControl additionalCodeControl1;
	}
}
