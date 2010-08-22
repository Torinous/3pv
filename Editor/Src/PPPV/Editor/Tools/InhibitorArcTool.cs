using System.Reflection;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Editor.Commands;

namespace PPPV.Editor.Tools
{
	public class InhibitorArcTool : Tool
	{
		/*Данные*/
		static string name;
		static string description;
		static Keys shortcutKeys;
		static Image pictogram;
		private InhibitorArc arc;
	
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
		
		public InhibitorArc Arc{
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

		//cons
		static InhibitorArcTool()
		{
			name = "Ингибиторная дуга";
			description = "Инструмент создание ингибиторных дуг сети";
			shortcutKeys = Keys.Control|Keys.Shift|Keys.I;
			pictogram  = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PPPV.Resources.Inhibitor Arc.png"), true);
		}

		public InhibitorArcTool()
		{
		}

		/*Методы*/
		public override void HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
		{
			if(args.Button == MouseButtons.Left)
			{
				PetriNet pn = (sender as Editor.NetCanvas).Net;
				NetElement clicked = pn.NetElementUnder(new Point(args.X, args.Y));
				if(Arc == null)
				{
					if(!(clicked is Arc) && clicked != null)
						Arc = new InhibitorArc(clicked);
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
				(sender as Editor.NetCanvas).Invalidate();
			}
			base.HandleMouseDown(sender, args);
		}

		public override void HandleMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
		{
			if(args.Button == MouseButtons.Left)
			{
				PetriNet pn = (sender as Editor.NetCanvas).Net;
				NetElement clicked = pn.NetElementUnder(new Point(args.X,args.Y));
				clicked = (clicked is Arc) ? null : clicked;
				//if(clicked != null && !pn.HaveUnfinishedArcs())
				//pn.AddArc(clicked);
				(sender as Editor.NetCanvas).Invalidate();
			}
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
		}
	}
}