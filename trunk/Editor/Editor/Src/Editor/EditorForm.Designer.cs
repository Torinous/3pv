/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.11.2010
 * Time: 15:02
 *
 *
 */
namespace Pppv.Editor
{
	partial class EditorForm
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
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.editStatusStrip = new System.Windows.Forms.StatusStrip();
			this.editorTabControl = new Pppv.Editor.EditorTabControl();
			this.toolsToolStrip = new System.Windows.Forms.ToolStrip();
			this.selectPointerToolToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.selectPlaceToolToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.selectTransitionToolToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.selectArcToolToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.selectInhibitorArcToolToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.selectAnnotationToolToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.viewToolStrip = new System.Windows.Forms.ToolStrip();
			this.zoomInToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.zoomOutToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.editToolStrip = new System.Windows.Forms.ToolStrip();
			this.undoToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.redoToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.fileToolStrip = new System.Windows.Forms.ToolStrip();
			this.newNetToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openNetToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveNetToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveAsNetToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.closeNetToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.quiteToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.analyzeToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.quiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.netToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectPointerToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectPlaceToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectTransitionToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectArcToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectInhibitorArcToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAnnotationToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.verificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.toolsToolStrip.SuspendLayout();
			this.viewToolStrip.SuspendLayout();
			this.editToolStrip.SuspendLayout();
			this.fileToolStrip.SuspendLayout();
			this.mainMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.BottomToolStripPanel
			// 
			this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.editStatusStrip);
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.editorTabControl);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(777, 287);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 24);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new System.Drawing.Size(777, 409);
			this.toolStripContainer.TabIndex = 1;
			this.toolStripContainer.Text = "toolStripContainer";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolsToolStrip);
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.viewToolStrip);
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.editToolStrip);
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.fileToolStrip);
			// 
			// editStatusStrip
			// 
			this.editStatusStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.editStatusStrip.Location = new System.Drawing.Point(0, 0);
			this.editStatusStrip.Name = "editStatusStrip";
			this.editStatusStrip.Size = new System.Drawing.Size(777, 22);
			this.editStatusStrip.TabIndex = 0;
			this.editStatusStrip.Text = "editStatusStrip";
			// 
			// editorTabControl
			// 
			this.editorTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorTabControl.Location = new System.Drawing.Point(0, 0);
			this.editorTabControl.Name = "editorTabControl";
			this.editorTabControl.SelectedIndex = -1;
			this.editorTabControl.Size = new System.Drawing.Size(777, 287);
			this.editorTabControl.TabIndex = 0;
			// 
			// toolsToolStrip
			// 
			this.toolsToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.selectPointerToolToolStripButton,
									this.selectPlaceToolToolStripButton,
									this.selectTransitionToolToolStripButton,
									this.selectArcToolToolStripButton,
									this.selectInhibitorArcToolToolStripButton,
									this.selectAnnotationToolToolStripButton});
			this.toolsToolStrip.Location = new System.Drawing.Point(3, 0);
			this.toolsToolStrip.Name = "toolsToolStrip";
			this.toolsToolStrip.Size = new System.Drawing.Size(150, 25);
			this.toolsToolStrip.TabIndex = 3;
			// 
			// selectPointerToolToolStripButton
			// 
			this.selectPointerToolToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.selectPointerToolToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.selectPointerToolToolStripButton.Name = "selectPointerToolToolStripButton";
			this.selectPointerToolToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.selectPointerToolToolStripButton.Text = "selectPointerToolToolStripButton";
			// 
			// selectPlaceToolToolStripButton
			// 
			this.selectPlaceToolToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.selectPlaceToolToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.selectPlaceToolToolStripButton.Name = "selectPlaceToolToolStripButton";
			this.selectPlaceToolToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.selectPlaceToolToolStripButton.Text = "selectPlaceToolToolStripButton";
			// 
			// selectTransitionToolToolStripButton
			// 
			this.selectTransitionToolToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.selectTransitionToolToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.selectTransitionToolToolStripButton.Name = "selectTransitionToolToolStripButton";
			this.selectTransitionToolToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.selectTransitionToolToolStripButton.Text = "selectTransitionToolToolStripButton";
			// 
			// selectArcToolToolStripButton
			// 
			this.selectArcToolToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.selectArcToolToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.selectArcToolToolStripButton.Name = "selectArcToolToolStripButton";
			this.selectArcToolToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.selectArcToolToolStripButton.Text = "selectArcToolToolStripButton";
			// 
			// selectInhibitorArcToolToolStripButton
			// 
			this.selectInhibitorArcToolToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.selectInhibitorArcToolToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.selectInhibitorArcToolToolStripButton.Name = "selectInhibitorArcToolToolStripButton";
			this.selectInhibitorArcToolToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.selectInhibitorArcToolToolStripButton.Text = "selectInhibitorArcToolToolStripButton";
			// 
			// selectAnnotationToolToolStripButton
			// 
			this.selectAnnotationToolToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.selectAnnotationToolToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.selectAnnotationToolToolStripButton.Name = "selectAnnotationToolToolStripButton";
			this.selectAnnotationToolToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.selectAnnotationToolToolStripButton.Text = "selectAnnotationToolToolStripButton";
			// 
			// viewToolStrip
			// 
			this.viewToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.viewToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.zoomInToolStripButton,
									this.zoomOutToolStripButton});
			this.viewToolStrip.Location = new System.Drawing.Point(92, 25);
			this.viewToolStrip.Name = "viewToolStrip";
			this.viewToolStrip.Size = new System.Drawing.Size(58, 25);
			this.viewToolStrip.TabIndex = 2;
			// 
			// zoomInToolStripButton
			// 
			this.zoomInToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.zoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.zoomInToolStripButton.Name = "zoomInToolStripButton";
			this.zoomInToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.zoomInToolStripButton.Text = "zoomInToolStripButton";
			// 
			// zoomOutToolStripButton
			// 
			this.zoomOutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.zoomOutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.zoomOutToolStripButton.Name = "zoomOutToolStripButton";
			this.zoomOutToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.zoomOutToolStripButton.Text = "zoomOutToolStripButton";
			// 
			// editToolStrip
			// 
			this.editToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.editToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.undoToolStripButton,
									this.redoToolStripButton,
									this.cutToolStripButton,
									this.copyToolStripButton,
									this.pasteToolStripButton,
									this.deleteToolStripButton});
			this.editToolStrip.Location = new System.Drawing.Point(3, 50);
			this.editToolStrip.Name = "editToolStrip";
			this.editToolStrip.Size = new System.Drawing.Size(150, 25);
			this.editToolStrip.TabIndex = 1;
			// 
			// undoToolStripButton
			// 
			this.undoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.undoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.undoToolStripButton.Name = "undoToolStripButton";
			this.undoToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.undoToolStripButton.Text = "undoToolStripButton";
			// 
			// redoToolStripButton
			// 
			this.redoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.redoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.redoToolStripButton.Name = "redoToolStripButton";
			this.redoToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.redoToolStripButton.Text = "redoCommandToolStripButton";
			this.redoToolStripButton.ToolTipText = "redoToolStripButton";
			// 
			// cutToolStripButton
			// 
			this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripButton.Name = "cutToolStripButton";
			this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.cutToolStripButton.Text = "cutCommandToolStripButton";
			this.cutToolStripButton.ToolTipText = "cutToolStripButton";
			// 
			// copyToolStripButton
			// 
			this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripButton.Name = "copyToolStripButton";
			this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.copyToolStripButton.Text = "copyCommandToolStripButton";
			this.copyToolStripButton.ToolTipText = "copyToolStripButton";
			// 
			// pasteToolStripButton
			// 
			this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripButton.Name = "pasteToolStripButton";
			this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.pasteToolStripButton.Text = "pasteCommandToolStripButton";
			this.pasteToolStripButton.ToolTipText = "pasteToolStripButton";
			// 
			// deleteToolStripButton
			// 
			this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.deleteToolStripButton.Name = "deleteToolStripButton";
			this.deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.deleteToolStripButton.Text = "deleteCommandToolStripButton";
			this.deleteToolStripButton.ToolTipText = "deleteToolStripButton";
			// 
			// fileToolStrip
			// 
			this.fileToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.fileToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.newNetToolStripButton,
									this.openNetToolStripButton,
									this.saveNetToolStripButton,
									this.saveAsNetToolStripButton,
									this.closeNetToolStripButton,
									this.toolStripSeparator2,
									this.quiteToolStripButton,
									this.analyzeToolStripButton});
			this.fileToolStrip.Location = new System.Drawing.Point(3, 75);
			this.fileToolStrip.Name = "fileToolStrip";
			this.fileToolStrip.Size = new System.Drawing.Size(179, 25);
			this.fileToolStrip.TabIndex = 0;
			// 
			// newNetToolStripButton
			// 
			this.newNetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newNetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newNetToolStripButton.Name = "newNetToolStripButton";
			this.newNetToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newNetToolStripButton.Text = "newNetToolStripButton";
			// 
			// openNetToolStripButton
			// 
			this.openNetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openNetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openNetToolStripButton.Name = "openNetToolStripButton";
			this.openNetToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.openNetToolStripButton.Text = "openNetToolStripButton";
			// 
			// saveNetToolStripButton
			// 
			this.saveNetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveNetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveNetToolStripButton.Name = "saveNetToolStripButton";
			this.saveNetToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveNetToolStripButton.Text = "saveNetToolStripButton";
			// 
			// saveAsNetToolStripButton
			// 
			this.saveAsNetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveAsNetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveAsNetToolStripButton.Name = "saveAsNetToolStripButton";
			this.saveAsNetToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveAsNetToolStripButton.Text = "saveAsNetToolStripButton";
			// 
			// closeNetToolStripButton
			// 
			this.closeNetToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.closeNetToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.closeNetToolStripButton.Name = "closeNetToolStripButton";
			this.closeNetToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.closeNetToolStripButton.Text = "closeNetToolStripButton";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// quiteToolStripButton
			// 
			this.quiteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.quiteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.quiteToolStripButton.Name = "quiteToolStripButton";
			this.quiteToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.quiteToolStripButton.Text = "quiteToolStripButton";
			// 
			// analyzeToolStripButton
			// 
			this.analyzeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.analyzeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.analyzeToolStripButton.Name = "analyzeToolStripButton";
			this.analyzeToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.analyzeToolStripButton.Text = "analyzeToolStripButton";
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.editToolStripMenuItem,
									this.viewToolStripMenuItem,
									this.netToolStripMenuItem,
									this.verificationToolStripMenuItem,
									this.helpToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(777, 24);
			this.mainMenuStrip.TabIndex = 2;
			this.mainMenuStrip.Text = "mainMenuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.newNetToolStripMenuItem,
									this.openNetToolStripMenuItem,
									this.saveNetToolStripMenuItem,
									this.saveAsNetToolStripMenuItem,
									this.closeNetToolStripMenuItem,
									this.toolStripSeparator1,
									this.quiteToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.fileToolStripMenuItem.Text = "&Файл";
			// 
			// newNetToolStripMenuItem
			// 
			this.newNetToolStripMenuItem.Name = "newNetToolStripMenuItem";
			this.newNetToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.newNetToolStripMenuItem.Text = "newNetToolStripMenuItem";
			// 
			// openNetToolStripMenuItem
			// 
			this.openNetToolStripMenuItem.Name = "openNetToolStripMenuItem";
			this.openNetToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.openNetToolStripMenuItem.Text = "openNetToolStripMenuItem";
			// 
			// saveNetToolStripMenuItem
			// 
			this.saveNetToolStripMenuItem.Name = "saveNetToolStripMenuItem";
			this.saveNetToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.saveNetToolStripMenuItem.Text = "saveNetToolStripMenuItem";
			// 
			// saveAsNetToolStripMenuItem
			// 
			this.saveAsNetToolStripMenuItem.Name = "saveAsNetToolStripMenuItem";
			this.saveAsNetToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.saveAsNetToolStripMenuItem.Text = "saveAsNetToolStripMenuItem";
			// 
			// closeNetToolStripMenuItem
			// 
			this.closeNetToolStripMenuItem.Name = "closeNetToolStripMenuItem";
			this.closeNetToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.closeNetToolStripMenuItem.Text = "closeNetToolStripMenuItem";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
			// 
			// quiteToolStripMenuItem
			// 
			this.quiteToolStripMenuItem.Name = "quiteToolStripMenuItem";
			this.quiteToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.quiteToolStripMenuItem.Text = "quiteToolStripMenuItem";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.undoToolStripMenuItem,
									this.redoToolStripMenuItem,
									this.toolStripSeparator3,
									this.cutToolStripMenuItem,
									this.copyToolStripMenuItem,
									this.pasteToolStripMenuItem,
									this.deleteToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.editToolStripMenuItem.Text = "&Правка";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
			this.undoToolStripMenuItem.Text = "undoToolStripMenuItem";
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
			this.redoToolStripMenuItem.Text = "redoToolStripMenuItem";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(206, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
			this.cutToolStripMenuItem.Text = "cutStripMenuItem";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
			this.copyToolStripMenuItem.Text = "copyToolStripMenuItem";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
			this.pasteToolStripMenuItem.Text = "pasteToolStripMenuItem";
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
			this.deleteToolStripMenuItem.Text = "deleteToolStripMenuItem";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.zoomInToolStripMenuItem,
									this.zoomOutToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.viewToolStripMenuItem.Text = "&Вид";
			// 
			// zoomInToolStripMenuItem
			// 
			this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
			this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.zoomInToolStripMenuItem.Text = "zoomInToolStripMenuItem";
			// 
			// zoomOutToolStripMenuItem
			// 
			this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
			this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.zoomOutToolStripMenuItem.Text = "zoomOutToolStripMenuItem";
			// 
			// netToolStripMenuItem
			// 
			this.netToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.selectPointerToolToolStripMenuItem,
									this.selectPlaceToolToolStripMenuItem,
									this.selectTransitionToolToolStripMenuItem,
									this.selectArcToolToolStripMenuItem,
									this.selectInhibitorArcToolToolStripMenuItem,
									this.selectAnnotationToolToolStripMenuItem});
			this.netToolStripMenuItem.Name = "netToolStripMenuItem";
			this.netToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.netToolStripMenuItem.Text = "&Сеть";
			// 
			// selectPointerToolToolStripMenuItem
			// 
			this.selectPointerToolToolStripMenuItem.Name = "selectPointerToolToolStripMenuItem";
			this.selectPointerToolToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
			this.selectPointerToolToolStripMenuItem.Text = "selectPointerToolToolStripMenuItem";
			// 
			// selectPlaceToolToolStripMenuItem
			// 
			this.selectPlaceToolToolStripMenuItem.Name = "selectPlaceToolToolStripMenuItem";
			this.selectPlaceToolToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
			this.selectPlaceToolToolStripMenuItem.Text = "selectPlaceToolToolStripMenuItem";
			// 
			// selectTransitionToolToolStripMenuItem
			// 
			this.selectTransitionToolToolStripMenuItem.Name = "selectTransitionToolToolStripMenuItem";
			this.selectTransitionToolToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
			this.selectTransitionToolToolStripMenuItem.Text = "selectTransitionToolToolStripMenuItem";
			// 
			// selectArcToolToolStripMenuItem
			// 
			this.selectArcToolToolStripMenuItem.Name = "selectArcToolToolStripMenuItem";
			this.selectArcToolToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
			this.selectArcToolToolStripMenuItem.Text = "selectArcToolToolStripMenuItem";
			// 
			// selectInhibitorArcToolToolStripMenuItem
			// 
			this.selectInhibitorArcToolToolStripMenuItem.Name = "selectInhibitorArcToolToolStripMenuItem";
			this.selectInhibitorArcToolToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
			this.selectInhibitorArcToolToolStripMenuItem.Text = "selectInhibitorArcToolToolStripMenuItem";
			// 
			// selectAnnotationToolToolStripMenuItem
			// 
			this.selectAnnotationToolToolStripMenuItem.Name = "selectAnnotationToolToolStripMenuItem";
			this.selectAnnotationToolToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
			this.selectAnnotationToolToolStripMenuItem.Text = "selectAnnotationToolToolStripMenuItem";
			// 
			// verificationToolStripMenuItem
			// 
			this.verificationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.analyzeToolStripMenuItem});
			this.verificationToolStripMenuItem.Name = "verificationToolStripMenuItem";
			this.verificationToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
			this.verificationToolStripMenuItem.Text = "В&ерификация";
			// 
			// analyzeToolStripMenuItem
			// 
			this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
			this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
			this.analyzeToolStripMenuItem.Text = "analyzeToolStripMenuItem";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
			this.helpToolStripMenuItem.Text = "&Помощь";
			// 
			// EditorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(777, 433);
			this.Controls.Add(this.toolStripContainer);
			this.Controls.Add(this.mainMenuStrip);
			this.KeyPreview = true;
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "EditorForm";
			this.Text = "EditorForm";
			this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.toolsToolStrip.ResumeLayout(false);
			this.toolsToolStrip.PerformLayout();
			this.viewToolStrip.ResumeLayout(false);
			this.viewToolStrip.PerformLayout();
			this.editToolStrip.ResumeLayout(false);
			this.editToolStrip.PerformLayout();
			this.fileToolStrip.ResumeLayout(false);
			this.fileToolStrip.PerformLayout();
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStrip toolsToolStrip;
		private System.Windows.Forms.ToolStripButton selectInhibitorArcToolToolStripButton;
		private System.Windows.Forms.ToolStripButton selectArcToolToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem selectAnnotationToolToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectTransitionToolToolStripMenuItem;
		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem selectInhibitorArcToolToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectArcToolToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectPlaceToolToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectPointerToolToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton selectAnnotationToolToolStripButton;
		private System.Windows.Forms.ToolStripButton selectTransitionToolToolStripButton;
		private System.Windows.Forms.ToolStripButton selectPlaceToolToolStripButton;
		private System.Windows.Forms.ToolStripButton selectPointerToolToolStripButton;
		private System.Windows.Forms.ToolStripButton zoomOutToolStripButton;
		private System.Windows.Forms.ToolStripButton zoomInToolStripButton;
		private System.Windows.Forms.ToolStrip viewToolStrip;
		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.ToolStrip editToolStrip;
		private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton undoToolStripButton;
		private System.Windows.Forms.ToolStripButton redoToolStripButton;
		private System.Windows.Forms.ToolStripButton cutToolStripButton;
		private System.Windows.Forms.ToolStripButton copyToolStripButton;
		private System.Windows.Forms.ToolStripButton pasteToolStripButton;
		private System.Windows.Forms.ToolStripButton deleteToolStripButton;
		private System.Windows.Forms.StatusStrip editStatusStrip;
		private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton analyzeToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem verificationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem netToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem saveAsNetToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton saveAsNetToolStripButton;
		private System.Windows.Forms.ToolStripButton saveNetToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem saveNetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeNetToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton closeNetToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem openNetToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton openNetToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem quiteToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton quiteToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem newNetToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton newNetToolStripButton;
		private Pppv.Editor.EditorTabControl editorTabControl;
		private System.Windows.Forms.ToolStrip fileToolStrip;
	}
}
