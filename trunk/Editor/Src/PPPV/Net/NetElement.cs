using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

namespace PPPV.Net
{
	[Serializable()]
	public abstract class NetElement : Graphical
	{
		/*Данные*/
		protected PetriNet parent;
		
		public NetElement(Point p):base(p)
		{
		}
		
		/*Акцессоры доступа*/
		public virtual string Id{
			get;
			protected set;
		}

		public PetriNet ParentNet{
			get{
				return parent;
			}
			set{
				if(parent != null)
				{
					parent.Paint                 -= this.Draw;
					parent.Paint                 -= this.PaintRetranslator;
				}
				parent = value;
				if(parent != null)
				{
					parent.Paint                 += this.Draw;
					parent.Paint                 += this.PaintRetranslator;
				}
			}
		}

		/*События*/
		public override event EventHandler Change;

		protected override void OnChange(EventArgs args){
			if(Change != null)
			{
				Change(this, args);
			}
			base.OnChange(args);
		}

		public override void PrepareToDeletion()
		{
			ParentNet = null;
		}
	}
}

