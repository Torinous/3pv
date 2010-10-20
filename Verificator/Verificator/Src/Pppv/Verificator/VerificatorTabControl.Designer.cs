/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 18.10.2010
 * Time: 21:49
 *
 *
 */
namespace Pppv.Verificator
{
   partial class VerificatorTabControl
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
      	this.tabControl1 = new System.Windows.Forms.TabControl();
      	this.tabPageWithStateSpace = new System.Windows.Forms.TabPage();
      	this.tabPage1 = new System.Windows.Forms.TabPage();
      	this.stateSpaceViewer1 = new Pppv.Verificator.StateSpaceViewer();
      	this.tabControl1.SuspendLayout();
      	this.tabPageWithStateSpace.SuspendLayout();
      	this.SuspendLayout();
      	// 
      	// tabControl1
      	// 
      	this.tabControl1.Controls.Add(this.tabPageWithStateSpace);
      	this.tabControl1.Controls.Add(this.tabPage1);
      	this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      	this.tabControl1.Location = new System.Drawing.Point(0, 0);
      	this.tabControl1.Name = "tabControl1";
      	this.tabControl1.SelectedIndex = 0;
      	this.tabControl1.Size = new System.Drawing.Size(536, 411);
      	this.tabControl1.TabIndex = 0;
      	// 
      	// tabPageWithStateSpace
      	// 
      	this.tabPageWithStateSpace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      	this.tabPageWithStateSpace.Controls.Add(this.stateSpaceViewer1);
      	this.tabPageWithStateSpace.Location = new System.Drawing.Point(4, 22);
      	this.tabPageWithStateSpace.Name = "tabPageWithStateSpace";
      	this.tabPageWithStateSpace.Padding = new System.Windows.Forms.Padding(3);
      	this.tabPageWithStateSpace.Size = new System.Drawing.Size(528, 385);
      	this.tabPageWithStateSpace.TabIndex = 0;
      	this.tabPageWithStateSpace.Text = "Пространство состояний";
      	this.tabPageWithStateSpace.UseVisualStyleBackColor = true;
      	// 
      	// tabPage1
      	// 
      	this.tabPage1.Location = new System.Drawing.Point(4, 22);
      	this.tabPage1.Name = "tabPage1";
      	this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      	this.tabPage1.Size = new System.Drawing.Size(528, 385);
      	this.tabPage1.TabIndex = 1;
      	this.tabPage1.Text = "tabPage1";
      	this.tabPage1.UseVisualStyleBackColor = true;
      	// 
      	// stateSpaceViewer1
      	// 
      	this.stateSpaceViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
      	this.stateSpaceViewer1.Location = new System.Drawing.Point(3, 3);
      	this.stateSpaceViewer1.Name = "stateSpaceViewer1";
      	this.stateSpaceViewer1.Size = new System.Drawing.Size(520, 377);
      	this.stateSpaceViewer1.TabIndex = 0;
      	// 
      	// VerificatorTabControl
      	// 
      	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      	this.Controls.Add(this.tabControl1);
      	this.DoubleBuffered = true;
      	this.Name = "VerificatorTabControl";
      	this.Size = new System.Drawing.Size(536, 411);
      	this.tabControl1.ResumeLayout(false);
      	this.tabPageWithStateSpace.ResumeLayout(false);
      	this.ResumeLayout(false);
      }
      private Pppv.Verificator.StateSpaceViewer stateSpaceViewer1;
      private System.Windows.Forms.TabPage tabPage1;
      private System.Windows.Forms.TabPage tabPageWithStateSpace;
      private System.Windows.Forms.TabControl tabControl1;
   }
}
