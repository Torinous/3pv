namespace Pppv.Editor
{
   using System;
   using System.Collections;
   using System.ComponentModel;
   using System.Data;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;
   using Pppv.Editor.Tools;

   public class MainForm : Form
   {
      private EditorApplication app;
      private EditorMainMenuStrip menuStrip;

      private EditorToolStrip fileToolStrip;
      private EditorToolStrip toolToolStrip;
      private EditorToolStrip editToolStrip;
      private EditorToolStrip viewToolStrip;
      private StatusStrip statusStrip;
      private TabControlForNets tabControl;
      private ToolStripContainer toolToolStripContainer;

      public MainForm(EditorApplication application)
      {
         this.app = application;
         this.KeyPreview = true;
         this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
         this.InitializeComponent();
      }

      public TabControlForNets TabControl
      {
         get { return this.tabControl; }
      }

      public EditorToolStrip ToolToolStrip
      {
         get { return this.toolToolStrip; }
      }

      public EditorMainMenuStrip MainEditorMenuStrip
      {
         get { return this.menuStrip; }
         private set { this.menuStrip = value; }
      }

      public EditorApplication App
      {
         get { return this.app; }
      }

      private void InitializeComponent()
      {
         this.MainEditorMenuStrip          = new EditorMainMenuStrip();

         this.toolToolStrip          = new EditorToolStrip(
                                                           new SelectToolCommand(typeof(PointerTool)),
                                                           new SelectToolCommand(typeof(PlaceTool)),
                                                           new SelectToolCommand(typeof(TransitionTool)),
                                                           new SelectToolCommand(typeof(ArcTool)),
                                                           new SelectToolCommand(typeof(InhibitorArcTool)),
                                                           new SelectToolCommand(typeof(AnnotationTool)));
         this.fileToolStrip          = new EditorToolStrip(
                                                           new NewNetCommand(),
                                                           new OpenNetCommand(),
                                                           new CloseNetCommand(),
                                                           new SaveCommand(),
                                                           new SaveAsCommand());
         this.editToolStrip          = new EditorToolStrip(
                                                           new UndoCommand(),
                                                           new RedoCommand(),
                                                           new CutCommand(),
                                                           new CopyCommand(),
                                                           new PasteCommand(),
                                                           new DeleteCommand());
         this.viewToolStrip          = new EditorToolStrip(
                                                           new ZoomInCommand(),
                                                           new ZoomOutCommand());

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
         this.Controls.Add(this.menuStrip);

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

      /*TODO: Пригодится при написании команды, потом убъём
       * private void EditAdditionalCode(object sender, EventArgs e)
      {
         EditorApplication app = EditorApplication.Instance;
         if (app.ActiveNet != null)
         {
            Form f = new AdditionalCodeEditForm(app.ActiveNet);
            f.ShowDialog(this);
            f.Dispose();
         }
      }*/
   }
}