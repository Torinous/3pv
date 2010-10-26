/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 24.09.2010
 * Time: 21:38
 */

namespace Pppv.Editor.Shapes
{
	using System;
	using System.Collections;
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;

	using Pppv.Net;
	using Pppv.Utils;

	public class ArcShape : NetElementShape, IArc
	{
		private Point sourceConnectPoint, targetConnectPoint;

		public ArcShape(IArc arc, PetriNetGraphical parentNet)
		{
			this.BaseElement = arc;
			this.ParentNetGraphical = parentNet;
			//this.Move += this.MoveHandler;
			int i = 0;
			foreach (Point point in arc.Points)
			{
				this.DependentShapes.Add(new ArcPointPilonShape(i));
				i++;
			}
		}

		public ArcType ArcType
		{
			get { return (this.BaseElement as IArc).ArcType; }
			set { (this.BaseElement as IArc).ArcType = value; }
		}

		public string ArcTypeName
		{
			get { return (this.BaseElement as IArc).ArcTypeName; }
		}

		public bool Unfinished
		{
			get { return (this.BaseElement as IArc).Unfinished; }
		}

		public string SourceId
		{
			get { return (this.BaseElement as IArc).SourceId; }
			set { (this.BaseElement as IArc).SourceId = value; }
		}

		public string TargetId
		{
			get { return (this.BaseElement as IArc).TargetId; }
			set { (this.BaseElement as IArc).TargetId = value; }
		}

		public IShape Target
		{
			get { return this.ParentNetGraphical.FindShapeForElement(this.ParentNet.GetElementById(this.TargetId)); }
		}

		public IShape Source
		{
			get { return this.ParentNetGraphical.FindShapeForElement(this.ParentNet.GetElementById(this.SourceId)); }
		}

		public PredicatesList Cortege
		{
			get { return (this.BaseElement as IArc).Cortege; }
		}

		public List<Point> Points
		{
			get { return (this.BaseElement as IArc).Points; }
		}
		
		public override Point Center
		{
			get
			{
				if (this.Points.Count == 0)
				{
					return new Point((this.sourceConnectPoint.X + this.targetConnectPoint.X) / 2, (this.sourceConnectPoint.Y + this.targetConnectPoint.Y) / 2);
				}
				else
				{
					if (this.Points.Count % 2 == 1)
					{
						Point p1 = this.Points[((this.Points.Count + 1) / 2) - 1];
						return new Point(p1.X, p1.Y);
					}
					else
					{
						Point p1, p2;
						p1 = this.Points[(this.Points.Count / 2) - 1];
						p2 = this.Points[((this.Points.Count / 2) + 1) - 1];
						return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
					}
				}
			}
		}
		
		public void AddPoint(Point p)
		{
			IArc arc = (this.BaseElement as IArc);
			arc.AddPoint(p);
			this.DependentShapes.Add(new ArcPointPilonShape(arc.Points.Count-1));
		}

		public void DeletePoint(int index)
		{
			IShape pilon = null;
			foreach (IShape shape in this.DependentShapes)
			{
				if (shape is ArcPointPilonShape)
				{
					if ((shape as ArcPointPilonShape).Index == index)
					{
						pilon = shape;
					}
				}
			}

			if (pilon != null)
			{
				this.DependentShapes.Remove(pilon);
			}
			(this.BaseElement as IArc).DeletePoint(index);
		}

		private Point SourceConnectPoint
		{
			get { return this.sourceConnectPoint; }
			set { this.sourceConnectPoint = value; }
		}

		private Point TargetConnectPoint
		{
			get { return this.targetConnectPoint; }
			set { this.targetConnectPoint = value; }
		}

		public override void Draw(PaintEventArgs e)
		{
			Graphics dc = e.Graphics;
			dc.SmoothingMode = SmoothingMode.HighQuality;
			this.UpdateConnectPoints();

			Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0));
			SolidBrush blackBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
			Font font1 = new Font(new FontFamily("Arial"), 16, FontStyle.Regular, GraphicsUnit.Pixel);

			if (this.Points.Count == 0)
			{
				dc.DrawLine(this.PenFactory(this.DeterminePenCapPlace()), this.SourceConnectPoint, this.TargetConnectPoint);
			}
			else
			{
				dc.DrawLine(blackPen, this.sourceConnectPoint, this.Points[0]);
				for (int i = 1; i < this.Points.Count; ++i)
				{
					dc.DrawLine(blackPen, this.Points[i - 1], this.Points[i]);
				}

				dc.DrawLine(this.PenFactory(this.DeterminePenCapPlace()), this.Points[this.Points.Count - 1], this.targetConnectPoint);
			}

			dc.DrawString(this.Cortege.Text, font1, blackBrush, this.Center.X, this.Center.Y - 15);
			this.OnPaint(new PaintEventArgs(e.Graphics, e.ClipRectangle));
		}

		public void UpdateConnectPoints()
		{
			Point mousePositionOnCanvas = this.ParentNetGraphical.Canvas.PointToClient(System.Windows.Forms.Control.MousePosition);
			IShape sourceShape = this.Source;
			IShape targetShape = this.Target;

			if (this.Points.Count == 0)
			{
				if (String.IsNullOrEmpty(this.TargetId))
				{
					this.SourceConnectPoint = sourceShape.GetConnectPoint(mousePositionOnCanvas);
				}
				else
				{
					this.SourceConnectPoint = sourceShape.GetConnectPoint(targetShape.Center);
				}
			}
			else
			{
				this.SourceConnectPoint = sourceShape.GetConnectPoint(this.Points[0]);
			}

			if (String.IsNullOrEmpty(this.TargetId))
			{
				this.TargetConnectPoint = mousePositionOnCanvas;
			}
			else
			{
				if (this.Points.Count == 0)
				{
					
					this.TargetConnectPoint = targetShape.GetConnectPoint(sourceShape.Center);
				}
				else
				{
					this.TargetConnectPoint = targetShape.GetConnectPoint(this.Points[this.Points.Count - 1]);
				}
			}

			this.UpdateHitRegion();
		}

		public override void UpdateHitRegion()
		{
			using (PreciseTimer pr = new PreciseTimer("Arc.UpdateRegion"))
			{
				this.HitRegion.MakeEmpty();
				GraphicsPath tmpPath = new GraphicsPath();

				Point lastPoint = this.SourceConnectPoint;

				foreach (Point p in this.Points)
				{
					tmpPath.AddLine(lastPoint.X, lastPoint.Y, p.X, p.Y);
					lastPoint = p;
				}

				if (lastPoint != this.TargetConnectPoint)
				{
					tmpPath.AddLine(lastPoint.X, lastPoint.Y, this.TargetConnectPoint.X, this.TargetConnectPoint.Y);

					tmpPath.Widen(new Pen(Color.Red, 4));
				}

				this.HitRegion.Union(tmpPath);
			}
		}

		protected static CustomLineCap ArrowCapFabric()
		{
			GraphicsPath capPath = new GraphicsPath();
			capPath.AddLine(new Point(4, -7), new Point(0, 0));
			capPath.AddLine(new Point(-4, -7), new Point(0, 0));
			CustomLineCap arrowCap = new CustomLineCap(null, capPath);
			return arrowCap;
		}

		protected static CustomLineCap RoundCapFabric()
		{
			GraphicsPath capPath = new GraphicsPath();
			capPath.AddEllipse(-4, -8, 8, 8);
			GraphicsPath capPath2 = new GraphicsPath();
			capPath2.AddLine(new Point(0, -8), new Point(0, 0));
			CustomLineCap roundCap = new CustomLineCap(null, capPath);
			return roundCap;
		}

		protected static PositionOnArc DeterminePenCapPlaceForBaseArc()
		{
			return PositionOnArc.End;
		}

		protected Pen PenFactory(PositionOnArc penCapPlace)
		{
			Pen p = new Pen(Color.Black, 1);
			if (this.ArcType == ArcType.NormalArc)
			{
				CustomLineCap arrowCap = ArrowCapFabric();
				arrowCap.SetStrokeCaps(LineCap.Triangle, LineCap.Triangle);
				p.CustomEndCap = arrowCap;
			}
			else
			{
				CustomLineCap roundCap = RoundCapFabric();
				if (penCapPlace == PositionOnArc.End)
				{
					p.CustomEndCap = roundCap;
				}
				else
				{
					p.CustomStartCap = roundCap;
				}
			}

			return p;
		}

		protected PositionOnArc DeterminePenCapPlace()
		{
			if (this.ArcType == ArcType.NormalArc)
			{
				return DeterminePenCapPlaceForBaseArc();
			}
			else
			{
				return this.DeterminePenCapPlaceForInhibitorArc();
			}
		}

		protected PositionOnArc DeterminePenCapPlaceForInhibitorArc()
		{
			if (this.Source is ITransition)
			{
				return PositionOnArc.Start;
			}
			else
			{
				return PositionOnArc.End;
			}
		}
		
		/*protected void MoveHandler(object sender, MoveEventArgs args)
		{
			foreach (IShape shape in this.DependentShapes)
			{
				shape.MoveBy(new Point(args.To.X - args.From.X, args.To.Y - args.From.Y));
			}
		}*/
	}
}