using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Windows.Forms;

using PPPV.Net;
using PPPV.Editor.Tools;

namespace PPPV.Editor
{
	[Serializable()]
	[XmlRoot("pnml")]
	public class PetriNetWrapper : PetriNet, IXmlSerializable
	{
		//Данные
		protected SelectedNetObjectsList selectedObjects;
		public   Tool currentTool,
					pointerTool,
					placeTool,
					transitionTool,
					arcTool,
					inhibitorArcTool,
					annotationTool;

		/*Путь к файлу в который сохранена сеть*/
		private string linkedFile;
		/*Флаг, было ли сохранено текущее состояние сети*/
		private bool saved;

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
			Saved = false;
			LinkedFile = "";
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

		public bool Saved{
			get{
				return saved;
			}
			private set{
				saved = value;
			}
		}

		public string LinkedFile{
			get{
				return linkedFile;
			}
			set{
				linkedFile = value;
			}
		}

		/*Событие генерируется при сохранении сети в файл*/
		public event SaveEventHandler Save;

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

		public bool SaveNet()
		{
			bool result = false;
			StreamWriter stream;
			if(LinkedFile != "")
			{
				if (File.Exists(LinkedFile))
				{
					File.Delete(LinkedFile);
				}
				stream = new StreamWriter(LinkedFile, false, Encoding.GetEncoding(1251));
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
					LinkedFile = fileName = saveFileDialog1.FileName;
					if(this.ID=="")
						this.ID = fileName.Substring(fileName.LastIndexOf("\\")+1);

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
			Saved = true;
			LinkedFile = args.fileName;
			if (Save != null)
			{
				Save(this, args);
			}
		}

		public void WriteXml (XmlWriter writer)
		{
			//writer.WriteStartElement("pnml");
			writer.WriteStartElement("net");
			writer.WriteAttributeString("id", ID);
			writer.WriteAttributeString("type", Type);
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
			this.ID = reader.GetAttribute("id");
			this.Type = reader.GetAttribute("type");

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
			Saved = true;
		}

		public XmlSchema GetSchema()
		{
			return(null);
		}
	
		private void ChangeController(object sender, System.EventArgs args)
		{
			Saved = false;
		}
	}
}