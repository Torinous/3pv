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

   public class PlotStateSpaceImage : Command
   {
      private Plotter plotterType;

      public PlotStateSpaceImage()
      {
         VerificatorConfigurationData config = Configuration<VerificatorConfigurationData>.Instance.Data;
         this.Name = "Изображение пространства состояний";
         this.PlotterType = config.DefaultPlotter;
      }

      public Plotter PlotterType
      {
         get { return this.plotterType; }
         set { this.plotterType = value; }
      }

      public override void Execute()
      {
         PetriNetVerificator verificator = PetriNetVerificator.Instance;
         GraphvizPlotter plotter = new GraphvizPlotter();
         plotter.PlotterType = this.PlotterType;
         verificator.MainForm.ShowStateSpace(plotter.Plot(StateSpaceInDotFormatTranslator.Create()));
      }

      public override void Unexecute()
      {
      }
   }
}

