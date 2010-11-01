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
			this.ParentNet.Paint += this.ParentNetPaintHandler;
		}

		public event EventHandler Change;

		public event PaintEventHandler Paint;

		public PetriNetGraphical ParentNet
		{
			get { return this.parentNet; }
		}

		public new void Add(IShape shape)
		{
			base.Add(shape);
			shape.ParentNetGraphical = this.parentNet;
			this.LinkEvents(shape);
		}

		public new void Remove(IShape shape)
		{
			base.Remove(shape);
			shape.ParentNetGraphical = null;
			this.UnlinkEvents(shape);
		}

		public new void Clear()
		{
			foreach (IShape shape in this)
			{
				shape.ParentNetGraphical = null;
				this.UnlinkEvents(shape);
			}

			base.Clear();
		}

		public void AddRange(IEnumerable<IShape> collection)
		{
			foreach (IShape shape in collection)
			{
				base.Add(shape);
				shape.ParentNetGraphical = this.parentNet;
				this.LinkEvents(shape);
			}
		}

		private void ShapeChangeHandler(object sender, System.EventArgs args)
		{
			this.OnChange(new EventArgs());
		}

		private void ParentNetPaintHandler(object sender, PaintEventArgs args)
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