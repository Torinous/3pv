namespace PPPV.Editor
{
   using System;
   using System.IO;
   using System.Text;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using PPPV.Editor.Tools;
   using PPPV.Net;

   [Serializable()]
   [XmlRoot("pnml")]
   public class PetriNetWrapper : PetriNet, IXmlSerializable
   {
      [NonSerializedAttribute]
      private SelectedNetObjectsList selectedObjects;
      [NonSerializedAttribute]
      private Tool currentTool,
                   pointerTool,
                   placeTool,
                   transitionTool,
                   arcTool,
                   inhibitorArcTool,
                   annotationTool;

      [NonSerializedAttribute]
      private string fileOfNetPath;
      [NonSerializedAttribute]
      private bool netSaved;

      public PetriNetWrapper():base()
      {
         selectedObjects  = new SelectedNetObjectsList(20);
         pointerTool      = new PointerTool();
         placeTool        = new PlaceTool();
         transitionTool   = new TransitionTool();
         arcTool          = new ArcTool();
         inhibitorArcTool = new InhibitorArcTool();
         annotationTool   = new AnnotationTool();
         //Установим стартовый инструмент
         currentTool      = pointerTool;
         NetSaved = false;
         FileOfNetPath = "";
         Change += ChangeController;
      }

      public Tool CurrentTool{
         get{
            return currentTool;
         }
         set{
            currentTool = value;
         }
      }

      public SelectedNetObjectsList SelectedObjects{
         get{
            return selectedObjects;
         }
      }

      public bool NetSaved{
         get{
            return netSaved;
         }
         private set{
            netSaved = value;
         }
      }

      public string FileOfNetPath{
         get{
            return fileOfNetPath;
         }
         set{
            fileOfNetPath = value;
         }
      }

      /*Событие генерируется при сохранении сети в файл*/
      public event EventHandler<SaveEventArgs> Save;

      public void SelectToolByType(Type toolType)
      {
         if(toolType == typeof(PointerTool))
            this.currentTool = pointerTool;
         else if(toolType == typeof(PlaceTool))
            currentTool = placeTool;
         else if(toolType == typeof(TransitionTool))
            currentTool = transitionTool;
         else if(toolType == typeof(ArcTool))
            currentTool = arcTool;
         else if(toolType == typeof(InhibitorArcTool))
            currentTool = inhibitorArcTool;
         else if(toolType == typeof(AnnotationTool))
            currentTool = annotationTool;
         else
            throw new Exception("Not appropriate tool type!");
      }

      public bool SaveNet()
      {
         bool result = false;
         StreamWriter stream;
         if(!String.IsNullOrEmpty(FileOfNetPath))
         {
            if (File.Exists(FileOfNetPath))
            {
               File.Delete(FileOfNetPath);
            }
            stream = new StreamWriter(FileOfNetPath, false, Encoding.GetEncoding(1251));
            if(stream != null)
            {
               XmlSerializer serealizer = new XmlSerializer(this.GetType());
               serealizer.Serialize(stream, this);
               stream.Close();
               result = true;
            }
         }
         else
         {
            result = SaveNetAs();
         }
         return result;
      }

      public bool SaveNetAs()
      {
         bool result = false;
         StreamWriter stream;
         string fileName = "";
         SaveFileDialog saveFileDialog1 = new SaveFileDialog();
         saveFileDialog1.Filter = "pnml files (*.pnml)|*.pnml|All files (*.*)|*.*";
         saveFileDialog1.FilterIndex = 1 ;
         saveFileDialog1.RestoreDirectory = true ;

         if(saveFileDialog1.ShowDialog() == DialogResult.OK)
         {
            stream = new StreamWriter(saveFileDialog1.FileName, false, Encoding.GetEncoding(1251));
            if(stream != null)
            {
               FileOfNetPath = fileName = saveFileDialog1.FileName;
               if(this.Id=="")
                  this.Id = fileName.Substring(fileName.LastIndexOf("\\")+1);

               XmlSerializer serializer = new XmlSerializer(this.GetType());
               serializer.Serialize(stream, this);
               stream.Close();
               result = true;
            }
         }
         return result;
      }

      private void OnSave(SaveEventArgs args)
      {
         NetSaved = true;
         FileOfNetPath = args.FileName;
         if (Save != null)
         {
            Save(this, args);
         }
      }

      public void WriteXml (XmlWriter writer)
      {
         //writer.WriteStartElement("pnml");
         writer.WriteStartElement("net");
         writer.WriteAttributeString("id", Id);
         writer.WriteAttributeString("type", NetType);
         foreach(Place place in Places)
         {
            writer.WriteStartElement("place");
            place.WriteXml(writer);
            writer.WriteEndElement(); // place
         }
         foreach(Transition transition in Transitions)
         {
            writer.WriteStartElement("transition");
            transition.WriteXml(writer);
            writer.WriteEndElement(); // transition
         }
         foreach(Arc arc in Arcs)
         {
            writer.WriteStartElement("arc");
            arc.WriteXml(writer);
            writer.WriteEndElement(); // arc
         }
         writer.WriteStartElement("additionalCode");
         writer.WriteString(this.AdditionalCode);
         writer.WriteEndElement(); // additionalCode
         writer.WriteEndElement(); // net
         //writer.WriteEndElement(); // pnml
      }

      public void ReadXml (XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.ReadStartElement("pnml");
         this.Id = reader.GetAttribute("id");
         this.NetType = reader.GetAttribute("type");

         if(!reader.IsEmptyElement)
         {
            reader.ReadStartElement("net");
            while(reader.NodeType != XmlNodeType.EndElement)
            {
               switch(reader.Name)
               {
                  case "place":
                     subTreeReader = reader.ReadSubtree();
                     ElementPortal = new Place(subTreeReader);
                     subTreeReader.Close();
                     reader.Skip();
                     break;
                  case "transition":
                     subTreeReader = reader.ReadSubtree();
                     ElementPortal = new Transition(subTreeReader);
                     subTreeReader.Close();
                     reader.Skip();
                     break;
                  case "arc":
                     subTreeReader = reader.ReadSubtree();
                     ElementPortal = new Arc(subTreeReader, this);
                     subTreeReader.Close();
                     reader.Skip();
                     break;
                  case "additionalCode":
                     if(!reader.IsEmptyElement)
                     {
                        reader.ReadStartElement("additionalCode");
                        /*Причину Replace см. Issue 27*/
                        this.AdditionalCode = reader.ReadString().Replace("\n", System.Environment.NewLine);
                        reader.ReadEndElement(); // additionalCode
                     }
                     else
                     {
                        reader.Skip();
                     }
                     break;
                  default:
                     reader.Read();
                     break;
               }
            }
            reader.ReadEndElement();
            reader.ReadEndElement();
         }
         else
         {
            reader.Skip();
            reader.ReadEndElement();
         }
         NetSaved = true;
      }

      public XmlSchema GetSchema()
      {
         return(null);
      }
      
      private void ChangeController(object sender, System.EventArgs args)
      {
         NetSaved = false;
      }
   }
}