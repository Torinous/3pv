﻿namespace Pppv.Editor.Commands
{
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Reflection;
   using System.Windows.Forms;

   using Pppv.Net;
   using Pppv.ApplicationFramework.Commands;

   public class CutCommand : EditorInterfaceCommand
   {
      public CutCommand()
      {
         this.Name = "Вырезать";
         this.Description = "Вырезать выделенный элемент сети";
         this.ShortcutKeys = Keys.Control | Keys.X;
         this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Cut.png"), true);
      }

      public override void Execute()
      {
      }

      public override void Unexecute()
      {
      }

      public override bool CheckEnabled()
      {
         return CheckFormAndActiveNet();
      }
   }
}