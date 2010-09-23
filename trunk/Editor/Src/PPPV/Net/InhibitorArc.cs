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
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   public class InhibitorArc : Arc
   {
      public InhibitorArc(NetElement startElement) : base(startElement)
      {
      }

      public InhibitorArc(XmlReader reader, PetriNet net) : this((NetElement)null)
      {
         ParentNet = net;
         this.ReadXml(reader);
      }

      public override string ArcType
      {
         get { return "inhibitorArc"; }
      }

      protected override Pen PenFactory(PenCapPlace penCapPlace)
      {
         Pen p = new Pen(Color.Black, 1);
         GraphicsPath capPath = new GraphicsPath();
         capPath.AddEllipse(-4, -8, 8, 8);
         GraphicsPath capPath2 = new GraphicsPath();
         capPath2.AddLine(new Point(0, -8), new Point(0, 0));
         CustomLineCap roundCap = new CustomLineCap(null, capPath);
         if (penCapPlace == PenCapPlace.End)
         {
            p.CustomEndCap = roundCap;
         }
         else
         {
            p.CustomStartCap = roundCap;
         }

         return p;
      }

      protected override PenCapPlace DeterminePenCapPlace()
      {
         if (Source is Transition)
         {
            return PenCapPlace.Start;
         }
         else
         {
            return PenCapPlace.End;
         }
      }
   }
}
