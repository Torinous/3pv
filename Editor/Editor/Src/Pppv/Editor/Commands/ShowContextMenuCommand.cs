﻿namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	using Pppv.Editor;
	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public class ShowContextMenuCommand : NetInterfaceCommand
	{
		private Point position;

		private ContextMenuStrip contextMenuStrip;
		private IShape contextMenuTarget;

		public ShowContextMenuCommand()
		{
			this.Name = "Контекстное меню";
			this.Description = "Команда вызывающая контекстное меню для элемента сети";
			this.Pictogram = null;
		}

		public ShowContextMenuCommand(Point position) : this()
		{
			this.Position = position;
		}

		public Point Position
		{
			get { return this.position; }
			set { this.position = value; }
		}

		public ContextMenuStrip ContextMenuStrip
		{
			get { return this.contextMenuStrip; }
			set { this.contextMenuStrip = value; }
		}

		public IShape ContextMenuTarget
		{
			get { return this.contextMenuTarget; }
			set { this.contextMenuTarget = value; }
		}

		public override void Execute()
		{
			PetriNetGraphical currentNet = MainForm.Instance.ActiveNet;

			if (currentNet != null)
			{
				this.ContextMenuTarget = currentNet.GetTopLevelShapeUnder(this.Position);
				this.ContextMenuStripFactory().Show(currentNet.Canvas.PointToScreen(this.Position));
			}
		}

		public override void Unexecute()
		{
		}

		private ContextMenuStrip ContextMenuStripFactory()
		{
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			this.AddEditSection(contextMenuStrip);
			this.AddDeleteSection(contextMenuStrip);
			return contextMenuStrip;
		}

		private void AddEditSection(ContextMenuStrip contextMenuStrip)
		{
			EditorContextToolStripMenuItem item = new EditorContextToolStripMenuItem(new EditShapeCommand(this.ContextMenuTarget));
			contextMenuStrip.Items.Add(item);
			item.CheckEnabled();
		}

		private void AddDeleteSection(ContextMenuStrip contextMenuStrip)
		{
			if (this.Net.SelectedObjects.Count == 0)
			{
				EditorContextToolStripMenuItem item = new EditorContextToolStripMenuItem(new DeleteCommand(this.ContextMenuTarget));
				contextMenuStrip.Items.Add(item);
				item.CheckEnabled();
			}
			else
			{
				EditorContextToolStripMenuItem item = new EditorContextToolStripMenuItem(new DeleteSelectedCommand(this.Net));
				contextMenuStrip.Items.Add(item);
				item.CheckEnabled();
			}
		}
	}
}