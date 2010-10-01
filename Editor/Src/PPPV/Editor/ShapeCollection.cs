/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 26.09.2010
 * Time: 17:06
 */
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

   public class ShapeCollection : Collection<IShape>
   {
      private PetriNetGraphical parentNet;

      public ShapeCollection(PetriNetGraphical net) : base()
      {
         this.parentNet = net;
      }

      public PetriNetGraphical ParentNet
      {
         get { return this.parentNet; }
      }

      public new void Add(IShape value)
      {
         base.Add(value);
         value.ParentNet = this.parentNet;
         this.ParentNet.Paint += value.ParentNetDrawHandler;
      }

      public new void Remove(IShape value)
      {
         base.Remove(value);
         value.ParentNet = null;
         this.ParentNet.Paint -= value.ParentNetDrawHandler;
      }

      public new void Clear()
      {
         foreach (IShape shape in this)
         {
            shape.ParentNet = null;
            this.ParentNet.Paint -= shape.ParentNetDrawHandler;
         }

         base.Clear();
      }

      public void AddRange(IEnumerable<IShape> collection)
      {
         foreach (IShape shape in collection)
         {
            base.Add(shape);
            shape.ParentNet = this.parentNet;
            this.ParentNet.Paint += shape.ParentNetDrawHandler;
         }
      }
   }
}