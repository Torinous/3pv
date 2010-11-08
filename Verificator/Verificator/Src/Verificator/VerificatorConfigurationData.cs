/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 19.10.2010
 * Time: 6:07
 *
 *
 */

namespace Pppv.Verificator
{
	using System;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	
	using Pppv.ApplicationFramework.Graphviz;

	public class VerificatorConfigurationData : IXmlSerializable
	{
		private Plotter defaultPlotter;
		private int edgeLength;
		private NodeShape defaultNodeShape;
		private bool useMarkingInStateLabel;
		
		public VerificatorConfigurationData()
		{
			this.defaultPlotter = Plotter.Neato;
			this.EdgeLength = 3;
			this.DefaultNodeShape = NodeShape.Rectangle;
			this.UseMarkingInStateLabel = false;
		}

		public Plotter DefaultPlotter
		{
			get { return this.defaultPlotter; }
			set { this.defaultPlotter = value; }
		}

		public int EdgeLength
		{
			get { return this.edgeLength; }
			set { this.edgeLength = value; }
		}

		public NodeShape DefaultNodeShape
		{
			get { return this.defaultNodeShape; }
			set { this.defaultNodeShape = value; }
		}

		public bool UseMarkingInStateLabel
		{
			get { return this.useMarkingInStateLabel; }
			set { this.useMarkingInStateLabel = value; }
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Graphviz");
			writer.WriteStartElement("defaultPlotter");
			writer.WriteString(((int)this.DefaultPlotter).ToString());
			writer.WriteEndElement();
			writer.WriteStartElement("defaultNodeShape");
			writer.WriteString(((int)this.DefaultNodeShape).ToString());
			writer.WriteEndElement();
			writer.WriteStartElement("edgeLength");
			writer.WriteString(this.EdgeLength.ToString());
			writer.WriteEndElement();
			writer.WriteStartElement("useMarkingInStateName");
			writer.WriteString(this.UseMarkingInStateLabel.ToString());
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		public void ReadXml(XmlReader reader)
		{
			reader.ReadStartElement("VerificatorConfigurationData");
			reader.ReadStartElement("Graphviz");
			reader.ReadStartElement("defaultPlotter");
			this.DefaultPlotter = (Plotter)reader.ReadContentAsInt();
			reader.ReadEndElement();
			reader.ReadStartElement("defaultNodeShape");
			this.DefaultNodeShape = (NodeShape)reader.ReadContentAsInt();
			reader.ReadEndElement();
			reader.ReadStartElement("edgeLength");
			this.EdgeLength = reader.ReadContentAsInt();
			reader.ReadEndElement();
			reader.ReadStartElement("useMarkingInStateName");
			this.UseMarkingInStateLabel = bool.Parse(reader.ReadString());
			reader.ReadEndElement();
			reader.ReadEndElement();
			reader.ReadEndElement(); // VerificatorConfigurationData
		}

		public XmlSchema GetSchema()
		{
			return null;
		}
	}
}
