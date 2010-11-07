/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.11.2010
 * Time: 15:02
 *
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Drawing;
	using System.Text;
	using System.IO;
	using System.Windows.Forms;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	
	using Pppv.Commands;
	using Pppv.Net;
	using Pppv.Editor.Shapes;
	using Pppv.Editor.Commands;
	using Pppv.Utils;
	using Pppv.ApplicationFramework;
	using Pppv.Verificator;
	using Pppv.Editor.Tools;

	public partial class EditorForm : Form
	{
		private CommandManager commandManager;
		private ToolsManager toolsManager;
		
		public EditorForm()
		{
			this.toolsManager = new ToolsManager();
			this.InitializeComponent();
			this.InitializeCommandManager();
			Application.Idle += this.OnIdle;
			this.ActiveNetChange += ToolsChekedSynchronizer;
			this.TabControl.SelectedIndexChanged += SelectedIndexChangedHandler;
			this.Closing += this.ClosingHandler;
		}
		
		public event EventHandler<EventArgs> ActiveNetChange;

		public ToolsManager ToolsManager
		{
			get { return toolsManager; }
			set { toolsManager = value; }
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
		
		public CommandManager CommandManager
		{
			get { return this.commandManager; }
			set { this.commandManager = value; }
		}
		
		public EditorTabControl TabControl
		{
			get { return this.editorTabControl; }
		}
		
		protected virtual void OnActiveNetChange(EventArgs e)
		{
			if (this.ActiveNetChange != null)
			{
				this.ActiveNetChange(this, e);
			}
		}

		private void InitializeCommandManager()
		{
			this.CommandManager = new CommandManager();
			this.AddCommandsToManager();
			this.AssociateCommandsWithUI();
		}

		private void AddCommandsToManager()
		{
			this.CommandManager.Commands.Add(new NewNetCommand(this.NewNetCommandHandler, null));
			this.CommandManager.Commands.Add(new OpenNetCommand(this.OpenNetCommandHandler, null));
			this.CommandManager.Commands.Add(new CloseNetCommand(this.CloseNetCommandHandler, null));
			this.CommandManager.Commands.Add(new SaveNetCommand(this.SaveNetCommandHandler, null));
			this.CommandManager.Commands.Add(new SaveAsNetCommand(this.SaveAsNetCommandHandler, null));
			this.CommandManager.Commands.Add(new QuitCommand(this.QuiteCommandHandler, null));
			
			this.CommandManager.Commands.Add(new UndoCommand(null, null));
			this.CommandManager.Commands.Add(new RedoCommand(null, null));
			this.CommandManager.Commands.Add(new CutCommand(null, null));
			this.CommandManager.Commands.Add(new CopyCommand(null, null));
			this.CommandManager.Commands.Add(new PasteCommand(null, null));
			this.CommandManager.Commands.Add(new DeleteSelectedCommand(null, null));
			
			this.CommandManager.Commands.Add(new ZoomInCommand(this.ZoomInCommandHandler, null));
			this.CommandManager.Commands.Add(new ZoomOutCommand(this.ZoomOutCommandHandler, null));
			
			this.CommandManager.Commands.Add(new SelectPointerToolCommand(this.SelectPointerToolCommandHandler, null));
			this.CommandManager.Commands.Add(new SelectPlaceToolCommand(this.SelectPlaceToolCommandHandler, null));
			this.CommandManager.Commands.Add(new SelectTransitionToolCommand(this.SelectTransitionToolCommandHandler, null));
			this.CommandManager.Commands.Add(new SelectArcToolCommand(this.SelectArcToolCommandHandler, null));
			this.CommandManager.Commands.Add(new SelectInhibitorArcToolCommand(this.SelectInhibitorArcToolCommandHandler, null));
			this.CommandManager.Commands.Add(new SelectAnnotationToolCommand(this.SelectAnnotationToolCommandHandler, null));
			
			this.CommandManager.Commands.Add(new AnalyzeCommand(this.AnalyzeCommandHandler, null));
		}
		
		private void SetCommandEnabled(bool state)
		{
			this.CommandManager.Commands[CloseNetCommand.id].Enabled = state;
			this.CommandManager.Commands[SaveNetCommand.id].Enabled = state;
			this.CommandManager.Commands[SaveAsNetCommand.id].Enabled = state;
			
			this.CommandManager.Commands[UndoCommand.id].Enabled = state;
			this.CommandManager.Commands[RedoCommand.id].Enabled = state;
			this.CommandManager.Commands[CutCommand.id].Enabled = state;
			this.CommandManager.Commands[CopyCommand.id].Enabled = state;
			this.CommandManager.Commands[PasteCommand.id].Enabled = state;
			this.CommandManager.Commands[DeleteSelectedCommand.id].Enabled = state;
			
			this.CommandManager.Commands[ZoomInCommand.id].Enabled = state;
			this.CommandManager.Commands[ZoomOutCommand.id].Enabled = state;
			
			this.CommandManager.Commands[SelectPointerToolCommand.id].Enabled = state;
			this.CommandManager.Commands[SelectPlaceToolCommand.id].Enabled = state;
			this.CommandManager.Commands[SelectTransitionToolCommand.id].Enabled = state;
			this.CommandManager.Commands[SelectArcToolCommand.id].Enabled = state;
			this.CommandManager.Commands[SelectInhibitorArcToolCommand.id].Enabled = state;
			this.CommandManager.Commands[SelectAnnotationToolCommand.id].Enabled = state;
			
			this.CommandManager.Commands[AnalyzeCommand.id].Enabled = state;
		}
		
		private void AssociateCommandsWithUI()
		{
			this.CommandManager.Commands[QuitCommand.id].CommandInstances.Add(new Object[]{this.quiteToolStripMenuItem, this.quiteToolStripButton});
			this.CommandManager.Commands[NewNetCommand.id].CommandInstances.Add(new Object[]{this.newNetToolStripMenuItem, this.newNetToolStripButton});
			this.CommandManager.Commands[OpenNetCommand.id].CommandInstances.Add(new Object[]{this.openNetToolStripMenuItem, this.openNetToolStripButton});
			this.CommandManager.Commands[CloseNetCommand.id].CommandInstances.Add(new Object[]{this.closeNetToolStripMenuItem, this.closeNetToolStripButton});
			this.CommandManager.Commands[SaveNetCommand.id].CommandInstances.Add(new Object[]{this.saveNetToolStripMenuItem, this.saveNetToolStripButton});
			this.CommandManager.Commands[SaveAsNetCommand.id].CommandInstances.Add(new Object[]{this.saveAsNetToolStripMenuItem, this.saveAsNetToolStripButton});
			
			this.CommandManager.Commands[UndoCommand.id].CommandInstances.Add(new Object[]{this.undoToolStripMenuItem, this.undoToolStripButton});
			this.CommandManager.Commands[RedoCommand.id].CommandInstances.Add(new Object[]{this.redoToolStripMenuItem, this.redoToolStripButton});
			this.CommandManager.Commands[CutCommand.id].CommandInstances.Add(new Object[]{this.cutToolStripMenuItem, this.cutToolStripButton});
			this.CommandManager.Commands[CopyCommand.id].CommandInstances.Add(new Object[]{this.copyToolStripMenuItem, this.copyToolStripButton});
			this.CommandManager.Commands[PasteCommand.id].CommandInstances.Add(new Object[]{this.pasteToolStripMenuItem, this.pasteToolStripButton});
			this.CommandManager.Commands[DeleteCommand.id].CommandInstances.Add(new Object[]{this.deleteToolStripMenuItem, this.deleteToolStripButton});
			
			this.CommandManager.Commands[ZoomInCommand.id].CommandInstances.Add(new Object[]{this.zoomInToolStripMenuItem, this.zoomInToolStripButton});
			this.CommandManager.Commands[ZoomOutCommand.id].CommandInstances.Add(new Object[]{this.zoomOutToolStripMenuItem, this.zoomOutToolStripButton});
			
			this.CommandManager.Commands[SelectPointerToolCommand.id].CommandInstances.Add(new Object[]{this.selectPointerToolToolStripMenuItem, this.selectPointerToolToolStripButton});
			this.CommandManager.Commands[SelectPlaceToolCommand.id].CommandInstances.Add(new Object[]{this.selectPlaceToolToolStripMenuItem, this.selectPlaceToolToolStripButton});
			this.CommandManager.Commands[SelectTransitionToolCommand.id].CommandInstances.Add(new Object[]{this.selectTransitionToolToolStripMenuItem, this.selectTransitionToolToolStripButton});
			this.CommandManager.Commands[SelectArcToolCommand.id].CommandInstances.Add(new Object[]{this.selectArcToolToolStripMenuItem, this.selectArcToolToolStripButton});
			this.CommandManager.Commands[SelectInhibitorArcToolCommand.id].CommandInstances.Add(new Object[]{this.selectInhibitorArcToolToolStripMenuItem, this.selectInhibitorArcToolToolStripButton});
			this.CommandManager.Commands[SelectAnnotationToolCommand.id].CommandInstances.Add(new Object[]{this.selectAnnotationToolToolStripMenuItem, this.selectAnnotationToolToolStripButton});
			
			
			this.CommandManager.Commands[AnalyzeCommand.id].CommandInstances.Add(new Object[]{this.analyzeToolStripMenuItem, this.analyzeToolStripButton});
		}
		
		private void LoadNet(TextReader netStream, string fileName)
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
		
		private void CloseNet(PetriNet net)
		{
			this.UnlinkAllTools();
			this.TabControl.CloseTab(this.TabControl.TabIdForNet(net));
			this.OnActiveNetChange(new EventArgs());
		}
		
		private void QuiteCommandHandler(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		private void NewNetCommandHandler(object sender, System.EventArgs e)
		{
			PetriNetGraphical net = new PetriNetGraphical();
			TabPageForNet addedTabPage = this.TabControl.AddNewTab();
			addedTabPage.PutNetOnTabPage(net);
			this.OnActiveNetChange(new EventArgs());
		}
		
		private void OpenNetCommandHandler(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "txt files (*.pnml)|*.pnml|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				StreamReader stream;
				stream = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding(1251));
				this.LoadNet(stream, openFileDialog.FileName);
				stream.Close();
			}
		}
		
		private void CloseNetCommandHandler(object sender, System.EventArgs e)
		{
			if (this.ActiveNet != null )
			{
				if(!this.ActiveNet.NetSaved)
				{
					DialogResult dialogResult = RtlAwareMessageBox.Show(this, "Сохранить сеть перед закрытием?", "Попытка закрыть несохранённую сеть", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, 0);
					switch (dialogResult)
					{
						case DialogResult.Yes:
							this.CommandManager.Commands[SaveNetCommand.id].Execute();
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							(sender as CloseNetCommand).Canceled = true;
							break;
						default:
							throw new PppvException("Invalid value for DialogResult");
					}
				}

				if (!(sender as CloseNetCommand).Canceled)
				{
					this.CloseNet(this.ActiveNet);	
				}
			}
		}

		private void SaveNetCommandHandler(object sender, System.EventArgs e)
		{
			if (this.ActiveNet != null)
			{
				this.ActiveNet.SaveNet();
			}
		}

		private void SaveAsNetCommandHandler(object sender, System.EventArgs e)
		{
			if (this.ActiveNet != null)
			{
				this.ActiveNet.SaveNetAs();
			}
		}
		
		private void AnalyzeCommandHandler(object sender, System.EventArgs e)
		{
			Form verificatorForm;
			if (this.ActiveNet != null)
			{
				verificatorForm = new VerificatorForm(this.ActiveNet.BaseNet);
			}
			else
			{
				verificatorForm = new VerificatorForm();
			}

			verificatorForm.ShowDialog(this);	
		}
		
		private void DeleteCommandHandler(object sender, System.EventArgs e)
		{
			foreach (IShape netElement in this.ActiveNet.SelectedObjects)
			{
				this.ActiveNet.DeleteElement(netElement);
			}
		}
		
		private void ZoomInCommandHandler(object sender, System.EventArgs e)
		{
			MainForm mainForm = Application.OpenForms[0] as MainForm;
			PetriNetGraphical p = mainForm.ActiveNet;
			if (p != null)
			{
				if (p.Canvas.ScaleAmount < 10.0F)
				{
					p.Canvas.ScaleAmount += 0.1F;
				}

				p.Canvas.Refresh();
			}
		}
		
		private void ZoomOutCommandHandler(object sender, System.EventArgs e)
		{
			PetriNetGraphical p = (Application.OpenForms[0] as MainForm).ActiveNet;
			if (p != null)
			{
				if (p.Canvas.ScaleAmount > 0.11F)
				{
					p.Canvas.ScaleAmount -= 0.1F;
				}

				p.Canvas.Refresh();
			}
		}

		private void OnIdle(object sender, System.EventArgs args)
		{
			this.SetCommandEnabled(this.ActiveNet != null);
		}
		
		private void SelectPointerToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectPointerToolCommand.id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.Pointer, this.ActiveNet);
				this.CommandManager.Commands[SelectPointerToolCommand.id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.Pointer, this.ActiveNet);
				this.CommandManager.Commands[SelectPointerToolCommand.id].Checked = true;
			}
		}
		
		private void SelectPlaceToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectPlaceToolCommand.id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.Place, this.ActiveNet);
				this.CommandManager.Commands[SelectPlaceToolCommand.id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.Place, this.ActiveNet);
				this.CommandManager.Commands[SelectPlaceToolCommand.id].Checked = true;
			}
		}
		
		private void SelectTransitionToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectTransitionToolCommand.id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.Transition, this.ActiveNet);
				this.CommandManager.Commands[SelectTransitionToolCommand.id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.Transition, this.ActiveNet);
				this.CommandManager.Commands[SelectTransitionToolCommand.id].Checked = true;
			}
		}

		private void SelectArcToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectArcToolCommand.id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.Arc, this.ActiveNet);
				this.CommandManager.Commands[SelectArcToolCommand.id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.Arc, this.ActiveNet);
				this.CommandManager.Commands[SelectArcToolCommand.id].Checked = true;
			}
		}

		private void SelectInhibitorArcToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectInhibitorArcToolCommand.id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.InhibitorArc, this.ActiveNet);
				this.CommandManager.Commands[SelectInhibitorArcToolCommand.id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.InhibitorArc, this.ActiveNet);
				this.CommandManager.Commands[SelectInhibitorArcToolCommand.id].Checked = true;
			}
		}
		
		private void SelectAnnotationToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectAnnotationToolCommand.id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.Annotation, this.ActiveNet);
				this.CommandManager.Commands[SelectAnnotationToolCommand.id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.Annotation, this.ActiveNet);
				this.CommandManager.Commands[SelectAnnotationToolCommand.id].Checked = true;
			}
		}

		private void UnlinkAllTools()
		{
			this.UncheckAllTools();
			this.ToolsManager.UnlinkAll(this.ActiveNet);
		}
		
		private void UncheckAllTools()
		{
			this.CommandManager.Commands[SelectPointerToolCommand.id].Checked = false;
			this.CommandManager.Commands[SelectPlaceToolCommand.id].Checked = false;
			this.CommandManager.Commands[SelectTransitionToolCommand.id].Checked = false;
			this.CommandManager.Commands[SelectArcToolCommand.id].Checked = false;
			this.CommandManager.Commands[SelectInhibitorArcToolCommand.id].Checked = false;
			this.CommandManager.Commands[SelectAnnotationToolCommand.id].Checked = false;
		}
		
		private void CheckTool(ToolsEnum tool)
		{
			switch (tool)
			{
				case ToolsEnum.Pointer:
					this.CommandManager.Commands[SelectPointerToolCommand.id].Checked = true;
					break;
				case ToolsEnum.Place:
					this.CommandManager.Commands[SelectPlaceToolCommand.id].Checked = true;
					break;
				case ToolsEnum.Transition:
					this.CommandManager.Commands[SelectTransitionToolCommand.id].Checked = true;
					break;
				case ToolsEnum.Arc:
					this.CommandManager.Commands[SelectArcToolCommand.id].Checked = true;
					break;
				case ToolsEnum.InhibitorArc:
					this.CommandManager.Commands[SelectInhibitorArcToolCommand.id].Checked = true;
					break;
				case ToolsEnum.Annotation:
					this.CommandManager.Commands[SelectAnnotationToolCommand.id].Checked = true;
					break;
				default:
					throw new PppvException("Invalid value for ToolsEnum");
			}
		}
		
		private void ToolsChekedSynchronizer(object sender, EventArgs args)
		{	
			this.UncheckAllTools();
			if (this.ActiveNet != null)
			{
				object obj = this.ToolsManager.ToolForNet[this.ActiveNet];
	
				if (obj != null)
				{
					this.CheckTool((ToolsEnum)obj);
				}
			}
		}
		
		private void SelectedIndexChangedHandler(object sender, EventArgs args)
		{
			this.OnActiveNetChange(EventArgs.Empty);
		}
		
		private void ClosingHandler(object sender, CancelEventArgs args)
		{
			CloseNetCommand command = this.CommandManager.Commands[CloseNetCommand.id] as CloseNetCommand;
			while(this.ActiveNet != null && !command.Canceled)
			{
				command.Execute();
			}
			if (command.Canceled)
			{
				args.Cancel = true;
			}
			command.Canceled = false;
		}
	}
}
