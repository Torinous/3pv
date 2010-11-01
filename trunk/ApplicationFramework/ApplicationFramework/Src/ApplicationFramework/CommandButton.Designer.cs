/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 19.10.2010
 * Time: 2:04
 *
 *
 */
namespace Pppv.ApplicationFramework
{
   partial class CommandButton
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
      	this.button1 = new System.Windows.Forms.Button();
      	this.SuspendLayout();
      	// 
      	// button1
      	// 
      	this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      	this.button1.Location = new System.Drawing.Point(0, 0);
      	this.button1.Name = "button1";
      	this.button1.Size = new System.Drawing.Size(75, 23);
      	this.button1.TabIndex = 0;
      	this.button1.Text = "button1";
      	this.button1.UseVisualStyleBackColor = true;
      	// 
      	// CommandButton
      	// 
      	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      	this.Controls.Add(this.button1);
      	this.Name = "CommandButton";
      	this.Size = new System.Drawing.Size(161, 87);
      	this.ResumeLayout(false);
      }
      private System.Windows.Forms.Button button1;
   }
}
