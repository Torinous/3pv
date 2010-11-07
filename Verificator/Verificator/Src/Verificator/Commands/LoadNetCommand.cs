/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 31.10.2010
 * Time: 17:23
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

	public class LoadNetCommand : InterfaceCommand
	{
		private PetriNet net;
		private VerificatorForm form;
		
		public LoadNetCommand()
		{
			this.Name = "Загрузить";
			this.Description = "Загрузить сеть в Prolog";
			this.ShortcutKeys = Keys.Control | Keys.L;
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Open.png"), true);
		}

		public LoadNetCommand(VerificatorForm form, PetriNet net) : this()
		{
			this.Form = form;
			this.Net = net;
		}

		public PetriNet Net
		{
			get { return this.net; }
			set { this.net = value; }
		}
		
		public VerificatorForm Form
		{
			get { return form; }
			set { form = value; }
		}

		public override void Execute()
		{
			DateTime startTime = DateTime.Now;
			this.Form.Net = this.Net;
			PetriNetPrologTranslated netTranslator = new PetriNetPrologTranslated(this.Net);

			string tmpFile = Path.GetTempFileName();
			StreamWriter tmpFilestream = new StreamWriter(tmpFile, false, Encoding.GetEncoding(1251));
			tmpFilestream.Write(netTranslator.ToProlog());
			this.Form.PublishPrologCode(netTranslator.ToProlog());
			tmpFilestream.Close();
			tmpFile = tmpFile.Replace("\\", "\\\\");
			PlQuery.PlCall("consult('" + tmpFile + "').");
			File.Delete(tmpFile);
			TimeSpan duration = DateTime.Now - startTime;
		}

		public override void Unexecute()
		{
		}

		public override bool CheckEnabled()
		{
			return true;
		}
	}
}