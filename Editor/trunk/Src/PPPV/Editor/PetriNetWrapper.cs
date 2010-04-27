using System;

using PPPV.Net;
using PPPV.Editor.Tools;

namespace PPPV.Editor
{
  public class PetriNetWrapper : PetriNet
  {
    //Данные
    protected SelectedNetObjectsList selectedObjects;
    public    Tool currentTool,
                   pointerTool,
                   placeTool,
                   transitionTool,
                   arcTool,
                   inhibitorArcTool,
                   annotationTool;
    
    public Tool CurrentTool
    {
      get
      {
        return currentTool;
      }
      set
      {
        currentTool = value;
      }
    }
    
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
      pointerTool      = new PointerTool();
      placeTool        = new PlaceTool();
      transitionTool   = new TransitionTool();
      arcTool          = new ArcTool();
      inhibitorArcTool = new InhibitorArcTool();
      annotationTool   = new AnnotationTool();
      //Установим стартовый инструмент
      currentTool      = pointerTool;
    }
    
    public void SelectToolByType(Type t)
    {
      if(t == typeof(PointerTool))
        currentTool = pointerTool;
      else if(t == typeof(PlaceTool))
        currentTool = placeTool;
      else if(t == typeof(TransitionTool))
        currentTool = transitionTool;
      else if(t == typeof(ArcTool))
        currentTool = arcTool;
      else if(t == typeof(InhibitorArcTool))
        currentTool = inhibitorArcTool;
      else if(t == typeof(AnnotationTool))
        currentTool = annotationTool;
      else
        throw new Exception("Not appropriate tool type!");
    }
  }
}

