using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;

namespace TokenStatiticsExample
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("server=.;database=TokenExample;uid=sa;pwd=abc;");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
                BindChart();
            }
        }
        internal void Bind()
        {

            SqlDataAdapter adapt = new SqlDataAdapter("SELECT Id,Name, Symbol, Contract_Address, Total_Supply, Total_Holders FROM Token", con);
            DataTable dt = new DataTable();
            con.Open();
            adapt.Fill(dt);
            decimal totalSupplySum = Convert.ToDecimal(dt.Compute("SUM(Total_Supply)", ""));

            dt.Columns.Add("Total_Supply_Percent", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                decimal supply = Convert.ToDecimal(row["Total_Supply"]);
                decimal percent = (supply / totalSupplySum) * 100;
                row["Total_Supply_Percent"] = percent.ToString("0.00") + "%";
            }
            con.Close();
            TokenGridDetails.DataSource = dt;
            TokenGridDetails.DataBind();


        }
        private void BindChart()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT Name, Total_Supply FROM Token", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Chart1.DataSource = dt;
            Chart1.DataBind();
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Token (Symbol, Name, Total_Supply, Contract_Address, Total_holders) " + "VALUES (@Symbol, @Name, @Total_Supply, @Contract_Address, @Total_holders)", con);
            string symbol = txtSymbol.Text;
            string name = txtName.Text;
            string totalsupply = txtTotalSupply.Text;
            string contractaddress = txtContractAddress.Text;
            string totalholders = txtTotalHolders.Text;
            cmd.Parameters.AddWithValue("@Symbol", symbol);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Total_Supply", totalsupply);
            cmd.Parameters.AddWithValue("@Contract_Address", contractaddress);
            cmd.Parameters.AddWithValue("Total_holders", totalholders);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                Bind();
                BindChart();

            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSymbol.Text = "";
            txtName.Text = "";
            txtContractAddress.Text = "";
            txtTotalHolders.Text = "";
            txtTotalSupply.Text = "";
            BindChart();



        }





        protected void TokenGridDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            TokenGridDetails.EditIndex = e.NewEditIndex;
            BindChart();

        }

        protected void TokenGridDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            TokenGridDetails.EditIndex = -1;
            Bind();
            lblmsg.Text = "Record is cancelled ";
            BindChart();

        }



        protected void TokenGridDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = TokenGridDetails.Rows[e.RowIndex];
            Label lben = (Label)row.FindControl("lblRank");
            int rank = int.Parse(lben.Text);
            LinkButton txtsymbol = (LinkButton)row.FindControl("lnkSymbol");
            string symbol = txtsymbol.Text;
            TextBox txtname = (TextBox)row.FindControl("txtName");
            string name = txtname.Text;
            TextBox txtcontractaddredd = (TextBox)row.FindControl("txtContractAddress");
            string contractaddreess = txtcontractaddredd.Text;
            TextBox txttotalholders = (TextBox)row.FindControl("txtTotalHolders");
            string totalholders = txttotalholders.Text;
            TextBox txttotalsupply = (TextBox)row.FindControl("txtTotalSupply");
            int totalsupply = int.Parse(txttotalsupply.Text);

            TokenGridDetails.EditIndex = -1;
            SqlCommand cmd = new SqlCommand("UPDATE Token SET Symbol = @Symbol,Name = @Name,Total_Supply = @Total_Supply,Contract_Address = @Contract_Address,Total_holders = @Total_holders WHERE Id = @Id;", con);
            cmd.Parameters.AddWithValue("Id", rank);
            cmd.Parameters.AddWithValue("@Symbol", symbol);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Total_Supply", totalsupply);
            cmd.Parameters.AddWithValue("@Contract_Address", contractaddreess);
            cmd.Parameters.AddWithValue("@Total_holders", totalholders);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                lblmsg.Text = "Record is updated...";
            }
            else
            {
                lblmsg.Text = "Not updated...";
            }
            Bind();
            BindChart();

        }

        protected void txtSymbol_Click(object sender, EventArgs e)
        {
            Response.Redirect("Details.aspx");
        }


        protected void TokenGridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                string symbol = e.CommandArgument.ToString();
                Response.Redirect("Details.aspx?symbol=" + symbol);
            }
        }

        protected void TokenGridDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TokenGridDetails.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT Id, Symbol, Name, Total_Supply, Contract_Address, Total_holders, Price FROM Token", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Tokens.xlsx");
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            GridView gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gv.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }
}