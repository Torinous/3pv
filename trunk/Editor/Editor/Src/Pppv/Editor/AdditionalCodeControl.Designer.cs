/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 29.10.2010
 * Time: 18:52
 *
 *
 */
namespace Pppv.Editor
{
	partial class AdditionalCodeControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			this.codeTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// codeTextBox
			// 
			this.codeTextBox.AcceptsReturn = true;
			this.codeTextBox.AcceptsTab = true;
			this.codeTextBox.Location = new System.Drawing.Point(0, 3);
			this.codeTextBox.Multiline = true;
			this.codeTextBox.Name = "codeTextBox";
			this.codeTextBox.Size = new System.Drawing.Size(444, 293);
			this.codeTextBox.TabIndex = 0;
			this.codeTextBox.WordWrap = false;
			// 
			// AdditionalCodeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.codeTextBox);
			this.Name = "AdditionalCodeControl";
			this.Size = new System.Drawing.Size(444, 299);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox codeTextBox;
	}
}
