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

      public AddNetElementCommand(NetElement element) : this()
      {
         Element = element;
      }

      public AddNetElementCommand(PetriNet net, NetElement element) : this()
      {
         Element = element;
         Net = net;
      }

      public override void Execute()
      {
         try
         {
            Net.AddElement(Element);

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