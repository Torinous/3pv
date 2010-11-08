/*
 * Created by SharpDevelop.
 * User: Torinous
 * Date: 22.10.2010
 * Time: 16:34
 *
 *
 */

namespace Pppv.Verificator.Commands
{
	using System;
	using System.Drawing;
	
	using System.Reflection;
	using System.Windows.Forms;
	
	using Pppv.ApplicationFramework.Commands;

	public class SaveStateSpaceImageCommand : Command
	{
		private static string id = "Граф достижимости";
		
		public SaveStateSpaceImageCommand(EventHandler<EventArgs> handlerExecute, EventHandler<EventArgs> handlerUpdate) : base(id, handlerExecute, handlerUpdate)
		{
			this.Description = "Сохранить изображение графа достижимости";
			this.Pictogram = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Pppv.Resources.Export.png"), true);
		}
		
		public static string Id
		{
			get { return id; }
		}
	}
}
