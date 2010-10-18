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
   using System.Drawing;
   using System.Data;
   using System.Windows.Forms;

   public class PicViewer : System.Windows.Forms.UserControl
   {
      private System.Windows.Forms.PictureBox pictureBox1;
      private System.ComponentModel.IContainer components;
      private SizeMode sizeMode;

      public PicViewer()
      {
         // This call is required by the Windows.Forms Form Designer.
         InitializeComponent();
         this.ImageSizeMode = SizeMode.RatioStretch;
      }

      protected override void Dispose( bool disposing )
      {
         if( disposing )
         {
            if(components != null)
            {
               components.Dispose();
            }
         }
         base.Dispose( disposing );
      }
      
      public Image Image
      {
         get{return this.pictureBox1.Image;}
         set
         {
            this.pictureBox1.Image = value;
            this.SetLayout();
            //this.ChangeSize();
         }
      }
      public SizeMode ImageSizeMode
      {
         get { return this.sizeMode; }
         set
         {
            this.sizeMode = value;
            this.AutoScroll = (this.sizeMode == SizeMode.Scrollable );
            this.SetLayout();
         }
      }

      private void RatioStretch()
      {
         float pRatio = (float)this.Width/this.Height;
         float imRatio = (float)this.pictureBox1.Image.Width/this.pictureBox1.Image.Height;

         if ( this.Width >= this.pictureBox1.Image.Width && this.Height >= this.pictureBox1.Image.Height )
         {
            this.pictureBox1.Width = this.pictureBox1.Image.Width;
            this.pictureBox1.Height = this.pictureBox1.Image.Height;
         }
         else if( this.Width > this.pictureBox1.Image.Width && this.Height < this.pictureBox1.Image.Height)
         {
            this.pictureBox1.Height = this.Height;
            this.pictureBox1.Width = (int)(this.Height * imRatio);
         }
         else if( this.Width < this.pictureBox1.Image.Width && this.Height > this.pictureBox1.Image.Height)
         {
            this.pictureBox1.Width = this.Width;
            this.pictureBox1.Height = (int)(this.Width / imRatio);
         }
         else if ( this.Width < this.pictureBox1.Image.Width && this.Height < this.pictureBox1.Image.Height )
         {
            if (this.Width >= this.Height )
            {
               //width image
               if ( this.pictureBox1.Image.Width >= this.pictureBox1.Image.Height && imRatio >= pRatio )
               {
                  this.pictureBox1.Width = this.Width;
                  this.pictureBox1.Height = (int)(this.Width / imRatio);
               }
               else
               {
                  this.pictureBox1.Height = this.Height;
                  this.pictureBox1.Width = (int)(this.Height * imRatio);
               }
            }
            else
            {
               //width image
               if ( this.pictureBox1.Image.Width < this.pictureBox1.Image.Height && imRatio < pRatio )
               {
                  this.pictureBox1.Height = this.Height;
                  this.pictureBox1.Width = (int)(this.Height * imRatio);
               }
               else // height image
               {
                  this.pictureBox1.Width = this.Width;
                  this.pictureBox1.Height = (int)(this.Width / imRatio);
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
         if ( this.pictureBox1.Image == null )
            return;
         if ( this.sizeMode == SizeMode.RatioStretch )
            this.RatioStretch();
         else
         {
            this.AutoScroll = false;
            this.Scrollable();
            this.AutoScroll = true;
            
         }
      }
      private void CenterImage()
      {
         int top = (int)((this.Height - this.pictureBox1.Height)/2.0);
         int left = (int)((this.Width - this.pictureBox1.Width)/2.0);
         if ( top < 0 )
            top = 0;
         if ( left < 0 )
            left = 0;
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
         this.components = new System.ComponentModel.Container();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         this.SuspendLayout();
         // 
         // pictureBox1
         // 
         this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
         this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.pictureBox1.Location = new System.Drawing.Point(0, 0);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(352, 272);
         this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.pictureBox1.TabIndex = 0;
         this.pictureBox1.TabStop = false;
         // 
         // PicViewer
         // 
         this.AutoScroll = true;
         this.BackColor = System.Drawing.Color.Black;
         this.Controls.Add(this.pictureBox1);
         this.Name = "PicViewer";
         this.Size = new System.Drawing.Size(352, 272);
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
      
   }
}
