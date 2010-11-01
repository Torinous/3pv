/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 18.10.2010
 * Time: 0:49
 *
 *
 */

namespace Pppv.Graphviz
{
   using System;

   using Pppv.ApplicationFramework;

   public static class OutputFormatFabric
   {
      public static string Get(OutputFormat format)
      {
         switch (format)
         {
            case Pppv.Graphviz.OutputFormat.Cmap:
               return "cmap";
            case Pppv.Graphviz.OutputFormat.Mif:
               return "mif";
            case Pppv.Graphviz.OutputFormat.Mp:
               return "mp";
            case Pppv.Graphviz.OutputFormat.Pcl:
               return "pcl";
            case Pppv.Graphviz.OutputFormat.Pic:
               return "pic";
            case Pppv.Graphviz.OutputFormat.Plain:
               return "plain";
            case Pppv.Graphviz.OutputFormat.Png:
               return "png";
            case Pppv.Graphviz.OutputFormat.Ps:
               return "ps";
            case Pppv.Graphviz.OutputFormat.Ps2:
               return "ps2";
            case Pppv.Graphviz.OutputFormat.Svg:
               return "svg";
            case Pppv.Graphviz.OutputFormat.Vrml:
               return "vrml";
            case Pppv.Graphviz.OutputFormat.Vtx:
               return "vtx";
            case Pppv.Graphviz.OutputFormat.Wbmp:
               return "wbmp";
            default:
               throw new PppvException("Invalid value for OutputFormat");
         }
      }
   }
}
