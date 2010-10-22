/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.10.2010
 * Time: 11:00
 *
 *
 */
namespace Pppv.Editor
{
	partial class PlaceEditControl
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
			this.components = new System.ComponentModel.Container();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.tokensEditControl = new Pppv.Editor.TokensEditControl();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.idTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.groupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.tokensEditControl);
			this.groupBox.Controls.Add(this.nameTextBox);
			this.groupBox.Controls.Add(this.idTextBox);
			this.groupBox.Controls.Add(this.label2);
			this.groupBox.Controls.Add(this.label1);
			this.groupBox.Location = new System.Drawing.Point(3, 3);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(435, 304);
			this.groupBox.TabIndex = 0;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "Место:";
			// 
			// tokensEditControl
			// 
			this.tokensEditControl.ListTokens = null;
			this.tokensEditControl.Location = new System.Drawing.Point(6, 65);
			this.tokensEditControl.Name = "tokensEditControl";
			this.tokensEditControl.Size = new System.Drawing.Size(423, 233);
			this.tokensEditControl.TabIndex = 4;
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(112, 39);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(297, 20);
			this.nameTextBox.TabIndex = 3;
			this.nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.NameTextBoxValidating);
			// 
			// idTextBox
			// 
			this.idTextBox.Enabled = false;
			this.idTextBox.Location = new System.Drawing.Point(112, 13);
			this.idTextBox.Name = "idTextBox";
			this.idTextBox.Size = new System.Drawing.Size(297, 20);
			this.idTextBox.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Имя:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Идентификатор:";
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// PlaceEditControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox);
			this.Name = "PlaceEditControl";
			this.Size = new System.Drawing.Size(443, 315);
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
		}
		private Pppv.Editor.TokensEditControl tokensEditControl;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.TextBox idTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox;
	}
}
