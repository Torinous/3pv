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

	using Pppv.ApplicationFramework;
	using Pppv.ApplicationFramework.Utils;
	using Pppv.Editor.Shapes;
	using Pppv.Editor.Tools;
	using Pppv.Net;

	public class PetriNetGraphical : PetriNet, IXmlSerializable
	{
		private SelectedNetElements selectedObjects;
		private int width, height;
		private string fileOfNetPath;
		private bool netSaved;
		private PetriNet baseNet;
		private Editor.NetCanvas canvas;
		private ShapeCollection shapes;

		public PetriNetGraphical(PetriNet baseNet)
		{
			this.baseNet = baseNet;
			this.NetSaved = true;
			this.FileOfNetPath = String.Empty;
			this.Change += this.ChangeController;
			this.shapes = new ShapeCollection(this);
			this.selectedObjects  = new SelectedNetElements();
			this.Shapes.Change += this.ShapesChangeHandler;
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

		public SelectedNetElements SelectedObjects
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

		public PetriNet BaseNet
		{
			get { return this.baseNet; }
		}

		public string FileName
		{
			get { return this.FileOfNetPath.Substring(this.FileOfNetPath.LastIndexOf("\\", StringComparison.Ordinal) + 1); }
		}

		public void AddElement(IShape shape)
		{
			this.baseNet.AddElement(shape.BaseElement);
			this.Shapes.Add(shape);
			this.OnChange(new EventArgs());
		}		
		
		public new void AddElement(INetElement element)
		{
			this.baseNet.AddElement(element);
			this.Shapes.Add(this.CreateShapeForNetElement(element));
			this.OnChange(new EventArgs());
		}

		public void DeleteElement(IShape element)
		{
			IShape element2 = element as IShape;
			this.baseNet.DeleteElement(element2.BaseElement);
			this.Shapes.Remove(element2);
			this.OnChange(new EventArgs());
	}

		public IShape GetTopLevelShapeUnder(Point testPoint)
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
		
		public IShape GetShapeUnder(Point testPoint)
		{
			IShape shape = this.GetTopLevelShapeUnder(testPoint);
			if (shape == null)
			{
				return null;
			}
			
			bool haveDeeperLevelForTest = true;
			while (haveDeeperLevelForTest)
			{
				haveDeeperLevelForTest = false;
				for (int index = shape.DependentShapes.Count - 1; index >= 0; --index)
				{
					if (shape.DependentShapes[index].Intersect(testPoint))
					{
						shape = 	shape.DependentShapes[index];
						haveDeeperLevelForTest = true;
						break;
					}
				}
			}
			
			return shape;
		}

		public Collection<IShape> GetShapeUnder(Rectangle selectedRectangle)
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
					XmlSerializer serealizer = new XmlSerializer(this.baseNet.GetType());
					serealizer.Serialize(stream, this.baseNet);
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
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "pnml files (*.pnml)|*.pnml|All files (*.*)|*.*";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.InitialDirectory = Environment.CurrentDirectory;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				stream = new StreamWriter(saveFileDialog.FileName, false, Encoding.GetEncoding(1251));
				if (stream != null)
				{
					this.FileOfNetPath = fileName = saveFileDialog.FileName;
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

		public IShape FindShapeForElement(INetElement element)
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

		public IShape CreateShapeForNetElement(INetElement baseElement)
		{
			if (baseElement is IArc)
			{
				return this.CreateShapeForArc(baseElement);
			}

			if (baseElement is ITransition)
			{
				return this.CreateShapeForTransition(baseElement);
			}

			if (baseElement is IPlace)
			{
				return this.CreateShapeForPlace(baseElement);
			}

			return null;
		}

		public IShape CreateShapeForArc(INetElement baseElement)
		{
			return new ArcShape((IArc)baseElement, this);
		}

		public IShape CreateShapeForPlace(INetElement baseElement)
		{
			return new PlaceShape((IPlace)baseElement, this);
		}

		public IShape CreateShapeForTransition(INetElement baseElement)
		{
			return new TransitionShape((ITransition)baseElement, this);
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

		private void ShapesChangeHandler(object sender, System.EventArgs args)
		{
			this.OnChange(new EventArgs());
		}

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