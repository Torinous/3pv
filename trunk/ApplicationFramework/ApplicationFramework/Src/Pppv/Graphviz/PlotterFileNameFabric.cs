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
            case Plotter.Dot:
               return "dot.exe";
            case Plotter.Dotty:
               return "dotty.exe";
            case Plotter.Lefty:
               return "lefty.exe";
            case Plotter.Neato:
               return "neato.exe";
            default:
               throw new PppvException("Invalid value for Plotter");
         }
      }
   }
}
