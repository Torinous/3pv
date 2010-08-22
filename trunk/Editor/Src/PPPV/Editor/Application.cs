namespace PPPV.Editor
{
	using System;
	using System.Collections;
	using System.Diagnostics;
	using System.Threading;
	using System.Reflection;
	using System.Windows.Forms;
	using System.IO;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using System.Text;
	using System.Globalization;
	
	using PPPV.Utils;
	using PPPV.Net;
	using PPPV.Editor.Commands;
	using PPPV.Editor.Tools;

public class EditorApplication : ApplicationContext
  {
    //Данные
    private Editor.MainForm _mainFormInst;

    private static EditorApplication instance;

    /*Акцессоры доступа*/
    public static EditorApplication Instance
    {
      get
      {
         if (instance == null)
         {
            instance = new EditorApplication();
         }
         return instance;
      }
    }
   
    public MainForm MainFormInst
    {
      get
      {
        return _mainFormInst;
      }
    }
    

    public PetriNetWrapper ActiveNet
    {
      get
      {
        if(MainFormInst.TabControl.SelectedIndex != -1)
        {
          return (MainFormInst.TabControl.TabPages[MainFormInst.TabControl.SelectedIndex] as TabPageForNet).Net;
        }
        else
        {
          return null;
        }
      }
    }

    /*Конструкторы*/
    private EditorApplication()
    {
      PrepareLog();
      InitializeComponent();
      _mainFormInst.Show();
    }

    private void InitializeComponent()
    {
      _mainFormInst = new Editor.MainForm(this);
      _mainFormInst.Closed += MainFormCloseHandler;
    }

    public static void PrepareLog()
    {
      TextWriterTraceListener tr1 = new TextWriterTraceListener(System.Console.Out);
      FileStream LogFile = new FileStream(System.Windows.Forms.Application.StartupPath + "\\log", FileMode.Create);
      TextWriterTraceListener MyListener = new TextWriterTraceListener(LogFile);
      
      System.Diagnostics.Debug.Listeners.Add(tr1);
      System.Diagnostics.Debug.Listeners.Add(MyListener);
      System.Diagnostics.Debug.AutoFlush = true;
    }

    public void Run()
    {
      /*TODO: Тут бы надо завернуть в Try и написать хорошую форму для показа Exceptions*/
      System.Windows.Forms.Application.Run(_mainFormInst);
      PreciseTimer.ShowTimeTable();
    }

    private void MainFormCloseHandler(object sender, EventArgs e)
    {
      ExitThread();
    }
    
    public void Quit()
    {
      _mainFormInst.Close();
    }

    [STAThread]
    private static void Main(string[] args)
    {
      /*Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);*/
      /* try
       {*/
      EditorApplication context = EditorApplication.Instance;

      Application.ThreadException += new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException);
      Application.Run(context);
      /*}
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
    }*/
    }

    /// <summary>
    /// Handles any thread exceptions
    /// </summary>
    private class ThreadExceptionHandler
    {
      public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
      {
        MessageBox.Show(e.Exception.Message, "An exception occurred:", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    public static string AssemblyVersion
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }

    public void NewNet(StreamReader netStream, string fileName)
    {
      if(netStream != null)
      {
        PetriNetWrapper _net = new PetriNetWrapper();
        XmlSerializer serealizer = new XmlSerializer(_net.GetType());
        _net = (PetriNetWrapper)serealizer.Deserialize(netStream);
        _net.LinkedFile = fileName;
        MainFormInst.TabControl.AddNewTab(_net);
      }
      else
      {
        PetriNetWrapper _net = new PetriNetWrapper();
        MainFormInst.TabControl.AddNewTab(_net);
      }
    }
    
    public void CloseNet(PetriNet net)
    {
      if(this.MainFormInst.TabControl.SelectedIndex != -1)
      {
        this.MainFormInst.TabControl.CloseTab(MainFormInst.TabControl.TabID(net));
      }
    }
  } // class Application
} // namespace
