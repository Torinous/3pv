/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 17.10.2010
 * Time: 5:27
 *
 *
 */

namespace Pppv.Verificator.Commands
{
	using System;
	using System.Drawing;
	using System.IO;
	using System.Reflection;
	using System.Text;
	using System.Windows.Forms;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Net;
	
	using SbsSW.SwiPlCs;

	public class OpenNetCommand : InterfaceCommand
	{
		public OpenNetCommand()
		{
			this.Name = "Открыть";
			this.Description = "Открыть сеть из файла";
			this.ShortcutKeys = Keys.Control | Keys.O;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Open.png"), true);
		}

		public override void Execute()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "txt files (*.pnml)|*.pnml|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				PetriNet net = new PetriNet();
				StreamReader stream;
				stream = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding(1251));
				XmlSerializer serializer = new XmlSerializer(typeof(PetriNet));
				net = serializer.Deserialize(stream) as PetriNet;
				stream.Close();
				this.LoadNetToPrologEngine(net);
			}
		}

		public override void Unexecute()
		{
		}

		public override bool CheckEnabled()
		{
			return true;
		}
		
		private void LoadNetToPrologEngine(PetriNet petriNet)
		{
			//this.SetStatusMessage("Загрузка сети: " + petriNet.Id);
			DateTime startTime = DateTime.Now;
			PetriNetPrologTranslated netTranslator = new PetriNetPrologTranslated(petriNet);

			string tmpFile = Path.GetTempFileName();
			StreamWriter tmpFilestream = new StreamWriter(tmpFile, false, Encoding.GetEncoding(1251));
			tmpFilestream.Write(netTranslator.ToProlog());
			tmpFilestream.Close();
			tmpFile = tmpFile.Replace("\\", "\\\\");
			PlQuery.PlCall("consult('" + tmpFile + "').");
			File.Delete(tmpFile);
			TimeSpan duration = DateTime.Now - startTime;
			//this.SetStatusMessage(String.Format("Сеть: {0} загружена(за {1} мс)", petriNet.Id, duration.TotalMilliseconds));
		}
	}
}