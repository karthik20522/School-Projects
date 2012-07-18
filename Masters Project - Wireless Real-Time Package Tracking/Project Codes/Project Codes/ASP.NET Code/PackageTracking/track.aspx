<%@ Page Language="vb" AutoEventWireup="false" Codebehind="track.aspx.vb" Inherits="PackageTracking.track" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>track</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta http-equiv="refresh" content="120">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<STYLE type="text/css" media="screen">.pin {
	FILTER: alpha(opacity=50); BACKGROUND-IMAGE: url(/images/ripple_10198.gif); WIDTH: 150px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 100px
}
		</STYLE>
		<![if !IE]><script src="http://local.live.com/JS/AtlasCompat.js"></script>
		<![endif]>
		<script src="http://local.live.com/MapControl.ashx"></script>
		<link href="http://dev.virtualearth.net/standard/v2/MapControl.css" type="text/css" rel="stylesheet">
		<script src="http://dev.virtualearth.net/standard/v2/MapControl.js">
		</script>
		<link href="http://local.live.com/css/MapControl.css" type="text/css" rel="stylesheet">
		<%=cur%>
	</HEAD>
	<body vLink="#9999cc" aLink="#0000cc" link="#3366cc" bgColor="#ffffff" onload="load_me()"
		MS_POSITIONING="GridLayout">
		<!-- RED LINE --><asp:label id="Label1" style="Z-INDEX: 103; LEFT: 648px; POSITION: absolute; TOP: 216px" runat="server"
			Font-Bold="True" Width="192px" ForeColor="#004000" Font-Size="Medium">Current Location:</asp:label>
		<table style="HEIGHT: 8px" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td width="100%" bgColor="#ff0000" height="0"></td>
			</tr>
		</table>
		<!-- TITLE -->
		<table cellSpacing="10" cellPadding="0" width="100%" border="0">
			<tr>
				<td vAlign="top" align="left"><a href="http://www.njit.edu" target="_blank"><IMG alt="NJIT" src="\images\njit.gif" border="0"></a>
					<font color="#ff0000">
						<h2>Package Tracking</h2>
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
		<div id="myMap" style="OVERFLOW: hidden; WIDTH: 600px; POSITION: relative; HEIGHT: 400px"></div>
		<form id="Form1" method="post" runat="server">
			<asp:button id="Button2" style="Z-INDEX: 106; LEFT: 840px; POSITION: absolute; TOP: 384px" runat="server"
				Width="78px" Text="Track" BackColor="ActiveBorder"></asp:button><asp:dropdownlist id="DropDownList1" style="Z-INDEX: 105; LEFT: 656px; POSITION: absolute; TOP: 384px"
				runat="server" Width="168px"></asp:dropdownlist><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 656px; POSITION: absolute; TOP: 240px"
				runat="server" Font-Bold="True" Width="296px" Height="96px" BackColor="White" TextMode="MultiLine" BorderStyle="None" ReadOnly="True"></asp:textbox><asp:listbox id="ListBox1" style="Z-INDEX: 101; LEFT: 800px; POSITION: absolute; TOP: 672px"
				runat="server" Width="120px" Height="26px" Visible="False"></asp:listbox></form>
		<!-- RED LINE -->
		<table style="HEIGHT: 8px" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td width="100%" bgColor="#ff0000" height="1"></td>
			</tr>
		</table>
	</body>
</HTML>
