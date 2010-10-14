/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 26.08.2010
 * Time: 17:50
 */

namespace Pppv.ApplicationFramework.Utils
{
   using System;
   using System.Globalization;
   using System.Windows.Forms;

   public static class RtlAwareMessageBox
   {
      public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
      {
         if (IsRightToLeft(owner))
         {
            options |= MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign;
         }

         return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options);
      }

      private static bool IsRightToLeft(IWin32Window owner)
      {
         Control control = owner as Control;
         
         if (control != null)
         {
            return control.RightToLeft == RightToLeft.Yes;
         }

         // If no parent control is available, ask the CurrentUICulture
         // if we are running under right-to-left.
         return CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
      }
   }
}