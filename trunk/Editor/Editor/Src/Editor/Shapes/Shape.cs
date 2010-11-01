/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.10.2010
 * Time: 14:46
 *
 *
 */

namespace Pppv.Editor.Shapes
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	
	using Pppv.Net;

	public abstract class Shape : IShape
	{
		private PetriNetGraphical parentNet;
		private Size size;
		private Region hitRegion;
		private int x, y;
		private DependentShapesList dependentShapes;
		private IShape parentShape;
		
		public Shape()
		{
			this.hitRegion = new Region();
			this.DependentShapes = new DependentShapesList(this);
		}
		
		public event EventHandler<MoveEventArgs> Move;

		public event PaintEventHandler Paint;

		public event EventHandler Change;
		
		public event EventHandler<ParentShapeChangedEventArgs> ParentShapeChanged;
		
		public virtual INetElement BaseElement
		{
			get { return null; }
			protected set { }
		}
		
		public PetriNetGraphical ParentNetGraphical
		{
			get { return this.parentNet; }
			set { this.parentNet = value; }
		}
		
		public Point Location
		{
			get { return new Point(this.X, this.Y); }
		}

		public virtual int X
		{
			get { return this.x; }
			set { this.x = value; }
		}

		public virtual int Y
		{
			get { return this.y; }
			set { this.y = value; }
		}
		
		public virtual Point Center
		{
			get
			{
				float xt, yt;
				xt = this.X + (this.Size.Width / 2);
				yt = this.Y + (this.Size.Height / 2);
				return new Point((int)xt, (int)yt);
			}
		}
		
		public Size Size
		{
			get
			{
				return this.size;
			}
			
			set
			{
				this.size = value;
				this.UpdateHitRegion();
			}
		}

		public Region HitRegion
		{
			get { return this.hitRegion; }
			set { this.hitRegion = value; }
		}
		
		public IShape ParentShape
		{
			get
			{
				return this.parentShape;
			}

			set
			{
				ParentShapeChangedEventArgs args = new ParentShapeChangedEventArgs(this.parentShape, value);
				this.parentShape = value;
				this.OnParentShapeChanged(args);
 			}
		}
		
		public DependentShapesList DependentShapes
		{
			get { return this.dependentShapes; }
			private set { this.dependentShapes = value; }
		}
		
		public virtual void MoveBy(Point radiusVector)
		{
			Point old = new Point(this.X, this.Y);
			this.X = this.X + radiusVector.X;
			this.Y = this.Y + radiusVector.Y;
			this.OnMove(new MoveEventArgs(old, this.Location));
			this.OnChange(new EventArgs());
			foreach (IShape shape in this.DependentShapes)
			{
				shape.MoveBy(radiusVector);
			}
		}

		public bool Intersect(Point point)
		{
			if (this.HitRegion.IsVisible(point))
			{
				return true;
			}
			
			foreach (IShape shape in this.DependentShapes)
			{
				if (shape.Intersect(point))
				{
					return true;
				}
			}

			return false;
		}

		public bool Intersect(Rectangle rectangle)
		{
			return this.HitRegion.IsVisible(rectangle);
		}

		public bool Intersect(Region region)
		{
			/*Region tmp = new Region(HitRegion);
			tmp.Intersect(_region);
			return tmp.IsEmpty;*/
			return false;
		}
		
		public virtual Point GetConnectPoint(Point from)
		{
			return this.Center;
		}
		
		public abstract void UpdateHitRegion();
		
		public virtual void Draw(PaintEventArgs e)
		{
			this.OnPaint(e);
		}
		
		public void DrawHandler(object sender, PaintEventArgs e)
		{
			this.Draw(e);
		}
		
		public void AddDependantShape(IShape shape)
		{
			this.DependentShapes.Add(shape);
		}
		
		public void RemoveDependantShape(int index)
		{
			this.DependentShapes.RemoveAt(index);
		}
		
		protected void OnMove(MoveEventArgs args)
		{
			this.UpdateHitRegion();
			if (this.Move != null)
			{
				this.Move(this, args);
			}

			this.OnChange(new EventArgs());
		}
		
		protected void OnChange(EventArgs args)
		{
			if (this.Change != null)
			{
				this.Change(this, args);
			}
		}

		protected virtual void OnPaint(PaintEventArgs e)
		{
			if (this.Paint != null)
			{
				this.Paint(this, e);
			}
		}
		
		protected virtual void OnParentShapeChanged(ParentShapeChangedEventArgs args)
		{
			if (this.ParentShapeChanged != null)
			{
				this.ParentShapeChanged(this, args);
			}
		}
	}
}
