/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 07.11.2010
 * Time: 3:32
 *
 *
 */

namespace Pppv.Editor.Tools
{
	using System;
	using System.Collections;

	public class ToolsManager
	{
		private Hashtable tools;
		private Hashtable toolForNet;
		
		public ToolsManager()
		{
			this.Tools = new Hashtable();
			this.ToolForNet = new Hashtable();
			this.Tools.Add(ToolsEnum.Pointer, new PointerTool());
			this.Tools.Add(ToolsEnum.Place, new PlaceTool());
			this.Tools.Add(ToolsEnum.Transition, new TransitionTool());
			this.Tools.Add(ToolsEnum.Arc, new ArcTool());
			this.Tools.Add(ToolsEnum.InhibitorArc, new InhibitorArcTool());
			this.Tools.Add(ToolsEnum.Annotation, new AnnotationTool());
		}

		public Hashtable Tools
		{
			get { return tools; }
			private set { tools = value; }
		}
		
		public Hashtable ToolForNet
		{
			get { return toolForNet; }
			private set { toolForNet = value; }
		}
		
		public void Link(ToolsEnum tool, PetriNetGraphical net)
		{
			(this.Tools[tool] as Tool).ConnectEvents(net);
			this.ToolForNet.Add(net, tool);
		}

		public void Unlink(ToolsEnum tool, PetriNetGraphical net)
		{
			(this.Tools[tool] as Tool).DisconnectEvents(net);
			this.ToolForNet.Add(net, tool);
			this.ToolForNet.Remove(net);
		}
		
		public void UnlinkAll(PetriNetGraphical net)
		{
			foreach (DictionaryEntry entry in this.Tools)
			{
				(entry.Value as Tool).DisconnectEvents(net);
			}
			this.ToolForNet.Remove(net);
		}
	}
}
