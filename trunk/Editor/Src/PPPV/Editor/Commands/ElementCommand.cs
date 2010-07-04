using System;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public abstract class ElementCommand: NetCommand
  {
    //Данные
    private NetElement ne;
    
    public NetElement Element
    {
      get
      {
        return ne;
      }
      set
      {
        ne = value;
      }
    }
    
    //Конструктор
    public ElementCommand():base()
    {

    }
    
    public ElementCommand(NetElement ne):base(ne.ParentNet as PetriNetWrapper)
    {

    }
  }
}
