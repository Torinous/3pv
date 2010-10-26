/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 25.10.2010
 * Time: 1:30
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

	using Pppv.Editor.Shapes;
	using Pppv.Editor.Tools;
	using Pppv.Net;

	public class DependentShapesList : List<IShape>
	{
		private IShape parentShape;
		
		public DependentShapesList(IShape parentShape) : base()
		{
			this.parentShape = parentShape;
			this.parentShape.Paint += this.ParentShapePaintHandler;
		}

		public event EventHandler Change;

		public event PaintEventHandler Paint;
		
		public IShape ParentShape
		{
			get { return this.parentShape; }
		}

		public new void Add(IShape shape)
		{
			base.Add(shape);
			shape.ParentShape = this.parentShape;
			this.LinkEvents(shape);
		}

		public new void Remove(IShape shape)
		{
			base.Remove(shape);
			shape.ParentShape = null;
			this.UnlinkEvents(shape);
		}

		public new void Clear()
		{
			foreach (IShape shape in this)
			{
				shape.ParentShape = null;
				this.UnlinkEvents(shape);
			}

			base.Clear();
		}

		public new void AddRange(IEnumerable<IShape> collection)
		{
			foreach (IShape shape in collection)
			{
				base.Add(shape);
				shape.ParentShape = this.parentShape;
				this.LinkEvents(shape);
			}
		}

		private void ShapeChangeHandler(object sender, System.EventArgs args)
		{
			this.OnChange(new EventArgs());
		}

		private void ParentShapePaintHandler(object sender, PaintEventArgs args)
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

		private void OnPaint(PaintEventArgs e)
		{
			if (this.Paint != null)
			{
				this.Paint(this, e);
			}
		}
		
		private void LinkEvents(IShape shape)
		{
			this.Paint += shape.DrawHandler;
			shape.Change += this.ShapeChangeHandler;
		}

		private void UnlinkEvents(IShape shape)
		{
			this.Paint -= shape.DrawHandler;
			shape.Change -= this.ShapeChangeHandler;
		}
	}
}
