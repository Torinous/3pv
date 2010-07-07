using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using PPPV.Editor;
using PPPV.Utils;

namespace PPPV.Net
{
  public partial class PetriNet: IXmlSerializable
  {
    private int width, height;
    private string id;
    private string type;
    /*Элементы сети*/
    private ArrayList places;
    private ArrayList transitions;
    private ArrayList arcs;
    private string additionalCode;

    //private ArrayList currentSelectedObjects;

    private Editor.NetCanvas canvas;

    /*Конструктор*/
    public PetriNet()
    {
      ID = "";
      Type = "PPr/T net";
      Places = new ArrayList(30);
      Transitions = new ArrayList(30);
      Arcs = new ArrayList(60);
      Change += CalculateSize;
    }

    /*Свойства*/
    public int Width
    {
      get
      {
        return width;
      }
      set
      {
       width = value;
      }
    }
    
    public int Height
    {
      get
      {
        return height;
      }
      set
      {
        height = value;
      }
    }
    
    public string ID
    {
      get
      {
        return id;
      }
      protected set
      {
        id = value;
      }
    }

    public string Type
    {
      get
      {
        return type;
      }
      protected set
      {
        type = value;
      }
    }

    public Editor.NetCanvas Canvas
    {
      get
      {
        return canvas;
      }
      set
      {
        if(canvas != null)
        {
          //canvas.Paint                  -= CanvasPaintHandler;
          canvas.Paint                  -= CanvasPaintRetranslator;
        }

        canvas = value;

        if(canvas != null)
        {
          //canvas.Paint                  += CanvasPaintHandler;
          canvas.Paint                  += CanvasPaintRetranslator;
        }
      }
    }

    public ArrayList Places
    {
      get
      {
        return places;
      }
      private set
      {
        places = value;
      }
    }

    public ArrayList Transitions
    {
      get
      {
        return transitions;
      }
      private set
      {
        transitions = value;
      }
    }
  
    public ArrayList Arcs
    {
      get
      {
        return arcs;
      }
      private set
      {
        arcs = value;
      }
    }

    /*Специальное свойство для добавления элементов в сеть*/
    public NetElement ElementPortal
    {
      set
      {
        if(value is Place)
        {
          Places.Add(value);
        }
        if(value is Transition)
        {
          Transitions.Add(value);
        }
        if(value is Arc)
        {
          Arcs.Add(value);
        }
        value.ParentNet = this;
        value.Change += NetElementChangeHandler;
        OnChange(new EventArgs());
      }
    }

    /*Специальное свойство для удаления элементов из сети*/
    public NetElement ElementNullPortal
    {
      set
      {
        value.PrepareToDeletion();
        if(value is Place)
        {
          Places.Remove(value);
        }
        if(value is Transition)
        {
          Transitions.Remove(value);
        }
        if(value is Arc)
        {
          Arcs.Remove(value);
        }
        value.Change -= NetElementChangeHandler;
        OnChange(new EventArgs());
      }
    }

    public string AdditionalCode
    {
      get
      {
        return additionalCode;
      }
      set
      {
        additionalCode = value;
        OnChange(new EventArgs());
      }
    }

    /*События*/
    public event PaintEventHandler Paint;

    public event EventHandler Change;
    
    
    private void CanvasPaintRetranslator(object sender, PaintEventArgs args)
    {
      OnPaint(args);
    }

    private void OnChange(EventArgs args)
    {
      if(Change != null)
      {
        Change(this,args);
      }
    }

    private void OnPaint(PaintEventArgs e)
    {
      if(Paint != null)
      {
        using(PreciseTimer pr = new PreciseTimer("PetriNet.Draw"))
        {
          Paint(this,e);
        }
      }
    }

    public NetElement NetElementUnder(Point _p)
    {
      int i = 0;
      for(i=0;i<Transitions.Count;++i)
      {
        if(((Graphical)Transitions[i]).Intersect(_p))
        {
          return (NetElement)Transitions[i];
        }
      }
      for(i=0;i<Places.Count;++i)
      {
        DebugAssistant.LogTrace(_p.ToString());
        if(((Graphical)Places[i]).Intersect(_p))
        {
          return (NetElement)Places[i];
        }
      }
      for(i=0;i<Arcs.Count;++i)
      {
        if(((Graphical)Arcs[i]).Intersect(_p))
        {
          return (NetElement)Arcs[i];
        }
      }
      return null;
    }

    public ArrayList NetElementUnder(Rectangle selectedRectangle)
    {
      ArrayList selectedObjects = new ArrayList();
      int i = 0;
      for(i=0;i<Transitions.Count;++i)
      {
        if(((NetElement)Transitions[i]).Intersect(selectedRectangle))
        {
          selectedObjects.Add((NetElement)Transitions[i]);
        }
      }
      for(i=0;i<Places.Count;++i)
      {
        if(((NetElement)Places[i]).Intersect(selectedRectangle))
        {
          selectedObjects.Add((NetElement)Places[i]);
        }
      }
      for(i=0;i<Arcs.Count;++i)
      {
        if(((NetElement)Arcs[i]).Intersect(selectedRectangle))
        {
          selectedObjects.Add((NetElement)Arcs[i]);
        }
      }
      return selectedObjects;
    }

    public bool HaveArcBetween(NetElement from_, NetElement to_)
    {
      for(int i=0;i<Arcs.Count;++i)
      {
        if((Arcs[i] as Arc).Source == from_ && (Arcs[i] as Arc).Target == to_)
        {
          return true;
        }
      }
      return false;
    }

    private void NetElementChangeHandler(object sender, System.EventArgs args)
    {
      OnChange(new EventArgs());
    }

    public NetElement GetElementByID(string ID_)
    {
      if(ID_ == "")
        return null;
      foreach(Place place in Places){
        if(place.ID == ID_)
          return place;
      }
      foreach(Transition transition in Transitions)
      {
        if(transition.ID == ID_)
          return transition;
      }
      foreach(Arc arc in Arcs){
        if(arc.ID == ID_)
          return arc;
      }
      return null;
    }

    private void CalculateSize(object sender, System.EventArgs args)
    {
      Width = Height = 0;
      int testX = 0,
          testY = 0;
      foreach(Place place in Places)
      {
        testX = place.X + place.Size.Width;
        testY = place.Y + place.Size.Height;
        if(testX > Width)
          Width = testX;
        if(testY > Height)
          Height = testY;
      }
      foreach(Transition transition in Transitions)
      {
        testX = transition.X + transition.Size.Width;
        testY = transition.Y + transition.Size.Height;
        if(testX > Width)
          Width = testX;
        if(testY > Height)
          Height = testY;
      }
    }
    /*Это видимо нужно чтобы наследники могли реализовать IXmlSerializable*/
    public void WriteXml (XmlWriter writer)
    {}

    public void ReadXml (XmlReader reader)
    {}

    public XmlSchema GetSchema()
    {
      return null;
    }

  } // PetriNet
} // namespace