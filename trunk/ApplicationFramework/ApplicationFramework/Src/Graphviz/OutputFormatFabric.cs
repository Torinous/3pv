/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 18.10.2010
 * Time: 0:49
 *
 *
 */

namespace Pppv.ApplicationFramework.Graphviz
{
	using System;

	using Pppv.ApplicationFramework;

	public static class OutputFormatFabric
	{
		public static string Get(OutputFormat format)
		{
			switch (format)
			{
				case OutputFormat.Cmap:
					return "cmap";
				case OutputFormat.Mif:
					return "mif";
				case OutputFormat.Mp:
					return "mp";
				case OutputFormat.Pcl:
					return "pcl";
				case OutputFormat.Pic:
					return "pic";
				case OutputFormat.Plain:
					return "plain";
				case OutputFormat.Png:
					return "png";
				case OutputFormat.Ps:
					return "ps";
				case OutputFormat.Ps2:
					return "ps2";
				case OutputFormat.Svg:
					return "svg";
				case OutputFormat.Vrml:
					return "vrml";
				case OutputFormat.Vtx:
					return "vtx";
				case OutputFormat.Wbmp:
					return "wbmp";
				default:
					throw new PppvException("Invalid value for OutputFormat");
			}
		}
	}
}
