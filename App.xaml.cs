using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Reflection;
using System.Threading;

namespace wpfLog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        static Logger mLogger;
        static FileLogger mFileLogger;

        public string dbFileName = "TDTDb.sqlite";


        public string logDirectory = "\\Temp\\Logs\\";

        //public UserControl priorView = null;
        //public TDTDbContext db;

        public App()
        {
            InitializeComponent();
            // Check if already running ========================>
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            bool sit = !RunSingleInstanceCheck(appName);
            if (sit)
            {
                //  
                //mLogger.AddLogMessage("Some one else already running.");

                //Already running.");
                // this.MainWindow.Show();
                Environment.Exit(0);
                // Add code to bring focus to existing instance
            }
            //  Preliminary check handled.
            mLogger = Logger.Instance;
            string logName0 = System.IO.Path.Combine(logDirectory + appName, appName + "_" + System.DateTime.Now.ToString("yyyyMMdd_HH_mm_ss") + ".log");

            mFileLogger = new FileLogger(logName0);
            mFileLogger.Init();
            mLogger.RegisterObserver(mFileLogger);

            mLogger.AddLogMessage("sit was->" + sit.ToString() );
            mLogger.AddLogMessage("+++");
            mLogger.AddLogMessage("Started App!  logName0->" + logName0);
            mLogger.AddLogMessage("+++");

            // AppSpecificSetUp();
        }

        private bool RunSingleInstanceCheck(string appName)
        {
            //return true ==> no previous version, we are the only instance.
            //throw new NotImplementedException();

            // SingleInstanceCheck ---------------------------------------------
            //var appName = Assembly.GetEntryAssembly().GetName().Name;

            bool notAlreadyRunning = true;
            Mutex theMutex = new Mutex(true, appName + "Singleton", out notAlreadyRunning);
            //   Should have initial ownership, mutex name,  was caller granted ownership.
            string statusMsg = "";
            if (notAlreadyRunning)
            {
            //    mLogger.AddLogMessage("Already not running");
                // got control of Mutex => nothing running, we are first
                return notAlreadyRunning;  //Already running.");

                //       System.AppDomain currentDomain = System.AppDomain.CurrentDomain;
                //       currentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(currentDomain_UnhandledException);
                // http://stackoverflow.com/questions/4625825/catching-exceptions-in-wpf-at-the-framework-level

                //this.Properties["dbFile"] = dbFileName;  // need access in views for dbContext
                //this.Properties["dbFile2"] = dbFileName;  // need access in views for dbContext

                //GetLogDirectory(logDirectory);

                //AppSpecificSetUp();

                //     GetData();  // get data all in memory at beginning.
            }
            else
            {
               // mLogger.AddLogMessage("Not single instance, already running!!!");
                return notAlreadyRunning;  // NOT running.");
               // Environment.Exit(0);
            }

        }

        protected override void OnExit(ExitEventArgs e)
        {
            int n = e.ApplicationExitCode;
            if (n > 1)
                mLogger.AddLogMessage("EXIT CODE was " + n);
            mLogger.AddLogMessage("<==== Exiting OnExit-App ===>");
            mFileLogger.Terminate();
            base.OnExit(e);
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            mLogger.AddLogMessage("<==== Exiting OnSessionEnding-App ===>");
            mFileLogger.Terminate();
            base.OnSessionEnding(e);
        }




        #region Logging
        private void SetUpLogging(string logDirectory)
        {
            mLogger = Logger.Instance;
            string logName0 = System.IO.Path.Combine(logDirectory, "logTDTnew_" + System.DateTime.Now.ToString("yyyyMMdd_HH_mm_ss") + ".log");

            mFileLogger = new FileLogger(logName0);
            mFileLogger.Init();
            mLogger.RegisterObserver(mFileLogger);
            mLogger.AddLogMessage("logName0->" + logName0);
        }
        #endregion Logging
    }
}
