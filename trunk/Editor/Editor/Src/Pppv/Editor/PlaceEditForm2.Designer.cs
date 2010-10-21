/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.10.2010
 * Time: 11:25
 *
 *
 */
namespace Pppv.Editor
{
	partial class PlaceEditForm2
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
			this.placeEditControl1 = new Pppv.Editor.PlaceEditControl();
			this.oKbutton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// placeEditControl1
			// 
			this.placeEditControl1.Location = new System.Drawing.Point(12, 12);
			this.placeEditControl1.Name = "placeEditControl1";
			this.placeEditControl1.Place = null;
			this.placeEditControl1.Size = new System.Drawing.Size(443, 315);
			this.placeEditControl1.TabIndex = 0;
			// 
			// oKbutton
			// 
			this.oKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.oKbutton.Location = new System.Drawing.Point(380, 331);
			this.oKbutton.Name = "oKbutton";
			this.oKbutton.Size = new System.Drawing.Size(75, 23);
			this.oKbutton.TabIndex = 1;
			this.oKbutton.Text = "OK";
			this.oKbutton.UseVisualStyleBackColor = true;
			this.oKbutton.Click += new System.EventHandler(this.OKbuttonClick);
			// 
			// PlaceEditForm2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(471, 366);
			this.Controls.Add(this.oKbutton);
			this.Controls.Add(this.placeEditControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "PlaceEditForm2";
			this.Text = "Редактирование места:";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button oKbutton;
		private Pppv.Editor.PlaceEditControl placeEditControl1;
	}
}
