/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 14.10.2010
 * Time: 4:17
 *
 *
 */
namespace Pppv.Editor.Commands
{
   using System;

   using Pppv.ApplicationFramework.Commands;

   public abstract class EditorInterfaceCommand : InterfaceCommand
   {
      protected EditorInterfaceCommand()
      {
      }

      protected static bool CheckFormAndActiveNet()
      {
         MainForm mainForm = MainForm.Instance;
         if (mainForm != null && mainForm.ActiveNet != null)
         {
            return true;
         }
         else
         {
            return false;
         }
      }
   }
}
