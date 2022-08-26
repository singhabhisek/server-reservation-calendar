using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using System.Threading;
using System.Web.DynamicData;
using System.Security.Cryptography;

public partial class ServerReservation : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
       // Button5.Attributes.Add("onclick", "window.close();");
        txtEndTimeCal.Attributes.Add("onchange", "diff_hours();");



        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //BindGridData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Session["CHECKED_ITEMS"] = null;
            if (Session["StartTime"] != null && Session["EndTime"] != null)
            {
                if (Session["RequestType"].ToString() == "New")
                {
                    txtStartTimeCal.Text = Session["StartTime"].ToString();
                    txtEndTimeCal.Text = Session["EndTime"].ToString();
                    LinkButton3_Click(sender,e);
                }
                else if (Session["RequestType"].ToString() == "Edit") 
                {
                    Button1.Text = "Update Entry";
                    txtUserName.Text = Session["RequestorName"].ToString();
                    txtTestName.Text = Session["TestName"].ToString();
                    txtProjectName.Text = Session["ProjectID"].ToString();
                    txtVUserCount.Text = Session["VUserCount"].ToString();
                    txtStartTimeCal.Text = Session["StartTime"].ToString();
                    txtEndTimeCal.Text = Session["EndTime"].ToString();
                    txtTestDuration.Value = Session["TestDuration"].ToString();

                    EditSchedule();

                    //txtComments.Text = Session["Comments"].ToString();
                    //Session["LoadGeneratorRefID"] 
                    //Session["LoadGeneratorCount"
                }
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        if (Session["RequestType"].ToString() == "New")
        {
            NewSchedule();
        }
        else if (Session["RequestType"].ToString() == "Edit")
        {
            EditSchedule();
        }
    }

    public void Messagebox(string xMessage)
    {
        Response.Write("<script>alert('" + xMessage + "')</script>");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
        //    e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
        //}
        
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }

    //This method is used to save the checkedstate of values
    private void SaveCheckedValues()
    {
        ArrayList serverDetails = new ArrayList();
        string index = "";
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            //Messagebox(GridView1.DataKeys[gvrow.RowIndex].Value.ToString());
            index = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
            bool result = ((CheckBox)gvrow.FindControl("CheckBox1")).Checked;

            // Check in the Session
            if (Session["CHECKED_ITEMS"] != null)
                serverDetails = (ArrayList)Session["CHECKED_ITEMS"];
            if (result)
            {
                if (!serverDetails.Contains(index))
                    serverDetails.Add(index);
            }
            else
                serverDetails.Remove(index);
        }
        if (serverDetails != null && serverDetails.Count > 0)
            Session["CHECKED_ITEMS"] = serverDetails;
    }

    private void PopulateCheckedValues()
    {
        ArrayList userdetails = (ArrayList)Session["CHECKED_ITEMS"];
        if (userdetails != null && userdetails.Count > 0)
        {
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                string index = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
                if (userdetails.Contains(index))
                {

                    CheckBox myCheckBox = (CheckBox)gvrow.FindControl("CheckBox1");
                    myCheckBox.Checked = true;
                }
            }
        }

    }




    protected void Button5_Click(object sender, EventArgs e)
    {
        //EditSchedule();
        Response.Write("<script language='javascript'> { window.close();}</script>");
    }

    void InsertServerMachineData(String LoadGeneratorRefID, DateTime StartTime, DateTime EndTime)
    {
        if (Session["CHECKED_ITEMS"] != null)
        {
            SaveCheckedValues();
            foreach (String sessVariable in (ArrayList)Session["CHECKED_ITEMS"])
            {
                string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                try
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO ServerReservation([ReservationId], [ServerName], [StartTime],[EndTime]) VALUES(@ReservationId, @ServerName, @StartTime, @EndTime)"))
                        {
                            //Random r = new Random();
                            //int num = r.Next(1000);
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@ReservationId", LoadGeneratorRefID);
                            cmd.Parameters.AddWithValue("@ServerName", sessVariable);
                            cmd.Parameters.AddWithValue("@StartTime", StartTime);
                            cmd.Parameters.AddWithValue("@EndTime", EndTime);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Messagebox(ex.ToString());
                }

            }
        }
        
    }

    protected void BindGridData(String EndTime, string sqlQuery = null)
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        
        if (sqlQuery == null)
        {
            sqlQuery = "select s.servername, s.ServerType,case when exists(select 1 from  ServerReservation as r where r.ServerName=s.ServerName and  CONVERT(varchar(16),'" + EndTime + "',121) between r.StartTime  and r.EndTime) then 'Busy'" +
                " else 'Available' END AS status from ServerInventory s";
        }
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SaveCheckedValues();
        GridView1.PageIndex = e.NewPageIndex;
        string EndTime = DateTime.Parse(txtEndTimeCal.Text, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd HH:mm:ss");
        BindGridData(EndTime, null);
        PopulateCheckedValues();
    }



    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        

            FindAvailableResources();
        
    }

    void FindAvailableResources()
    {
        if (IsValidDateTimeTest(txtEndTimeCal.Text) == true)
        {
            string EndTime = DateTime.Parse(txtEndTimeCal.Text, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd HH:mm:ss");
            //DataTable dt = new DataTable();
            //GridView1.DataSource = dt;
            //GridView1.DataBind(); 
            BindGridData(EndTime, null);
        }
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        if (IsValidDateTimeTest(txtEndTimeCal.Text) == true)
        {
            //Controller Filter
            string EndTime = DateTime.Parse(txtEndTimeCal.Text, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd HH:mm:ss");
            string sqlQuery = "select s.servername, s.ServerType,case when exists(select 1 from  ServerReservation as r where r.ServerName=s.ServerName and  CONVERT(varchar(16),'" + EndTime + "',121) between r.StartTime  and r.EndTime) then 'Busy'" +
                " else 'Available' END AS status from ServerInventory s where s.ServerType= 'Controller'";
            BindGridData(EndTime, sqlQuery);
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        //LG Filter
        if (IsValidDateTimeTest(txtEndTimeCal.Text) == true) 
        { 
            string EndTime = DateTime.Parse(txtEndTimeCal.Text, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd HH:mm:ss");
            string sqlQuery = "select s.servername, s.ServerType,case when exists(select 1 from  ServerReservation as r where r.ServerName=s.ServerName and  CONVERT(varchar(16),'" + EndTime + "',121) between r.StartTime  and r.EndTime) then 'Busy'" +
                " else 'Available' END AS status from ServerInventory s where s.ServerType= 'Load Generator'";

            BindGridData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), sqlQuery);
        }
        

    }

    public bool IsValidDateTimeTest(string dateTime)
    {
        //2022 - 08 - 26T17: 08
        string[] formats = { "yyyy-MM-ddTHH:mm" };
        DateTime parsedDateTime;
        return DateTime.TryParseExact(dateTime, formats, new CultureInfo("en-US"),
                                       DateTimeStyles.None, out parsedDateTime);
    }

    void NewSchedule()
    {
        DateTime dob = DateTime.Parse(Request.Form[txtStartTimeCal.UniqueID]);
        Random r = new Random();
        int num = r.Next(1000);
        string LoadGeneratorRefID = "L00" + num.ToString("000");
        string Id = "R00" + num.ToString("000");
        DateTime startTime = DateTime.Parse(txtStartTimeCal.Text, CultureInfo.InvariantCulture, DateTimeStyles.None);
        DateTime EndTime = DateTime.Parse(txtEndTimeCal.Text, CultureInfo.InvariantCulture, DateTimeStyles.None);
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        int LGSelectCount = 0;
        if (Session["CHECKED_ITEMS"] == null)
        {
            SaveCheckedValues();
        }
        //if still null
        if (Session["CHECKED_ITEMS"] != null)
        {
            ArrayList ai = (ArrayList)Session["CHECKED_ITEMS"];
            LGSelectCount = ai.Count;
        }
        else
        {
            LGSelectCount = 0;
        }


        if (LGSelectCount == 0)
        {
            Messagebox("No Load Generator/Controller was selected");
            return;
        }

        String TestEventDetails = "ID: " + Id + " <br/> TestName: " + txtTestName.Text + " <br/> VUsers : " + txtVUserCount.Text;
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ReservationTable([Id], [RequestorName], [TestName],[ProjectID],[VUserCount], [StartTime], [EndTime], [TestDuration], [Comments], [LoadGeneratorRefID],[LoadGeneratorCount],[CreatedOn],[TestEventDetails]) VALUES(" +
                    "@Id, @RequestorName, @TestName, @ProjectID, @VUserCount, @StartTime, @EndTime, @TestDuration, @Comments, @LoadGeneratorRefID,@LoadGeneratorCount,@CreatedOn,@TestEventDetails)"))
                {

                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@RequestorName", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@TestName", txtTestName.Text);
                    cmd.Parameters.AddWithValue("@ProjectID", txtProjectName.Text);
                    cmd.Parameters.AddWithValue("@VUserCount", txtVUserCount.Text);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);
                    cmd.Parameters.AddWithValue("@EndTime", EndTime);
                    cmd.Parameters.AddWithValue("@TestDuration", txtTestDuration.Value);
                    cmd.Parameters.AddWithValue("@Comments", txtComments.Text);
                    cmd.Parameters.AddWithValue("@LoadGeneratorRefID", LoadGeneratorRefID);
                    cmd.Parameters.AddWithValue("@LoadGeneratorCount", LGSelectCount);
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TestEventDetails", TestEventDetails);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            //Messagebox(txtTestDuration.Value);
            InsertServerMachineData(LoadGeneratorRefID, startTime, EndTime);
            Messagebox("Insert Successful !!");
            //Refresh gridview if required
            FindAvailableResources();
            //BindGridData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Messagebox(ex.ToString());
        }
    }
    void EditSchedule()
    {
        if ((Session["RequestType"].ToString() == "Edit"))
        {
            string GeneratorID = Session["LoadGeneratorRefID"].ToString();
            string strSQL = "select s.servername, s.ServerType, case when exists(select 1 from  ServerReservation as r where r.ServerName=s.ServerName and  CONVERT(varchar(16),'2022-08-31 22:38',121) between r.StartTime  and r.EndTime) then 'Busy' else 'Available' END AS status, case when exists(select 1 from ServerReservation r where r.ReservationId='" + GeneratorID + "' and  r.ServerName=s.ServerName) then 'Selected' else 'Free' END as selection from ServerInventory s";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            DataTable dt = new DataTable();
            da.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();

            int a = GridView1.PageIndex;

            for (int i = 0; i < GridView1.PageCount; i++)
            {
                //Set Page Index
                GridView1.SetPageIndex(i);

                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if ((row["selection"].ToString() == "Selected") && GridView1.DataKeys[gvrow.RowIndex].Value.ToString() == row["ServerName"].ToString())
                        {

                            //Messagebox();
                            ((CheckBox)gvrow.FindControl("CheckBox1")).Checked = true;

                            //add logic to select other rows in paginated tabs
                        }
                    }
                }
            }
            GridView1.SetPageIndex(a);

            //Messagebox(GridView1.DataKeys[gvrow.RowIndex].Value.ToString());
            //if GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
            //bool result = ((CheckBox)gvrow.FindControl("CheckBox1")).Checked;
            //}
        }
    }

}