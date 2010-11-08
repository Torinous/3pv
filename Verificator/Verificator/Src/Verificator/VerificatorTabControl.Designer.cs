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
      	this.tabControl = new System.Windows.Forms.TabControl();
      	this.tabPageWithStateSpace = new System.Windows.Forms.TabPage();
      	this.stateSpaceViewer1 = new Pppv.Verificator.StateSpaceViewer();
      	this.tabPageWithProlog = new System.Windows.Forms.TabPage();
      	this.PrologTextBox = new System.Windows.Forms.TextBox();
      	this.tabControl.SuspendLayout();
      	this.tabPageWithStateSpace.SuspendLayout();
      	this.tabPageWithProlog.SuspendLayout();
      	this.SuspendLayout();
      	// 
      	// tabControl
      	// 
      	this.tabControl.Controls.Add(this.tabPageWithProlog);
      	this.tabControl.Controls.Add(this.tabPageWithStateSpace);
      	this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      	this.tabControl.Location = new System.Drawing.Point(0, 0);
      	this.tabControl.Name = "tabControl";
      	this.tabControl.SelectedIndex = 0;
      	this.tabControl.Size = new System.Drawing.Size(536, 411);
      	this.tabControl.TabIndex = 0;
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
      	// stateSpaceViewer1
      	// 
      	this.stateSpaceViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
      	this.stateSpaceViewer1.Location = new System.Drawing.Point(3, 3);
      	this.stateSpaceViewer1.Name = "stateSpaceViewer1";
      	this.stateSpaceViewer1.Size = new System.Drawing.Size(520, 377);
      	this.stateSpaceViewer1.TabIndex = 0;
      	// 
      	// tabPageWithProlog
      	// 
      	this.tabPageWithProlog.Controls.Add(this.PrologTextBox);
      	this.tabPageWithProlog.Location = new System.Drawing.Point(4, 22);
      	this.tabPageWithProlog.Name = "tabPageWithProlog";
      	this.tabPageWithProlog.Padding = new System.Windows.Forms.Padding(3);
      	this.tabPageWithProlog.Size = new System.Drawing.Size(528, 385);
      	this.tabPageWithProlog.TabIndex = 1;
      	this.tabPageWithProlog.Text = "Prolog";
      	this.tabPageWithProlog.UseVisualStyleBackColor = true;
      	// 
      	// PrologTextBox
      	// 
      	this.PrologTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      	this.PrologTextBox.Location = new System.Drawing.Point(3, 3);
      	this.PrologTextBox.Multiline = true;
      	this.PrologTextBox.Name = "PrologTextBox";
      	this.PrologTextBox.ReadOnly = true;
      	this.PrologTextBox.Size = new System.Drawing.Size(522, 379);
      	this.PrologTextBox.TabIndex = 0;
      	this.PrologTextBox.WordWrap = false;
      	// 
      	// VerificatorTabControl
      	// 
      	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      	this.Controls.Add(this.tabControl);
      	this.DoubleBuffered = true;
      	this.Name = "VerificatorTabControl";
      	this.Size = new System.Drawing.Size(536, 411);
      	this.tabControl.ResumeLayout(false);
      	this.tabPageWithStateSpace.ResumeLayout(false);
      	this.tabPageWithProlog.ResumeLayout(false);
      	this.tabPageWithProlog.PerformLayout();
      	this.ResumeLayout(false);
      }
      private System.Windows.Forms.TabControl tabControl;
      public System.Windows.Forms.TabPage tabPageWithProlog;
      public System.Windows.Forms.TextBox PrologTextBox;
      public Pppv.Verificator.StateSpaceViewer stateSpaceViewer1;
      public System.Windows.Forms.TabPage tabPageWithStateSpace;
   }
}
