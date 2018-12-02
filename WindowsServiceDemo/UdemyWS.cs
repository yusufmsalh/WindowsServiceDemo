using System;
using System.Diagnostics;
using System.ServiceProcess;
using log4net;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;

namespace WindowsServiceDemo
{
    public partial class UdemyWS : ServiceBase
    {
        ILog mLogger;
        private Timer mRepeatingTimer;
        private double mCounter;
        private FileSystemWatcher fileSystemWatcher;
        public UdemyWS()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                EventLog.WriteEntry("Hello World,udemy Windows Service Started", EventLogEntryType.Information);
                System.Diagnostics.Debugger.Launch();//Lanuching Debugger,make sure to stop service before this line,no need 
                                                     //to install it again,already installed
                ConfigureLog4Net();
                 mLogger.Debug(string.Format("Hello World from Log4net at {0}", DateTime.Now.ToLongDateString()));

                for (int i = 0; i < 5; i++)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Value of {0} is at {1} ", i, DateTime.Now.ToShortDateString()));
                }
                //mRepeatingTimer = new Timer(myTimerCallback, mRepeatingTimer, 1000, 1000);
                fileSystemWatcher = new FileSystemWatcher(@"C: \Users\Yusuf M.Salh\Documents\visual studio 2015\Projects\WindowsServiceDemo\WindowsServiceDemo\bin\Debug\WatchMe");
                fileSystemWatcher.EnableRaisingEvents = true;
                fileSystemWatcher.IncludeSubdirectories = true;
                //when ever a file is x,x: created, call method written below
                fileSystemWatcher.Created += FileSystemWatcherSomethingChangedInFolder;
                fileSystemWatcher.Changed += FileSystemWatcherSomethingChangedInFolder;
                fileSystemWatcher.Deleted += FileSystemWatcherSomethingChangedInFolder;
                fileSystemWatcher.Renamed += FileSystemWatcherSomethingChangedInFolder;

            }
            catch (Exception)
            {

                //logging
            }

        }

        private void FileSystemWatcherSomethingChangedInFolder(object sender, FileSystemEventArgs e)
        {
mLogger.Debug(string.Format("\r\n {0},\r\n {1}",e.ChangeType , e.FullPath));


        }

        protected override void OnStop()
        {
            System.Diagnostics.Debugger.Launch();//Lanuching Debugger,make sure to stop service before this line,no need 

            ConfigureLog4Net();
            mLogger.Debug(string.Format("Hello World from Log4net at {0}", DateTime.Now.ToLongDateString()));
            mLogger.Error("Nice Error");



        }
        private void ConfigureLog4Net()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                mLogger = LogManager.GetLogger("servicelog");
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }
        public void myTimerCallback(object objParam)
        {
            mLogger.Debug(string.Format("Value of counter is: {0}", mCounter++));
            System.Diagnostics.Debug.WriteLine(string.Format("Value of counter is: {0}", mCounter));
        }
    }
}
