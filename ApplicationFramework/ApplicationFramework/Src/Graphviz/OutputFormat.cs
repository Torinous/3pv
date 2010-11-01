/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 18.10.2010
 * Time: 0:46
 *
 *
 */

namespace Pppv.Graphviz
{
   using System;

   public enum OutputFormat
   {
      Cmap,
      Mif,
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Mp", Justification = "Для имён в emum`е не обязательно")]
      Mp,
      Pcl,
      Pic,
      Plain,
      Png,
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ps", Justification = "Для имён в emum`е не обязательно")]
      Ps,
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ps", Justification = "Для имён в emum`е не обязательно")]
      Ps2,
      Svg,
      Vrml,
      Vtx,
      Wbmp
   }
}
