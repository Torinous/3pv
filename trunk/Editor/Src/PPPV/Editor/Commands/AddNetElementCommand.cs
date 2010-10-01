﻿namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;

   using Pppv.Net;

   public class AddNetElementCommand : NetCommand
   {
      private NetElement element;

      public AddNetElementCommand()
      {
         Name = "Добавить элемент к сети";
         Description = "Команда добавляющая к заданной сети элемент";
         Pictogram = null;
      }

      public AddNetElementCommand(PetriNetGraphical net) : this()
      {
         Net = net;
      }

      public AddNetElementCommand(NetElement element) : this()
      {
         this.Element = element;
      }

      public AddNetElementCommand(PetriNetGraphical net, NetElement element) : this()
      {
         this.Element = element;
         Net = net;
      }

      public NetElement Element
      {
         get { return this.element; }
         set { this.element = value; }
      }

      public override void Execute()
      {
         try
         {
            Net.AddElement(this.Element);

            if (Net.Canvas != null)
            {
               Net.Canvas.Invalidate();
            }
         }
         catch (Exception e)
         {
            throw new EditorException("Внутреннее исключение в команде AddNetElementCommand", e);
         }
      }

      public override void Unexecute()
      {
      }
   }
}