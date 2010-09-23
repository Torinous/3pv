namespace Pppv.Net
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Collections.ObjectModel;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.Globalization;
   using System.IO;
   using System.Text;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.Editor;
   using Pppv.Utils;

   public class PetriNet
   {
      private int width, height;
      private string id;
      private string type;

      private ArrayList places;
      private ArrayList transitions;
      private ArrayList arcs;
      private string additionalCode;

      private Editor.NetCanvas canvas;

      public PetriNet()
      {
         this.Id = String.Empty;
         this.NetType = "PPr/T net";
         this.places = new ArrayList(30);
         this.transitions = new ArrayList(30);
         this.arcs = new ArrayList(60);
         this.Change += this.CalculateSize;
      }

      public event PaintEventHandler Paint;

      public event EventHandler Change;

      public event EventHandler CanvasChange;

      public int Width
      {
         get { return this.width; }
         set { this.width = value; }
      }

      public int Height
      {
         get { return this.height; }
         set { this.height = value; }
      }

      public string Id
      {
         get { return this.id; }
         protected set { this.id = value; }
      }

      public string NetType
      {
         get { return this.type; }
         protected set { this.type = value; }
      }

      public Editor.NetCanvas Canvas
      {
         get
         {
            return this.canvas;
         }

         set
         {
            if (this.canvas != null)
            {
               // this.canvas.Paint -= this.CanvasPaintHandler;
               this.canvas.Paint -= this.CanvasPaintRetranslator;
            }

            this.canvas = value;
            this.OnCanvasChange(new EventArgs());

            if (this.canvas != null)
            {
               // this.canvas.Paint += this.CanvasPaintHandler;
               this.canvas.Paint += this.CanvasPaintRetranslator;
            }
         }
      }

      public ArrayList Places
      {
         get { return this.places; }
      }

      public ArrayList Transitions
      {
         get { return this.transitions; }
      }

      public ArrayList Arcs
      {
         get { return this.arcs; }
      }

      public string AdditionalCode
      {
         get
         {
            return this.additionalCode;
         }

         set
         {
            this.additionalCode = value;
            this.OnChange(new EventArgs());
         }
      }

      public void AddElement(NetElement element)
      {
         if (element is Place)
         {
            element.SetId(this.Places.Count);
            this.Places.Add(element);
         }

         if (element is Transition)
         {
            element.SetId(this.Transitions.Count);
            this.Transitions.Add(element);
         }

         if (element is Arc)
         {
            element.SetId(this.Arcs.Count);
            this.Arcs.Add(element);
         }

         element.ParentNet = this;
         element.Change += this.NetElementChangeHandler;
         this.OnChange(new EventArgs());
      }

      public void DeleteElement(NetElement element)
      {
         element.PrepareToDeletion();
         if (element is Place)
         {
            this.Places.Remove(element);
         }

         if (element is Transition)
         {
            this.Transitions.Remove(element);
         }

         if (element is Arc)
         {
            this.Arcs.Remove(element);
         }

         element.Change -= this.NetElementChangeHandler;
         this.OnChange(new EventArgs());
      }

      public NetElement NetElementUnder(Point testPoint)
      {
         int i = 0;
         for (i = 0; i < this.Transitions.Count; ++i)
         {
            if (((Graphical)this.Transitions[i]).Intersect(testPoint))
            {
               return (NetElement)this.Transitions[i];
            }
         }

         for (i = 0; i < this.Places.Count; ++i)
         {
            DebugAssistant.LogTrace(testPoint.ToString());
            if (((Graphical)this.Places[i]).Intersect(testPoint))
            {
               return (NetElement)this.Places[i];
            }
         }

         for (i = 0; i < this.Arcs.Count; ++i)
         {
            if (((Graphical)this.Arcs[i]).Intersect(testPoint))
            {
               return (NetElement)this.Arcs[i];
            }
         }

         return null;
      }

      public Collection<NetElement> NetElementUnder(Rectangle selectedRectangle)
      {
         Collection<NetElement> selectedObjects = new Collection<NetElement>();
         int i = 0;
         for (i = 0; i < this.Transitions.Count; ++i)
         {
            if (((NetElement)this.Transitions[i]).Intersect(selectedRectangle))
            {
               selectedObjects.Add((NetElement)this.Transitions[i]);
            }
         }

         for (i = 0; i < this.Places.Count; ++i)
         {
            if (((NetElement)this.Places[i]).Intersect(selectedRectangle))
            {
               selectedObjects.Add((NetElement)this.Places[i]);
            }
         }

         for (i = 0; i < this.Arcs.Count; ++i)
         {
            if (((NetElement)this.Arcs[i]).Intersect(selectedRectangle))
            {
               selectedObjects.Add((NetElement)this.Arcs[i]);
            }
         }

         return selectedObjects;
      }

      public bool HaveArcBetween(NetElement fromElement, NetElement toElement)
      {
         for (int i = 0; i < this.Arcs.Count; ++i)
         {
            if ((this.Arcs[i] as Arc).Source == fromElement && (this.Arcs[i] as Arc).Target == toElement)
            {
               return true;
            }
         }

         return false;
      }

      public NetElement GetElementById(string searchingId)
      {
         if (String.IsNullOrEmpty(searchingId))
         {
            return null;
         }

         foreach (Place place in this.Places)
         {
            if (place.Id == searchingId)
            {
               return place;
            }
         }

         foreach (Transition transition in this.Transitions)
         {
            if (transition.Id == searchingId)
            {
               return transition;
            }
         }

         foreach (Arc arc in this.Arcs)
         {
            if (arc.Id == searchingId)
            {
               return arc;
            }
         }

         return null;
      }

      private void NetElementChangeHandler(object sender, System.EventArgs args)
      {
         this.OnChange(new EventArgs());
      }

      private void CanvasPaintRetranslator(object sender, PaintEventArgs args)
      {
         this.OnPaint(args);
      }

      private void OnChange(EventArgs args)
      {
         if (this.Change != null)
         {
            this.Change(this, args);
         }
      }

      private void OnCanvasChange(EventArgs args)
      {
         if (this.CanvasChange != null)
         {
            this.CanvasChange(this, args);
         }
      }

      private void OnPaint(PaintEventArgs e)
      {
         if (this.Paint != null)
         {
            using (PreciseTimer pr = new PreciseTimer("PetriNet.Draw"))
            {
               this.Paint(this, e);
            }
         }
      }

      private void CalculateSize(object sender, System.EventArgs args)
      {
         this.Width = this.Height = 0;
         int testX = 0,
         testY = 0;
         foreach (Place place in this.Places)
         {
            testX = place.X + place.Size.Width;
            testY = place.Y + place.Size.Height;
            if (testX > this.Width)
            {
               this.Width = testX;
            }

            if (testY > this.Height)
            {
               this.Height = testY;
            }
         }

         foreach (Transition transition in this.Transitions)
         {
            testX = transition.X + transition.Size.Width;
            testY = transition.Y + transition.Size.Height;
            if (testX > this.Width)
            {
               this.Width = testX;
            }

            if (testY > this.Height)
            {
               this.Height = testY;
            }
         }
      }
   }
}