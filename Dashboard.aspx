<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard Form</title>
    <style type="text/css" style="font-weight: bold; font-family: Calibri">
        .auto-style1 {
            height: 42px;
        }
        .auto-style4 {
            width: 82%;
        }
        .auto-style5 {
            width: 4%;
        }
        .auto-style6 {
            width: 3%;
        }
        .auto-style7 {
            height: 49px;
        }
        .auto-style8 {
            height: 42px;
            width: 4%;
        }
        .auto-style9 {
            height: 42px;
            width: 3%;
        }
        .auto-style10 {
            width: 252px;
        }
        .auto-style13 {
            width: 35%;
            font-weight: bold; 
            font-family: Calibri; 
            font-size: medium
        }
        .auto-style14 {
            width: 65%;
        }
        .auto-style15 {
            width: 35%;
            font-weight: bold; 
            font-family: Calibri; 
            font-size: medium
        }
        .auto-style16 {
            width: 65%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="auto-style7" style="background-color: #E9E9E9; border-style: solid; border-width: thin">

                <table width="25%">
                    <tr>
                        <td width="10%">
                            <asp:ImageButton ID="btnNewSchedule" runat="server" ImageUrl="~/Images/new.png" AlternateText="New" Width="31px" Height="35px" OnClick="btnNewSchedule_Click" />
                        </td>
                        <td class="auto-style5"></td>
                        <td width="10%">
                            <asp:ImageButton ID="btnEditSchedule" runat="server" ImageUrl="~/Images/edit.png" AlternateText="New" Width="31px" Height="35px" OnClick="btnEditSchedule_Click" />
                        </td>
                        <td class="auto-style5"></td>
                        <td width="10%">
                            <asp:ImageButton ID="btnDeleteSchedule" runat="server" ImageUrl="~/Images/del.png" AlternateText="Delete" Width="31px" Height="35px" CausesValidation="False"
    OnClick="btnDeleteSchedule_Click" />
                        </td>
                        <td class="auto-style6"></td>
                        <td width="10%">
                            <asp:ImageButton ID="btnRefreshSchedule" runat="server" ImageUrl="~/Images/refresh.png" AlternateText="Refresh" Width="31px" Height="35px" OnClick="btnRefreshSchedule_Click" />
                        </td>
                        <td width="5%"></td>
                        <td class="auto-style4">


                            <table>
                                <tr>
                                    <td width="15%" class="auto-style1">
                                        <asp:LinkButton ID="lnkDaily" Text="Day" runat="server" OnClick="lnkDaily_Click" />
                                    </td>
                                    <td class="auto-style9"></td>
                                    <td width="15%" class="auto-style1">
                                        <asp:LinkButton ID="lnkWeekly" Text="Week" runat="server" OnClick="lnkWeekly_Click" />
                                    </td>
                                    <td class="auto-style8"></td>
                                    <td  width="50%" >
                                        &nbsp;</td>
                                    
                                    
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gv" runat="server" Visible="false">
                </asp:GridView>
            </div>
            <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>--%>

            <table>

                <tr>
                    <td style="vertical-align: top" width="10%">
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="Calendar1_SelectionChanged" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" SelectionMode="DayWeek" Width="200px">
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <OtherMonthDayStyle ForeColor="#808080" />
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <WeekendDayStyle BackColor="#FFFFCC" />
                        </asp:Calendar>

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [ReservationTable]"></asp:SqlDataSource>
                    </td>
                    <td width="70%" style="vertical-align: top">
                        <DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server" ClientObjectName="dp_week" StartDate="2022-08-21" ViewType="Week"
                            HeightSpec="Full" ScrollPositionHour="12" HeaderDateFormat="d MMMM yyyy" OnBeforeEventRender="DayPilotCalendar1_BeforeEventRender"
                            EventClickHandling="PostBack"
                            OnEventClick="DayPilotCalendar1_EventClick"
                            TimeRangeSelectedHandling="PostBack"
                            OnTimeRangeSelected="DayPilotCalendar1_TimeRangeSelected"
                            EventMoveHandling="PostBack"
                            OnEventMove="DayPilotCalendar1_OnEventMove"
                            
                            EventResizeHandling="PostBack"
                            OnEventResize="DayPilotCalendar1_OnEventResize" />
                    </td>
                    <td width="20%" style="vertical-align: top; background-color: #E9E9E9;">
                        <table class="auto-style10" width="100%">
                            <tr>
                                <td colspan="2" style="font-family: Calibri; font-size: large; font-weight: bold">Summary</td>
                            </tr>
                            <tr>
                                <td class="auto-style13">&nbsp;</td>
                                <td class="auto-style14">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style13">Timeslot ID:</td>
                                <td class="auto-style14">
                                    <asp:Label ID="lblTimeSlotID" runat="server" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">Test Name:</td>
                                <td class="auto-style14">
                                    <asp:Label ID="lblTestName" runat="server" Font-Names="Calibri" Font-Size="Medium" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">Start Time:</td>
                                <td class="auto-style14">
                                    <asp:Label ID="lblStartTime" runat="server" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">End Time:</td>
                                <td class="auto-style14">
                                    <asp:Label ID="lblEndTime" runat="server" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style15">Duration:</td>
                                <td class="auto-style16">
                                    <asp:Label ID="lblDuration" runat="server" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style15">Host(s):</td>
                                <td class="auto-style16">
                                    <asp:Label ID="lblHostCount" runat="server" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">Vusers:</td>
                                <td class="auto-style14">
                                    <asp:Label ID="lblVuserCount" runat="server" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">Project:</td>
                                <td class="auto-style14">
                                    <asp:Label ID="lblProjectName" runat="server" Font-Names="Calibri" Font-Size="Medium" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">Created By:</td>
                                <td class="auto-style14">
                                    <asp:Label ID="lblCreatedBy" runat="server" Font-Names="Calibri" Font-Size="Medium" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">Created On:</td>
                                <td class="auto-style14">
                                    <asp:Label ID="lblCreatedOn" runat="server" Font-Names="Calibri" Font-Size="Medium" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">Comments:</td>
                                <td class="auto-style14">
                                    <asp:Label ID="lblComments" runat="server" Font-Names="Calibri" Font-Size="Medium" Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13">Host Names:</td>
                                <td class="auto-style14">
                                    <asp:Label ID="lblHostNames" runat="server" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>

   
            <script type = "text/javascript">
                function Confirm() {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    var a = document.getElementById('<%= lblTimeSlotID.Text %>');
                    if (typeof a !== 'undefined') {
                        if (confirm("Do you want to delete the timeslot?")) {
                            confirm_value.value = "Yes";
                        } else {
                            confirm_value.value = a;
                        }
                        document.forms[0].appendChild(confirm_value);
                    }
                }
            </script>

            <script type="text/javascript">
                function ShowPopup(message, title) {
                    $(function () {
                        $("#dialog").html(message);
                        $("#dialog").dialog({
                            title: title,
                            width: 450,
                            buttons: {
                                Close: function () {
                                    $(this).dialog('close');

                                }
                            },
                            modal: true
                        });
                    });
                };
            </script>

            <script language="javascript">
                function popitup(url) {
                    newwindow = window.open(url, 'name', 'height=400,width=500');
                    if (window.focus) { newwindow.focus() }
                    return false;
                }

                //function showModal() {
                //    window.showModalDialog("ServerReservation.aspx", "", "dialogHeight:500px; dialogWidth:600px")
                //}
            </script>

        </div>
    </form>
</body>
</html>
