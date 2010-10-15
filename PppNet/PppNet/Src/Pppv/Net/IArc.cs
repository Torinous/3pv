namespace Pppv.Net
{
   using System;
   using System.Collections;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Globalization;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   public interface IArc : INetElement
   {
      ArcType ArcType { get; set; }

      ArrayList Points { get; }

      PredicatesList Cortege { get; }

      string TargetId { get; set; }

      string SourceId { get; set; }

      bool Unfinished { get; }

      string ArcTypeName { get; }
   }
}
