namespace Pppv.Editor
{
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;
   using Pppv.Editor.Tools;

   public class EditorMainMenuStrip : MenuStrip
   {
      private EditorToolStripMenuItem toolStripMenuFile;
      private EditorToolStripMenuItem toolStripMenuNew;
      private EditorToolStripMenuItem toolStripMenuOpen;
      private EditorToolStripMenuItem toolStripMenuClose;
      private EditorToolStripMenuItem toolStripMenuSave;
      private EditorToolStripMenuItem toolStripMenuSaveAs;
      private EditorToolStripMenuItem toolStripMenuExit;

      private EditorToolStripMenuItem toolStripMenuEdit;
      private EditorToolStripMenuItem toolStripMenuUndo;
      private EditorToolStripMenuItem toolStripMenuRedo;
      private EditorToolStripMenuItem toolStripMenuCut;
      private EditorToolStripMenuItem toolStripMenuCopy;
      private EditorToolStripMenuItem toolStripMenuPaste;
      private EditorToolStripMenuItem toolStripMenuDelete;

      private EditorToolStripMenuItem toolStripMenuView;
      private EditorToolStripMenuItem toolStripMenuZoomIn;
      private EditorToolStripMenuItem toolStripMenuZoomOut;

      private EditorToolStripMenuItem toolStripMenuNet;
      private EditorToolStripMenuItem toolStripMenuPointer;
      private EditorToolStripMenuItem toolStripMenuPlace;
      private EditorToolStripMenuItem toolStripMenuTransition;
      private EditorToolStripMenuItem toolStripMenuArc;
      private EditorToolStripMenuItem toolStripMenuInhibitorArc;
      private EditorToolStripMenuItem toolStripMenuAnnotation;

      private EditorToolStripMenuItem toolStripMenuHelp;
      private EditorToolStripMenuItem toolStripMenuAbout;

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
         this.toolStripMenuFile    = new EditorToolStripMenuItem();
         this.toolStripMenuNew     = new EditorToolStripMenuItem(new NewNetCommand());
         this.toolStripMenuOpen    = new EditorToolStripMenuItem(new OpenNetCommand());
         this.toolStripMenuClose   = new EditorToolStripMenuItem(new CloseNetCommand());
         this.toolStripMenuSave    = new EditorToolStripMenuItem(new SaveCommand());
         this.toolStripMenuSaveAs  = new EditorToolStripMenuItem(new SaveAsCommand());
         this.toolStripMenuExit    = new EditorToolStripMenuItem(new QuitCommand());

         this.toolStripMenuEdit    = new EditorToolStripMenuItem();
         this.toolStripMenuUndo    = new EditorToolStripMenuItem(new UndoCommand());
         this.toolStripMenuRedo    = new EditorToolStripMenuItem(new RedoCommand());
         this.toolStripMenuCut     = new EditorToolStripMenuItem(new CutCommand());
         this.toolStripMenuCopy    = new EditorToolStripMenuItem(new CopyCommand());
         this.toolStripMenuPaste   = new EditorToolStripMenuItem(new PasteCommand());
         this.toolStripMenuDelete  = new EditorToolStripMenuItem(new DeleteCommand());

         this.toolStripMenuView    = new EditorToolStripMenuItem();
         this.toolStripMenuZoomIn  = new EditorToolStripMenuItem(new ZoomInCommand());
         this.toolStripMenuZoomOut = new EditorToolStripMenuItem(new ZoomOutCommand());

         this.toolStripMenuNet          = new EditorToolStripMenuItem();
         this.toolStripMenuPointer      = new EditorToolStripMenuItem(new SelectToolCommand(typeof(PointerTool)));
         this.toolStripMenuPlace        = new EditorToolStripMenuItem(new SelectToolCommand(typeof(PlaceTool)));
         this.toolStripMenuTransition   = new EditorToolStripMenuItem(new SelectToolCommand(typeof(TransitionTool)));
         this.toolStripMenuArc          = new EditorToolStripMenuItem(new SelectToolCommand(typeof(ArcTool)));
         this.toolStripMenuInhibitorArc = new EditorToolStripMenuItem(new SelectToolCommand(typeof(InhibitorArcTool)));
         this.toolStripMenuAnnotation   = new EditorToolStripMenuItem(new SelectToolCommand(typeof(AnnotationTool)));

         this.toolStripMenuHelp    = new EditorToolStripMenuItem();
         this.toolStripMenuAbout   = new EditorToolStripMenuItem(new AboutCommand(this as Control));

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