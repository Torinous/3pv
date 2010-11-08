/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 18.10.2010
 * Time: 0:17
 *
 *
 */

namespace Pppv.ApplicationFramework.Graphviz
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Globalization;
	using System.IO;
	using System.Text;
	using Microsoft.Win32;

	using Pppv.ApplicationFramework;

	public sealed class GraphvizPlotter
	{
		private string plotterPath;
		private Plotter plotterType;
		private OutputFormat outputFormatType;

		public GraphvizPlotter()
		{
			if (InstallPath == null)
			{
				throw new PppvException("В системе не обнаружена установленного GraphViz (http://www.graphviz.org).\n");
			}

			this.PlotterPath = Path.Combine(InstallPath, "bin");
			if (!Directory.Exists(this.PlotterPath))
			{
				throw new PppvException("Дистрибутив GraphViz повреждён. Не обнаружена папка Bin\n");
			}

			this.PlotterType = Plotter.Neato;
			this.OutputFormatType = OutputFormat.Png;
		}

		public static string InstallPath
		{
			get
			{
				RegistryKey gv = Registry.LocalMachine.OpenSubKey("SOFTWARE\\AT&T Research Labs\\Graphviz");
				if (gv == null)
				{
					return null;
				}

				string installPath = (string)gv.GetValue("InstallPath");
				return installPath;
			}
		}

		public string PlotterPath
		{
			get { return this.plotterPath; }
			private set { this.plotterPath = value; }
		}

		public OutputFormat OutputFormatType
		{
			get { return this.outputFormatType; }
			set { this.outputFormatType = value; }
		}

		public Plotter PlotterType
		{
			get { return this.plotterType; }
			set { this.plotterType = value; }
		}

		public Image Plot(string dotFormatText)
		{
			string srcFile = Path.GetTempFileName();
			string dstFile = Path.GetTempFileName();
			SaveString(dotFormatText, srcFile);
			Process proc = new Process();
			proc.StartInfo.FileName = this.GetProtter(this.PlotterType);
			proc.StartInfo.Arguments = this.GetProtterArguments(srcFile, dstFile);
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.CreateNoWindow = true;
			proc.Start();
			proc.WaitForExit();
			string pngFile = Path.ChangeExtension(dstFile, "png");
			File.Move(dstFile, pngFile);
			Image image = new Bitmap(pngFile);
			File.Delete(srcFile);
			File.Delete(dstFile);
			return image;
		}

		private static void SaveString(string str, string file)
		{
			StreamWriter wr = new StreamWriter(new FileStream(file, FileMode.Create), new UTF8Encoding(false));
			wr.Write(str);
			wr.Close();
		}

		private string GetProtter(Plotter plotter)
		{
			return Path.Combine(this.PlotterPath, PlotterFileNameFabric.Get(plotter));
		}

		private string GetProtterArguments(string srcFile, string dstFile)
		{
			return string.Format(CultureInfo.InvariantCulture, "-T{0} \"{1}\" -o \"{2}\"", OutputFormatFabric.Get(this.OutputFormatType), srcFile, dstFile);
		}
	}
}