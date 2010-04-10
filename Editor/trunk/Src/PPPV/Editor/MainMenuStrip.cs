using System.Windows.Forms;
using System.Drawing;

using PPPV.Editor.Commands;

namespace PPPV.Editor
{
public class MainMenuStrip : MenuStrip
  {
    public EditorToolStripMenuItem toolStripMenuFile;
    public EditorToolStripMenuItem toolStripMenuNew;
    public EditorToolStripMenuItem toolStripMenuOpen;
    public EditorToolStripMenuItem toolStripMenuSave;
    public EditorToolStripMenuItem toolStripMenuSaveAs;
    public EditorToolStripMenuItem toolStripMenuExit;
    public EditorToolStripMenuItem toolStripMenuEdit;
    public EditorToolStripMenuItem toolStripMenuUndo;
    public EditorToolStripMenuItem toolStripMenuRedo;

    public EditorToolStripMenuItem toolStripMenuView;
    public EditorToolStripMenuItem toolStripMenuNet;
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
      toolStripMenuNew     = new EditorToolStripMenuItem( new NullCommand() );
      toolStripMenuOpen    = new EditorToolStripMenuItem( new NullCommand() );
      toolStripMenuSave    = new EditorToolStripMenuItem( new NullCommand() );
      toolStripMenuSaveAs  = new EditorToolStripMenuItem( new NullCommand() );
      toolStripMenuExit    = new EditorToolStripMenuItem( new QuitCommand( this as Control ) );
      toolStripMenuEdit    = new EditorToolStripMenuItem( );
      toolStripMenuUndo    = new EditorToolStripMenuItem( new UndoCommand() );
      toolStripMenuRedo    = new EditorToolStripMenuItem( new RedoCommand() );
      toolStripMenuView    = new EditorToolStripMenuItem( );
      toolStripMenuNet     = new EditorToolStripMenuItem( );
      toolStripMenuHelp    = new EditorToolStripMenuItem( );
      toolStripMenuAbout   = new EditorToolStripMenuItem( new AboutCommand( this as Control ) );

      /* toolStripMenuFile*/
      this.Items.AddRange(new ToolStripItem[] {this.toolStripMenuFile});
      this.toolStripMenuFile.Name = this.toolStripMenuFile.Text = "Файл";
      this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuNew});
      this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuOpen});
      this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuSaveAs});
      this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuSave});
      this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuExit});

      this.Items.AddRange(new ToolStripItem[] {this.toolStripMenuEdit});
      this.toolStripMenuEdit.Name = this.toolStripMenuEdit.Text = "Правка";
      this.toolStripMenuEdit.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuUndo});
      this.toolStripMenuEdit.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuRedo});

      this.Items.AddRange(new ToolStripItem[] {this.toolStripMenuView});
      this.toolStripMenuView.Name = this.toolStripMenuView.Text = "Вид";

      this.Items.AddRange(new ToolStripItem[] {this.toolStripMenuNet});
      this.toolStripMenuNet.Name = this.toolStripMenuNet.Text = "Сеть";

      this.Items.AddRange(new ToolStripItem[] {this.toolStripMenuHelp});
      this.toolStripMenuHelp.Name = this.toolStripMenuHelp.Text = "Помощь";
      this.toolStripMenuHelp.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuAbout});
    }
  }
}
