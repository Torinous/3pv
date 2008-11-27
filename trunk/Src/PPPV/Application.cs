using System;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;

using PPPv.Editor;
using PPPv.Utils;

namespace PPPv {
   public class PPPVApplication {
      private ArrayList _netList;
      private Editor.MainForm _mainFormInst;
      /*Обработчики событий меню*/
      private EventHandler _NewNetHandler;
      private EventHandler _CloseAppHandler;
      /**/
      private NetToolStrip _netToolStrip;
      private MainMenuStrip _mainMenuStrip;

      public EventHandler NewNetHandler{
        get{
          return _NewNetHandler;
        }
        private set{
          _NewNetHandler = new EventHandler(value);
        }
      }

      public EventHandler CloseAppHandler{
        get{
          return _CloseAppHandler;
        }
        private set{
          _CloseAppHandler = new EventHandler(value);
        }
      }

      public PPPVApplication() {
        /*Инициализация таблицы обработчиков*/
        NewNetHandler   = _NewNet;
        CloseAppHandler = _CloseApplication;

        InitializeComponent();
        /*получим ссылки на важные элементы*/
        _netToolStrip = _mainFormInst.toolStrip;
        _mainMenuStrip = _mainFormInst.menuStrip;

        /*Привязка обработчиков*/
        _mainMenuStrip.toolStripMenuNew.Click += NewNetHandler;
        _mainMenuStrip.toolStripMenuExit.Click += CloseAppHandler;

      }
      private void InitializeComponent() {
        NetList       = new ArrayList(10);
        _mainFormInst = new Editor.MainForm();
      }

      private void _NewNet(object sender, EventArgs e){
        Net.PetriNet _net = new Net.PetriNet();
        NetList.Add(_net);
        _mainFormInst.TabControl.AddNewTab(_net);
      }

      private void _CloseApplication(object sender, EventArgs e){
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
        FileStream LogFile = new FileStream(Application.StartupPath + "\\log", FileMode.Create);
        TextWriterTraceListener MyListener = new TextWriterTraceListener(LogFile);
        Trace.Listeners.Add(MyListener);
        Trace.AutoFlush = true;
      }

      public void Run() {
        PrepareLog();
        System.Windows.Forms.Application.Run(_mainFormInst);
        PreciseTimer.ShowTimeTable();
      }
   }
}
