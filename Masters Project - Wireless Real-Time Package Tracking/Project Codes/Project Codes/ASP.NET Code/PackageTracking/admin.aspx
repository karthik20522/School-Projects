<%@ Page Language="vb" AutoEventWireup="false" Codebehind="admin.aspx.vb" Inherits="PackageTracking.admin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>admin</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body vLink="#9999cc" aLink="#0000cc" link="#3366cc" bgColor="#ffffff" MS_POSITIONING="GridLayout">
		<table style="Z-INDEX: 104; LEFT: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 8px" cellSpacing="0"
			cellPadding="0" width="100%" border="0">
			<tr>
				<td width="100%" bgColor="#ff0000" height="0"></td>
			</tr>
		</table>
		<asp:image id="Image3" style="Z-INDEX: 115; LEFT: 408px; POSITION: absolute; TOP: 328px" runat="server"
			ImageUrl="/images/left_arrow.jpg"></asp:image><asp:image id="Image2" style="Z-INDEX: 114; LEFT: 416px; POSITION: absolute; TOP: 232px" runat="server"
			ImageUrl="/images/cross_arrow.jpg"></asp:image><asp:image id="Image1" style="Z-INDEX: 113; LEFT: 408px; POSITION: absolute; TOP: 208px" runat="server"
			ImageUrl="/images/left_arrow.jpg"></asp:image>
		<!-- TITLE -->
		<table cellSpacing="10" cellPadding="0" width="100%" border="0">
			<tr>
				<td vAlign="top" align="left"><a href="http://www.njit.edu" target="_blank"><IMG alt="NJIT" src="\images\njit.gif" border="0"></a>
					<font color="#ff0000">
						<h2>Admin Page</h2>
					</font>
				</td>
				<td vAlign="top" align="right"><font style="FONT-WEIGHT: bold; OVERFLOW: visible; TEXT-ALIGN: left" color="#006600">
						<h4>Karthik Srinivasan
							<br>
							NJIT ID: 213-75-778<br>
							ks234@njit.edu<br>
							Advisor: Dr.Borcea
						</h4>
					</font>
				</td>
			</tr>
		</table>
		<!-- RED LINE -->
		<table style="HEIGHT: 8px" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td width="100%" bgColor="#ff0000" height="0"></td>
			</tr>
		</table>
		<br>
		<form id="Form1" method="post" runat="server">
			<asp:button id="Button2" style="Z-INDEX: 117; LEFT: 488px; POSITION: absolute; TOP: 368px" runat="server"
				Width="134px" Text="Bind the Above Data"></asp:button><asp:button id="Button1" style="Z-INDEX: 116; LEFT: 312px; POSITION: absolute; TOP: 368px" runat="server"
				Width="126px" Text="Scan For Changes"></asp:button><asp:label id="Label1" style="Z-INDEX: 105; LEFT: 256px; POSITION: absolute; TOP: 184px" runat="server"
				Width="144px" ForeColor="#004000" Font-Bold="True">Select Package ID</asp:label><asp:dropdownlist id="DropDownList4" style="Z-INDEX: 112; LEFT: 536px; POSITION: absolute; TOP: 328px"
				runat="server" Width="128px">
				<asp:ListItem Value="T01">T01</asp:ListItem>
				<asp:ListItem Value="T02">T02</asp:ListItem>
				<asp:ListItem Value="T03">T03</asp:ListItem>
			</asp:dropdownlist><asp:label id="Label4" style="Z-INDEX: 111; LEFT: 520px; POSITION: absolute; TOP: 304px" runat="server"
				Width="206px" ForeColor="#004000" Font-Bold="True">Associate it to a Truck</asp:label><asp:dropdownlist id="DropDownList3" style="Z-INDEX: 110; LEFT: 256px; POSITION: absolute; TOP: 328px"
				runat="server" Width="128px"></asp:dropdownlist><asp:label id="Label3" style="Z-INDEX: 109; LEFT: 256px; POSITION: absolute; TOP: 304px" runat="server"
				Width="214px" ForeColor="#004000" Font-Bold="True">Assign A Tracking Number</asp:label><asp:dropdownlist id="DropDownList2" style="Z-INDEX: 108; LEFT: 536px; POSITION: absolute; TOP: 208px"
				runat="server" Width="120px">
				<asp:ListItem Value="0103ab7301">0103ab7301</asp:ListItem>
				<asp:ListItem Value="0103ab7300">0103ab7300</asp:ListItem>
				<asp:ListItem Value="0103957de7">0103957de7</asp:ListItem>
				<asp:ListItem Value="010394f45f">010394f45f</asp:ListItem>
				<asp:ListItem Value="0103c5e63d">0103c5e63d</asp:ListItem>
				<asp:ListItem Value="0103c5b4d6">0103c5b4d6</asp:ListItem>
				<asp:ListItem Value="0103c5dba1">0103c5dba1</asp:ListItem>
				<asp:ListItem Value="0103c5e4de">0103c5e4de</asp:ListItem>
				<asp:ListItem Value="01039551e3">01039551e3</asp:ListItem>
			</asp:dropdownlist><asp:label id="Label2" style="Z-INDEX: 107; LEFT: 520px; POSITION: absolute; TOP: 184px" runat="server"
				Width="222px" ForeColor="#004000" Font-Bold="True">Associate RFID to Package</asp:label><asp:dropdownlist id="DropDownList1" style="Z-INDEX: 106; LEFT: 256px; POSITION: absolute; TOP: 208px"
				runat="server" Width="128px"></asp:dropdownlist>&nbsp;
			<asp:textbox id="TextBox1" style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 400px" runat="server"
				Width="214px" TextMode="MultiLine" Height="158px"></asp:textbox><asp:calendar id="Calendar1" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 184px"
				runat="server" Width="220px" ForeColor="#663399" Font-Size="8pt" Height="200px" BorderWidth="1px" BackColor="#FFFFCC" DayNameFormat="FirstLetter" Font-Names="Verdana"
				BorderColor="#FFCC66" ShowGridLines="True">
				<SelectorStyle BackColor="#FFCC66"></SelectorStyle>
				<NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle>
				<DayHeaderStyle Height="1px" BackColor="#FFCC66"></DayHeaderStyle>
				<SelectedDayStyle Font-Bold="True" BackColor="#CCCCFF"></SelectedDayStyle>
				<TitleStyle Font-Size="9pt" Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></TitleStyle>
				<OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle>
			</asp:calendar></form>
		<table style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 600px; HEIGHT: 8px" cellSpacing="0"
			cellPadding="0" width="100%" border="0">
			<tr>
				<td width="100%" bgColor="#ff0000" height="1"></td>
			</tr>
		</table>
	</body>
</HTML>
