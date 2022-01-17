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
using System.Threading;

namespace UdemyWindowsService
{
    public partial class UdemyWindowsService : ServiceBase
    {


        ILog mLogger;

        Timer mRepeatingTimer;
        double mCounter;
        public UdemyWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Udemy Windows Service by Yaksh is Starting", EventLogEntryType.Information);
            ConfigureLog4Net();

            int i = 0;
            //System.Diagnostics.Debugger.Launch();
            do
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Value of i is: {0}", i));
                mLogger.Debug(string.Format("value of i is: {0}", i));
            }
            while (i++ < 5);

            mLogger.Error("this is an error.");

            mRepeatingTimer = new Timer(myTimerCallback, mRepeatingTimer, 1000, 1000);
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Udemy Windows Service by Yaksh is Stopping", EventLogEntryType.Information);

        }

        private void ConfigureLog4Net()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                mLogger = LogManager.GetLogger("servicelog");
            }
            catch(Exception ex)
            {
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }


        public void myTimerCallback(object objParam)
        {
            mLogger.Debug(string.Format("value of counter is: {0}", mCounter++));
            System.Diagnostics.Debug.WriteLine(string.Format("Value pf counter is : {0}", mCounter));
        }
    }
}
