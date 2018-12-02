using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace WindowsServiceDemo
{
    public partial class UdemyWS : ServiceBase
    {
        ILog mLogger;
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
            }
            catch (Exception)
            {

                //logging
            }

        }

        protected override void OnStop()
        {
            System.Diagnostics.Debugger.Launch();//Lanuching Debugger,make sure to stop service before this line,no need 

            ConfigureLog4Net();
            mLogger.Debug(string.Format("Hello World from Log4net at {0}", DateTime.Now.ToLongDateString()));
            //   another example
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

    }
}
