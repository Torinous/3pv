namespace Pppv.Editor.Tools
{
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Editor.Commands;
	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public class ArcTool : Tool
	{
		private ArcShape arc;

		public ArcShape Arc
		{
			get { return this.arc; }
			private set { this.arc = value; }
		}

		protected override void HandleMouseDown(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			if (args.Button == MouseButtons.Left)
			{
				IShape clicked = canvas.Net.GetTopLevelShapeUnder(args.Location);
				if (this.Arc == null)
				{
					if (!(clicked is Arc) && clicked != null)
					{
						this.Arc = this.ArcFabric(canvas.Net, clicked.BaseElement);
						this.Arc.BaseElement.ParentNet = canvas.Net.BaseNet;
						canvas.Net.Paint += this.Arc.DrawHandler;
					}
				}
				else
				{
					if (clicked != null)
					{
						if (this.Arc.Source.GetType() != clicked.GetType())
						{
							this.Arc.TargetId = clicked.BaseElement.Id;
							AddShapeCommand c = new AddShapeCommand(canvas.Net);
							c.Shape = this.Arc;
							c.Execute();
							canvas.Net.Paint -= this.Arc.DrawHandler;
							this.Arc = null;
						}
					}
					else
					{
						this.Arc.AddPoint(args.Location);
					}
				}

				canvas.Invalidate();
			}

			base.HandleMouseDown(canvas, args);
		}

		protected override void HandleMouseMove(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			if (this.Arc != null)
			{
				canvas.Invalidate();
			}

			base.HandleMouseMove(canvas, args);
		}

		protected override void HandleMouseUp(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			base.HandleMouseUp(canvas, args);
		}

		protected override void HandleMouseClick(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			base.HandleMouseClick(canvas, args);
		}

		protected override void HandleKeyDown(NetCanvas canvas, KeyEventArgs args)
		{
			if (args.KeyCode == Keys.Escape)
			{
				if (this.Arc != null && this.Arc.Target == null)
				{
					this.ClearTemporaryArc(canvas.Net);
					canvas.Invalidate();
				}
			}

			base.HandleKeyDown(canvas, args);
		}

		protected virtual ArcShape ArcFabric(PetriNetGraphical net, INetElement clicked)
		{
			Arc arc = new Arc(clicked, ArcType.NormalArc);
			arc.Cortege.Add(new Predicate("X"));
			return (ArcShape)net.CreateShapeForNetElement(arc);
		}

		protected void ClearTemporaryArc(PetriNetGraphical net)
		{
			this.Arc.SourceId = string.Empty;
			net.Paint -= this.Arc.DrawHandler;
			this.Arc = null;
		}
	}
}