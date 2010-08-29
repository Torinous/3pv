namespace Pppv.Editor
{
   using System;
   using System.IO;
   using System.Text;
   using System.Windows.Forms;
   using System.Xml;
   using System.Xml.Schema;
   using System.Xml.Serialization;

   using Pppv.Editor.Tools;
   using Pppv.Net;

   [Serializable()]
   [XmlRoot("pnml")]
   public class PetriNetWrapper : PetriNet, IXmlSerializable
   {
      [NonSerializedAttribute]
      private NetElementCollection selectedObjects;
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

      public PetriNetWrapper() : base()
      {
         this.selectedObjects  = new NetElementCollection();
         this.pointerTool      = new PointerTool();
         this.placeTool        = new PlaceTool();
         this.transitionTool   = new TransitionTool();
         this.arcTool          = new ArcTool();
         this.inhibitorArcTool = new InhibitorArcTool();
         this.annotationTool   = new AnnotationTool();
         this.currentTool      = this.pointerTool;
         this.NetSaved = false;
         this.FileOfNetPath = String.Empty;
         this.Change += this.ChangeController;
      }

      public event EventHandler<SaveNetEventArgs> Save;

      public Tool CurrentTool
      {
         get
         { 
            return this.currentTool;
         }

         set
         {
            if (this.currentTool != null)
            {
               this.CurrentTool.EventSourceNet = null;
            }

            this.currentTool = value;
            if (this.currentTool != null)
            {
               this.CurrentTool.EventSourceNet = this;
            }
         }
      }

      public NetElementCollection SelectedObjects
      {
         get { return this.selectedObjects; }
      }

      public bool NetSaved
      {
         get { return this.netSaved; }
         private set { this.netSaved = value; }
      }

      public string FileOfNetPath
      {
         get { return this.fileOfNetPath; }
         set { this.fileOfNetPath = value; }
      }

      public void SelectToolByType(Type toolType)
      {
         if (toolType == typeof(PointerTool))
         {
            this.currentTool = this.pointerTool;
         }
         else if (toolType == typeof(PlaceTool))
         {
            this.currentTool = this.placeTool;
         }
         else if (toolType == typeof(TransitionTool))
         {
            this.currentTool = this.transitionTool;
         }
         else if (toolType == typeof(ArcTool))
         {
            this.currentTool = this.arcTool;
         }
         else if (toolType == typeof(InhibitorArcTool))
         {
            this.currentTool = this.inhibitorArcTool;
         }
         else if (toolType == typeof(AnnotationTool))
         {
            this.currentTool = this.annotationTool;
         }
         else
         {
            throw new EditorException("Not appropriate tool type!");
         }
      }

      public bool SaveNet()
      {
         bool result = false;
         StreamWriter stream;
         if (!String.IsNullOrEmpty(this.FileOfNetPath))
         {
            if (File.Exists(this.FileOfNetPath))
            {
               File.Delete(this.FileOfNetPath);
            }

            stream = new StreamWriter(this.FileOfNetPath, false, Encoding.GetEncoding(1251));
            if (stream != null)
            {
               XmlSerializer serealizer = new XmlSerializer(this.GetType());
               serealizer.Serialize(stream, this);
               stream.Close();
               this.OnSave(new SaveNetEventArgs(this));
               result = true;
            }
         }
         else
         {
            result = this.SaveNetAs();
         }

         return result;
      }

      public bool SaveNetAs()
      {
         bool result = false;
         StreamWriter stream;
         string fileName = String.Empty;
         SaveFileDialog saveFileDialog1 = new SaveFileDialog();
         saveFileDialog1.Filter = "pnml files (*.pnml)|*.pnml|All files (*.*)|*.*";
         saveFileDialog1.FilterIndex = 1;
         saveFileDialog1.RestoreDirectory = true;

         if (saveFileDialog1.ShowDialog() == DialogResult.OK)
         {
            stream = new StreamWriter(saveFileDialog1.FileName, false, Encoding.GetEncoding(1251));
            if (stream != null)
            {
               this.FileOfNetPath = fileName = saveFileDialog1.FileName;
               if (String.IsNullOrEmpty(this.Id))
               {
                  this.Id = fileName.Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1);
               }

               XmlSerializer serializer = new XmlSerializer(this.GetType());
               serializer.Serialize(stream, this);
               stream.Close();
               this.OnSave(new SaveNetEventArgs(this));
               result = true;
            }
         }

         return result;
      }

      public void WriteXml(XmlWriter writer)
      {
         writer.WriteStartElement("net");
         writer.WriteAttributeString("id", Id);
         writer.WriteAttributeString("type", NetType);
         foreach (Place place in this.Places)
         {
            writer.WriteStartElement("place");
            place.WriteXml(writer);
            writer.WriteEndElement(); // place
         }

         foreach (Transition transition in this.Transitions)
         {
            writer.WriteStartElement("transition");
            transition.WriteXml(writer);
            writer.WriteEndElement(); // transition
         }

         foreach (Arc arc in this.Arcs)
         {
            writer.WriteStartElement("arc");
            arc.WriteXml(writer);
            writer.WriteEndElement(); // arc
         }

         writer.WriteStartElement("additionalCode");
         writer.WriteString(this.AdditionalCode);
         writer.WriteEndElement(); // additionalCode
         writer.WriteEndElement(); // net
      }

      public void ReadXml(XmlReader reader)
      {
         XmlReader subTreeReader;
         reader.ReadStartElement("pnml");
         this.Id = reader.GetAttribute("id");
         this.NetType = reader.GetAttribute("type");

         if (!reader.IsEmptyElement)
         {
            reader.ReadStartElement("net");
            while (reader.NodeType != XmlNodeType.EndElement)
            {
               switch (reader.Name)
               {
                  case "place":
                     subTreeReader = reader.ReadSubtree();
                     this.AddElement(new Place(subTreeReader));
                     subTreeReader.Close();
                     reader.Skip();
                     break;
                  case "transition":
                     subTreeReader = reader.ReadSubtree();
                     this.AddElement(new Transition(subTreeReader));
                     subTreeReader.Close();
                     reader.Skip();
                     break;
                  case "arc":
                     subTreeReader = reader.ReadSubtree();
                     this.AddElement(new Arc(subTreeReader, this));
                     subTreeReader.Close();
                     reader.Skip();
                     break;
                  case "additionalCode":
                     if (!reader.IsEmptyElement)
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

         this.NetSaved = true;
      }

      public XmlSchema GetSchema()
      {
         return null;
      }
      
      private void ChangeController(object sender, System.EventArgs args)
      {
         this.NetSaved = false;
      }

      private void OnSave(SaveNetEventArgs args)
      {
         this.NetSaved = true;
         if (this.Save != null)
         {
            this.Save(this, args);
         }
      }
   }
}