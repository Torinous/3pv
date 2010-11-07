namespace Pppv.Editor.Tools
{
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Editor.Commands;
	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public class InhibitorArcTool : ArcTool
	{
		protected override ArcShape ArcFabric(PetriNetGraphical net, INetElement clicked)
		{
			Arc arc = new Arc(clicked, ArcType.InhibitorArc);
			arc.Cortege.Add(new Predicate("X"));
			return (ArcShape)net.CreateShapeForNetElement(arc);
		}
	}
}