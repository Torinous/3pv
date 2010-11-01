/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 17.10.2010
 * Time: 5:43
 *
 */

namespace Pppv.Verificator.Commands
{
	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.ApplicationFramework.Commands;
	using Pppv.Graphviz;
	using Pppv.Net;
	using Pppv.Utils;

	public class PlotStateSpaceImage : InterfaceCommand
	{
		private Plotter plotterType;
		private Image resultImage;
		
		public PlotStateSpaceImage()
		{
			VerificatorConfigurationData config = Configuration<VerificatorConfigurationData>.Instance.Data;
			this.Name = "Изображение пространства состояний";
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.StateSpace.png"), true);
			this.PlotterType = config.DefaultPlotter;
		}

		public Image ResultImage
		{
			get { return this.resultImage; }
			set { this.resultImage = value; }
		}
		
		public Plotter PlotterType
		{
			get { return this.plotterType; }
			set { this.plotterType = value; }
		}

		public override void Execute()
		{
			GraphvizPlotter plotter = new GraphvizPlotter();
			plotter.PlotterType = this.PlotterType;
			this.ResultImage =	plotter.Plot(StateSpaceInDotFormatTranslator.Create());
		}

		public override void Unexecute()
		{
		}
	}
}

