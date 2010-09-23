namespace Pppv.Editor
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Collections.ObjectModel;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;

   using Pppv.Editor.Tools;
   using Pppv.Net;

   public class SelectedNetObjectList : Collection<NetElement>
   {
      public SelectedNetObjectList() : base()
      {
      }

      public new void Add(NetElement value)
      {
         value.Paint += this.DrawSelectionMarker;
         base.Add(value);
      }

      public new void Clear()
      {
         foreach (NetElement element in this)
         {
            element.Paint -= this.DrawSelectionMarker;
         }

         base.Clear();
      }

      public void AddRange(IEnumerable<NetElement> collection)
      {
         foreach (NetElement element in collection)
         {
            element.Paint += this.DrawSelectionMarker;
            base.Add(element);
         }
      }

      private void DrawSelectionMarker(object sender, PaintEventArgs e)
      {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;
         SolidBrush b = new SolidBrush(Color.FromArgb(125, 245, 0, 0));
         dc.FillRegion(b, ((NetElement)sender).HitRegion);
       }
   }
}