using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Text;

namespace PPPV.Editor 
{
   public class MainForm : Form{

      /*Меню*/
      public MainMenuStrip menuStrip;


      /*Панель инструментов*/
      public Tools.ToolStrip toolStrip;


      private StatusStrip _statusStrip;

      private TabControlForNets _tabControl;
      
      public TabControlForNets TabControl {
         get{
            return _tabControl;
         }
      }

      public ToolStrip ToolStrip {
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
         /*Привязка обработчиков*/
         MainMenuStrip.toolStripMenuNew.Click += NewNet;
         MainMenuStrip.toolStripMenuOpen.Click += OpenNet;
         MainMenuStrip.toolStripMenuExit.Click += CloseApplication;
         MainMenuStrip.toolStripMenuAbout.Click += ShowAboutForm;
         //ToolStrip.toolStripButtonAdditionalCode.Click += EditAdditionalCode;
      }

      private void InitializeComponent() {
         /*Меню*/
         this.menuStrip          = new MainMenuStrip();

         /*Панель инструментов*/
         this.toolStrip          = new Tools.ToolStrip();
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
      
      private void NewNet(object sender, EventArgs e){
         Net.PetriNet _net = new Net.PetriNet();
         TabControl.AddNewTab(_net);
      }

      private void OpenNet(object sender, EventArgs e){
         StreamReader stream;
         OpenFileDialog openFileDialog = new OpenFileDialog();

         openFileDialog.Filter = "txt files (*.pnml)|*.pnml|All files (*.*)|*.*";
         openFileDialog.FilterIndex = 1 ;
         openFileDialog.RestoreDirectory = true ;

         if(openFileDialog.ShowDialog() == DialogResult.OK){
            stream = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding(1251));
            if(stream != null){
               Net.PetriNet _net = new Net.PetriNet();
               XmlSerializer serealizer = new XmlSerializer(_net.GetType());
               _net = (Net.PetriNet)serealizer.Deserialize(stream);
               _net.LinkedFile = openFileDialog.FileName;
               TabControl.AddNewTab(_net);
               stream.Close();
            }
         }
      }

      private void CloseApplication(object sender, EventArgs e){
         this.Close();
      }

      private void EditAdditionalCode(object sender, EventArgs e){
         if(_tabControl.ActiveNet != null){
            Form f = new AdditionalCodeEditForm(_tabControl.ActiveNet);
            f.ShowDialog(this);
            f.Dispose();
         }
      }

      private void ShowAboutForm(object sender, EventArgs e){
         Form f = new AboutForm();
         f.ShowDialog(this);
         f.Dispose();
      }
   } // class
} // namespace
