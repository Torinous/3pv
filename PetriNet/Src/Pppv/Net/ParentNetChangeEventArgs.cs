namespace Pppv.Net
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   public class ParentNetChangeEventArgs : EventArgs
   {
      private PetriNet oldParentNet, newParentNet;

      public ParentNetChangeEventArgs(PetriNet oldNet, PetriNet newNet)
      {
         this.OldParentNet = oldNet;
         this.NewParentNet = newNet;
      }

      public PetriNet NewParentNet
      {
         get { return this.newParentNet; }
         set { this.newParentNet = value; }
      }

      public PetriNet OldParentNet
      {
         get { return this.oldParentNet; }
         set { this.oldParentNet = value; }
      }
   }
}