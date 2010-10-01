﻿namespace Pppv.Editor
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Collections.ObjectModel;
   using System.Drawing;
   using System.Drawing.Drawing2D;
   using System.IO;
   using System.Text;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.Editor.Shapes;
   using Pppv.Editor.Tools;
   using Pppv.Net;
   using Pppv.Utils;

   public class PetriNetGraphical : PetriNet, IXmlSerializable
   {
      private SelectedNetObjectList selectedObjects;
      private Tool currentTool,
                   pointerTool,
                   placeTool,
                   transitionTool,
                   arcTool,
                   inhibitorArcTool,
                   annotationTool;

      private int width, height;
      private string fileOfNetPath;
      private bool netSaved;

      private PetriNet baseNet;
      private Editor.NetCanvas canvas;
      private ShapeCollection shapes;

      public PetriNetGraphical(PetriNet baseNet)
      {
         this.baseNet = baseNet;
         this.selectedObjects  = new SelectedNetObjectList();
         this.pointerTool      = new PointerTool(this);
         this.placeTool        = new PlaceTool(this);
         this.transitionTool   = new TransitionTool(this);
         this.arcTool          = new ArcTool(this);
         this.inhibitorArcTool = new InhibitorArcTool(this);
         this.annotationTool   = new AnnotationTool(this);
         this.NetSaved = false;
         this.FileOfNetPath = String.Empty;
         this.Change += this.ChangeController;
         this.shapes = new ShapeCollection(this);
         this.CreateShapesForBaseNet();
      }

      public PetriNetGraphical() : this(new PetriNet())
      {
      }

      public event EventHandler<SaveNetEventArgs> Save;

      public event PaintEventHandler Paint;

      public event EventHandler CanvasChange;

      public event EventHandler Change;

      public new string Id
      {
         get { return this.baseNet.Id; }
         protected set { this.baseNet.Id = value; }
      }
      
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

      public new string NetType
      {
         get { return this.baseNet.NetType; }
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
               this.canvas.Paint -= this.CanvasPaintRetranslator;
            }

            this.canvas = value;
            this.OnCanvasChange(new EventArgs());

            if (this.canvas != null)
            {
               this.canvas.Paint += this.CanvasPaintRetranslator;
            }
         }
      }

      public new ArrayList Places
      {
         get { return this.baseNet.Places; }
      }

      public new ArrayList Transitions
      {
         get { return this.baseNet.Transitions; }
      }

      public new ArrayList Arcs
      {
         get { return this.baseNet.Arcs; }
      }

      public ShapeCollection Shapes
      {
         get { return this.shapes; }
      }

      public Tool CurrentTool
      {
         get
         { 
            return this.currentTool;
         }

         set
         {
            if (this.currentTool != null)
            {
               this.currentTool.DisconnectEvents();
            }

            this.currentTool = value;
            if (this.currentTool != null)
            {
               this.currentTool.ConnectEvents();
            }
         }
      }

      public SelectedNetObjectList SelectedObjects
      {
         get { return this.selectedObjects; }
      }

      public bool NetSaved
      {
         get { return this.netSaved; }
         private set { this.netSaved = value; }
      }

      public string FileOfNetPath
      {
         get { return this.fileOfNetPath; }
         set { this.fileOfNetPath = value; }
      }

      public new string AdditionalCode
      {
         get
         {
            return this.baseNet.AdditionalCode;
         }

         set
         {
            this.baseNet.AdditionalCode = value;
            this.OnChange(new EventArgs());
         }
      }

      public new void AddElement(NetElement element)
      {
         this.baseNet.AddElement(element);
         this.Shapes.Add(this.CreateShapeForNetElement(element));
         this.OnChange(new EventArgs());
      }

      public new void DeleteElement(NetElement element)
      {
         this.baseNet.DeleteElement(element);
         this.Shapes.Remove(this.GetShapeForElement(element));
         this.OnChange(new EventArgs());
      }

      public void DeleteElement(IShape element)
      {
         this.baseNet.DeleteElement(element.BaseElement);
         this.Shapes.Remove(element);
         this.OnChange(new EventArgs());
      }

      public IShape GetElementUnder(Point testPoint)
      {
         for (int index = this.Shapes.Count - 1; index >= 0; --index)
         {
            if (this.Shapes[index].Intersect(testPoint))
            {
               return this.Shapes[index];
            }
         }

         return null;
      }

      public Collection<IShape> GetElementUnder(Rectangle selectedRectangle)
      {
         Collection<IShape> selectedObjects = new Collection<IShape>();
         int i = 0;
         for (i = 0; i < this.Shapes.Count; ++i)
         {
            if (this.Shapes[i].Intersect(selectedRectangle))
            {
               selectedObjects.Add(this.Shapes[i]);
            }
         }

         return selectedObjects;
      }

      public new NetElement GetElementById(string searchingId)
      {
         return this.baseNet.GetElementById(searchingId);
      }

      public void SelectToolByType(Type toolType)
      {
         if (toolType == typeof(PointerTool))
         {
            this.CurrentTool = this.pointerTool;
         }
         else if (toolType == typeof(PlaceTool))
         {
            this.CurrentTool = this.placeTool;
         }
         else if (toolType == typeof(TransitionTool))
         {
            this.CurrentTool = this.transitionTool;
         }
         else if (toolType == typeof(ArcTool))
         {
            this.CurrentTool = this.arcTool;
         }
         else if (toolType == typeof(InhibitorArcTool))
         {
            this.CurrentTool = this.inhibitorArcTool;
         }
         else if (toolType == typeof(AnnotationTool))
         {
            this.CurrentTool = this.annotationTool;
         }
         else
         {
            throw new EditorException("Not appropriate tool type!");
         }
      }

      public bool SaveNet()
      {
         bool result = false;
         StreamWriter stream;
         if (!String.IsNullOrEmpty(this.FileOfNetPath))
         {
            if (File.Exists(this.FileOfNetPath))
            {
               File.Delete(this.FileOfNetPath);
            }

            stream = new StreamWriter(this.FileOfNetPath, false, Encoding.GetEncoding(1251));
            if (stream != null)
            {
               XmlSerializer serealizer = new XmlSerializer(this.GetType());
               serealizer.Serialize(stream, this);
               stream.Close();
               this.OnSave(new SaveNetEventArgs(this));
               result = true;
            }
         }
         else
         {
            result = this.SaveNetAs();
         }

         return result;
      }

      public bool SaveNetAs()
      {
         bool result = false;
         StreamWriter stream;
         string fileName = String.Empty;
         SaveFileDialog saveFileDialog1 = new SaveFileDialog();
         saveFileDialog1.Filter = "pnml files (*.pnml)|*.pnml|All files (*.*)|*.*";
         saveFileDialog1.FilterIndex = 1;
         saveFileDialog1.RestoreDirectory = true;

         if (saveFileDialog1.ShowDialog() == DialogResult.OK)
         {
            stream = new StreamWriter(saveFileDialog1.FileName, false, Encoding.GetEncoding(1251));
            if (stream != null)
            {
               this.FileOfNetPath = fileName = saveFileDialog1.FileName;
               if (String.IsNullOrEmpty(this.Id))
               {
                  this.Id = fileName.Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1);
               }

               XmlSerializer serializer = new XmlSerializer(this.baseNet.GetType());
               serializer.Serialize(stream, this.baseNet);
               stream.Close();
               this.OnSave(new SaveNetEventArgs(this));
               result = true;
            }
         }

         return result;
      }

      public void SetSelected()
      {
         EditorApplication app = EditorApplication.Instance;
         if (this.CurrentTool != null)
         {
            app.MainFormInst.ToolToolStrip.CheckToolByType(this.CurrentTool.GetType());
         }
         else
         {
            app.MainFormInst.ToolToolStrip.UncheckTool();
         }
      }

      public IShape GetShapeForElement(NetElement element)
      {
         foreach (IShape shape in this.Shapes)
         {
            if (shape.BaseElement == element)
            {
               return shape;
            }
         }

         return null;
      }

      public IShape CreateShapeForNetElement(NetElement baseElement)
      {
         if (baseElement is Arc)
         {
            return this.CreateShapeForArc(baseElement);
         }

         if (baseElement is Transition)
         {
            return this.CreateShapeForTransition(baseElement);
         }

         if (baseElement is Place)
         {
            return this.CreateShapeForPlace(baseElement);
         }

         return null;
      }

      public IShape CreateShapeForArc(NetElement baseElement)
      {
         return new ArcShape((Arc)baseElement, this);
      }

      public IShape CreateShapeForPlace(NetElement baseElement)
      {
         return new PlaceShape((Place)baseElement, this);
      }

      public IShape CreateShapeForTransition(NetElement baseElement)
      {
         return new TransitionShape((Transition)baseElement, this);
      }

      private void CanvasPaintRetranslator(object sender, PaintEventArgs args)
      {
         this.OnPaint(args);
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

      private void CalculateSize()
      {
         this.Width = this.Height = 0;
         int testX = 0,
         testY = 0;
         foreach (IShape shape in this.Shapes)
         {
            testX = shape.X + shape.Size.Width;
            testY = shape.Y + shape.Size.Height;
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

      private void ChangeController(object sender, System.EventArgs args)
      {
         this.NetSaved = false;
         this.CalculateSize();
      }

      private void OnSave(SaveNetEventArgs args)
      {
         this.NetSaved = true;
         if (this.Save != null)
         {
            this.Save(this, args);
         }
      }

      /*private void NetElementChangeHandler(object sender, System.EventArgs args)
      {
         this.OnChange(new EventArgs());
      }*/

      private void OnChange(EventArgs args)
      {
         if (this.Change != null)
         {
            this.Change(this, args);
         }
      }

      private void CreateShapesForBaseNet()
      {
         foreach (Place place in this.baseNet.Places)
         {
            this.Shapes.Add(this.CreateShapeForNetElement(place));
         }

         foreach (Transition transition in this.baseNet.Transitions)
         {
            this.Shapes.Add(this.CreateShapeForNetElement(transition));
         }

         foreach (Arc arc in this.baseNet.Arcs)
         {
            this.Shapes.Add(this.CreateShapeForNetElement(arc));
         }
      }
   }
}