using DayPilot.Web.Ui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DayPilot.Web.Ui.Events;
using System.Activities.Expressions;
using static System.Net.Mime.MediaTypeNames;
using System.ServiceModel.Activities;
using System.Globalization;
using DayPilot.Utils;

public partial class Dashboard : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Session["CalendarSelect"] = null;
            DayPilotCalendar1.DataStartField = "StartTime";
            DayPilotCalendar1.DataEndField = "EndTime";
            DayPilotCalendar1.DataTextField = "TestEventDetails";

            DayPilotCalendar1.DataIdField = "Id";
            // DayPilotCalendar1.ViewType = "Week";
            DayPilotCalendar1.DataSource = GetData(DateTime.Today, DateTime.Today);// dt;

            DayPilotCalendar1.StartDate = DateTime.Today;
            DayPilotCalendar1.Days = 7;

            DayPilotCalendar1.DataBind();

            gv.DataSource = GetData(DateTime.Today, DateTime.Today);// dt;
            gv.DataBind();
            if (Session["CalendarSelect"] == null || Session["CalendarSelect"].ToString() == "Week")
            {

                Calendar1.SelectionMode = CalendarSelectionMode.DayWeek;
                ArrayList selectedDates = new ArrayList();
                DateTime today = DateTime.Now;
                Session["CalendarSelect"] = "Week";
                DateTime firstDay = today.AddDays(-(double)(today.DayOfWeek));
                for (int loop = 0; loop < 7; loop++)
                    Calendar1.SelectedDates.Add(firstDay.AddDays(loop));
            }

        }
    }

    private DataTable GetData(DateTime start, DateTime end)
    {
        //clearlabels();
        //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [ReservationTable] WHERE NOT (([eventend] <= @start) OR ([eventstart] >= @end))", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [ReservationTable]", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //da.SelectCommand.Parameters.AddWithValue("start", start);
        //da.SelectCommand.Parameters.AddWithValue("end", end);

        DataTable dt = new DataTable();
        da.Fill(dt);

        DayPilotCalendar1.DataStartField = "StartTime";
        DayPilotCalendar1.DataEndField = "EndTime";
        DayPilotCalendar1.DataTextField = "TestEventDetails";

        DayPilotCalendar1.DataIdField = "Id";

        return dt;
    }

    private void GetDataByID(String pID)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [RequestorName], [TestName], [ProjectID], [VUserCount], [StartTime], [EndTime], [TestDuration], [Comments], [LoadGeneratorRefID], [LoadGeneratorCount], [CreatedOn] FROM [ReservationTable] where ID=@Id", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("Id", pID);
        //da.SelectCommand.Parameters.AddWithValue("end", end);

        DataTable dt = new DataTable();
        da.Fill(dt);

        lblTimeSlotID.Text = dt.Rows[0][0].ToString();
        lblTestName.Text = dt.Rows[0][2].ToString();
        lblStartTime.Text = dt.Rows[0][5].ToString();
        lblEndTime.Text = dt.Rows[0][6].ToString();
        lblDuration.Text = dt.Rows[0][7].ToString();
        lblHostCount.Text = dt.Rows[0][10].ToString();
        lblVuserCount.Text = dt.Rows[0][4].ToString();
        lblProjectName.Text = dt.Rows[0][3].ToString();
        lblCreatedBy.Text = dt.Rows[0][1].ToString();
        lblCreatedOn.Text = dt.Rows[0][11].ToString();
        lblComments.Text = dt.Rows[0][8].ToString();
        lblHostNames.Text = dt.Rows[0][9].ToString();
    }

    private void UpdateReservationSchedule(string RowIdentifier, DateTime startTime, DateTime endTime)
    {
        // clearlabels();
        DataTable table = GetData(DateTime.Today, DateTime.Today);
        DataColumn[] keyColumns = new DataColumn[1];
        keyColumns[0] = table.Columns["Id"];
        table.PrimaryKey = keyColumns;
        DataRow dr = table.Rows.Find(RowIdentifier);

        if (dr != null)
        {
            //dr["StartTime"] = e.NewStart;
            //dr["EndTime"] = e.NewEnd;
            //table.AcceptChanges();
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            var sql = "UPDATE ReservationTable SET StartTime = @StartTime, EndTime = @EndTime " +
        "WHERE Id = @Id";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    // Add the parameters for the UpdateCommand.
                    command.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = startTime;
                    command.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = endTime;
                    command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = RowIdentifier;
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();

                }
            }
        }
    }


    public void Messagebox(string xMessage)
    {
        Response.Write("<script>alert('" + xMessage + "')</script>");
    }


    protected void DayPilotCalendar1_OnEventMove(object sender, EventMoveEventArgs e)
    {
        //clearlabels();
        UpdateReservationSchedule(e.Id, e.NewStart, e.NewEnd);

        //gv.DataSource = GetData(DateTime.Today, DateTime.Today);
        //gv.DataBind();
        DayPilotCalendar1.DataSource = GetData(DateTime.Today, DateTime.Today);
        DayPilotCalendar1.DataBind();
        GetDataByID(e.Id);

    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        clearlabels();
        if (Session["CalendarSelect"] != null && Session["CalendarSelect"].ToString() == "Week")
        {
            Calendar1.SelectionMode = CalendarSelectionMode.DayWeek;
            //Messagebox(Calendar1.SelectedDate.ToString());
            ArrayList selectedDates = new ArrayList();
            DateTime today = Calendar1.SelectedDate;
            DateTime firstDay = today.AddDays(-(double)(today.DayOfWeek));
            for (int loop = 0; loop < 7; loop++)
                Calendar1.SelectedDates.Add(firstDay.AddDays(loop));

            DayPilotCalendar1.StartDate = Calendar1.SelectedDate;
        }

        if (Session["CalendarSelect"] != null && Session["CalendarSelect"].ToString() == "Day")
        {
            Calendar1.SelectionMode = CalendarSelectionMode.Day;
            DayPilotCalendar1.Days = 1;
            DayPilotCalendar1.ViewType = 0;
            DayPilotCalendar1.StartDate = Calendar1.SelectedDate;
        }
    }

    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        if (Session["CalendarSelect"] != null && Session["CalendarSelect"].ToString() == "Week")
        {
            Calendar1.SelectionMode = CalendarSelectionMode.DayWeek;
            ArrayList selectedDates = new ArrayList();
            DateTime today = new DateTime(Calendar1.VisibleDate.Year, Calendar1.VisibleDate.Month, 1);
            DateTime firstDay = today.AddDays(-(double)(today.DayOfWeek));
            for (int loop = 0; loop < 7; loop++)
                Calendar1.SelectedDates.Add(firstDay.AddDays(loop));

            DayPilotCalendar1.StartDate = new DateTime(Calendar1.VisibleDate.Year, Calendar1.VisibleDate.Month, 1);
        }



        if (Session["CalendarSelect"] != null && Session["CalendarSelect"].ToString() == "Day")
        {
            Calendar1.SelectionMode = CalendarSelectionMode.Day;
            Calendar1.SelectedDate = new DateTime(Calendar1.VisibleDate.Year, Calendar1.VisibleDate.Month, 1);
            DayPilotCalendar1.StartDate = new DateTime(Calendar1.VisibleDate.Year, Calendar1.VisibleDate.Month, 1);
        }
    }

    protected void DayPilotCalendar1_BeforeEventRender(object sender, DayPilot.Web.Ui.Events.Calendar.BeforeEventRenderEventArgs e)
    {
        //if ((string)e.DataItem["Id"] == "R002")  // "type" field must be available in the DataSource
        //{
        //    e.CssClass = "special";
        //    e.BackgroundColor = "lightyellow";
        //    e.Html = "<i>WARNING: This is an unusual event.</i><br>" + e.Html;
        //}
    }

    protected void DayPilotCalendar1_EventClick(object sender, EventClickEventArgs e)
    {
        GetDataByID(e.Id);
    }
    protected void DayPilotCalendar1_TimeRangeSelected(object sender, TimeRangeSelectedEventArgs e)
    {
        clearlabels();
        Session["StartTime"] = e.Start.ToString("yyyy-MM-ddTHH:mm");
        Session["EndTime"] = e.End.ToString("yyyy-MM-ddTHH:mm");
        Session["RequestType"] = "New";

        string url = "ServerReservation.aspx";
        // string url = "ServerReservation.aspx?start=2022-08-22T01:58 & end=2022-08-22T02:58";
        //Messagebox(url);
        string s = "window.open('" + url + "', 'popup_window', 'width=1297,height=570,left=100,top=100,resizable=no');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        DayPilotCalendar1.DataSource = GetData(DateTime.Today, DateTime.Today);
        DayPilotCalendar1.DataBind();

    }

    protected void DayPilotCalendar1_OnEventResize(object sender, EventResizeEventArgs e)
    {
        UpdateReservationSchedule(e.Id, e.NewStart, e.NewEnd);
        gv.DataSource = GetData(DateTime.Today, DateTime.Today);
        gv.DataBind();
        DayPilotCalendar1.DataSource = GetData(DateTime.Today, DateTime.Today);
        DayPilotCalendar1.DataBind();

    }

    protected void btnEditSchedule_Click(object sender, EventArgs e)
    {
        // DayPilotCalendar1.ViewType = "Day";
        SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [RequestorName], [TestName], [ProjectID], [VUserCount], [StartTime], [EndTime], [TestDuration], [Comments], [LoadGeneratorRefID], [LoadGeneratorCount], [CreatedOn] FROM [ReservationTable] where ID=@Id", ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("Id", lblTimeSlotID.Text);

        DataTable dt = new DataTable();
        da.Fill(dt);

        CultureInfo culture = new CultureInfo("en-US");
        DateTime StartTime = Convert.ToDateTime(dt.Rows[0][5], culture);
        DateTime EndTime = Convert.ToDateTime(dt.Rows[0][6], culture);



        Session["RequestorName"] = lblCreatedBy.Text;
        Session["TestName"] = lblTestName.Text;
        Session["ProjectID"] = lblProjectName.Text;
        Session["VUserCount"] = lblVuserCount.Text;
        Session["StartTime"] = StartTime.ToString("yyyy-MM-ddTHH:mm");
        Session["EndTime"] = EndTime.ToString("yyyy-MM-ddTHH:mm");
        Session["TestDuration"] = lblDuration.Text;
        Session["Comments"] = lblComments.Text;
        Session["LoadGeneratorRefID"] = dt.Rows[0][9].ToString();
        Session["LoadGeneratorCount"] = lblHostCount.Text;
        Session["RequestType"] = "Edit";
        string url = "ServerReservation.aspx";

        string s = "window.open('" + url + "', 'popup_window', 'width=1297,height=570,left=100,top=100,resizable=no');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        DayPilotCalendar1.DataSource = GetData(DateTime.Today, DateTime.Today);
        DayPilotCalendar1.DataBind();
    }

    protected void btnDeleteSchedule_Click(object sender, ImageClickEventArgs e)
    {
        if (lblTimeSlotID.Text.Length < 1)
        {
            Messagebox("Please select a timeslot to delete!!");
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "Confirm()", true);

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                var sql = "DELETE FROM ReservationTable WHERE Id = @Id";

                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        // Add the parameters for the UpdateCommand.
                        command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = lblTimeSlotID.Text;
                        connection.Open();

                        command.ExecuteNonQuery();

                        connection.Close();
                    }
                }
                clearlabels();
                DayPilotCalendar1.DataSource = GetData(DateTime.Today, DateTime.Today);
                DayPilotCalendar1.DataBind();
            }
        }
    }

    public void clearlabels()
    {
        lblTimeSlotID.Text = "";
        lblTestName.Text = "";
        lblStartTime.Text = "";
        lblEndTime.Text = "";
        lblDuration.Text = "";
        lblHostCount.Text = "";
        lblVuserCount.Text = "";
        lblProjectName.Text = "";
        lblCreatedBy.Text = "";
        lblCreatedOn.Text = "";
        lblComments.Text = "";
        lblHostNames.Text = "";
    }

    protected void btnNewSchedule_Click(object sender, ImageClickEventArgs e)
    {

        clearlabels();
        DateTime dtFirstInterval = RoundUpToPreviousQuarter(DateTime.Now, TimeSpan.FromMinutes(15));
        DateTime dtSecondInterval = dtFirstInterval.AddMinutes(30);

        Session["StartTime"] = dtFirstInterval;
        Session["EndTime"] = dtSecondInterval;
        Session["RequestType"] = "New";
        string url = "ServerReservation.aspx";

        string s = "window.open('" + url + "', 'popup_window', 'width=1297,height=570,left=100,top=100,resizable=no');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        DayPilotCalendar1.DataSource = GetData(DateTime.Today, DateTime.Today);
        DayPilotCalendar1.DataBind();
    }
    private DateTime RoundUpToPreviousQuarter(DateTime date, TimeSpan d)
    {
        return new DateTime(((date.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
    }

    // call the method


    protected void btnRefreshSchedule_Click(object sender, ImageClickEventArgs e)
    {
        clearlabels();
        DayPilotCalendar1.DataSource = GetData(DateTime.Today, DateTime.Today);
        DayPilotCalendar1.DataBind();
    }

    protected void lnkDaily_Click(object sender, EventArgs e)
    {
        Session["CalendarSelect"] = "Day";

        Calendar1.TodaysDate = System.DateTime.Now;
        Calendar1.SelectedDate = DateTime.Today;
        DayPilotCalendar1.ViewType = 0;
        DayPilotCalendar1.Days = 1;
        DayPilotCalendar1.StartDate = Calendar1.SelectedDate;
    }

    protected void lnkWeekly_Click(object sender, EventArgs e)
    {
        Session["CalendarSelect"] = "Week";
        DayPilotCalendar1.ViewType = (DayPilot.Web.Ui.Enums.Calendar.ViewTypeEnum)2;
        DayPilotCalendar1.Days = 7;
        DayPilotCalendar1.StartDate = DateTime.Today;
        Calendar1.SelectionMode = CalendarSelectionMode.DayWeek;
        ArrayList selectedDates = new ArrayList();
        DateTime today = DateTime.Now;
        DateTime firstDay = today.AddDays(-(double)(today.DayOfWeek));
        for (int loop = 0; loop < 7; loop++)
            Calendar1.SelectedDates.Add(firstDay.AddDays(loop));
    }


}