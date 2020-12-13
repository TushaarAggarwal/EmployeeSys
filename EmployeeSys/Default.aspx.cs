using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace EmployeeSys
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string query, constr;
        SqlConnection con;
        SqlCommand com;

        public void connection()
        {

            constr = ConfigurationManager.ConnectionStrings["ems"].ToString();
            con = new SqlConnection(constr);
            con.Open();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                gedata();

            
            }
            Label1.Visible = false;
        }

        protected void empsave(object sender, EventArgs e)
        {

            connection();
            HiddenField1.Value = "Insert";
            query = "EmpEntry";
            com = new SqlCommand(query, con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action",HiddenField1.Value).ToString();
            com.Parameters.AddWithValue("@FName", TextBox1.Text).ToString();
            com.Parameters.AddWithValue("@MName", TextBox2.Text).ToString();
            com.Parameters.AddWithValue("@LName", TextBox3.Text).ToString();
            int result=  com.ExecuteNonQuery();
            con.Close();
            if (result >= 1)
            {
                Label1.Text = "Records Are Added";
            
            }

        
        }
        public void gedata()
        {
            connection();
            HiddenField1.Value = "view";
            query = "EmpEntry";
            com = new SqlCommand(query, con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", HiddenField1.Value).ToString();
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            con.Close();
        
        }
        protected void edit(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex= e.NewEditIndex;
            gedata();


           
        }
        protected void canceledit(object sender, GridViewCancelEditEventArgs e)
        {

            GridView1.EditIndex = -1;
            gedata();
        }
        protected void update(object sender, GridViewUpdateEventArgs e)
        {
            connection();
            int id=int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            HiddenField1.Value = "update";
            query = "EmpEntry";
            com = new SqlCommand(query, con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", HiddenField1.Value).ToString();
            com.Parameters.AddWithValue("@FName", ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
            com.Parameters.AddWithValue("@MName", ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
            com.Parameters.AddWithValue("@LName", ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString());
            com.Parameters.AddWithValue("@id", SqlDbType.Int).Value = id;
            com.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            gedata();
        
        }

        protected void delete(object sender, GridViewDeleteEventArgs e)
        {
            connection();
            int id = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            HiddenField1.Value = "Delete";
            query = "EmpEntry";
            com = new SqlCommand(query, con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Action", HiddenField1.Value).ToString();
            com.Parameters.AddWithValue("id", SqlDbType.Int).Value = id;
            com.ExecuteNonQuery();
            con.Close();
            gedata();
        
        
        
        }
    }
}