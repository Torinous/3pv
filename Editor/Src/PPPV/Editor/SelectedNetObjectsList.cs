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

   public class NetElementCollection : ICollection
   {
      private Collection<NetElement> elementContainer;

      public NetElementCollection()
      {
         this.elementContainer = new Collection<NetElement>();
      }

      public bool IsSynchronized
      {
         get { return (this.elementContainer as ICollection).IsSynchronized; }
      }

      public object SyncRoot
      {
         get { return (this.elementContainer as ICollection).SyncRoot; }
      }

      public int Count
      {
         get { return (this.elementContainer as ICollection).Count; }
      }

      public NetElement this[int index]
      {
         get { return this.elementContainer[index]; }
         set { this.elementContainer.Insert(index, value); }
      }

      public void Add(NetElement value)
      {
         value.Paint += this.DrawSelectionMarker;
         this.elementContainer.Add(value);
      }

      public void AddRange(IEnumerable<NetElement> collection)
      {
         foreach (NetElement element in collection)
         {
            element.Paint += this.DrawSelectionMarker;
            this.elementContainer.Add(element);
         }
      }
      
      public bool Contains(NetElement testedElement)
      {
         return this.elementContainer.Contains(testedElement);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return new NetElementEnumerator(this.elementContainer.GetEnumerator());
      }

      public NetElementEnumerator GetEnumerator()
      {
         return new NetElementEnumerator(this.elementContainer.GetEnumerator());
      }

      public void Reset()
      {
         (this.elementContainer as IEnumerator).Reset();
      }

      public bool MoveNext()
      {
         return (this.elementContainer as IEnumerator).MoveNext();
      }

      public void Clear()
      {
         foreach (NetElement element in this.elementContainer)
         {
            element.Paint -= this.DrawSelectionMarker;
         }

         this.elementContainer.Clear();
      }

      public void CopyTo(Array array, int index)
      {
         (this.elementContainer as ICollection).CopyTo(array, index);
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
