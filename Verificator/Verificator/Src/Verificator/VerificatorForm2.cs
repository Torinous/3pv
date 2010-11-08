/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 08.11.2010
 * Time: 3:59
 *
 *
 */

namespace Pppv.Verificator
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Reflection;
	using System.Text;
	using System.Threading;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Net;
	using Pppv.Utils;
	using Pppv.Verificator.Commands;

	public partial class VerificatorForm2 : Form
	{
		private PetriNet net;
		private CommandManager commandManager;
		private Configuration<VerificatorConfigurationData> configuration;
		
		public VerificatorForm2(PetriNet net)
		{
			this.Net = net;
			SWIProlog.InitPrologEngineIfNeed();
			this.InitializeConfiguration();
			this.InitializeComponent();
			this.InitializeCommandManager();
			this.Text = "3Pv:Verificator " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
			SWIProlog.LoadNetToProlog(this.Net);
			this.verificatorTabControl.PrologTextBox.Text = (new PetriNetPrologTranslated(this.Net)).ToProlog();
		}

		public PetriNet Net
		{
			get { return this.net; }
			set { this.net = value; }
		}
		
		public CommandManager CommandManager
		{
			get { return this.commandManager; }
			private set { this.commandManager = value; }
		}

		protected override void OnClosed(EventArgs e)
		{
			this.configuration.Save();
			base.OnClosed(e);
		}

		private void InitializeConfiguration()
		{
			this.configuration = Configuration<VerificatorConfigurationData>.Instance;
			this.configuration.SourceFile = Environment.CurrentDirectory + "\\Verificator.conf";
			this.configuration.Load();
		}
		
		private void InitializeCommandManager()
		{
			this.CommandManager = new CommandManager();
			this.AddCommandsToManager();
			this.AssociateCommandsWithUI();
		}
		
		private void AddCommandsToManager()
		{
			CommandsList commandList = this.CommandManager.Commands;
			commandList.Add(new QuitCommand(this.QuitCommandHandler, null));
			commandList.Add(new StartPrologInterfaceCommand(this.StartPrologInterfaceCommandHandler, null));
			commandList.Add(new BuildStateSpaceCommand(this.BuildStateSpaceCommandHandler, null));
			commandList.Add(new SaveStateSpaceImageCommand(this.SaveStateSpaceImageCommandHandler, null));
		}
		
		private void AssociateCommandsWithUI()
		{
			CommandsList commandList = this.CommandManager.Commands;
			commandList[QuitCommand.Id].CommandInstances.Add(new object[] { this.quitToolStripMenuItem, this.quitToolStripButton });
			commandList[StartPrologInterfaceCommand.Id].CommandInstances.Add(new object[] { this.startPrologInterfaceToolStripMenuItem, this.startPrologInterfaceToolStripButton });
			commandList[BuildStateSpaceCommand.Id].CommandInstances.Add(new object[] { this.buildStateSpaceToolStripButton, this.buildStateSpaceToolStripMenuItem });
			commandList[SaveStateSpaceImageCommand.Id].CommandInstances.Add(this.saveStateSpaceImageToolStripMenuItem);
		}
		
		private void QuitCommandHandler(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		private void StartPrologInterfaceCommandHandler(object sender, System.EventArgs e)
		{
			Process prologProcess = new Process();
			prologProcess.StartInfo.FileName = "swipl-win.exe";
			string libraryPath = Environment.CurrentDirectory + "\\Prolog";
			prologProcess.StartInfo.Arguments = "-q -p pppv_library=" + libraryPath;
			if (this.Net != null)
			{
				PetriNetPrologTranslated netTranslator = new PetriNetPrologTranslated(this.Net);
				string tmpFile = Path.GetTempFileName();
				StreamWriter tmpFilestream = new StreamWriter(tmpFile, false, Encoding.GetEncoding(1251));
				tmpFilestream.Write(netTranslator.ToProlog());
				tmpFilestream.Close();
				
				tmpFile = tmpFile.Replace("\\", "\\\\");
				prologProcess.StartInfo.Arguments += " -g consult('" + tmpFile + "')";
			}
			
			DebugAssistant.LogTrace("Start process by [" + prologProcess.StartInfo.FileName + " " + prologProcess.StartInfo.Arguments + "]");
			prologProcess.Start();

			DebugAssistant.LogTrace("\tOK");
		}
		
		private void BuildStateSpaceCommandHandler(object sender, System.EventArgs e)
		{
			this.SetStatusMessage("Вычисление пространства состояний сети: " + this.Net.Id);
			DateTime startTime = DateTime.Now;
			SWIProlog.CreateStateSpace();
			TimeSpan duration = DateTime.Now - startTime;
			this.SetStatusMessage(String.Format("Пространство состояний сети: {0} построено({1} мс)", this.Net.Id, duration.TotalMilliseconds));
		}
		
		private void SaveStateSpaceImageCommandHandler(object sender, System.EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			
			Image image = this.verificatorTabControl.StateSpaceImage;
			if (image == null)
			{
				return;
			}
			
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (image != null)
				{
					image.Save(saveFileDialog.FileName + ".png", ImageFormat.Png);
				}
			}
		}
		
		private void SetStatusMessage(string message)
		{
			this.toolStripStatusLabel.Text = message;
		}
	}
}
