namespace Pppv.Editor
{
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Editor.Commands;

   public class EditorToolStripButton : System.Windows.Forms.ToolStripButton
   {
      private Command command;

      public EditorToolStripButton(Command command) : base(command.Description, command.Pictogram, null, command.Name)
      {
         this.ImageScaling = ToolStripItemImageScaling.SizeToFit;
         this.command = command;
      }

      public Command Command
      {
         get { return this.command; }
      }

      protected override void OnClick(System.EventArgs e)
      {
         this.command.Execute();
         base.OnClick(e);
      }
   }
}