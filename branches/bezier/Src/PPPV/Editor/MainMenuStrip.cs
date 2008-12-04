using System.Windows.Forms;
using System.Drawing;

namespace PPPv.Editor{

  public class MainMenuStrip : MenuStrip{

    public ToolStripMenuItem toolStripMenuFile;
    public ToolStripMenuItem toolStripMenuNew;
    public ToolStripMenuItem toolStripMenuExit;

    public MainMenuStrip() {
      this.Location = new Point(0, 0);
      this.Name = this.Text = "_menuStrip";
      this.Size = new System.Drawing.Size(599, 24);
      this.TabIndex = 0;
      InitializeComponent();
    }

    private void InitializeComponent() {
      toolStripMenuFile = new ToolStripMenuItem();
      toolStripMenuNew  = new ToolStripMenuItem();
      toolStripMenuExit = new ToolStripMenuItem();

      /* toolStripMenuFile*/
      this.Items.AddRange(new ToolStripItem[] {this.toolStripMenuFile});
      this.toolStripMenuFile.Name = "menuStripFile";
      this.toolStripMenuFile.Size = new System.Drawing.Size(25,20);
      this.toolStripMenuFile.Text = "Файл";

      /* toolStripMenuNew*/
      this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuNew});
      this.toolStripMenuNew.Name = "toolStripMenuNew";
      this.toolStripMenuNew.Size = new System.Drawing.Size(25, 20);
      this.toolStripMenuNew.Text = "Создать";

      /* toolStripMenuExit */
      this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuExit});
      this.toolStripMenuExit.Name = "toolStripMenuNew";
      this.toolStripMenuExit.Size = new System.Drawing.Size(25, 20);
      this.toolStripMenuExit.Text = "Выход";
    }
  }
}
