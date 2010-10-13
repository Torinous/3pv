/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 12.10.2010
 * Time: 3:29
 *
 *
 */

namespace Pppv.Verificator
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;

   public partial class VerificatorForm : Form
   {
      private PetriNet net;

      public VerificatorForm(PetriNet net)
      {
         InitializeComponent();
         this.Net = net;
      }

      public PetriNet Net
      {
         get { return net; }
         private set { net = value; }
      }
   }
}
