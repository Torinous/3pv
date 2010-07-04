using System.Windows.Forms;
using System.Drawing;

using PPPV.Editor.Commands;
using PPPV.Editor.Tools;

namespace PPPV.Editor
{
public class MainMenuStrip : MenuStrip
  {
    public EditorToolStripMenuItem toolStripMenuFile;
    public EditorToolStripMenuItem toolStripMenuNew;
    public EditorToolStripMenuItem toolStripMenuOpen;
    public EditorToolStripMenuItem toolStripMenuClose;
    public EditorToolStripMenuItem toolStripMenuSave;
    public EditorToolStripMenuItem toolStripMenuSaveAs;
    public EditorToolStripMenuItem toolStripMenuExit;
    
    public EditorToolStripMenuItem toolStripMenuEdit;
    public EditorToolStripMenuItem toolStripMenuUndo;
    public EditorToolStripMenuItem toolStripMenuRedo;
    public EditorToolStripMenuItem toolStripMenuCut;
    public EditorToolStripMenuItem toolStripMenuCopy;
    public EditorToolStripMenuItem toolStripMenuPaste;
    public EditorToolStripMenuItem toolStripMenuDelete;

    public EditorToolStripMenuItem toolStripMenuView;
    public EditorToolStripMenuItem toolStripMenuZoomIn;
    public EditorToolStripMenuItem toolStripMenuZoomOut;
    
    public EditorToolStripMenuItem toolStripMenuNet;
    public EditorToolStripMenuItem toolStripMenuPointer;
    public EditorToolStripMenuItem toolStripMenuPlace;
    public EditorToolStripMenuItem toolStripMenuTransition;
    public EditorToolStripMenuItem toolStripMenuArc;
    public EditorToolStripMenuItem toolStripMenuInhibitorArc;
    public EditorToolStripMenuItem toolStripMenuAnnotation;
    
    public EditorToolStripMenuItem toolStripMenuHelp;
    public EditorToolStripMenuItem toolStripMenuAbout;

    public MainMenuStrip()
    {
      this.Location = new Point(0, 0);
      this.Name     = this.Text = "menuStrip";
      this.Size     = new System.Drawing.Size(599, 24);
      this.TabIndex = 0;
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      toolStripMenuFile    = new EditorToolStripMenuItem( );
      toolStripMenuNew     = new EditorToolStripMenuItem( new NewNetCommand() );
      toolStripMenuOpen    = new EditorToolStripMenuItem( new OpenNetCommand() );
      toolStripMenuClose   = new EditorToolStripMenuItem( new CloseNetCommand() );
      toolStripMenuSave    = new EditorToolStripMenuItem( new SaveCommand() );
      toolStripMenuSaveAs  = new EditorToolStripMenuItem( new SaveAsCommand() );
      toolStripMenuExit    = new EditorToolStripMenuItem( new QuitCommand() );
      
      toolStripMenuEdit    = new EditorToolStripMenuItem( );
      toolStripMenuUndo    = new EditorToolStripMenuItem( new UndoCommand() );
      toolStripMenuRedo    = new EditorToolStripMenuItem( new RedoCommand() );
      toolStripMenuCut     = new EditorToolStripMenuItem( new CutCommand() );
      toolStripMenuCopy    = new EditorToolStripMenuItem( new CopyCommand() );
      toolStripMenuPaste   = new EditorToolStripMenuItem( new PasteCommand() );
      toolStripMenuDelete  = new EditorToolStripMenuItem( new DeleteCommand() );
      
      toolStripMenuView    = new EditorToolStripMenuItem( );
      toolStripMenuZoomIn  = new EditorToolStripMenuItem( new ZoomInCommand() );
      toolStripMenuZoomOut = new EditorToolStripMenuItem( new ZoomOutCommand() );
      
      toolStripMenuNet          = new EditorToolStripMenuItem( );
      toolStripMenuPointer      = new EditorToolStripMenuItem( new SelectToolCommand( typeof(PointerTool) ) );
      toolStripMenuPlace        = new EditorToolStripMenuItem( new SelectToolCommand( typeof(PlaceTool) ) );
      toolStripMenuTransition   = new EditorToolStripMenuItem( new SelectToolCommand( typeof(TransitionTool) ) );
      toolStripMenuArc          = new EditorToolStripMenuItem( new SelectToolCommand( typeof(ArcTool) ) );
      toolStripMenuInhibitorArc = new EditorToolStripMenuItem( new SelectToolCommand( typeof(InhibitorArcTool) ) );
      toolStripMenuAnnotation   = new EditorToolStripMenuItem( new SelectToolCommand( typeof(AnnotationTool) ) );
      
      toolStripMenuHelp    = new EditorToolStripMenuItem( );
      toolStripMenuAbout   = new EditorToolStripMenuItem( new AboutCommand( this as Control ) );

      /* toolStripMenuFile*/
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
