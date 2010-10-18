/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 17.10.2010
 * Time: 5:43
 *
 *
 */

namespace Pppv.Verificator.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.ApplicationFramework.Commands;
   using Pppv.Net;

   public class ConstructStateSpaceCommand : InterfaceCommand
   {
      public ConstructStateSpaceCommand()
      {
         this.Name = "Пространство состояний";
         this.Description = "Построить пространство состояний сети";
         this.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Exit.png"), true);
      }

      public override void Execute()
      {
         PetriNetVerificator verificator = PetriNetVerificator.Instance;
         verificator.CalculateStateSpace();
      }

      public override void Unexecute()
      {
      }
   }
}
