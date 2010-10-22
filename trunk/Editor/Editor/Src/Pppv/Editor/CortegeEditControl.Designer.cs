/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.10.2010
 * Time: 15:21
 *
 *
 */
namespace Pppv.Editor
{
	partial class CortegeEditControl
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
			this.textBoxCurrentPredicate = new System.Windows.Forms.TextBox();
			this.listBoxPredicates = new Pppv.ApplicationFramework.RefreshingListBox();
			this.groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.buttonDelete);
			this.groupBox.Controls.Add(this.buttonAdd);
			this.groupBox.Controls.Add(this.textBoxCurrentPredicate);
			this.groupBox.Controls.Add(this.listBoxPredicates);
			this.groupBox.Location = new System.Drawing.Point(9, 7);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(436, 277);
			this.groupBox.TabIndex = 0;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "groupBox1";
			// 
			// buttonDelete
			// 
			this.buttonDelete.Location = new System.Drawing.Point(179, 48);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(75, 23);
			this.buttonDelete.TabIndex = 3;
			this.buttonDelete.Text = "-";
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += new System.EventHandler(this.ButtonDeleteClick);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(179, 19);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdd.TabIndex = 2;
			this.buttonAdd.Text = "+";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.ButtonAddClick);
			// 
			// textBoxCurrentPredicate
			// 
			this.textBoxCurrentPredicate.Enabled = false;
			this.textBoxCurrentPredicate.Location = new System.Drawing.Point(260, 19);
			this.textBoxCurrentPredicate.Multiline = true;
			this.textBoxCurrentPredicate.Name = "textBoxCurrentPredicate";
			this.textBoxCurrentPredicate.Size = new System.Drawing.Size(170, 251);
			this.textBoxCurrentPredicate.TabIndex = 1;
			this.textBoxCurrentPredicate.TextChanged += new System.EventHandler(this.TextBoxCurrentPredicateTextChanged);
			// 
			// listBoxPredicates
			// 
			this.listBoxPredicates.FormattingEnabled = true;
			this.listBoxPredicates.Location = new System.Drawing.Point(6, 19);
			this.listBoxPredicates.Name = "listBoxPredicates";
			this.listBoxPredicates.Size = new System.Drawing.Size(167, 251);
			this.listBoxPredicates.TabIndex = 0;
			this.listBoxPredicates.SelectedIndexChanged += new System.EventHandler(this.ListBoxPredicatesSelectedIndexChanged);
			// 
			// CortegeEditControl2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox);
			this.Name = "CortegeEditControl2";
			this.Size = new System.Drawing.Size(448, 287);
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.GroupBox groupBox;
		private Pppv.ApplicationFramework.RefreshingListBox listBoxPredicates;
		private System.Windows.Forms.TextBox textBoxCurrentPredicate;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonDelete;
	}
}
