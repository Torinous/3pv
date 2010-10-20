/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 18.10.2010
 * Time: 5:29
 *
 *
 */

namespace Pppv.Verificator
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Windows.Forms;

	public class PicViewer : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.ComponentModel.IContainer components;
		private SizeMode sizeMode;

		public PicViewer()
		{
			// This call is required by the Windows.Forms Form Designer.
			this.InitializeComponent();
			this.ImageSizeMode = SizeMode.Scrollable;
		}
		
		public Image Image
		{
			get
			{
				return this.pictureBox1.Image;
			}
			
			set
			{
				this.pictureBox1.Image = value;
				this.SetLayout();
			}
		}

		public SizeMode ImageSizeMode
		{
			get
			{
				return this.sizeMode;
			}
			
			set
			{
				this.sizeMode = value;
				this.AutoScroll = this.sizeMode == SizeMode.Scrollable;
				this.SetLayout();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			
			base.Dispose(disposing);
		}
		
		private void RatioStretch()
		{
			float controlRatio = (float)this.Width / this.Height;
			float imageRatio = (float)this.pictureBox1.Image.Width / this.pictureBox1.Image.Height;

			if (this.Width >= this.pictureBox1.Image.Width && this.Height >= this.pictureBox1.Image.Height)
			{
				this.pictureBox1.Width = this.pictureBox1.Image.Width;
				this.pictureBox1.Height = this.pictureBox1.Image.Height;
			}
			else if (this.Width > this.pictureBox1.Image.Width && this.Height < this.pictureBox1.Image.Height)
			{
				this.pictureBox1.Height = this.Height;
				this.pictureBox1.Width = (int)(this.Height * imageRatio);
			}
			else if (this.Width < this.pictureBox1.Image.Width && this.Height > this.pictureBox1.Image.Height)
			{
				this.pictureBox1.Width = this.Width;
				this.pictureBox1.Height = (int)(this.Width / imageRatio);
			}
			else if (this.Width < this.pictureBox1.Image.Width && this.Height < this.pictureBox1.Image.Height)
			{
				if (this.Width >= this.Height)
				{
					// width image
					if (this.pictureBox1.Image.Width >= this.pictureBox1.Image.Height && imageRatio >= controlRatio)
					{
						this.pictureBox1.Width = this.Width;
						this.pictureBox1.Height = (int)(this.Width / imageRatio);
					}
					else
					{
						this.pictureBox1.Height = this.Height;
						this.pictureBox1.Width = (int)(this.Height * imageRatio);
					}
				}
				else
				{
					if (this.pictureBox1.Image.Width < this.pictureBox1.Image.Height && imageRatio < controlRatio)
					{
						this.pictureBox1.Height = this.Height;
						this.pictureBox1.Width = (int)(this.Height * imageRatio);
					}
					else
					{
						this.pictureBox1.Width = this.Width;
						this.pictureBox1.Height = (int)(this.Width / imageRatio);
					}
				}
			}
			
			this.CenterImage();
		}
		
		private void Scrollable()
		{
			this.pictureBox1.Width = this.pictureBox1.Image.Width;
			this.pictureBox1.Height = this.pictureBox1.Image.Height;
			this.CenterImage();
		}
		
		private void SetLayout()
		{
			if (this.pictureBox1.Image == null)
			{
				return;
			}

			if (this.sizeMode == SizeMode.RatioStretch)
			{
				this.RatioStretch();
			}
			else
			{
				this.AutoScroll = false;
				this.Scrollable();
				this.AutoScroll = true;
			}
		}

		private void CenterImage()
		{
			int top = (int)((this.Height - this.pictureBox1.Height) / 2.0);
			int left = (int)((this.Width - this.pictureBox1.Width) / 2.0);
			if (top < 0)
			{
				top = 0;
			}
			
			if (left < 0)
			{
				left = 0;
			}
			
			this.pictureBox1.Top = top;
			this.pictureBox1.Left = left;
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureBox1.Location = new System.Drawing.Point(16, 17);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(336, 255);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.PictureBox1Click);
			// 
			// PicViewer
			// 
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.pictureBox1);
			this.Name = "PicViewer";
			this.Size = new System.Drawing.Size(370, 296);
			this.Load += new System.EventHandler(this.Viewer_Load);
			this.Resize += new System.EventHandler(this.Viewer_Resize);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
		}
		#endregion

		private void Viewer_Load(object sender, System.EventArgs e)
		{
			this.pictureBox1.Width = 0;
			this.pictureBox1.Height = 0;
			this.SetLayout();
		}

		private void Viewer_Resize(object sender, System.EventArgs e)
		{
			this.SetLayout();
		}
		
		private void PictureBox1Click(object sender, EventArgs e)
		{
			if (this.ImageSizeMode == SizeMode.RatioStretch)
			{
				this.ImageSizeMode = SizeMode.Scrollable;
			}
			else
			{
				this.ImageSizeMode = SizeMode.RatioStretch;
			}
		}
	}
}
