
using PPPV.Net;

namespace PPPV.Editor
{
  public class PetriNetWrapper : PetriNet
  {
    //Данные
    private SelectedNetObjectsList selectedObjects;
    
    public SelectedNetObjectsList SelectedObjects
    {
      get
      {
        return selectedObjects;
      }
    }
    
    public PetriNetWrapper():base()
    {
      selectedObjects = new SelectedNetObjectsList(20);
    }
  }
}

