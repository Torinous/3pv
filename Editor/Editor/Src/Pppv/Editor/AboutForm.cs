namespace Pppv.Editor
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Globalization;
	using System.Reflection;
	using System.Text;
	using System.Windows.Forms;

	public class AboutForm : Form
	{
		private Label label1;
		private Button button1;
		private Label label2;
		private Label label3;
		private LinkLabel linkLabel1;
		private TextBox descBox;
		private System.ComponentModel.IContainer components = null;

		public AboutForm()
		{
			this.InitializeComponent();
		}

		public static string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (!String.IsNullOrEmpty(titleAttribute.Title))
					{
						return titleAttribute.Title;
					}
				}

				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public static string AssemblyDescription
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (attributes.Length == 0)
				{
					return String.Empty;
				}

				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}

			base.Dispose(disposing);
		}

		private void CloseClickHandler(object sender, EventArgs e)
		{
			this.Close();
		}

		private void AboutForm_Load(object sender, EventArgs e)
		{
			this.label1.Text = AssemblyTitle + String.Format(CultureInfo.CurrentCulture, "  Version {0}", EditorApplication.AssemblyVersion);
			this.descBox.Text = AssemblyDescription;
		}

		private void LinkClickHandler(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:" + this.linkLabel1.Text);
			this.linkLabel1.LinkVisited = true;
		}

		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.descBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();

			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";

			this.button1.Location = new System.Drawing.Point(238, 160);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.CloseClickHandler);

			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 108);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Authors:";

			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(23, 121);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(150, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Antonov Dmitry  aka  Torinous";

			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(179, 121);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(89, 13);
			this.linkLabel1.TabIndex = 4;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Torinous@gmail.com";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClickHandler);

			this.descBox.BackColor = System.Drawing.Color.PowderBlue;
			this.descBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.descBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.descBox.Enabled = false;
			this.descBox.Location = new System.Drawing.Point(14, 30);
			this.descBox.Multiline = true;
			this.descBox.Name = "DescBox";
			this.descBox.ReadOnly = true;
			this.descBox.Size = new System.Drawing.Size(298, 65);
			this.descBox.TabIndex = 6;

			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(325, 195);
			this.Controls.Add(this.descBox);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About 3Pv:Editor";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
	}
}
