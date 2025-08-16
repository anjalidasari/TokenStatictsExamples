using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace TokenStatiticsExample
{
    public partial class Details : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("server=.;database=TokenExample;uid=sa;pwd=abc;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string symbol = Request.QueryString["symbol"];
                if (!string.IsNullOrEmpty(symbol))
                {
                    BindDetails(symbol);
                }
            }
        }

        private void BindDetails(string symbol)
        {
            using (SqlCommand cmd = new SqlCommand(
                "SELECT Symbol, Name, Total_Supply, Contract_Address, Total_holders, Price " +
                "FROM Token WHERE Symbol = @Symbol", con))
            {
                cmd.Parameters.AddWithValue("@Symbol", symbol);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        lblSymbol.Text = dr["Symbol"].ToString();
                        lblName.Text = dr["Name"].ToString();
                        lblTotalSupply.Text = dr["Total_Supply"].ToString();
                        lblContractAddress.Text = dr["Contract_Address"].ToString();
                        lblTotalHolders.Text = dr["Total_holders"].ToString();
                        lblPrice.Text = dr["Price"].ToString();
                    }
                }
            }
        }
    }
}
