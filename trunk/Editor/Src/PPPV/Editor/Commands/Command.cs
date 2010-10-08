namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;

   public abstract class Command : ICommand
   {
      private string name;
      private bool isHistorical;

      protected Command()
      {
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

      public abstract void Execute();

      public abstract void Unexecute();
   }
}
