/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 12.10.2010
 * Time: 18:34
 *
 *
 */
namespace Pppv.Verificator
{
	using System;
	using System.Diagnostics;
	using System.IO;
	using System.Text;

	using Pppv.ApplicationFramework;
	using Pppv.Graphviz;
	using Pppv.Net;
	using Pppv.Utils;

	using SbsSW.SwiPlCs;
	using SbsSW.SwiPlCs.Exceptions;

	public static class StateSpaceInDotFormatTranslator
	{
		public static string Create()
		{
			PrepareConfiguration();
			PlTerm fileName = PlTerm.PlVar();
			PlTermV vec = new PlTermV(fileName);
			if (PlQuery.PlCall("stateSpace", "stateSpaceToDotFormatTmpFile", vec))
			{
				StreamReader tmpFileStream = new StreamReader(fileName.ToString(), Encoding.GetEncoding(1251));
				string result = tmpFileStream.ReadToEnd();
				tmpFileStream.Close();
				return result;
			}
			else
			{
				throw new PppvException("Выгрузка в dot формат закончилась неудачно");
			}
		}

		private static void PrepareConfiguration()
		{
			VerificatorConfigurationData config = Configuration<VerificatorConfigurationData>.Instance.Data;
			PlQuery.PlCall("retractall(statespace:defaultNodeShape(_)).");
			PlQuery.PlCall(String.Format("assert(statespace:defaultNodeShape({0})).", NodeShapeFabric.Get(config.DefaultNodeShape)));

			PlQuery.PlCall("retractall(statespace:defaultEdgeLength(_)).");
			PlQuery.PlCall(String.Format("assert(statespace:defaultEdgeLength({0})).", config.EdgeLength));

			PlQuery.PlCall("retractall(statespace:useMarkingInStateLabel)");
			if (config.UseMarkingInStateLabel)
			{
				PlQuery.PlCall("assert(statespace:useMarkingInStateLabel)");
			}
		}
	}
}
