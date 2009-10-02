using System;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using PPPV.Editor;
using PPPV.Utils;

namespace PPPV {
   public class Application {
      private ArrayList _netList;
      private Editor.MainForm _mainFormInst;
      /**/
      private NetToolStrip _netToolStrip;
      private MainMenuStrip _mainMenuStrip;

      public Application() {

         InitializeComponent();
         /*получим ссылки на важные элементы*/
         _netToolStrip = _mainFormInst.toolStrip;
         _mainMenuStrip = _mainFormInst.menuStrip;

         /*Привязка обработчиков*/
         _mainMenuStrip.toolStripMenuNew.Click += NewNet;
         _mainMenuStrip.toolStripMenuOpen.Click += OpenNet;
         _mainMenuStrip.toolStripMenuExit.Click += CloseApplication;
         _mainMenuStrip.toolStripMenuAbout.Click += ShowAboutForm;
      }

      private void ShowAboutForm(object sender, EventArgs e){
         (new AboutForm()).Show();
      }

      private void InitializeComponent() {
         NetList       = new ArrayList(10);
         _mainFormInst = new Editor.MainForm();
      }

      private void NewNet(object sender, EventArgs e){
         Net.PetriNet _net = new Net.PetriNet();
         NetList.Add(_net);
         _mainFormInst.TabControl.AddNewTab(_net);
      }

      private void OpenNet(object sender, EventArgs e){
         Stream stream;
         OpenFileDialog openFileDialog = new OpenFileDialog();

         openFileDialog.Filter = "txt files (*.pnml)|*.pnml|All files (*.*)|*.*";
         openFileDialog.FilterIndex = 1 ;
         openFileDialog.RestoreDirectory = true ;

         if(openFileDialog.ShowDialog() == DialogResult.OK){
            if((stream = openFileDialog.OpenFile()) != null){
               Net.PetriNet _net = new Net.PetriNet();
               XmlSerializer serealizer = new XmlSerializer(_net.GetType());
               _net = (Net.PetriNet)serealizer.Deserialize(stream);
               NetList.Add(_net);
               _net.LinkedFile = (stream as FileStream).Name;
               _mainFormInst.TabControl.AddNewTab(_net);
               stream.Close();
            }
         }
      }

      private void CloseApplication(object sender, EventArgs e){
         _mainFormInst.Close();
      }

      public ArrayList NetList{
         get{
            return _netList;
         }
         private set{
            _netList = value;
         }
      }

      public void PrepareLog(){
         //Log preparing
         FileStream LogFile = new FileStream(System.Windows.Forms.Application.StartupPath + "\\log", FileMode.Create);
         TextWriterTraceListener MyListener = new TextWriterTraceListener(LogFile);
         Trace.Listeners.Add(MyListener);
         Trace.AutoFlush = true;
      }

      public void Run() {
         PrepareLog();
         /*TODO: Тут бы надо завернуть в Try и написать хорошую форму для показа Exceptions*/
         System.Windows.Forms.Application.Run(_mainFormInst);
         PreciseTimer.ShowTimeTable();
      }
   } // class Application
} // namespace
