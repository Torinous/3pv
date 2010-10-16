/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 12.10.2010
 * Time: 3:29
 *
 *
 */

namespace Pppv.Verificator
{
   using System;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.ApplicationFramework;
   using Pppv.ApplicationFramework.Commands;

   public class VerificatorForm : Form
   {
      private PetriNetVerificator netVerificator;

      public VerificatorForm(PetriNetVerificator netVerificator)
      {
         InitializeComponent();
         this.NetVerificator = netVerificator;
      }

      public PetriNetVerificator NetVerificator
      {
         get { return netVerificator; }
         private set { netVerificator = value; }
      }

      private VerificatorMainMenuStrip menuStrip;
      private CommandToolStrip commonToolStrip;
      private StatusStrip statusStrip;
      private TabControl tabControl;
      private ToolStripContainer toolToolStripContainer;

      public VerificatorMainMenuStrip MainVerificatorMenuStrip
      {
         get { return this.menuStrip; }
         private set { this.menuStrip = value; }
      }

      private void InitializeComponent()
      {
         this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.MainVerificatorMenuStrip = new VerificatorMainMenuStrip();

         this.commonToolStrip = new CommandToolStrip();
         this.commonToolStrip.AddCommand(new NullCommand());

         this.statusStrip = new StatusStrip();

         this.tabControl = new TabControl();
         this.toolToolStripContainer = new ToolStripContainer();

         this.toolToolStripContainer.ContentPanel.SuspendLayout();
         this.toolToolStripContainer.TopToolStripPanel.SuspendLayout();
         this.toolToolStripContainer.SuspendLayout();
         this.tabControl.SuspendLayout();
         this.commonToolStrip.SuspendLayout();
         this.SuspendLayout();

         this.statusStrip.Location = new System.Drawing.Point(0, 275);
         this.statusStrip.Name = this.statusStrip.Text = "_statusStrip";
         this.statusStrip.Size = new System.Drawing.Size(599, 24);
         this.statusStrip.TabIndex = 1;

         this.commonToolStrip.AutoSize = true;

         this.toolToolStripContainer.Anchor = (AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
         this.toolToolStripContainer.Location = new System.Drawing.Point(0, 24);
         this.toolToolStripContainer.Name = this.toolToolStripContainer.Text = "toolToolStripContainer";
         this.toolToolStripContainer.Size = new System.Drawing.Size(599, 252);
         this.toolToolStripContainer.AutoSize = true;
         this.toolToolStripContainer.TabIndex = 2;

         this.toolToolStripContainer.ContentPanel.Size = new System.Drawing.Size(599, 228);
         this.toolToolStripContainer.TopToolStripPanel.AutoSize = true;

         this.ClientSize = new System.Drawing.Size(599, 299);
         this.Name = "MainForm";
         this.Text = "3Pv:Editor " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

         this.toolToolStripContainer.ContentPanel.ResumeLayout(false);
         this.toolToolStripContainer.ContentPanel.PerformLayout();
         this.toolToolStripContainer.TopToolStripPanel.ResumeLayout(false);
         this.toolToolStripContainer.TopToolStripPanel.PerformLayout();
         this.toolToolStripContainer.ResumeLayout(false);

         this.Controls.Add(this.toolToolStripContainer);
         this.Controls.Add(this.statusStrip);
         this.Controls.Add(this.MainVerificatorMenuStrip);

         this.toolToolStripContainer.TopToolStripPanel.Controls.Add(this.commonToolStrip);
         this.toolToolStripContainer.ContentPanel.Controls.Add(this.tabControl);

         this.toolToolStripContainer.PerformLayout();
         this.tabControl.ResumeLayout(false);
         this.tabControl.PerformLayout();
         this.commonToolStrip.ResumeLayout(false);
         this.commonToolStrip.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();
      }
   }
}
