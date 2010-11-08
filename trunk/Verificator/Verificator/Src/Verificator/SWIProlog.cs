/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 31.10.2010
 * Time: 2:48
 *
 *
 */

namespace Pppv.Verificator
{
	using System;
	using System.IO;
	using System.Text;
	
	using Pppv.Net;
	
	using SbsSW.SwiPlCs;

	public static class SWIProlog
	{
		public static void InitPrologEngineIfNeed()
		{
			if (!PlEngine.IsInitialized)
			{
				string[] param = { "-q", "-p", "pppv_library=" + Environment.CurrentDirectory + "\\Prolog" };
				PlEngine.Initialize(param);
			}
		}
		
		public static void LoadNetToProlog(PetriNet net)
		{
			PetriNetPrologTranslated netTranslator = new PetriNetPrologTranslated(net);

			string tmpFile = Path.GetTempFileName();
			StreamWriter tmpFilestream = new StreamWriter(tmpFile, false, Encoding.GetEncoding(1251));
			tmpFilestream.Write(netTranslator.ToProlog());
			tmpFilestream.Close();
			tmpFile = tmpFile.Replace("\\", "\\\\");
			PlQuery.PlCall("consult('" + tmpFile + "').");
			File.Delete(tmpFile);
		}
		
		public static void CreateStateSpace()
		{
			PlQuery.PlCall("statespace:createStateSpace.");
		}
	}
}
