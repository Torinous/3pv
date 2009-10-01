using System.Windows.Forms;
using System.Drawing;

namespace PPPv.Editor{

   public class MainMenuStrip : MenuStrip{

      public ToolStripMenuItem toolStripMenuFile;
      public ToolStripMenuItem toolStripMenuNew;
      public ToolStripMenuItem toolStripMenuOpen;
      public ToolStripMenuItem toolStripMenuSave;
      public ToolStripMenuItem toolStripMenuSaveAs;
      public ToolStripMenuItem toolStripMenuExit;
      public ToolStripMenuItem toolStripMenuHelp;
      public ToolStripMenuItem toolStripMenuAbout;

      public MainMenuStrip() {
         this.Location = new Point(0, 0);
         this.Name     = this.Text = "menuStrip";
         this.Size     = new System.Drawing.Size(599, 24);
         this.TabIndex = 0;
         InitializeComponent();
      }

      private void InitializeComponent() {
         toolStripMenuFile    = new ToolStripMenuItem();
         toolStripMenuNew     = new ToolStripMenuItem();
         toolStripMenuOpen    = new ToolStripMenuItem();
         toolStripMenuSave    = new ToolStripMenuItem();
         toolStripMenuSaveAs  = new ToolStripMenuItem();
         toolStripMenuExit    = new ToolStripMenuItem();
         toolStripMenuHelp    = new ToolStripMenuItem();
         toolStripMenuAbout   = new ToolStripMenuItem();

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

         /* toolStripMenuOpen*/
         this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuOpen});
         this.toolStripMenuOpen.Name = "toolStripMenuNew";
         this.toolStripMenuOpen.Size = new System.Drawing.Size(25, 20);
         this.toolStripMenuOpen.Text = "Открыть";

         /* toolStripMenuSaveAs*/
         this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuSaveAs});
         this.toolStripMenuSaveAs.Name = "toolStripMenuSaveAs";
         this.toolStripMenuSaveAs.Size = new System.Drawing.Size(25, 20);
         this.toolStripMenuSaveAs.Text = "Сохранить как...";

         /* toolStripMenuSave*/
         this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuSave});
         this.toolStripMenuSave.Name = "toolStripMenuSave";
         this.toolStripMenuSave.Size = new System.Drawing.Size(25, 20);
         this.toolStripMenuSave.Text = "Сохранить";

         /* toolStripMenuExit */
         this.toolStripMenuFile.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuExit});
         this.toolStripMenuExit.Name = "toolStripMenuNew";
         this.toolStripMenuExit.Size = new System.Drawing.Size(25, 20);
         this.toolStripMenuExit.Text = "Выход";

         /* toolStripMenuHelp*/
         this.Items.AddRange(new ToolStripItem[] {this.toolStripMenuHelp});
         this.toolStripMenuHelp.Name = "menuStripHelp";
         this.toolStripMenuHelp.Size = new System.Drawing.Size(25,20);
         this.toolStripMenuHelp.Text = "Помощь";

         /* toolStripMenuAbout */
         this.toolStripMenuHelp.DropDownItems.AddRange(new ToolStripItem[] {this.toolStripMenuAbout});
         this.toolStripMenuAbout.Name = "toolStripMenuNew";
         this.toolStripMenuAbout.Size = new System.Drawing.Size(25, 20);
         this.toolStripMenuAbout.Text = "О программе";
      }
   }
}
