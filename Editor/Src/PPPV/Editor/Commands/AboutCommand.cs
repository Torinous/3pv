﻿using System;
using System.Windows.Forms;
using System.Drawing;

using Pppv.Net;

namespace Pppv.Editor.Commands
{
   public class AboutCommand : Command
   {
      //Данные
      private Control sender;

      //Конструктор
      public AboutCommand(Control sender)
      {
         Name = "О программе";
         Description = "Вызов формы \"О программе\"";
         this.sender = sender;
      }
      //Методы
      public override void Execute()
      {
         Form f = new AboutForm();
         f.ShowDialog(sender.FindForm());
         f.Dispose();
      }

      public override void Unexecute()
      {
      
      }
   }
}
