/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.10.2010
 * Time: 3:36
 *
 *
 */
namespace Pppv.Editor
{
	partial class TransitionEditControl
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
			this.transitionGroupBox = new System.Windows.Forms.GroupBox();
			this.addArcsParametersButton = new System.Windows.Forms.Button();
			this.guardFunctionTextBox = new System.Windows.Forms.TextBox();
			this.guardFunctionLabel = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.idTextBox = new System.Windows.Forms.TextBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.idLabel = new System.Windows.Forms.Label();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.transitionGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// transitionGroupBox
			// 
			this.transitionGroupBox.Controls.Add(this.addArcsParametersButton);
			this.transitionGroupBox.Controls.Add(this.guardFunctionTextBox);
			this.transitionGroupBox.Controls.Add(this.guardFunctionLabel);
			this.transitionGroupBox.Controls.Add(this.nameTextBox);
			this.transitionGroupBox.Controls.Add(this.idTextBox);
			this.transitionGroupBox.Controls.Add(this.nameLabel);
			this.transitionGroupBox.Controls.Add(this.idLabel);
			this.transitionGroupBox.Location = new System.Drawing.Point(3, 3);
			this.transitionGroupBox.Name = "transitionGroupBox";
			this.transitionGroupBox.Size = new System.Drawing.Size(516, 101);
			this.transitionGroupBox.TabIndex = 0;
			this.transitionGroupBox.TabStop = false;
			this.transitionGroupBox.Text = "Переход:";
			// 
			// addArcsParametersButton
			// 
			this.addArcsParametersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.addArcsParametersButton.Location = new System.Drawing.Point(422, 42);
			this.addArcsParametersButton.Name = "addArcsParametersButton";
			this.addArcsParametersButton.Size = new System.Drawing.Size(88, 23);
			this.addArcsParametersButton.TabIndex = 6;
			this.addArcsParametersButton.Text = "+Параметры";
			this.addArcsParametersButton.UseVisualStyleBackColor = true;
			this.addArcsParametersButton.Click += new System.EventHandler(this.AddArcsParametersButtonClick);
			// 
			// guardFunctionTextBox
			// 
			this.guardFunctionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.guardFunctionTextBox.Location = new System.Drawing.Point(112, 68);
			this.guardFunctionTextBox.Name = "guardFunctionTextBox";
			this.guardFunctionTextBox.Size = new System.Drawing.Size(289, 20);
			this.guardFunctionTextBox.TabIndex = 5;
			// 
			// guardFunctionLabel
			// 
			this.guardFunctionLabel.Location = new System.Drawing.Point(6, 68);
			this.guardFunctionLabel.Name = "guardFunctionLabel";
			this.guardFunctionLabel.Size = new System.Drawing.Size(100, 23);
			this.guardFunctionLabel.TabIndex = 4;
			this.guardFunctionLabel.Text = "Функция охраны:";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.nameTextBox.Location = new System.Drawing.Point(112, 42);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(289, 20);
			this.nameTextBox.TabIndex = 3;
			// 
			// idTextBox
			// 
			this.idTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.idTextBox.Location = new System.Drawing.Point(112, 13);
			this.idTextBox.Name = "idTextBox";
			this.idTextBox.ReadOnly = true;
			this.idTextBox.Size = new System.Drawing.Size(289, 20);
			this.idTextBox.TabIndex = 2;
			// 
			// nameLabel
			// 
			this.nameLabel.Location = new System.Drawing.Point(6, 42);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(100, 23);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "Имя:";
			// 
			// idLabel
			// 
			this.idLabel.Location = new System.Drawing.Point(6, 16);
			this.idLabel.Name = "idLabel";
			this.idLabel.Size = new System.Drawing.Size(100, 23);
			this.idLabel.TabIndex = 0;
			this.idLabel.Text = "Идентификатор:";
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// TransitionEditControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.transitionGroupBox);
			this.Name = "TransitionEditControl";
			this.Size = new System.Drawing.Size(543, 161);
			this.Validating += new System.ComponentModel.CancelEventHandler(this.TransitionEditControlValidating);
			this.transitionGroupBox.ResumeLayout(false);
			this.transitionGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.Button addArcsParametersButton;
		private System.Windows.Forms.TextBox guardFunctionTextBox;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.TextBox idTextBox;
		private System.Windows.Forms.Label idLabel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label guardFunctionLabel;
		private System.Windows.Forms.GroupBox transitionGroupBox;
	}
}
