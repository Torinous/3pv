namespace Pppv.Editor.Commands
{
   using System;
   using System.Drawing;

   using Pppv.Net;

   public class AddNetElementCommand : ElementCommand
   {
      public AddNetElementCommand()
      {
         Name = "Добавить элемент к сети";
         Description = "Команда добавляющая к заданной сети элемент";
         Pictogram = null;
      }

      public AddNetElementCommand(PetriNet net) : this()
      {
         Net = net;
      }
      
      public AddNetElementCommand(NetElement element) : this(element.ParentNet)
      {
         Element = element;
      }

      public override void Execute()
      {
         Net.AddElement(Element);
         Net.Canvas.Invalidate();
      }

      public override void Unexecute()
      {
      }
   }
}