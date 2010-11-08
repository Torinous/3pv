namespace Pppv.Editor.Commands
{
	using System;
	using System.Drawing;
	using System.Reflection;
	using System.Windows.Forms;

	using Pppv.Commands;
	using Pppv.Editor.Shapes;
	using Pppv.Net;

	public class EditShapeCommand : Command
	{
		private static string id = "Редактировать";
		
		public EditShapeCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Редактировать элемент сети";
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Edit.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}