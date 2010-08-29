/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 21.08.2010
 * Time: 1:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   
   public class InhibitorArc : Arc
   {
      public InhibitorArc(NetElement startElement) : base(startElement)
      {
      }

      protected override Pen PenFactory()
      {
         Pen p = new Pen(Color.Black, 1);
         GraphicsPath capPath = new GraphicsPath();
         capPath.AddEllipse(-4, -8, 8, 8);
         GraphicsPath capPath2 = new GraphicsPath();
         capPath2.AddLine(new Point(0, -8), new Point(0, 0));
         CustomLineCap roundCap = new CustomLineCap(null, capPath);
         p.CustomEndCap = roundCap;
         p.CustomStartCap = roundCap;
         return p;
      }
   }
}
