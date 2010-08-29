namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;

   public abstract class Command
   {
      private string name;
      private string description;
      private Keys shortcutKeys;
      private Image pictogram;
      private bool isHistorical;

      protected Command()
      {
         this.Name = String.Empty;
         this.Description = String.Empty;
         this.Pictogram = null;
      }

      public bool IsHistorical
      {
         get { return this.isHistorical; }
         set { this.isHistorical = value; }
      }

      public string Name
      {
         get { return this.name; }
         set { this.name = value; }
      }

      public string Description
      {
         get { return this.description; }
         set { this.description = value; }
      }

      public Keys ShortcutKeys
      {
         get { return this.shortcutKeys; }
         protected set { this.shortcutKeys = value; }
      }

      public Image Pictogram
      {
         get { return this.pictogram; }
         protected set { this.pictogram = value; }
      }

      public abstract void Execute();

      public abstract void Unexecute();
   }
}