/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 17.10.2010
 * Time: 18:55
 *
 *
 */

namespace Pppv.Verificator.Commands
{
	using System;
	using System.Diagnostics;
	using System.IO;
	using System.Drawing;
	using System.Reflection;
	using System.Text;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;
	using Pppv.Utils;

	public class StartPrologInterfaceCommand : InterfaceCommand
	{
		private PetriNet net;
		
		public StartPrologInterfaceCommand()
		{
			this.Name = "Prolog интерпретатор";
			this.Description = "Запустить Prolog интерпретатор";
			this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.swiprolog.ico"), true);
		}
		
		public StartPrologInterfaceCommand(PetriNet net) : this()
		{
			this.Net = net;
		}
		
		public PetriNet Net
		{
			get { return this.net; }
			set { this.net = value; }
		}

		public override void Execute()
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

		public override void Unexecute()
		{
		}
	}
}

