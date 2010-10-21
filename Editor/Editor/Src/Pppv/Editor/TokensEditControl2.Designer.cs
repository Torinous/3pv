/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.10.2010
 * Time: 10:36
 *
 *
 */
namespace Pppv.Editor
{
	partial class TokensEditControl2
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
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.textBoxCurrentToken = new System.Windows.Forms.TextBox();
			this.listBoxTokens = new Pppv.ApplicationFramework.RefreshingListBox();
			this.groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.buttonDelete);
			this.groupBox.Controls.Add(this.buttonAdd);
			this.groupBox.Controls.Add(this.textBoxCurrentToken);
			this.groupBox.Controls.Add(this.listBoxTokens);
			this.groupBox.Location = new System.Drawing.Point(3, 3);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(417, 227);
			this.groupBox.TabIndex = 0;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "groupBox1";
			// 
			// buttonDelete
			// 
			this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonDelete.Location = new System.Drawing.Point(185, 48);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(56, 23);
			this.buttonDelete.TabIndex = 3;
			this.buttonDelete.Text = "-";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.ButtonDeleteClick);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonAdd.Location = new System.Drawing.Point(185, 19);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(56, 23);
			this.buttonAdd.TabIndex = 2;
			this.buttonAdd.Text = "+";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.ButtonAddClick);
			// 
			// textBoxCurrentToken
			// 
			this.textBoxCurrentToken.AcceptsReturn = true;
			this.textBoxCurrentToken.Enabled = false;
			this.textBoxCurrentToken.Location = new System.Drawing.Point(247, 19);
			this.textBoxCurrentToken.Multiline = true;
			this.textBoxCurrentToken.Name = "textBoxCurrentToken";
			this.textBoxCurrentToken.Size = new System.Drawing.Size(164, 199);
			this.textBoxCurrentToken.TabIndex = 1;
			this.textBoxCurrentToken.TextChanged += new System.EventHandler(this.TextBoxCurrentTokenTextChanged);
			// 
			// listBoxTokens
			// 
			this.listBoxTokens.FormattingEnabled = true;
			this.listBoxTokens.Location = new System.Drawing.Point(6, 19);
			this.listBoxTokens.Name = "listBoxTokens";
			this.listBoxTokens.Size = new System.Drawing.Size(173, 199);
			this.listBoxTokens.TabIndex = 0;
			this.listBoxTokens.SelectedIndexChanged += new System.EventHandler(this.ListBoxTokensSelectedIndexChanged);
			// 
			// TokensEditControl2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox);
			this.Name = "TokensEditControl2";
			this.Size = new System.Drawing.Size(421, 233);
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.TextBox textBoxCurrentToken;
		private Pppv.ApplicationFramework.RefreshingListBox listBoxTokens;
	}
}
