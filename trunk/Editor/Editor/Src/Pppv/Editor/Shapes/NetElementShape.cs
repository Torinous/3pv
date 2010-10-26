/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 01.10.2010
 * Time: 17:18
 */

namespace Pppv.Editor.Shapes
{
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	using Pppv.Editor;
	using Pppv.Net;

	public abstract class NetElementShape : Shape, IShape
	{
		private INetElement baseElement;
		
		protected NetElementShape()
		{
		}

		public override INetElement BaseElement
		{
			get { return this.baseElement; }
			protected set {this.baseElement = value; }
		}
				
		public new Point Location
		{
			get { return new Point(this.BaseElement.X, this.BaseElement.Y); }
		}

		public override int X
		{
			get { return this.BaseElement.X; }
			set { base.X = this.BaseElement.X = value; }
		}

		public override int Y
		{
			get { return this.BaseElement.Y; }
			set { base.Y = this.BaseElement.Y = value; }
		}

		public string Name
		{
			get { return this.BaseElement.Name; }
			set { this.BaseElement.Name = value; }
		}

		public string Id
		{
			get { return this.BaseElement.Id; }
		}

		public PetriNet ParentNet
		{
			get { return this.BaseElement.ParentNet; }
			set { this.BaseElement.ParentNet = value; }
		}

		public override Point GetConnectPoint(Point from)
		{
			return this.GetConnectPoint(from, this.ParentNetGraphical.Canvas);
		}
		
		public void WriteXml(XmlWriter writer)
		{
			this.BaseElement.WriteXml(writer);
		}

		public void ReadXml(XmlReader reader)
		{
			this.BaseElement.ReadXml(reader);
		}

		public XmlSchema GetSchema()
		{
			return this.BaseElement.GetSchema();
		}

		public void SetId(int number)
		{
			this.BaseElement.SetId(number);
		}
		
		public override void MoveBy(Point radiusVector)
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

		protected Point GetConnectPoint(Point from, Control canvas)
		{
			Graphics g;
			Point pilon = new Point();
			if (canvas != null)
			{
				g = canvas.CreateGraphics();
				Region reg = new Region();
				reg = this.HitRegion.Clone();
				Pen greenPen = new Pen(Color.Black, 1);
				GraphicsPath gp = new GraphicsPath();
				Rectangle rect = new Rectangle();

				/*Если не посчитается, просто вернём центр*/
				pilon.X = this.Center.X;
				pilon.Y = this.Center.Y;

				if (from != this.Center)
				{
					gp.AddLine(from, this.Center);
					gp.Widen(greenPen);
					reg.Intersect(gp);
					RectangleF bounds = reg.GetBounds(g);
					rect = Rectangle.Ceiling(bounds);
					if (from.X <= this.Center.X)
					{
						if (from.Y <= this.Center.Y)
						{
							pilon.X = rect.Left;
							pilon.Y = rect.Top;
						}
						else
						{
							pilon.X = rect.Left;
							pilon.Y = rect.Bottom;
						}
					}
					else
					{
						if (from.Y <= this.Center.Y)
						{
							pilon.X = rect.Right;
							pilon.Y = rect.Top;
						}
						else
						{
							pilon.X = rect.Right;
							pilon.Y = rect.Bottom;
						}
					}

					g.Dispose();
				}
			}
			else
			{
				pilon.X = this.Center.X;
				pilon.Y = this.Center.Y;
			}

			return pilon;
		}
	}
}