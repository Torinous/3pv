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
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.Editor;

   public class VerificatorForm : Form
   {
      private PetriNet netVerificator;

      public VerificatorForm(PetriNetVerificator netVerificator)
      {
         InitializeComponent();
         this.NetVerificator = netVerificator;
      }

      public PetriNet NetVerificator
      {
         get { return netVerificator; }
         private set { netVerificator = value; }
      }

      private EditorMainMenuStrip menuStrip;
      private EditorToolStrip fileToolStrip;
      private EditorToolStrip toolToolStrip;
      private EditorToolStrip editToolStrip;
      private EditorToolStrip viewToolStrip;
      private StatusStrip statusStrip;
      private TabControlForNets tabControl;
      private ToolStripContainer toolToolStripContainer;

      public EditorToolStrip ToolToolStrip
      {
         get { return this.toolToolStrip; }
      }

      public EditorMainMenuStrip MainEditorMenuStrip
      {
         get { return this.menuStrip; }
         private set { this.menuStrip = value; }
      }

      private void InitializeComponent()
      {
         this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.MainEditorMenuStrip = new EditorMainMenuStrip();

         this.toolToolStrip = new EditorToolStrip();
         this.toolToolStrip.AddCommand(new SelectToolCommand(typeof(PointerTool)));
         this.toolToolStrip.AddCommand(new SelectToolCommand(typeof(PlaceTool)));
         this.toolToolStrip.AddCommand(new SelectToolCommand(typeof(TransitionTool)));
         this.toolToolStrip.AddCommand(new SelectToolCommand(typeof(ArcTool)));
         this.toolToolStrip.AddCommand(new SelectToolCommand(typeof(InhibitorArcTool)));
         this.toolToolStrip.AddCommand(new SelectToolCommand(typeof(AnnotationTool)));

         this.fileToolStrip = new EditorToolStrip();
         this.fileToolStrip.AddCommand(new NewNetCommand());
         this.fileToolStrip.AddCommand(new OpenNetCommand());
         this.fileToolStrip.AddCommand(new CloseNetCommand());
         this.fileToolStrip.AddCommand(new SaveCommand());
         this.fileToolStrip.AddCommand(new SaveAsCommand());
         this.fileToolStrip.AddCommand(new AnalyzeCommand());

         this.editToolStrip = new EditorToolStrip();
         this.editToolStrip.AddCommand(new UndoCommand());
         this.editToolStrip.AddCommand(new RedoCommand());
         this.editToolStrip.AddCommand(new CutCommand());
         this.editToolStrip.AddCommand(new CopyCommand());
         this.editToolStrip.AddCommand(new PasteCommand());
         this.editToolStrip.AddCommand(new DeleteSelectedCommand());

         this.viewToolStrip = new EditorToolStrip();
         this.viewToolStrip.AddCommand(new ZoomInCommand());
         this.viewToolStrip.AddCommand(new ZoomOutCommand());

         this.statusStrip = new StatusStrip();

         this.tabControl = new TabControlForNets();
         this.toolToolStripContainer = new ToolStripContainer();

         this.toolToolStripContainer.ContentPanel.SuspendLayout();
         this.toolToolStripContainer.TopToolStripPanel.SuspendLayout();
         this.toolToolStripContainer.SuspendLayout();
         this.tabControl.SuspendLayout();
         this.toolToolStrip.SuspendLayout();
         this.SuspendLayout();

         this.statusStrip.Location = new System.Drawing.Point(0, 275);
         this.statusStrip.Name = this.statusStrip.Text = "_statusStrip";
         this.statusStrip.Size = new System.Drawing.Size(599, 24);
         this.statusStrip.TabIndex = 1;

         this.toolToolStrip.AutoSize = true;

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
         this.Text = "3Pv:Editor " + EditorApplication.AssemblyVersion;

         this.toolToolStripContainer.ContentPanel.ResumeLayout(false);
         this.toolToolStripContainer.ContentPanel.PerformLayout();
         this.toolToolStripContainer.TopToolStripPanel.ResumeLayout(false);
         this.toolToolStripContainer.TopToolStripPanel.PerformLayout();
         this.toolToolStripContainer.ResumeLayout(false);

         this.Controls.Add(this.toolToolStripContainer);
         this.Controls.Add(this.statusStrip);
         this.Controls.Add(this.MainEditorMenuStrip);

         this.toolToolStripContainer.TopToolStripPanel.Controls.Add(this.viewToolStrip);
         this.toolToolStripContainer.TopToolStripPanel.Controls.Add(this.toolToolStrip);
         this.toolToolStripContainer.TopToolStripPanel.Controls.Add(this.editToolStrip);
         this.toolToolStripContainer.TopToolStripPanel.Controls.Add(this.fileToolStrip);
         this.toolToolStripContainer.ContentPanel.Controls.Add(this.tabControl);

         this.toolToolStripContainer.PerformLayout();
         this.tabControl.ResumeLayout(false);
         this.tabControl.PerformLayout();
         this.toolToolStrip.ResumeLayout(false);
         this.toolToolStrip.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();
      }
   }
}
