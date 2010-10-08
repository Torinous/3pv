namespace Pppv.Editor
{
   using System.Windows.Forms;

   using Pppv.Editor.Commands;

   public class EditorToolStripButton : System.Windows.Forms.ToolStripButton
   {
      private IInterfaceCommand command;

      public EditorToolStripButton()
      {
         this.ImageScaling = ToolStripItemImageScaling.SizeToFit;
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Не страшно")]
      public EditorToolStripButton(IInterfaceCommand command) : this()
      {
         this.Command = command;
      }

      public IInterfaceCommand Command
      {
         get 
         { 
            return this.command;
         }

         set
         {
            this.command = value;
            this.command.ParentItem = this as ToolStripItem;
            this.Text = this.command.Description;
            this.Image = this.command.Pictogram;
            this.Name = this.command.Name;
         }
      }

      protected override void OnClick(System.EventArgs e)
      {
         this.command.Execute();
         base.OnClick(e);
      }
   }
}