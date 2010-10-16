/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 14.10.2010
 * Time: 4:22
 *
 *
 */

namespace Pppv.Verificator
{
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.ApplicationFramework;
   using Pppv.ApplicationFramework.Commands;
   using Pppv.Verificator.Commands;

   public class VerificatorMainMenuStrip : MenuStrip
   {
      private CommandToolStripMenuItem toolStripMenuFile;
      private CommandToolStripMenuItem toolStripMenuExit;

      private CommandToolStripMenuItem toolStripMenuAnalize;
      private CommandToolStripMenuItem toolStripMenuManual;

      private CommandToolStripMenuItem toolStripMenuHelp;
      private CommandToolStripMenuItem toolStripMenuAbout;

      public VerificatorMainMenuStrip()
      {
         this.Location = new Point(0, 0);
         this.Name     = this.Text = "menuStrip";
         this.Size     = new System.Drawing.Size(599, 24);
         this.TabIndex = 0;
         this.InitializeComponent();
      }

      private void InitializeComponent()
      {
         this.toolStripMenuFile = new CommandToolStripMenuItem();
         this.toolStripMenuExit = new CommandToolStripMenuItem(new QuitCommand());

         this.toolStripMenuAnalize = new CommandToolStripMenuItem();
         this.toolStripMenuManual  = new CommandToolStripMenuItem(new NullCommand());

         this.toolStripMenuHelp    = new CommandToolStripMenuItem();
         this.toolStripMenuAbout   = new CommandToolStripMenuItem(new NullCommand());

         this.Items.Add(this.toolStripMenuFile);
         this.toolStripMenuFile.Name = this.toolStripMenuFile.Text = "Файл";
         this.toolStripMenuFile.DropDownItems.Add(new ToolStripSeparator());
         this.toolStripMenuFile.DropDownItems.Add(this.toolStripMenuExit);

         this.Items.Add(this.toolStripMenuAnalize);
         this.toolStripMenuAnalize.Name = this.toolStripMenuAnalize.Text = "Анализ";
         this.toolStripMenuAnalize.DropDownItems.Add(this.toolStripMenuManual);

         this.Items.Add(this.toolStripMenuHelp);
         this.toolStripMenuHelp.Name = this.toolStripMenuHelp.Text = "Помощь";
         this.toolStripMenuHelp.DropDownItems.Add(this.toolStripMenuAbout);
      }
   }
}