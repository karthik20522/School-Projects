Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient

Public Class track_all
    Inherits System.Web.UI.Page

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim s1 As SqlDataReader

    Dim tno4 As String = ""
    Dim dt As Date
    Public cur As String
    Dim xml As XmlTextReader
    Dim xml_loc As String

    Protected WithEvents ListBox2 As System.Web.UI.WebControls.ListBox
    Protected WithEvents ListBox1 As System.Web.UI.WebControls.ListBox
    Protected WithEvents ListBox3 As System.Web.UI.WebControls.ListBox

    Dim listbox4 As New ListBox



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        tno4 = Request.QueryString("tno3")
        dt = Request.QueryString("dt")

        If Not Page.IsPostBack Then
            Try
                con = New SqlConnection
                con.ConnectionString = "server = don; integrated security=SSPI; database=master"
                con.Open()
                cmd = New SqlCommand
                cmd.Parameters.Clear()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "select * from truck_geo where truck_no = '" & tno4.ToString & "'"
                cmd.Connection = con
                s1 = cmd.ExecuteReader
                While s1.Read
                    If Date.Parse(s1(3)).Date = dt.Date Then
                        ListBox2.Items.Add(s1(1))
                        ListBox3.Items.Add(s1(2))
                    End If
                    
                End While
                con.Close()
                s1.Close()
            Catch ex As Exception
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End Try

        End If

        Dim cc As Integer
        cc = ListBox2.Items.Count

        cur = "<script language=""javascript"">" & vbCrLf & _
        "function load_me(){" & vbCrLf & "var params = new Object();" & vbCrLf & _
  "params.latitude =" & ListBox2.Items(cc - 1).ToString & ";" & vbCrLf & _
  "params.longitude = " & ListBox3.Items(cc - 1).ToString & ";" & vbCrLf & _
  "params.zoomlevel = 10;" & vbCrLf & _
  "params.mapstyle = Msn.VE.MapStyle.Road;" & vbCrLf & _
  "params.showScaleBar = true;" & vbCrLf & _
  "params.showDashboard = true;" & vbCrLf & _
  "params.dashboardSize = Msn.VE.DashboardSize.Normal;" & vbCrLf & _
  "params.dashboardX = 3;" & vbCrLf & _
  "params.dashboardY = 3; params.obliqueEnabled = true;	params.obliqueUrl = ""http://local.live.com/imagery.ashx""" & vbCrLf & _
  "map = 	new Msn.VE.MapControl(document.getElementById(""myMap""), params);" & vbCrLf & _
  "map.Init();" & vbCrLf & _
  "map.ClearPushpins();" '& vbCrLf & _
        '"map.AddPushpin('pin'," & ListBox2.Items(cc - 1).ToString & "," & ListBox3.Items(cc - 1).ToString & ",65,75,'pin','',2);"

        For i As Integer = ListBox3.Items.Count - 1 To 0 Step -1
            cur &= vbCrLf & "map.AddPushpin('pin'," & ListBox2.Items(i).ToString & "," & ListBox3.Items(i).ToString & ",65,75,'pin','" & (i + 1).ToString & "',2);"
        Next

        cur &= vbCrLf & "} </script>" & vbCrLf

        '& vbCrLf & _
        ' "map.AddPushpin('pin'," & lat & "," & lon & ",65,75,'pin','',2);" & vbCrLf & _
        '"map.SetCenter(" & lat & "," & lon & "); }" & vbCrLf & _
        '"</script>" & vbCrLf
    End Sub

End Class
