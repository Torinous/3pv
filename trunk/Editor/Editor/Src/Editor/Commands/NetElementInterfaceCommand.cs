/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 06.10.2010
 * Time: 17:48
 */

namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public abstract class NetElementCommand : NetCommand
	{
		private IShape shape;

		protected NetElementCommand() : base()
		{
		}

		protected NetElementCommand(NetElement ne) : base(ne.ParentNet as PetriNetGraphical)
		{
		}

		public IShape Shape
		{
			get { return this.shape; }
			set { this.shape = value; }
		}
	}
}