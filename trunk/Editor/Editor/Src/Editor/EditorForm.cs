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
	using System.IO;
	using System.Reflection;
	using System.Text;
	using System.Windows.Forms;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	using Pppv.ApplicationFramework;	
	using Pppv.Commands;
	using Pppv.Editor.Commands;
	using Pppv.Editor.Shapes;
	using Pppv.Editor.Tools;
	using Pppv.Net;
	using Pppv.Utils;
	using Pppv.Verificator;

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
			this.ActiveNetChange += this.ToolsChekedSynchronize;
			this.TabControl.SelectedIndexChanged += this.SelectedIndexChangedHandler;
			this.Closing += this.ClosingHandler;
			this.Text = "3Pv:Editor " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}
		
		public event EventHandler<EventArgs> ActiveNetChange;

		public ToolsManager ToolsManager
		{
			get { return this.toolsManager; }
			set { this.toolsManager = value; }
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
			this.CommandManager.Commands.Add(new QuitCommand(this.QuitCommandHandler, null));
			
			this.CommandManager.Commands.Add(new UndoCommand(null, null));
			this.CommandManager.Commands.Add(new RedoCommand(null, null));
			this.CommandManager.Commands.Add(new CutCommand(null, null));
			this.CommandManager.Commands.Add(new CopyCommand(null, null));
			this.CommandManager.Commands.Add(new PasteCommand(null, null));
			this.CommandManager.Commands.Add(new DeleteSelectedCommand(this.DeleteCommandHandler, null));
			
			this.CommandManager.Commands.Add(new ZoomInCommand(this.ZoomInCommandHandler, null));
			this.CommandManager.Commands.Add(new ZoomOutCommand(this.ZoomOutCommandHandler, null));
			this.CommandManager.Commands.Add(new AdditionalCodeEditCommand(this.AdditionalCodeEditCommandHandler, null));
			
			this.CommandManager.Commands.Add(new SelectPointerToolCommand(this.SelectPointerToolCommandHandler, null));
			this.CommandManager.Commands.Add(new SelectPlaceToolCommand(this.SelectPlaceToolCommandHandler, null));
			this.CommandManager.Commands.Add(new SelectTransitionToolCommand(this.SelectTransitionToolCommandHandler, null));
			this.CommandManager.Commands.Add(new SelectArcToolCommand(this.SelectArcToolCommandHandler, null));
			this.CommandManager.Commands.Add(new SelectInhibitorArcToolCommand(this.SelectInhibitorArcToolCommandHandler, null));
			this.CommandManager.Commands.Add(new SelectAnnotationToolCommand(this.SelectAnnotationToolCommandHandler, null));
			
			this.CommandManager.Commands.Add(new AnalyzeCommand(this.AnalyzeCommandHandler, null));
			
			this.CommandManager.Commands.Add(new AboutCommand(this.AboutCommandHandler, null));
		}
		
		private void SetCommandEnabled(bool state)
		{
			this.CommandManager.Commands[CloseNetCommand.Id].Enabled = state;
			this.CommandManager.Commands[SaveNetCommand.Id].Enabled = state;
			this.CommandManager.Commands[SaveAsNetCommand.Id].Enabled = state;
			
			this.CommandManager.Commands[UndoCommand.Id].Enabled = state;
			this.CommandManager.Commands[RedoCommand.Id].Enabled = state;
			this.CommandManager.Commands[CutCommand.Id].Enabled = state;
			this.CommandManager.Commands[CopyCommand.Id].Enabled = state;
			this.CommandManager.Commands[PasteCommand.Id].Enabled = state;
			this.CommandManager.Commands[DeleteSelectedCommand.Id].Enabled = state;
			this.CommandManager.Commands[AdditionalCodeEditCommand.Id].Enabled = state;
			
			this.CommandManager.Commands[ZoomInCommand.Id].Enabled = state;
			this.CommandManager.Commands[ZoomOutCommand.Id].Enabled = state;
			
			this.CommandManager.Commands[SelectPointerToolCommand.Id].Enabled = state;
			this.CommandManager.Commands[SelectPlaceToolCommand.Id].Enabled = state;
			this.CommandManager.Commands[SelectTransitionToolCommand.Id].Enabled = state;
			this.CommandManager.Commands[SelectArcToolCommand.Id].Enabled = state;
			this.CommandManager.Commands[SelectInhibitorArcToolCommand.Id].Enabled = state;
			this.CommandManager.Commands[SelectAnnotationToolCommand.Id].Enabled = state;
			
			this.CommandManager.Commands[AnalyzeCommand.Id].Enabled = state;
		}
		
		private void AssociateCommandsWithUI()
		{
			CommandsList commandList = this.CommandManager.Commands;
			commandList[QuitCommand.Id].CommandInstances.Add(this.quitToolStripMenuItem);
			commandList[NewNetCommand.Id].CommandInstances.Add(new object[] { this.newNetToolStripMenuItem, this.newNetToolStripButton });
			commandList[OpenNetCommand.Id].CommandInstances.Add(new object[] { this.openNetToolStripMenuItem, this.openNetToolStripButton });
			commandList[CloseNetCommand.Id].CommandInstances.Add(new object[] { this.closeNetToolStripMenuItem, this.closeNetToolStripButton });
			commandList[SaveNetCommand.Id].CommandInstances.Add(new object[] { this.saveNetToolStripMenuItem, this.saveNetToolStripButton });
			commandList[SaveAsNetCommand.Id].CommandInstances.Add(new object[] { this.saveAsNetToolStripMenuItem, this.saveAsNetToolStripButton });
			
			commandList[UndoCommand.Id].CommandInstances.Add(new object[] { this.undoToolStripMenuItem, this.undoToolStripButton });
			commandList[RedoCommand.Id].CommandInstances.Add(new object[] { this.redoToolStripMenuItem, this.redoToolStripButton });
			commandList[CutCommand.Id].CommandInstances.Add(new object[] { this.cutToolStripMenuItem, this.cutToolStripButton });
			commandList[CopyCommand.Id].CommandInstances.Add(new object[] { this.copyToolStripMenuItem, this.copyToolStripButton });
			commandList[PasteCommand.Id].CommandInstances.Add(new object[] { this.pasteToolStripMenuItem, this.pasteToolStripButton });
			commandList[DeleteCommand.Id].CommandInstances.Add(new object[] { this.deleteToolStripMenuItem, this.deleteToolStripButton });
			commandList[AdditionalCodeEditCommand.Id].CommandInstances.Add(new object[] { this.additionalCodeEditToolStripMenuItem, this.additionalCodeEditToolStripButton });
			
			commandList[ZoomInCommand.Id].CommandInstances.Add(new object[] { this.zoomInToolStripMenuItem, this.zoomInToolStripButton });
			commandList[ZoomOutCommand.Id].CommandInstances.Add(new object[] { this.zoomOutToolStripMenuItem, this.zoomOutToolStripButton });
			
			commandList[SelectPointerToolCommand.Id].CommandInstances.Add(new object[] { this.selectPointerToolToolStripMenuItem, this.selectPointerToolToolStripButton });
			commandList[SelectPlaceToolCommand.Id].CommandInstances.Add(new object[] { this.selectPlaceToolToolStripMenuItem, this.selectPlaceToolToolStripButton });
			commandList[SelectTransitionToolCommand.Id].CommandInstances.Add(new object[] { this.selectTransitionToolToolStripMenuItem, this.selectTransitionToolToolStripButton });
			commandList[SelectArcToolCommand.Id].CommandInstances.Add(new object[] { this.selectArcToolToolStripMenuItem, this.selectArcToolToolStripButton });
			commandList[SelectInhibitorArcToolCommand.Id].CommandInstances.Add(new object[] { this.selectInhibitorArcToolToolStripMenuItem, this.selectInhibitorArcToolToolStripButton });
			commandList[SelectAnnotationToolCommand.Id].CommandInstances.Add(new object[] { this.selectAnnotationToolToolStripMenuItem, this.selectAnnotationToolToolStripButton });
			
			commandList[AnalyzeCommand.Id].CommandInstances.Add(new object[] { this.analyzeToolStripMenuItem, this.analyzeToolStripButton });
			
			commandList[AboutCommand.Id].CommandInstances.Add(this.aboutToolStripMenuItem);
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
		
		private void QuitCommandHandler(object sender, System.EventArgs e)
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
			if (this.ActiveNet != null)
			{
				if (!this.ActiveNet.NetSaved)
				{
					DialogResult dialogResult = RtlAwareMessageBox.Show(this, "Сохранить сеть перед закрытием?", "Попытка закрыть несохранённую сеть", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, 0);
					switch (dialogResult)
					{
						case DialogResult.Yes:
							this.CommandManager.Commands[SaveNetCommand.Id].Execute();
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
			verificatorForm = new VerificatorForm2(this.ActiveNet.BaseNet);
			verificatorForm.ShowDialog(this);	
		}
		
		private void DeleteCommandHandler(object sender, System.EventArgs e)
		{
			foreach (IShape netElement in this.ActiveNet.SelectedObjects)
			{
				this.ActiveNet.DeleteElement(netElement);
			}

			this.ActiveNet.Canvas.Invalidate();
		}
		
		private void ZoomInCommandHandler(object sender, System.EventArgs e)
		{
			PetriNetGraphical net = this.ActiveNet;
			if (net != null)
			{
				if (net.Canvas.ScaleAmount < 10.0F)
				{
					net.Canvas.ScaleAmount += 0.1F;
				}

				net.Canvas.Refresh();
			}
		}
		
		private void ZoomOutCommandHandler(object sender, System.EventArgs e)
		{
			PetriNetGraphical net = this.ActiveNet;
			if (net != null)
			{
				if (net.Canvas.ScaleAmount > 0.11F)
				{
					net.Canvas.ScaleAmount -= 0.1F;
				}

				net.Canvas.Refresh();
			}
		}
		
		private void AboutCommandHandler(object sender, System.EventArgs e)
		{
			Form f = new AboutForm();
			f.ShowDialog(this);
			f.Dispose();
		}

		private void OnIdle(object sender, System.EventArgs args)
		{
			this.SetCommandEnabled(this.ActiveNet != null);
		}
		
		private void SelectPointerToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectPointerToolCommand.Id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.Pointer, this.ActiveNet);
				this.CommandManager.Commands[SelectPointerToolCommand.Id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.Pointer, this.ActiveNet);
				this.CommandManager.Commands[SelectPointerToolCommand.Id].Checked = true;
			}
		}
		
		private void SelectPlaceToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectPlaceToolCommand.Id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.Place, this.ActiveNet);
				this.CommandManager.Commands[SelectPlaceToolCommand.Id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.Place, this.ActiveNet);
				this.CommandManager.Commands[SelectPlaceToolCommand.Id].Checked = true;
			}
		}
		
		private void SelectTransitionToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectTransitionToolCommand.Id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.Transition, this.ActiveNet);
				this.CommandManager.Commands[SelectTransitionToolCommand.Id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.Transition, this.ActiveNet);
				this.CommandManager.Commands[SelectTransitionToolCommand.Id].Checked = true;
			}
		}

		private void SelectArcToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectArcToolCommand.Id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.Arc, this.ActiveNet);
				this.CommandManager.Commands[SelectArcToolCommand.Id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.Arc, this.ActiveNet);
				this.CommandManager.Commands[SelectArcToolCommand.Id].Checked = true;
			}
		}

		private void SelectInhibitorArcToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectInhibitorArcToolCommand.Id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.InhibitorArc, this.ActiveNet);
				this.CommandManager.Commands[SelectInhibitorArcToolCommand.Id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.InhibitorArc, this.ActiveNet);
				this.CommandManager.Commands[SelectInhibitorArcToolCommand.Id].Checked = true;
			}
		}
		
		private void SelectAnnotationToolCommandHandler(object sender, System.EventArgs e)
		{
			if (this.CommandManager.Commands[SelectAnnotationToolCommand.Id].Checked)
			{
				this.ToolsManager.Unlink(ToolsEnum.Annotation, this.ActiveNet);
				this.CommandManager.Commands[SelectAnnotationToolCommand.Id].Checked = false;
			}
			else
			{
				this.UnlinkAllTools();
				this.ToolsManager.Link(ToolsEnum.Annotation, this.ActiveNet);
				this.CommandManager.Commands[SelectAnnotationToolCommand.Id].Checked = true;
			}
		}
		
		private void AdditionalCodeEditCommandHandler(object sender, System.EventArgs e)
		{
			Form f = new AdditionalCodeEditForm(this.ActiveNet);
			f.ShowDialog(this);
			f.Dispose();
		}

		private void UnlinkAllTools()
		{
			this.UncheckAllTools();
			this.ToolsManager.UnlinkAll(this.ActiveNet);
		}
		
		private void UncheckAllTools()
		{
			this.CommandManager.Commands[SelectPointerToolCommand.Id].Checked = false;
			this.CommandManager.Commands[SelectPlaceToolCommand.Id].Checked = false;
			this.CommandManager.Commands[SelectTransitionToolCommand.Id].Checked = false;
			this.CommandManager.Commands[SelectArcToolCommand.Id].Checked = false;
			this.CommandManager.Commands[SelectInhibitorArcToolCommand.Id].Checked = false;
			this.CommandManager.Commands[SelectAnnotationToolCommand.Id].Checked = false;
		}
		
		private void CheckTool(ToolsEnum tool)
		{
			switch (tool)
			{
				case ToolsEnum.Pointer:
					this.CommandManager.Commands[SelectPointerToolCommand.Id].Checked = true;
					break;
				case ToolsEnum.Place:
					this.CommandManager.Commands[SelectPlaceToolCommand.Id].Checked = true;
					break;
				case ToolsEnum.Transition:
					this.CommandManager.Commands[SelectTransitionToolCommand.Id].Checked = true;
					break;
				case ToolsEnum.Arc:
					this.CommandManager.Commands[SelectArcToolCommand.Id].Checked = true;
					break;
				case ToolsEnum.InhibitorArc:
					this.CommandManager.Commands[SelectInhibitorArcToolCommand.Id].Checked = true;
					break;
				case ToolsEnum.Annotation:
					this.CommandManager.Commands[SelectAnnotationToolCommand.Id].Checked = true;
					break;
				default:
					throw new PppvException("Invalid value for ToolsEnum");
			}
		}
		
		private void ToolsChekedSynchronize(object sender, EventArgs args)
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
			CloseNetCommand command = this.CommandManager.Commands[CloseNetCommand.Id] as CloseNetCommand;
			while (this.ActiveNet != null && !command.Canceled)
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
