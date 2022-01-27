using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReadXmlFile2
{
    public class Program : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/Data.xml"));
            }
        }
    }
}