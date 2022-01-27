using System;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Protocols;

namespace ReadXml
{
    public partial class Program
    {

        static void Main(string[] args)
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


            //protected void Page_Load(object sender, EventArgs e)
            //{

            //}
            //protected void Button1_Click(object sender, EventArgs e)
            //{
            //    string cs = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(cs))
            //    {
            //        DataSet ds = new DataSet();
            //    }
            //}

        }
    }
}
