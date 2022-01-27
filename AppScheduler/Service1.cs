using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace AppScheduler
{


        public class AppScheduler : System.ServiceProcess.ServiceBase

        {

            string configPath;

            System.Timers.Timer _timer = new System.Timers.Timer();

            DataSet ds = new DataSet();

            /// <summary>

            /// Required designer variable.

            /// </summary>

            private System.ComponentModel.Container components = null;



            /// <summary>

            /// Class that launches applications on demand.

            /// </summary>

            class AppLauncher

            {

                string app2Launch;

                public AppLauncher(string path)

                {

                    app2Launch = path;

                }

                public void runApp()

                {

                    ProcessStartInfo pInfo = new ProcessStartInfo(app2Launch);

                    pInfo.WindowStyle = ProcessWindowStyle.Normal;

                    Process p = Process.Start(pInfo);

                }

            }



            void timeElapsed(object sender, ElapsedEventArgs args)

            {

                DateTime currTime = DateTime.Now;

                foreach (DataRow dRow in ds.Tables["task"].Rows)

                {

                    DateTime runTime = Convert.ToDateTime(dRow["time"]);

                    string formatString = "MM/dd/yyyy HH:mm";

                    if (runTime.ToString(formatString) == currTime.ToString(formatString))

                    {

                        string exePath = dRow["exePath"].ToString();

                        AppLauncher launcher = new AppLauncher(exePath);

                        new Thread(new ThreadStart(launcher.runApp)).Start();

                        // Update the next run time

                        string strInterval = dRow["repeat"].ToString().ToUpper();

                        switch (strInterval)

                        {

                            case "D":

                                runTime = runTime.AddDays(1);

                                break;

                            case "W":

                                runTime = runTime.AddDays(7);

                                break;

                            case "M":

                                runTime = runTime.AddMonths(1);

                                break;

                        }

                        dRow["time"] = runTime.ToString(formatString);

                        ds.AcceptChanges();

                        StreamWriter sWrite = new StreamWriter(configPath);

                        XmlTextWriter xWrite = new XmlTextWriter(sWrite);

                        ds.WriteXml(xWrite, XmlWriteMode.WriteSchema);

                        xWrite.Close();

                    }

                }

            }



            public AppScheduler()

            {

                // This call is required by the Windows.Forms Component Designer.

                InitializeComponent();



                // TODO: Add any initialization after the InitComponent call

            }



            // The main entry point for the process

            static void Main()

            {

                System.ServiceProcess.ServiceBase[] ServicesToRun;



                // More than one user Service may run within the same process. To add

                // another service to this process, change the following line to

                // create a second service object. For example,

                //

                //   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};

                //

                ServicesToRun = new System.ServiceProcess.ServiceBase[] { new AppScheduler() };



                System.ServiceProcess.ServiceBase.Run(ServicesToRun);

            }



            /// <summary>

            /// Required method for Designer support - do not modify

            /// the contents of this method with the code editor.

            /// </summary>

            private void InitializeComponent()

            {

                //

                // AppScheduler

                //

                this.CanPauseAndContinue = true;

                this.ServiceName = "Application Scheduler";



            }



            /// <summary>

            /// Clean up any resources being used.

            /// </summary>

            protected override void Dispose(bool disposing)

            {

                if (disposing)

                {

                    if (components != null)

                    {

                        components.Dispose();

                    }

                }

                base.Dispose(disposing);

            }



            /// <summary>

            /// Set things in motion so your service can do its work.

            /// </summary>

            protected override void OnStart(string[] args)

            {

                // TODO: Add code here to start your service.

                configPath = ConfigurationSettings.AppSettings["configpath"];

                try

                {

                    XmlTextReader xRead = new XmlTextReader(configPath);

                    XmlValidatingReader xvRead = new XmlValidatingReader(xRead);

                    xvRead.ValidationType = ValidationType.DTD;

                    ds.ReadXml(xvRead);

                    xvRead.Close();

                    xRead.Close();

                }

                catch (Exception)

                {

                    ServiceController srvcController = new ServiceController(ServiceName);

                    srvcController.Stop();

                }

                _timer.Interval = 30000;

                _timer.Elapsed += new ElapsedEventHandler(timeElapsed);

                _timer.Start();

            }



            /// <summary>

            /// Stop this service.

            /// </summary>

            protected override void OnStop()

            {

                // TODO: Add code here to perform any tear-down necessary to stop your service.

            }

        }


    }

