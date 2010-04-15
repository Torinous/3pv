using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Editor.Commands;
using PPPV.Editor.Tools;

namespace PPPV.Editor
{
  public class MainForm : Form
  {
    private EditorApplication app;
    /*Меню*/
    public MainMenuStrip menuStrip;

    /*Панели инструментов*/
    private EditorToolStrip fileToolStrip;
    private EditorToolStrip toolToolStrip;
    private EditorToolStrip editToolStrip;
    private EditorToolStrip viewToolStrip;
    

    private StatusStrip _statusStrip;

    private TabControlForNets _tabControl;

    public TabControlForNets TabControl
    {
      get
      {
        return _tabControl;
      }
    }

    public EditorToolStrip ToolToolStrip
    {
      get
      {
        return toolToolStrip;
      }
    }

    public MainMenuStrip MainMenuStrip
    {
      get
      {
        return menuStrip;
      }
      private set
      {
        menuStrip = value;
      }
    }

    public EditorApplication App
    {
      get
      {
        return app;
      }
    }

    private ToolStripContainer toolToolStripContainer;

    public MainForm(EditorApplication a)
    {
      app = a;
      this.KeyPreview = true;
      this.SetStyle( ControlStyles.AllPaintingInWmPaint |  ControlStyles.UserPaint |  ControlStyles.DoubleBuffer, true);
      InitializeComponent();
      //ToolStrip.toolToolStripButtonAdditionalCode.Click += EditAdditionalCode;
    }

    private void InitializeComponent()
    {
      /*Меню*/
      this.menuStrip          = new MainMenuStrip();

      /*Панель инструментов*/
      
      this.toolToolStrip          = new EditorToolStrip( new SelectToolCommand( PointerTool.Instance ),
                                                         new SelectToolCommand( PlaceTool.Instance ),
                                                         new SelectToolCommand( TransitionTool.Instance ),
                                                         new SelectToolCommand( ArcTool.Instance ),
                                                         new SelectToolCommand( InhibitorArcTool.Instance ),
                                                         new SelectToolCommand( AnnotationTool.Instance )
                                                       );
      this.fileToolStrip          = new EditorToolStrip( new NewNetCommand(),
                                                         new OpenNetCommand(),
                                                         new CloseNetCommand(),
                                                         new SaveCommand(),
                                                         new SaveAsCommand() 
                                                       );
      this.editToolStrip          = new EditorToolStrip( new UndoCommand(),
                                                         new RedoCommand(),
                                                         new CutCommand(),
                                                         new CopyCommand(),
                                                         new PasteCommand(),
                                                         new DeleteCommand()
                                                       );
      this.viewToolStrip          = new EditorToolStrip( new ZoomInCommand(),
                                                         new ZoomOutCommand()
                                                       );
      
      /*Статус строка*/
      this._statusStrip       = new StatusStrip();

      this._tabControl        = new TabControlForNets();
      this.toolToolStripContainer = new ToolStripContainer();
      /*System.ComponentModel.ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));*/

      this.toolToolStripContainer.ContentPanel.SuspendLayout();
      this.toolToolStripContainer.TopToolStripPanel.SuspendLayout();
      this.toolToolStripContainer.SuspendLayout();
      this._tabControl.SuspendLayout();
      this.toolToolStrip.SuspendLayout();
      this.SuspendLayout();
      //
      // _statusStrip
      //
      this._statusStrip.Location = new System.Drawing.Point(0, 275);
      this._statusStrip.Name = this._statusStrip.Text = "_statusStrip";
      this._statusStrip.Size = new System.Drawing.Size(599, 24);
      this._statusStrip.TabIndex = 1;
      //
      // toolToolStrip
      //
      this.toolToolStrip.AutoSize = true;
      //
      // toolToolStripContainer
      //
      this.toolToolStripContainer.Anchor = ( (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)| System.Windows.Forms.AnchorStyles.Left)| System.Windows.Forms.AnchorStyles.Right)));
      this.toolToolStripContainer.Location = new System.Drawing.Point(0, 24);
      this.toolToolStripContainer.Name = this.toolToolStripContainer.Text = "toolToolStripContainer";
      this.toolToolStripContainer.Size = new System.Drawing.Size(599, 252);
      this.toolToolStripContainer.AutoSize = true;
      this.toolToolStripContainer.TabIndex = 2;
      //
      // toolToolStripContainer.ContentPanel
      //

      this.toolToolStripContainer.ContentPanel.Size = new System.Drawing.Size(599, 228);
      this.toolToolStripContainer.TopToolStripPanel.AutoSize = true;
      //
      // Form1
      //
      this.ClientSize = new System.Drawing.Size(599, 299);
      this.Name = "MainForm";
      this.Text = "3Pv:Editor " + App.AssemblyVersion;

      this.toolToolStripContainer.ContentPanel.ResumeLayout(false);
      this.toolToolStripContainer.ContentPanel.PerformLayout();
      this.toolToolStripContainer.TopToolStripPanel.ResumeLayout(false);
      this.toolToolStripContainer.TopToolStripPanel.PerformLayout();
      this.toolToolStripContainer.ResumeLayout(false);

      this.Controls.Add(this.toolToolStripContainer);
      this.Controls.Add(this._statusStrip);
      this.Controls.Add(this.menuStrip);

      this.toolToolStripContainer.TopToolStripPanel.Controls.Add(this.viewToolStrip);
      this.toolToolStripContainer.TopToolStripPanel.Controls.Add(this.toolToolStrip);
      this.toolToolStripContainer.TopToolStripPanel.Controls.Add(this.editToolStrip);
      this.toolToolStripContainer.TopToolStripPanel.Controls.Add(this.fileToolStrip);
      this.toolToolStripContainer.ContentPanel.Controls.Add(this._tabControl);

      this.toolToolStripContainer.PerformLayout();
      this._tabControl.ResumeLayout(false);
      this._tabControl.PerformLayout();
      this.toolToolStrip.ResumeLayout(false);
      this.toolToolStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void EditAdditionalCode(object sender, EventArgs e)
    {
      EditorApplication app = EditorApplication.Instance;
      if(app.ActiveNet != null)
      {
        Form f = new AdditionalCodeEditForm(app.ActiveNet);
        f.ShowDialog(this);
        f.Dispose();
      }
    }
  } // class
} // namespace
