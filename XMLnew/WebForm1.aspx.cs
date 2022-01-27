using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace XMLnew
{
    public partial class WebForm1 : System.Web.UI.Page
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

                DataTable dtBoo = ds.Tables["Book"];
                con.Open();

                using (SqlBulkCopy bc = new SqlBulkCopy(con))
                {
                    bc.DestinationTableName = "Books";
                    bc.ColumnMappings.Add("ID", "ID");
                    bc.ColumnMappings.Add("Author", "Author");
                    bc.ColumnMappings.Add("Title", "Title");
                    bc.WriteToServer(dtBoo);

                }

            }
                 
        }
    }
}