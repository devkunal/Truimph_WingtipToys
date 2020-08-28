using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppV1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Add some methods to perform any operations on Page load..
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GridView1.SelectedRow;
            var OrderId = gr.Cells[1].Text; // Get the selected OrderId

            string _query2 = "SELECT O.OrderId, O.Username, P.ProductName, P.Description, O.Quantity, P.UnitPrice FROM OrderDetails O left join Products P on O.ProductId = P.ProductId WHERE O.OrderId = " + OrderId + "";
            //connect SQL
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WINGTIPTOYSConnectionString"].ToString());
            //use Dataset in combination with DataAdapter Class
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(_query2, con);
            adapter.TableMappings.Add("Table", "OrderDetails");
            adapter.TableMappings.Add("Table1", "Products");
            adapter.Fill(ds);
            GridView2.DataSource = ds;
            GridView2.DataBind();
            
            //For better representation We want to hide a few fields before any row is selected form GridView
            Label15.Visible = true;
            TextBox15.Visible = true;
            Button1.Visible = true;
        }

        protected void SearchData(object sender, EventArgs e)
        {
            GridViewRow gr = GridView1.SelectedRow;
            var OrderId = gr.Cells[1].Text;

            // Retrieve the inserted value from searchbox
            string val = TextBox15.Text.ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WINGTIPTOYSConnectionString"].ToString());
            
            //Join Order and Product table and extract the matching records form the DataBase
            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT O.OrderId, O.Username, P.ProductName, P.Description, O.Quantity, P.UnitPrice FROM OrderDetails O left join Products P on O.ProductId = P.ProductId WHERE O.OrderId = " + OrderId + " AND (P.ProductName like '%" + val + "%' OR P.Description like '%" + val + "%')", con);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "orders");
            GridView2.DataSource = ds;
            GridView2.DataBind();
            //Empty SearchBox after every request
            TextBox15.Text = "";
        }

    }
}