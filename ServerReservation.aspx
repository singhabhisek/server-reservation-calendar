<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ServerReservation.aspx.cs" Inherits="ServerReservation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ServerReservationForm</title>

    <style type="text/css" style="font-family: Calibri; font-size: small">
        
        .auto-style4 {
            height: 464px;
            width: 1213px;
            margin-bottom: 0px;
        }

        .auto-style5 {
            width: 101%;
            height: 551px;
            font-family: Calibri; 
            font-size: medium;
            /*font-weight: bold;*/
        }

        .auto-style8 {
            margin-left: 0px;
        }

        .auto-style23 {
            width: 172px;
        }
        .auto-style24 {
            width: 172px;
            height: 35px;
        }

        .auto-style25 {
            width: 112px;
             height: 35px;
        }
        .auto-style26 {
            width: 112px;
            height: 35px;
        }
        .auto-style27 {
            width: 107px;
             height: 35px;
        }
        
        .auto-style29 {
            width: 112px;
            height: 35px;
        }
        .auto-style31 {
            width: 172px;
            height: 35px;
        }

        .auto-style32 {
            width: 107px;
        }

        </style>

    <script type="text/javascript">
        
        function resizeWindow() 
{           
    // you can get height and width from serverside as well      
    var width=400;
    var height=400; 
            this.resizeTo(width, height); 
            this.focus();

            alert("Hell:");
}
    </script>

</head>
<body style="overflow-x: hidden;overflow-y: hidden;">

    <form id="form1" runat="server">

        <div class="auto-style5">
            <%--style="border: 1px solid #808080; margin-top: 15px;"--%>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />
            <table style="background-color: #ffffff; filter: alpha(opacity=40); opacity: 0.95; border: 1px red solid;">
                <tr>
                    <td width="2%"></td>
                    <td width="96%">

                        <asp:Label ID="lblHeading" runat="server" Text="Add a new Timeslot" Font-Size="X-Large" Font-Bold="True"></asp:Label>

                        <table class="auto-style4">
                            <tr>
                                <td class="auto-style25" width="25%">
                                    <asp:Label runat="server" Text="User Name: " Font-Bold="True"></asp:Label>
                                </td>
                                <td class="auto-style27">
                                    <asp:TextBox ID="txtUserName" runat="server" Height="30px" Width="235px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <br />
                                </td>
                                <td class="auto-style23">
                                    <asp:Label ID="lnkButtonLGController" Font-Bold="True" runat="server">Available Resources</asp:Label>
                                </td>
                                <td rowspan="8" style="vertical-align: top" width="50%">
                                    <%--</asp:Panel>--%><%--<asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Button3" />
                                    </Triggers>
                                    <ContentTemplate>--%>
                                    <table width="60%">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" OnClick="LinkButton3_Click">All</asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" CausesValidation ="false" runat="server" OnClick="LinkButton1_Click1">Filter Controller</asp:LinkButton> <%--OnClientClick="Filter('Controller');return false;" --%>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton2" CausesValidation ="false" runat="server"  OnClick="LinkButton2_Click">Filter Load Generator</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="GridView1" runat="server" EmptyDataText="Click on above link to show relevant resources"  AllowPaging="True" DataKeyNames="ServerName" AutoGenerateColumns="False" CellPadding="3" Font-Names="Calibri" Font-Size="Medium" OnRowDataBound="GridView1_RowDataBound"  Width="516px" PageSize="8" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="True"   >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select Data">

                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Enabled='<%# Eval("Status").ToString().Equals("Available") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ServerType" HeaderText="ServerType" SortExpression="ServerType" />
                                            <asp:BoundField DataField="ServerName" HeaderText="ServerName" SortExpression="ServerName" />
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Image ImageUrl='<%# "~/Images/" + (Eval("Status").ToString() == "Available" ? "P.png" : "A.png") %>' runat="server" Height="25" Width="25" />
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>


                                    <%--<asp:Button ID="Button3" runat="server" Text="OK" OnClick="Button3_Click" />
                                        <asp:Button ID="Button4" runat="server" Text="Close" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                           
                                </td>
                            </tr>
                            <br />
                            <tr>
                                <td class="auto-style29">
                                    <asp:Label runat="server" Font-Bold="True" Text="Test Name:" ID="Label7"></asp:Label>
                                </td>
                                <td class="auto-style27">
                                    <asp:TextBox ID="txtTestName" runat="server" Height="30px" Width="235px"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTestName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style31"></td>
                            </tr>
                            <br />
                            <tr>
                                <td class="auto-style25">
                                    <asp:Label runat="server" Font-Bold="True" Text="Project Name: " ID="Label2"></asp:Label>
                                </td>
                                <td class="auto-style27">
                                    <asp:TextBox ID="txtProjectName" runat="server" Height="30px" Width="235px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProjectName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style23">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style25">
                                    <asp:Label runat="server" Font-Bold="True" Text="VUser Count: " ID="Label6"></asp:Label>
                                </td>
                                <td style="vertical-align: top" class="auto-style32">
                                    <asp:TextBox ID="txtVUserCount" runat="server" Height="21px" Width="143px"></asp:TextBox>
                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtVUserCount" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                                </td>
                                <td class="auto-style23">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style26">
                                    <asp:Label runat="server" Font-Bold="True" Text="Start Time"></asp:Label>
                                </td>
                                <td class="auto-style27">
                                    <asp:TextBox ID="txtStartTimeCal" runat="server" onchange="validate_hours()" TextMode="DateTimeLocal" placeholder="mm/dd/yyyy" Height="29px" Width="235px"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStartTimeCal" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style24">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style26">
                                    <asp:Label ID="Label1" Font-Bold="True" runat="server" Text="End Time:"></asp:Label>
                                </td>
                                <td class="auto-style27">
                                    <asp:TextBox ID="txtEndTimeCal" runat="server" onchange="diff_hours()" TextMode="DateTimeLocal" placeholder="mm/dd/yyyy" Height="30px" Width="235px" CssClass="auto-style8" AutoPostBack="False" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndTimeCal" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style24">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style26">
                                    <asp:Label runat="server" Font-Bold="True" Text="Total Duration: " ID="Label5"></asp:Label>
                                </td>
                                <td class="auto-style27">
                                    <input type="text" readonly="readonly" runat="server" id="txtTestDuration"/>
                                    <%--<asp:TextBox ID="txtTestDuration" runat="server" Height="30px" Width="123px" AutoPostBack="False" ReadOnly="true"></asp:TextBox>--%>
                                    </td>
                                <td class="auto-style24"></td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top" class="auto-style25">
                                    <asp:Label ID="Label3" Font-Bold="True" runat="server" Text="Comments:"></asp:Label></td>
                                <td style="vertical-align: top" class="auto-style27">
                                    <asp:TextBox ID="txtComments" runat="server" Height="30px" Width="235px"></asp:TextBox></td>
                                <td style="vertical-align: top" class="auto-style23">&nbsp;</td>

                            </tr>
                            <tr>

                                <td colspan="2"><cc1:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server"
                                        TargetControlID="txtVUserCount" Width="100" Maximum="9999" Minimum="1"></cc1:NumericUpDownExtender>
&nbsp;</td>

                                <%--<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkButtonLGController" PopupControlID="Panel1" CancelControlID="Button4" BackgroundCssClass="popup"></cc1:ModalPopupExtender>--%><%--<asp:Panel ID="Panel1" runat="server">
                            Select Controller and Load Generators--%>
                                <td colspan="2" width="50%">
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [ServerType], [ServerName], [Status] FROM [ServerInventory]"></asp:SqlDataSource>

                                    <asp:Button ID="Button1" runat="server" Text="Book Schedule" OnClick="Button1_Click" Height="35px" />
                                    &nbsp;&nbsp;


                        <asp:Button ID="Button5" runat="server" Text="Cancel" OnClick="Button5_Click" CausesValidation="false" Height="35px" />
                                </td>


                            </tr>

                        </table>

                    </td>
                    <td width="2%"></td>
                </tr>
            </table>


        </div>


        <p>
            &nbsp;
        </p>

        <%--Highlight GridView row on mouseover event--%>

        <script type="text/javascript">

            function Filter(searchString) {
                        
                var tblGrid = document.getElementById('<%= GridView1.ClientID %>');
                var rows = tblGrid.rows;
                var i = 0, row, cell;
                for (i = 1; i < rows.length; i++) {
                    row = rows[i];
                    var found = false;
                    for (var j = 0; j < row.cells.length; j++) {
                        cell = row.cells[j];
                        if (cell.innerHTML.indexOf(searchString) >= 0) {
                            found = true;
                            break;
                        }
                    }
                    if (!found) {
                        row.style['display'] = 'none';

                    }
                    else {
                        row.style.display = '';
                    }
                }

                return false;
            }

        </script>
        <script type="text/javascript">
            function MouseEvents(objRef, evt) {
                var checkbox = objRef.getElementsByTagName("input")[0];
                if (evt.type == "mouseover") {
                    objRef.style.backgroundColor = "green";
                }
                else {
                    if (checkbox.checked) {
                        objRef.style.backgroundColor = "red";
                    }
                    else if (evt.type == "mouseout") {
                        if (objRef.rowIndex % 2 == 0) {
                            //Alternating Row 
                            objRef.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            objRef.style.backgroundColor = "white";
                        }
                    }
                }
            }
        </script>


        <%--Highlight Row when checkbox is checked--%>

        <script type="text/javascript">
            function Check_Click(objRef) {
                //Get the Row based on checkbox
                var row = objRef.parentNode.parentNode;
                if (objRef.checked) {
                    //If checked change color to Aqua
                    row.style.backgroundColor = "red";
                }
                else {
                    //If not checked change back to original color
                    if (row.rowIndex % 2 == 0) {
                        //Alternating Row Color
                        row.style.backgroundColor = "#C2D69B";
                    }
                    else {
                        row.style.backgroundColor = "white";
                    }
                }

                //Get the reference of GridView
                var GridView = row.parentNode;
                //Get all input elements in Gridview
                var inputList = GridView.getElementsByTagName("input");
                for (var i = 0; i < inputList.length; i++) {
                    //The First element is the Header Checkbox
                    var headerCheckBox = inputList[0];
                    //Based on all or none checkboxes
                    //are checked check/uncheck Header Checkbox
                    var checked = true;
                    if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                        if (!inputList[i].checked) {
                            checked = false;
                            break;
                        }
                    }
                }
                headerCheckBox.checked = checked;
            }

        </script>

        
     <script type="text/javascript">
         function diff_hours() {
             
             var dt1 = document.getElementById('txtStartTimeCal').value;
             var dt2 = document.getElementById('txtEndTimeCal').value;
             
             var dt3 = new Date(dt1.replace('T', ' '));
             var dt4 = new Date(dt2.replace('T', ' '));
             if (dt3 > dt4) {
                 alert("Start Time is greater than End Time. Please correct");
                 document.getElementById('txtStartTimeCal').value = new Date().toISOString().split('T')[0] + "T" + new Date().toLocaleTimeString('en-US', {
                     hour12: false,
                     hour: "numeric",
                     minute: "numeric"
                 });
                 return;
             }

             var hourDiff = (dt4 - dt3);
             //alert(hourDiff);
             var minDiff = hourDiff / 60 / 1000; //in minutes
             var hDiff = hourDiff / 3600 / 1000; //in hours
             var humanReadable = {};
             humanReadable.hours = Math.floor(hDiff);
             humanReadable.minutes = minDiff - 60 * humanReadable.hours;

             if (hourDiff / 1000 / 60 < 60) {
                 alert("Please select a test duration of one hour at least. Please correct");
                 document.getElementById('txtEndTimeCal').value = "";
                return;
             }
             if (!dt1) {
                 document.getElementById("txtTestDuration").value = "";
             }
             else {
                 document.getElementById("txtTestDuration").value = humanReadable.hours + "hr " + humanReadable.minutes + "min";
             }
             document.getElementById("LinkButton3").click();
         }
     </script>

        <script type="text/javascript">
            function validate_hours() { 
                var dt1 = document.getElementById('txtStartTimeCal').value;
                var dt3 = new Date(dt1.replace('T', ' '));
                var today = new Date();
                today.setHours(0, 0, 0,0);
                
                if (dt3 < today) {
                    alert("Start Time cannot be in past. Please correct");
                    document.getElementById('txtStartTimeCal').value = new Date().toISOString().split('T')[0] + "T" + new Date().toLocaleTimeString('en-US', {
                        hour12: false,
                        hour: "numeric",
                        minute: "numeric"
                    });
                    return;
                }
            }
        </script>


    </form>
</body>
</html>
