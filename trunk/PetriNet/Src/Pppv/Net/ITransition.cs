namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Globalization;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   public interface ITransition : INetElement
   {
      string GuardFunction { get; set; }
   }
}
