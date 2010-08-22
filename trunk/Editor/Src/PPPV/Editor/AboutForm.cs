namespace PPPV.Editor
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Text;
	using System.Windows.Forms;
	using System.Reflection;
	using System.Globalization;
	
  public class AboutForm : Form
  {
    public AboutForm()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void AboutForm_Load(object sender, EventArgs e)
    {
      label1.Text = AssemblyTitle + String.Format(CultureInfo.CurrentCulture, "  Version {0}", EditorApplication.AssemblyVersion);
      DescBox.Text = AssemblyDescription;
    }

    public static string AssemblyTitle
    {
      get
      {
        // Get all Title attributes on this assembly
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        // If there is at least one Title attribute
        if (attributes.Length > 0)
      {
        // Select the first one
        AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
          // If it is not an empty string, return it
          if (!String.IsNullOrEmpty(titleAttribute.Title))
            return titleAttribute.Title;
        }
        // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
        return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    public static string AssemblyDescription
    {
      get
      {
        // Get all Description attributes on this assembly
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
        // If there aren't any Description attributes, return an empty string
        if (attributes.Length == 0)
        return "";
        // If there is a Description attribute, return its value
        return ((AssemblyDescriptionAttribute)attributes[0]).Description;
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) 
    {
      System.Diagnostics.Process.Start("mailto:"+linkLabel1.Text);
      linkLabel1.LinkVisited = true;
    }

    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.DescBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      //
      // label1
      //
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "label1";
      //
      // button1
      //
      this.button1.Location = new System.Drawing.Point(238, 160);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "OK";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      //
      // label2
      //
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 108);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(46, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Authors:";
      //
      // label3
      //
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(23, 121);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(150, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Antonov Dmitry  aka  Torinous";
      //
      // linkLabel1
      //
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(179, 121);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(89, 13);
      this.linkLabel1.TabIndex = 4;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Torinous@gmail.com";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      //
      // DescBox
      //
      this.DescBox.BackColor = System.Drawing.Color.PowderBlue;
      this.DescBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.DescBox.Cursor = System.Windows.Forms.Cursors.Default;
      this.DescBox.Enabled = false;
      this.DescBox.Location = new System.Drawing.Point(14, 30);
      this.DescBox.Multiline = true;
      this.DescBox.Name = "DescBox";
      this.DescBox.ReadOnly = true;
      this.DescBox.Size = new System.Drawing.Size(298, 65);
      this.DescBox.TabIndex = 6;
      //
      // AboutForm
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(325, 195);
      this.Controls.Add(this.DescBox);
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

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.TextBox DescBox;
  }
}
