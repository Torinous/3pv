using System.Drawing;
using System.Windows.Forms;

using Pppv.Net;
using Pppv.Editor;
using Pppv.Editor.Commands;

namespace Pppv.Editor.Tools
{
   public abstract class Tool
   {
      //string name;
      //string description;
      //Keys shortcutKeys;
      //Image pictogram;

      //Свойства
      public virtual string Name{
         get;
         set;
      }

      public virtual string Description{
         get;
         set;
      }

      public virtual Keys ShortcutKeys{
         get;
         set;
      }

      public virtual Image Pictogram{
         get;
         set;
      }

      protected Tool()
      {
      }

      public virtual void HandleMouseDown( object sender, System.Windows.Forms.MouseEventArgs args )
      {
      }

      public virtual void HandleMouseMove( object sender, System.Windows.Forms.MouseEventArgs args )
      {
      }

      public virtual void HandleMouseUp( object sender, System.Windows.Forms.MouseEventArgs args )
      {
      }

      public virtual void HandleMouseClick( object sender, System.Windows.Forms.MouseEventArgs args )
      {
         /*Контекстное меню по умолчанию показывают все инструменты*/
         if(args.Button == MouseButtons.Right)
         {
            ShowContextMenuCommand c = new ShowContextMenuCommand(sender as Editor.NetCanvas, args.Location);
            c.Execute();
         }
      }

      protected virtual void HandleKeyDown( object sender, KeyEventArgs arg )
      {

      }
   }
}
