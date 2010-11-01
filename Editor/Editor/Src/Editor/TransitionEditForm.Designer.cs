/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.10.2010
 * Time: 4:40
 *
 *
 */
namespace Pppv.Editor
{
	partial class TransitionEditForm
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
			this.transitionEditControl = new Pppv.Editor.TransitionEditControl();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// transitionEditControl
			// 
			this.transitionEditControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.transitionEditControl.Location = new System.Drawing.Point(12, 12);
			this.transitionEditControl.Name = "transitionEditControl";
			this.transitionEditControl.Size = new System.Drawing.Size(519, 109);
			this.transitionEditControl.TabIndex = 0;
			this.transitionEditControl.Transition = null;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(456, 127);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// TransitionEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(545, 156);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.transitionEditControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "TransitionEditForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Редактирование перехода";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button okButton;
		private Pppv.Editor.TransitionEditControl transitionEditControl;
	}
}
