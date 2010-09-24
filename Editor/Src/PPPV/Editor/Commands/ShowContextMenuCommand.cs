namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor;
   using Pppv.Net;

   public class ShowContextMenuCommand : Command
   {
      private Point position;
      private PetriNetWrapper net;
      private ContextMenuStrip contextMenuStrip;
      private NetElement contextMenuTarget;

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

      public PetriNetWrapper Net
      {
         get { return this.net; }
         set { this.net = value; }
      }

      public ContextMenuStrip ContextMenuStrip
      {
         get { return this.contextMenuStrip; }
         set { this.contextMenuStrip = value; }
      }

      public NetElement ContextMenuTarget
      {
         get { return this.contextMenuTarget; }
         set { this.contextMenuTarget = value; }
      }

      public override void Execute()
      {
         EditorApplication application = EditorApplication.Instance;
         PetriNetWrapper currentNet = application.ActiveNet;

         if (currentNet != null)
         {
            this.ContextMenuTarget = currentNet.NetElementUnder(this.Position);
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
         EditorContextToolStripMenuItem item = new EditorContextToolStripMenuItem(new EditNetElementCommand(this.ContextMenuTarget));
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