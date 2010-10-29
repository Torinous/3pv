namespace Pppv.Editor
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.IO;
	using System.Text;
	using System.Windows.Forms;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Editor.Commands;
	using Pppv.Editor.Tools;
	using Pppv.Net;

	public class MainForm : Form
	{
		private static MainForm instance;
		private EditorMainMenuStrip menuStrip;
		private EditorToolStrip fileToolStrip;
		private EditorToolStrip toolToolStrip;
		private EditorToolStrip editToolStrip;
		private EditorToolStrip viewToolStrip;
		private StatusStrip statusStrip;
		private TabControlForNets tabControl;
		private ToolStripContainer toolToolStripContainer;

		public MainForm()
		{
			this.KeyPreview = true;
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
			this.InitializeComponent();
			this.TabControl.SelectedIndexChanged += this.TabIndexChangeHandler;
			this.Closing += this.ClosingHandler;
			instance = this;
		}

		public event EventHandler<EventArgs> ActiveNetChange;

		public static MainForm Instance
		{
			get { return instance; }
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

		public PetriNetGraphical ActiveNet
		{
			get
			{
				if (this.TabControl.SelectedIndex != -1)
				{
					return (this.TabControl.TabPages[this.TabControl.SelectedIndex] as TabPageForNet).Net;
				}
				else
				{
					return null;
				}
			}
		}

		public void CloseNet(PetriNet net)
		{
			if (this.TabControl.SelectedIndex != -1)
			{
				this.TabControl.CloseTab(this.TabControl.TabIdForNet(net));
				this.OnActiveNetChange(new EventArgs());
			}
		}

		public void NewNet()
		{
			PetriNetGraphical net = new PetriNetGraphical();
			TabPageForNet addedTabPage = this.TabControl.AddNewTab();
			addedTabPage.PutNetOnTabPage(net);
			this.OnActiveNetChange(new EventArgs());
		}

		public void LoadNet(TextReader netStream, string fileName)
		{
			if (netStream != null)
			{
				PetriNet net = new PetriNet();
				XmlSerializer serealizer = new XmlSerializer(net.GetType());
				net = (PetriNet)serealizer.Deserialize(netStream);
				PetriNetGraphical gnet = new PetriNetGraphical(net);
				gnet.FileOfNetPath = fileName;
				TabPageForNet addedTabPage = this.TabControl.AddNewTab();
				addedTabPage.PutNetOnTabPage(gnet);
				this.OnActiveNetChange(new EventArgs());
			}
		}

		protected virtual void OnActiveNetChange(EventArgs e)
		{
			if (this.ActiveNetChange != null)
			{
				this.ActiveNetChange(this, e);
			}
		}

		private void InitializeComponent()
		{
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
			this.viewToolStrip.AddCommand(new AdditionalCodeEditCommand());

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

		private void TabIndexChangeHandler(object sender, EventArgs e)
		{
			this.OnActiveNetChange(new EventArgs());
		}
		
		private void ClosingHandler(object sender, CancelEventArgs args)
		{
			MacroCommand command = new MacroCommand();
			
			foreach (TabPageForNet page in this.TabControl.TabPages)
			{
				command.Add(new CloseNetCommand(page.Net));
			}

			command.Execute();
			if (this.TabControl.TabPages.Count != 0)
			{
				args.Cancel = true;
			}
		}
	}
}