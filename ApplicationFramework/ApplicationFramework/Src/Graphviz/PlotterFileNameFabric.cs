/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 18.10.2010
 * Time: 0:42
 *
 *
 */

namespace Pppv.Graphviz
{
   using System;

   using Pppv.ApplicationFramework;

   public static class PlotterFileNameFabric
   {
      public static string Get(Plotter plotter)
      {
         switch (plotter)
         {
            case Plotter.Neato:
               return "neato.exe";
            case Plotter.Dot:
               return "dot.exe";
            case Plotter.Twopi:
               return "twopi.exe";
            case Plotter.Fdp:
               return "fdp.exe";
            case Plotter.Circo:
               return "circo.exe";
            default:
               throw new PppvException("Invalid value for Plotter");
         }
      }
   }
}
