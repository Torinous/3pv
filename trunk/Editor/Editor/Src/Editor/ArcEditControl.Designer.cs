/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.10.2010
 * Time: 15:52
 *
 *
 */
namespace Pppv.Editor
{
	partial class ArcEditControl
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
			this.cortegeEditControl = new Pppv.Editor.CortegeEditControl();
			this.label1 = new System.Windows.Forms.Label();
			this.idTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cortegeEditControl
			// 
			this.cortegeEditControl.ListPredicates = null;
			this.cortegeEditControl.Location = new System.Drawing.Point(0, 26);
			this.cortegeEditControl.Name = "cortegeEditControl";
			this.cortegeEditControl.Size = new System.Drawing.Size(448, 287);
			this.cortegeEditControl.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Идентификатор:";
			// 
			// idTextBox
			// 
			this.idTextBox.Enabled = false;
			this.idTextBox.Location = new System.Drawing.Point(96, 0);
			this.idTextBox.Name = "idTextBox";
			this.idTextBox.Size = new System.Drawing.Size(352, 20);
			this.idTextBox.TabIndex = 2;
			// 
			// ArcEditControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.idTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cortegeEditControl);
			this.Name = "ArcEditControl";
			this.Size = new System.Drawing.Size(449, 315);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox idTextBox;
		private Pppv.Editor.CortegeEditControl cortegeEditControl;
		private System.Windows.Forms.Label label1;
	}
}
