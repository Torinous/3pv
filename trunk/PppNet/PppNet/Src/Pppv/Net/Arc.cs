namespace Pppv.Net
{
	using System;
	using System.Collections;
	using System.Collections.ObjectModel;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Globalization;
	using System.Windows.Forms;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	[Serializable()]
	[XmlRoot("arc")]
	public class Arc : NetElement, IArc
	{
		private string sourceId, targetId;
		private ArcType arcType;
		private PredicatesList cortege;
		private List<Point> points;

		public Arc(ArcType type) : base(new Point(0, 0))
		{
			this.ArcType = type;
			this.points = new List<Point>(20);
			this.cortege = new PredicatesList();
		}

		public Arc() : this(ArcType.NormalArc)
		{
		}

		public Arc(string startElementId, ArcType type) : this(type)
		{
			this.SourceId = startElementId;
		}

		public Arc(INetElement startElement, ArcType type) : this(startElement.Id, type)
		{
		}

		public ArcType ArcType
		{
			get { return this.arcType; }
			set { this.arcType = value; }
		}

		public List<Point> Points
		{
			get { return this.points; }
		}

		public PredicatesList Cortege
		{
			get { return this.cortege; }
			protected set { this.cortege = value; }
		}

		public string TargetId
		{
			get
			{
				return this.targetId;
			}

			set
			{
				this.targetId = value;
				this.Id = this.MakeId();
			}
		}

		public string SourceId
		{
			get
			{
				return this.sourceId;
			}

			set
			{
				this.sourceId = value;
				this.Id = this.MakeId();
			}
		}

		public bool Unfinished
		{
			get { return String.IsNullOrEmpty(this.TargetId); }
		}

		public string ArcTypeName
		{
			get
			{
				if (this.ArcType == ArcType.NormalArc)
				{
					return "normal";
				}
				else
				{
					return "inhibitor";
				}
			}
		}

		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("id", this.Id);
			writer.WriteAttributeString("source", this.SourceId);
			writer.WriteAttributeString("target", this.TargetId);
			writer.WriteStartElement("graphics");
			foreach (Point point in points)
			{
				writer.WriteStartElement("position");
				writer.WriteAttributeString("x", point.X.ToString(CultureInfo.CurrentCulture) + ".0");
				writer.WriteAttributeString("y", point.Y.ToString(CultureInfo.CurrentCulture) + ".0");
				writer.WriteEndElement(); // position
			}
			writer.WriteEndElement(); // graphics
			XmlSerializer cortegeSerealizer = new XmlSerializer(typeof(PredicatesList));
			cortegeSerealizer.Serialize(writer, this.Cortege);

			writer.WriteStartElement("type");
			writer.WriteAttributeString("value", this.ArcTypeName);
			writer.WriteEndElement(); // type
		}

		public override void ReadXml(XmlReader reader)
		{
			XmlReader subTreeReader;
			reader.MoveToAttribute("id");
			reader.MoveToAttribute("source");
			this.SourceId = reader.Value;
			reader.MoveToAttribute("target");
			this.TargetId = reader.Value;
			reader.ReadStartElement("arc");
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				switch (reader.Name)
				{
					case "cortege":
						subTreeReader = reader.ReadSubtree();
						XmlSerializer serealizer = new XmlSerializer(typeof(PredicatesList));
						this.Cortege = new PredicatesList();
						this.Cortege = serealizer.Deserialize(subTreeReader) as PredicatesList;
						subTreeReader.Close();
						reader.Skip();
						break;
					case "graphics":
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement("graphics");
							while ( reader.Name == "position")
							{
								int x,y;
								reader.MoveToAttribute("x");
								x = (int)reader.ReadContentAsDouble();
								reader.MoveToAttribute("y");
								y = (int)reader.ReadContentAsDouble();
								this.points.Add(new Point(x, y));
								reader.MoveToElement();
								reader.Skip();
							}
							reader.ReadEndElement();
						}
						else
						{
							reader.Skip();
						}
						break;
					case "type":
						reader.MoveToAttribute("value");
						if (reader.Value == "normal")
						{
							this.ArcType = ArcType.NormalArc;
						}
						else
						{
							this.ArcType = ArcType.InhibitorArc;
						}

						reader.MoveToElement();
						reader.Skip();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		public override XmlSchema GetSchema()
		{
			return null;
		}

		public override void SetId(int number)
		{
			if (String.IsNullOrEmpty(this.Id))
			{
				this.Id = this.MakeId();
			}
		}

		public void AddPoint(Point p)
		{
			points.Add(p);
		}

		public void DeletePoint(int index)
		{
			points.RemoveAt(index);
		}

		private string MakeId()
		{
			return this.SourceId + " to " + this.TargetId;
		}
	}
}
