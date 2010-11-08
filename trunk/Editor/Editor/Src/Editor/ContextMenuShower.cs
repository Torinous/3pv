/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 07.11.2010
 * Time: 21:23
 *
 *
 */

namespace Pppv.Editor
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;
	
	using Pppv.ApplicationFramework.Commands;
	using Pppv.Editor.Commands;
	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public class ContextMenuShower
	{
		private CommandManager commandManager;
		private ContextMenuStrip contextMenu;
		private IShape target;
	
		public ContextMenuShower(IShape shape)
		{
			this.CommandManager = new CommandManager();
			this.ContextMenu = new ContextMenuStrip();
			this.Target = shape;
			this.AddEditCommand();
			this.AddDeleteCommand();
		}
		
		public CommandManager CommandManager
		{
			get { return this.commandManager; }
			private set { this.commandManager = value; }
		}
		
		public ContextMenuStrip ContextMenu
		{
			get { return this.contextMenu; }
			private set { this.contextMenu = value; }
		}
		
		public IShape Target
		{
			get { return this.target; }
			set { this.target = value; }
		}
		
		public EditorForm MainForm
		{
			get
			{
				return this.Target.ParentNetGraphical.Canvas.FindForm() as EditorForm;
			}
		}
		
		public void Show(Point position)
		{
			this.ContextMenu.Show(position);
		}
		
		public void Show()
		{
			this.ContextMenu.Show();
		}
		
		private void AddEditCommand()
		{
			this.CommandManager.Commands.Add(new EditShapeCommand(this.EditShapeCommandHandler, null));
			ToolStripMenuItem editToolsStripMenuItem = new ToolStripMenuItem();
			this.ContextMenu.Items.Add(editToolsStripMenuItem);
			this.CommandManager.Commands[EditShapeCommand.Id].CommandInstances.Add(editToolsStripMenuItem);
			if (this.Target == null)
			{
				this.CommandManager.Commands[EditShapeCommand.Id].Enabled = false;
			}
		}
		
		private void AddDeleteCommand()
		{	
			this.CommandManager.Commands.Add(new DeleteCommand(this.DeleteCommandHandler, null));
			ToolStripMenuItem deleteToolsStripMenuItem = new ToolStripMenuItem();
			this.ContextMenu.Items.Add(deleteToolsStripMenuItem);
			this.CommandManager.Commands[DeleteCommand.Id].CommandInstances.Add(deleteToolsStripMenuItem);
			if (this.Target == null)
			{
				this.CommandManager.Commands[DeleteCommand.Id].Enabled = false;
			}
		}
		
		private void EditShapeCommandHandler(object sender, System.EventArgs e)
		{
			if (this.Target is ArcShape)
			{
				Form f = new ArcEditForm((IArc)this.Target);
				f.ShowDialog(this.MainForm);
				f.Dispose();
			}

			if (this.Target is TransitionShape)
			{
				Form f = new TransitionEditForm((ITransition)this.Target);
				f.ShowDialog(this.MainForm);
				f.Dispose();
			}

			if (this.Target is PlaceShape)
			{
				Form f = new PlaceEditForm((IPlace)this.Target);
				f.ShowDialog(this.MainForm);
				f.Dispose();
			}

			this.Target.ParentNetGraphical.Canvas.Invalidate();
		}
		
		private void DeleteCommandHandler(object sender, System.EventArgs e)
		{
			PetriNetGraphical net = this.MainForm.ActiveNet;
			if (net.SelectedObjects.Count > 0 && net.SelectedObjects.Contains(this.Target))
			{
				foreach (IShape netElement in net.SelectedObjects)
				{
					net.DeleteElement(netElement);
				}
			}
			else
			{
				net.DeleteElement(this.Target);
			}

			net.Canvas.Invalidate();
		}
	}
}
