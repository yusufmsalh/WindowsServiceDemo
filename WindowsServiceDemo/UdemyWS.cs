using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceDemo
{
    public partial class UdemyWS : ServiceBase
    {
        public UdemyWS()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Hello World,udemy Windows Service Started",EventLogEntryType.Information);
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Hello World,udemy Windows Service  Stopped", EventLogEntryType.Information);

        }
    }
}
