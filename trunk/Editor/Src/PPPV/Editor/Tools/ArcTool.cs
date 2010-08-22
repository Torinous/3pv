using System.Reflection;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Utils;
using PPPV.Editor.Commands;

namespace PPPV.Editor.Tools
{
	public class ArcTool : Tool
	{
		/*Данные*/
		static string name  = "Дуга";
		static string description = "Инструмент создание дуг сети";
		static Keys shortcutKeys = Keys.Control|Keys.Shift|Keys.A;
		static Image pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PPPV.Resources.Arc.png"), true);
		private Arc arc;
	
		/*Акцессоры доступа*/
		public override string Name{
			get{
				return name;
			}
			set{
				name = value;
			}
		}

		public override string Description{
			get{
				return description;
			}
			set{
			description = value;
			}
		}

		public override Keys ShortcutKeys{
			get{
				return shortcutKeys;
			}
			set{
				shortcutKeys = value;
			}
		}

		public override Image Pictogram{
			get{
				return pictogram;
			}
			set{
				pictogram = value;
			}
		}
		
		public Arc Arc{
			get{
				return arc;
			}
			private set{
				if(arc != null)
				{
					EditorApplication app = EditorApplication.Instance;
					app.ActiveNet.Paint -=arc.Draw;
				}
				arc = value;
				if(arc != null)
				{
					EditorApplication app = EditorApplication.Instance;
					app.ActiveNet.Paint +=arc.Draw;
				}
			}
		}

		public ArcTool()
		{
		}

		public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
		{
			NetCanvas someCanvas = sender as Editor.NetCanvas;
			if(args.Button == MouseButtons.Left)
			{
				PetriNet pn = someCanvas.Net;
				NetElement clicked = pn.NetElementUnder(new Point(args.X, args.Y));
				if(Arc == null)
				{
					if(!(clicked is Arc) && clicked != null)
						Arc = new Arc(clicked);
				}
				else
				{
					if(clicked != null && Arc.Source.GetType() != clicked.GetType())
					{
						Arc.Target = clicked;
						AddNetElementCommand c = new AddNetElementCommand(pn);
						c.Element = Arc;
						c.Execute();
						Arc = null;
					}
				}
				someCanvas.Invalidate();
			}

			base.HandleMouseDown(sender, args);
		}

		public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
		{
			if(Arc != null)
				Arc.TargetPilon = new Point(args.X, args.Y);
			base.HandleMouseMove(sender, args);
		}

		public override void HandleMouseUp(object sender, System.Windows.Forms.MouseEventArgs args)
		{
			base.HandleMouseUp(sender, args);
		}

		public override void HandleMouseClick(object sender, System.Windows.Forms.MouseEventArgs args)
		{
			base.HandleMouseClick(sender, args);
		}

		public override void HandleKeyDown( object sender, KeyEventArgs args )
		{
			base.HandleKeyDown(sender, args);
			if(args.KeyCode == Keys.Escape)
			{
			if(Arc.Target == null)
			{
				Arc = null;
				(sender as PetriNet).Canvas.Invalidate();//TODO: полный Invalidate это нехорошо!!!
			}
			}
		}
	}
}