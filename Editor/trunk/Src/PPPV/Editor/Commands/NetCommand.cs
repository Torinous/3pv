using System;
using System.Drawing;
using System.Windows.Forms;

using PPPV.Net;

namespace PPPV.Editor.Commands
{
  public abstract class NetCommand:Command
  {
    //Данные
    private PetriNet net;
    
    //Акцессоры
    public PetriNet Net
    {
      get
      {
        return net;
      }
      set
      {
        net = value;
      }
    }
    
    //Конструктор
    public NetCommand(PetriNetWrapper n):base()
    {
      Net = n;
    }
    public NetCommand():base()
    {
    }
  }
}
