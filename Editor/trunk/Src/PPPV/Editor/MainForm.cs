using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PPPv.Editor {
   public class MainForm : Form{

      /*Меню*/
      public MainMenuStrip menuStrip;


      /*Панель инструментов*/
      public NetToolStrip toolStrip;


      private StatusStrip _statusStrip;

      private TabControlForNets _tabControl;
      
      public TabControlForNets TabControl {
         get{
            return _tabControl;
         }
      }

      public NetToolStrip ToolController {
         get{
            return toolStrip;
         }
      }
      
      public MainMenuStrip MainMenuStrip{
         get{
            return menuStrip;
         }
         private set{
            menuStrip = value;
         }
      }

      private ToolStripContainer toolStripContainer;

      public MainForm() {
         this.KeyPreview = true;
         InitializeComponent();
      }

      private void InitializeComponent() {
         /*Меню*/
         this.menuStrip          = new MainMenuStrip();

         /*Панель инструментов*/
         this.toolStrip          = new NetToolStrip();
         /*Статус строка*/
         this._statusStrip       = new StatusStrip();

         this._tabControl        = new TabControlForNets();
         this.toolStripContainer = new ToolStripContainer();
         /*System.ComponentModel.ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));*/

         this.toolStripContainer.ContentPanel.SuspendLayout();
         this.toolStripContainer.TopToolStripPanel.SuspendLayout();
         this.toolStripContainer.SuspendLayout();
         this._tabControl.SuspendLayout();
         this.toolStrip.SuspendLayout();
         this.SuspendLayout();
         //
         // _statusStrip
         //
         this._statusStrip.Location = new System.Drawing.Point(0, 275);
         this._statusStrip.Name = this._statusStrip.Text = "_statusStrip";
         this._statusStrip.Size = new System.Drawing.Size(599, 24);
         this._statusStrip.TabIndex = 1;
         //
         // toolStrip
         //
         this.toolStrip.AutoSize = true;
         //
         // toolStripContainer
         //
         this.toolStripContainer.Anchor = ( (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)| System.Windows.Forms.AnchorStyles.Left)| System.Windows.Forms.AnchorStyles.Right)));
         this.toolStripContainer.Location = new System.Drawing.Point(0, 24);
         this.toolStripContainer.Name = this.toolStripContainer.Text = "toolStripContainer";
         this.toolStripContainer.Size = new System.Drawing.Size(599, 252);
         this.toolStripContainer.AutoSize = true;
         this.toolStripContainer.TabIndex = 2;
         //
         // toolStripContainer.ContentPanel
         //
         
         this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(599, 228);
         this.toolStripContainer.TopToolStripPanel.AutoSize = true;
         //
         // Form1
         //
         //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(599, 299);
         this.MainMenuStrip = this.menuStrip;
         this.Name = "MainForm";
         this.Text = "3Pv";

         this.toolStripContainer.ContentPanel.ResumeLayout(false);
         this.toolStripContainer.ContentPanel.PerformLayout();
         this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
         this.toolStripContainer.TopToolStripPanel.PerformLayout();
         this.toolStripContainer.ResumeLayout(false);

         this.Controls.Add(this.toolStripContainer);
         this.Controls.Add(this._statusStrip);
         this.Controls.Add(this.menuStrip);
         this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
         this.toolStripContainer.ContentPanel.Controls.Add(this._tabControl);

         this.toolStripContainer.PerformLayout();
         this._tabControl.ResumeLayout(false);
         this._tabControl.PerformLayout();
         this.toolStrip.ResumeLayout(false);
         this.toolStrip.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();
      }
   }
}
