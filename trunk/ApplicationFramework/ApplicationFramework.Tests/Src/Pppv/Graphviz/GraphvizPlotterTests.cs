/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 18.10.2010
 * Time: 3:45
 *
 *
 */

namespace Pppv.Graphviz
{
   using System;
   using System.Drawing;
   using System.IO;

   using NUnit.Framework;
   using NUnit.Framework.Constraints;

   [TestFixture]
   public class GraphvizPlotterTests
   {
      [Test]
      public void TestOfFindingInstallation()
      {
         GraphvizPlotter plotter = new GraphvizPlotter();
         Assert.That(File.Exists(plotter.PlotterPath + "\\dot.exe"), Is.True, "Установленный путь к Graphviz неверен");
      }

      [Test]
      public void TestOfSimplePlotting()
      {
         GraphvizPlotter plotter = new GraphvizPlotter();
         Image image = plotter.Plot("digraph net{}");
      }
   }
}