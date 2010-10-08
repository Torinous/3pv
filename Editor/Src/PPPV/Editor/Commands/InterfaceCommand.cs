﻿/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 06.10.2010
 * Time: 17:16
 */

namespace Pppv.Editor.Commands
{
   using System;
   using System.ComponentModel;
   using System.Drawing;
   using System.Windows.Forms;

   using Pppv.Net;

   public abstract class InterfaceCommand : Command, IInterfaceCommand
   {
      private string description;
      private Keys shortcutKeys;
      private Image pictogram;
      private ToolStripItem parent;

      protected InterfaceCommand()
      {
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

      public ToolStripItem ParentItem
      {
         get { return this.parent; }
         set { this.parent = value; }
      }

      /*private Form GetOwnerFormForMenu(ToolStripItem theItem)
      {
         if (theItem.Owner != null)
         {
            if (theItem.Owner.FindForm() != null)
            {
               return theItem.Owner.FindForm();
            }
            return this.GetOwnerFormForMenu(((ToolStripDropDown)theItem.Owner).OwnerItem);
         }
         return null;
      }*/

      /*private Form GetOwnerFormForTool()
      {
         if (this.ParentItem.GetCurrentParent() == null)
         {
            return null;
         }

         Form mainForm = this.ParentItem.GetCurrentParent().FindForm();
         if (mainForm != null)
         {
            return mainForm;
         }
         else
         {
            return null;
         }
      }*/

      public virtual bool CheckEnabled()
      {
         return false;
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