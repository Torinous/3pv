/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 08.11.2010
 * Time: 3:59
 *
 *
 */
namespace Pppv.Verificator
{
	partial class VerificatorForm2
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
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.analizeStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startPrologInterfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buildStateSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.quitToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.startPrologInterfaceToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.buildStateSpaceToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.verificatorTabControl = new Pppv.Verificator.VerificatorTabControl();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveStateSpaceImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip.SuspendLayout();
			this.mainMenuStrip.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 490);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(579, 22);
			this.statusStrip.TabIndex = 0;
			this.statusStrip.Text = "statusStrip";
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.analizeStripMenuItem,
									this.helpToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(579, 24);
			this.mainMenuStrip.TabIndex = 1;
			this.mainMenuStrip.Text = "mainMenuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.saveToolStripMenuItem,
									this.toolStripSeparator1,
									this.quitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.fileToolStripMenuItem.Text = "&Файл";
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.quitToolStripMenuItem.Text = "quitToolStripMenuItem";
			// 
			// analizeStripMenuItem
			// 
			this.analizeStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.startPrologInterfaceToolStripMenuItem,
									this.buildStateSpaceToolStripMenuItem});
			this.analizeStripMenuItem.Name = "analizeStripMenuItem";
			this.analizeStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.analizeStripMenuItem.Text = "&Анализ";
			// 
			// startPrologInterfaceToolStripMenuItem
			// 
			this.startPrologInterfaceToolStripMenuItem.Name = "startPrologInterfaceToolStripMenuItem";
			this.startPrologInterfaceToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
			this.startPrologInterfaceToolStripMenuItem.Text = "startPrologInterfaceToolStripMenuItem";
			// 
			// buildStateSpaceToolStripMenuItem
			// 
			this.buildStateSpaceToolStripMenuItem.Name = "buildStateSpaceToolStripMenuItem";
			this.buildStateSpaceToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
			this.buildStateSpaceToolStripMenuItem.Text = "buildStateSpaceToolStripMenuItem";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
			this.helpToolStripMenuItem.Text = "&Помощь";
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.quitToolStripButton,
									this.startPrologInterfaceToolStripButton,
									this.buildStateSpaceToolStripButton});
			this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(579, 25);
			this.mainToolStrip.TabIndex = 2;
			this.mainToolStrip.Text = "mainToolStrip";
			// 
			// quitToolStripButton
			// 
			this.quitToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.quitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.quitToolStripButton.Name = "quitToolStripButton";
			this.quitToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.quitToolStripButton.Text = "quitToolStripButton";
			// 
			// startPrologInterfaceToolStripButton
			// 
			this.startPrologInterfaceToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.startPrologInterfaceToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.startPrologInterfaceToolStripButton.Name = "startPrologInterfaceToolStripButton";
			this.startPrologInterfaceToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.startPrologInterfaceToolStripButton.Text = "startPrologInterfaceToolStripButton";
			// 
			// buildStateSpaceToolStripButton
			// 
			this.buildStateSpaceToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buildStateSpaceToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buildStateSpaceToolStripButton.Name = "buildStateSpaceToolStripButton";
			this.buildStateSpaceToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.buildStateSpaceToolStripButton.Text = "buildStateSpaceToolStripButton";
			// 
			// verificatorTabControl
			// 
			this.verificatorTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.verificatorTabControl.Location = new System.Drawing.Point(0, 52);
			this.verificatorTabControl.Name = "verificatorTabControl";
			this.verificatorTabControl.Size = new System.Drawing.Size(579, 435);
			this.verificatorTabControl.TabIndex = 3;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.saveStateSpaceImageToolStripMenuItem});
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.saveToolStripMenuItem.Text = "&Сохранить";
			// 
			// saveStateSpaceImageToolStripMenuItem
			// 
			this.saveStateSpaceImageToolStripMenuItem.Name = "saveStateSpaceImageToolStripMenuItem";
			this.saveStateSpaceImageToolStripMenuItem.Size = new System.Drawing.Size(290, 22);
			this.saveStateSpaceImageToolStripMenuItem.Text = "saveStateSpaceImageToolStripMenuItem";
			// 
			// VerificatorForm2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(579, 512);
			this.Controls.Add(this.verificatorTabControl);
			this.Controls.Add(this.mainToolStrip);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.mainMenuStrip);
			this.DoubleBuffered = true;
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "VerificatorForm2";
			this.Text = "VerificatorForm";
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private Pppv.Verificator.VerificatorTabControl verificatorTabControl;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem saveStateSpaceImageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton buildStateSpaceToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem buildStateSpaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.ToolStripButton startPrologInterfaceToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem startPrologInterfaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem analizeStripMenuItem;
		private System.Windows.Forms.ToolStripButton quitToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.MenuStrip mainMenuStrip;
	}
}
