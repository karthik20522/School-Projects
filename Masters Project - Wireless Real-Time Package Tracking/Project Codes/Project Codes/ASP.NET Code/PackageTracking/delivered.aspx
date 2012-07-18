<%@ Page Language="vb" AutoEventWireup="false" Codebehind="delivered.aspx.vb" Inherits="PackageTracking.delivered" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>delivered</title>
		<meta name="vs_snapToGrid" content="True">
		<meta name="vs_showGrid" content="True">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" vLink="#9999cc" aLink="#0000cc" link="#3366cc" bgColor="#ffffff">
		<asp:Label id="Label1" style="Z-INDEX: 102; LEFT: 328px; POSITION: absolute; TOP: 184px" runat="server"
			Width="368px" Font-Size="X-Large" Font-Bold="True" ForeColor="#004000">Package Delivery Details</asp:Label>
		<asp:Label id="Label9" style="Z-INDEX: 111; LEFT: 344px; POSITION: absolute; TOP: 384px" runat="server"
			Width="294px" Font-Bold="True" ForeColor="#C00000"></asp:Label>
		<asp:Label id="Label8" style="Z-INDEX: 110; LEFT: 344px; POSITION: absolute; TOP: 320px" runat="server"
			Width="174px" Height="56px" Font-Bold="True" ForeColor="#C00000"></asp:Label>
		<asp:Label id="Label7" style="Z-INDEX: 109; LEFT: 344px; POSITION: absolute; TOP: 288px" runat="server"
			Width="168px" Font-Bold="True" ForeColor="#C00000"></asp:Label>
		<asp:Label id="Label6" style="Z-INDEX: 108; LEFT: 344px; POSITION: absolute; TOP: 256px" runat="server"
			Width="128px" Font-Bold="True" ForeColor="#C00000"></asp:Label>
		<asp:Label id="Label5" style="Z-INDEX: 107; LEFT: 208px; POSITION: absolute; TOP: 384px" runat="server"
			Width="120px" Font-Bold="True" ForeColor="#004000"> Delivered Day:</asp:Label>
		<asp:Label id="Label4" style="Z-INDEX: 106; LEFT: 208px; POSITION: absolute; TOP: 320px" runat="server"
			Width="136px" Font-Bold="True" ForeColor="#004000">Delivery Address:</asp:Label>
		<asp:Label id="Label3" style="Z-INDEX: 105; LEFT: 208px; POSITION: absolute; TOP: 288px" runat="server"
			Width="96px" Font-Bold="True" ForeColor="#004000">Delivered To:</asp:Label>
		<asp:Label id="Label2" style="Z-INDEX: 104; LEFT: 208px; POSITION: absolute; TOP: 256px" runat="server"
			Width="128px" Font-Bold="True" ForeColor="#004000">Tracking Number:</asp:Label>
		<asp:Image id="Image1" style="Z-INDEX: 103; LEFT: 24px; POSITION: absolute; TOP: 256px" runat="server"
			ImageUrl="\images\package.jpg"></asp:Image>
		<table style="HEIGHT: 8px" cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td width="100%" bgColor="#ff0000" height="0"></td>
			</tr>
		</table>
		<!-- TITLE -->
		<table cellSpacing="10" cellPadding="0" border="0" width="100%">
			<tr>
				<td align="left" vAlign="top"><a href="http://www.njit.edu" target="_blank"><IMG alt="NJIT" src="\images\njit.gif" border="0"></a>
					<font color="#ff0000">
						<h2>Package Tracking</h2>
					</font>
				</td>
				<td vAlign="top" align="right"><font color="#006600" style="FONT-WEIGHT: bold; OVERFLOW: visible; TEXT-ALIGN: left">
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
		</form>
		<!-- RED LINE -->
		<table style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 600px; HEIGHT: 8px" cellSpacing="0"
			cellPadding="0" width="100%" border="0">
			<tr>
				<td width="100%" bgColor="#ff0000" height="1"></td>
			</tr>
		</table>
	</body>
</HTML>
