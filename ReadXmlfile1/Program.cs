using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReadXmlfile1
{
        public class Program
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string cs = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        DataSet ds = new DataSet();
        //        ds.ReadXml(Server.MapPath("~/Data.xml"));
        //    }

        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                        new Service1()
            };
            ServiceBase.Run(ServicesToRun);

            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(@"C:\Users\Yaksh Tyagi\source\repos\ReadXml\Books.xml");

                XmlNodeList nodes = xdoc.SelectNodes("//Data/Book");

                foreach (XmlNode node in nodes)
                {
                    XmlNode author = node.SelectSingleNode("Author");
                    if (author != null)
                    {
                        Console.WriteLine(author.InnerText);
                    }

                    XmlNode title = node.SelectSingleNode("Title");
                    if (title != null)
                    {
                        Console.WriteLine(title.InnerText);
                    }
                }


            }
        }
    }
}
