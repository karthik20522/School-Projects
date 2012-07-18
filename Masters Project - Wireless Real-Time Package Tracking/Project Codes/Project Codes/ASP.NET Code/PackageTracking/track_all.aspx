<%@ Page Language="vb" AutoEventWireup="false" Codebehind="track_all.aspx.vb" Inherits="PackageTracking.track_all" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>track</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<STYLE type="text/css" media="screen">.pin {
	BORDER-RIGHT: #ff0000 1px solid; BORDER-TOP: #ff0000 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 8pt; Z-INDEX: 5; BACKGROUND: #0000ff; OVERFLOW: hidden; BORDER-LEFT: #ff0000 1px solid; WIDTH: 40px; CURSOR: pointer; COLOR: white; BORDER-BOTTOM: #ff0000 1px solid; FONT-FAMILY: Arial,sans-serif; HEIGHT: 17px; TEXT-ALIGN: center; TEXT-DECORATION: none
}
		</STYLE>
		<![if !IE]><script src="http://local.live.com/JS/AtlasCompat.js"></script>
		<![endif]><link href="http://dev.virtualearth.net/standard/v2/MapControl.css" type="text/css" rel="stylesheet">
		<script src="http://dev.virtualearth.net/standard/v2/MapControl.js">
		</script>
		<link href="http://local.live.com/css/MapControl.css" type="text/css" rel="stylesheet">
		<script src="http://local.live.com/MapControl.ashx">
		</script>
		<%=cur%>
	</HEAD>
	<body vLink="#9999cc" aLink="#0000cc" link="#3366cc" bgColor="#ffffff" scroll="no" onload="load_me()"
		MS_POSITIONING="GridLayout">
		<asp:listbox id="ListBox3" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 16px" runat="server"
			Visible="False"></asp:listbox><asp:listbox id="ListBox1" style="Z-INDEX: 104; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
			Visible="False"></asp:listbox><asp:listbox id="ListBox2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
			Visible="False"></asp:listbox>
		<div id="myMap" style="OVERFLOW: hidden; WIDTH: 1280px; HEIGHT: 1024px"></div>
		<form id="Form1" method="post" runat="server">
		</form>
	</body>
</HTML>
