namespace Pppv.Editor
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Collections.ObjectModel;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Windows.Forms;

   using Pppv.Editor.Shapes;
   using Pppv.Editor.Tools;
   using Pppv.Net;

   public class SelectedNetObjectList : Collection<IShape>
   {
      public SelectedNetObjectList() : base()
      {
      }

      public new void Add(IShape value)
      {
         value.Paint += this.DrawSelectionMarker;
         base.Add(value);
      }

      public new void Clear()
      {
         foreach (IShape shape in this)
         {
            shape.Paint -= this.DrawSelectionMarker;
         }

         base.Clear();
      }

      public void AddRange(IEnumerable<IShape> collection)
      {
         foreach (IShape shape in collection)
         {
            shape.Paint += this.DrawSelectionMarker;
            base.Add(shape);
         }
      }

      private void DrawSelectionMarker(object sender, PaintEventArgs e)
      {
         Graphics dc = e.Graphics;
         dc.SmoothingMode = SmoothingMode.HighQuality;
         SolidBrush b = new SolidBrush(Color.FromArgb(125, 245, 0, 0));
         dc.FillRegion(b, ((IShape)sender).HitRegion);
       }
   }
}