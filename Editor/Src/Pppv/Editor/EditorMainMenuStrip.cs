namespace Pppv.Editor
{
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;
   using Pppv.Editor.Tools;
   using Pppv.ApplicationFramework;

   public class EditorMainMenuStrip : MenuStrip
   {
      private CommandToolStripMenuItem toolStripMenuFile;
      private CommandToolStripMenuItem toolStripMenuNew;
      private CommandToolStripMenuItem toolStripMenuOpen;
      private CommandToolStripMenuItem toolStripMenuClose;
      private CommandToolStripMenuItem toolStripMenuSave;
      private CommandToolStripMenuItem toolStripMenuSaveAs;
      private CommandToolStripMenuItem toolStripMenuExit;

      private CommandToolStripMenuItem toolStripMenuEdit;
      private CommandToolStripMenuItem toolStripMenuUndo;
      private CommandToolStripMenuItem toolStripMenuRedo;
      private CommandToolStripMenuItem toolStripMenuCut;
      private CommandToolStripMenuItem toolStripMenuCopy;
      private CommandToolStripMenuItem toolStripMenuPaste;
      private CommandToolStripMenuItem toolStripMenuDelete;

      private CommandToolStripMenuItem toolStripMenuView;
      private CommandToolStripMenuItem toolStripMenuZoomIn;
      private CommandToolStripMenuItem toolStripMenuZoomOut;

      private CommandToolStripMenuItem toolStripMenuNet;
      private CommandToolStripMenuItem toolStripMenuPointer;
      private CommandToolStripMenuItem toolStripMenuPlace;
      private CommandToolStripMenuItem toolStripMenuTransition;
      private CommandToolStripMenuItem toolStripMenuArc;
      private CommandToolStripMenuItem toolStripMenuInhibitorArc;
      private CommandToolStripMenuItem toolStripMenuAnnotation;

      private CommandToolStripMenuItem toolStripMenuHelp;
      private CommandToolStripMenuItem toolStripMenuAbout;

      public EditorMainMenuStrip()
      {
         this.Location = new Point(0, 0);
         this.Name     = this.Text = "menuStrip";
         this.Size     = new System.Drawing.Size(599, 24);
         this.TabIndex = 0;
         this.InitializeComponent();
      }

      private void InitializeComponent()
      {
         this.toolStripMenuFile    = new CommandToolStripMenuItem();
         this.toolStripMenuNew     = new CommandToolStripMenuItem(new NewNetCommand());
         this.toolStripMenuOpen    = new CommandToolStripMenuItem(new OpenNetCommand());
         this.toolStripMenuClose   = new CommandToolStripMenuItem(new CloseNetCommand());
         this.toolStripMenuSave    = new CommandToolStripMenuItem(new SaveCommand());
         this.toolStripMenuSaveAs  = new CommandToolStripMenuItem(new SaveAsCommand());
         this.toolStripMenuExit    = new CommandToolStripMenuItem(new QuitCommand());

         this.toolStripMenuEdit    = new CommandToolStripMenuItem();
         this.toolStripMenuUndo    = new CommandToolStripMenuItem(new UndoCommand());
         this.toolStripMenuRedo    = new CommandToolStripMenuItem(new RedoCommand());
         this.toolStripMenuCut     = new CommandToolStripMenuItem(new CutCommand());
         this.toolStripMenuCopy    = new CommandToolStripMenuItem(new CopyCommand());
         this.toolStripMenuPaste   = new CommandToolStripMenuItem(new PasteCommand());
         this.toolStripMenuDelete  = new CommandToolStripMenuItem(new DeleteCommand());

         this.toolStripMenuView    = new CommandToolStripMenuItem();
         this.toolStripMenuZoomIn  = new CommandToolStripMenuItem(new ZoomInCommand());
         this.toolStripMenuZoomOut = new CommandToolStripMenuItem(new ZoomOutCommand());

         this.toolStripMenuNet          = new CommandToolStripMenuItem();
         this.toolStripMenuPointer      = new CommandToolStripMenuItem(new SelectToolCommand(typeof(PointerTool)));
         this.toolStripMenuPlace        = new CommandToolStripMenuItem(new SelectToolCommand(typeof(PlaceTool)));
         this.toolStripMenuTransition   = new CommandToolStripMenuItem(new SelectToolCommand(typeof(TransitionTool)));
         this.toolStripMenuArc          = new CommandToolStripMenuItem(new SelectToolCommand(typeof(ArcTool)));
         this.toolStripMenuInhibitorArc = new CommandToolStripMenuItem(new SelectToolCommand(typeof(InhibitorArcTool)));
         this.toolStripMenuAnnotation   = new CommandToolStripMenuItem(new SelectToolCommand(typeof(AnnotationTool)));

         this.toolStripMenuHelp    = new CommandToolStripMenuItem();
         this.toolStripMenuAbout   = new CommandToolStripMenuItem(new AboutCommand(this as Control));

         this.Items.Add(this.toolStripMenuFile);
         this.toolStripMenuFile.Name = this.toolStripMenuFile.Text = "Файл";
         this.toolStripMenuFile.DropDownItems.Add(this.toolStripMenuNew);
         this.toolStripMenuFile.DropDownItems.Add(this.toolStripMenuOpen);
         this.toolStripMenuFile.DropDownItems.Add(this.toolStripMenuClose);
         this.toolStripMenuFile.DropDownItems.Add(this.toolStripMenuSave);
         this.toolStripMenuFile.DropDownItems.Add(this.toolStripMenuSaveAs);
         this.toolStripMenuFile.DropDownItems.Add(new ToolStripSeparator());
         this.toolStripMenuFile.DropDownItems.Add(this.toolStripMenuExit);

         this.Items.Add(this.toolStripMenuEdit);
         this.toolStripMenuEdit.Name = this.toolStripMenuEdit.Text = "Правка";
         this.toolStripMenuEdit.DropDownItems.Add(this.toolStripMenuUndo);
         this.toolStripMenuEdit.DropDownItems.Add(this.toolStripMenuRedo);
         this.toolStripMenuEdit.DropDownItems.Add(new ToolStripSeparator());
         this.toolStripMenuEdit.DropDownItems.Add(this.toolStripMenuCut);
         this.toolStripMenuEdit.DropDownItems.Add(this.toolStripMenuCopy);
         this.toolStripMenuEdit.DropDownItems.Add(this.toolStripMenuPaste);
         this.toolStripMenuEdit.DropDownItems.Add(this.toolStripMenuDelete);

         this.Items.Add(this.toolStripMenuView);
         this.toolStripMenuView.Name = this.toolStripMenuView.Text = "Вид";
         this.toolStripMenuView.DropDownItems.Add(this.toolStripMenuZoomIn);
         this.toolStripMenuView.DropDownItems.Add(this.toolStripMenuZoomOut);
         this.toolStripMenuView.DropDownItems.Add(new ToolStripSeparator());

         this.Items.Add(this.toolStripMenuNet);
         this.toolStripMenuNet.Name = this.toolStripMenuNet.Text = "Сеть";
         this.toolStripMenuNet.DropDownItems.Add(this.toolStripMenuPointer);
         this.toolStripMenuNet.DropDownItems.Add(this.toolStripMenuPlace);
         this.toolStripMenuNet.DropDownItems.Add(this.toolStripMenuTransition);
         this.toolStripMenuNet.DropDownItems.Add(this.toolStripMenuArc);
         this.toolStripMenuNet.DropDownItems.Add(this.toolStripMenuInhibitorArc);
         this.toolStripMenuNet.DropDownItems.Add(this.toolStripMenuAnnotation);

         this.Items.Add(this.toolStripMenuHelp);
         this.toolStripMenuHelp.Name = this.toolStripMenuHelp.Text = "Помощь";
         this.toolStripMenuHelp.DropDownItems.Add(this.toolStripMenuAbout);
      }
   }
}