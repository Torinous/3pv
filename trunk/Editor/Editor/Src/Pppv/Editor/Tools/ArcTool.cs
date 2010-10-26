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
		private static string name  = "Дуга";
		private static string description = "Инструмент создание дуг сети";
		private static Keys shortcutKeys = Keys.Control | Keys.Shift | Keys.A;
		private static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Arc.png"), true);
		private ArcShape arc;

		public ArcTool(PetriNetGraphical net) : base(net)
		{
		}

		public override string Name
		{
			get { return name; }
			set { name = value; }
		}

		public override string Description
		{
			get { return description; }
			set { description = value; }
		}

		public override Keys ShortcutKeys
		{
			get { return shortcutKeys; }
			set { shortcutKeys = value; }
		}

		public override Image Pictogram
		{
			get { return pictogram; }
			set { pictogram = value; }
		}

		public ArcShape Arc
		{
			get { return this.arc; }
			private set { this.arc = value; }
		}

		protected override void HandleMouseDown(NetCanvas canvas, System.Windows.Forms.MouseEventArgs args)
		{
			if (args.Button == MouseButtons.Left)
			{
				IShape clicked = EventSourceNet.GetTopLevelShapeUnder(args.Location);
				if (this.Arc == null)
				{
					if (!(clicked is Arc) && clicked != null)
					{
						this.Arc = this.ArcFabric(clicked.BaseElement);
						this.Arc.BaseElement.ParentNet = this.EventSourceNet.BaseNet;
						this.EventSourceNet.Paint += this.Arc.DrawHandler;
					}
				}
				else
				{
					if (clicked != null)
					{
						if (this.Arc.Source.GetType() != clicked.GetType())
						{
							this.Arc.TargetId = clicked.BaseElement.Id;
							AddShapeCommand c = new AddShapeCommand(EventSourceNet);
							c.Shape = this.Arc;
							c.Execute();
							EventSourceNet.Paint -= this.Arc.DrawHandler;
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
				EventSourceNet.Canvas.Invalidate();
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
					this.ClearTemporaryArc();
					canvas.Invalidate();
				}
			}

			base.HandleKeyDown(canvas, args);
		}

		protected virtual ArcShape ArcFabric(INetElement clicked)
		{
			Arc arc = new Arc(clicked, ArcType.NormalArc);
			arc.Cortege.Add(new Predicate("X"));
			return (ArcShape)EventSourceNet.CreateShapeForNetElement(arc);
		}

		protected void ClearTemporaryArc()
		{
			this.Arc.SourceId = string.Empty;
			EventSourceNet.Paint -= this.Arc.DrawHandler;
			this.Arc = null;
		}
	}
}