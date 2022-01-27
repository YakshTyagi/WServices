using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace AppScheduler
{
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

                    DS.WriteXml(xWrite, XmlWriteMode.WriteSchema);

                    xWrite.Close();

                }

            }

        }

    }
}
