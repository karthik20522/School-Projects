Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient

Public Class track
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ListBox1 As System.Web.UI.WebControls.ListBox
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim s1 As SqlDataReader

    Dim tno1 As String = ""
    Dim i As Integer = 0
    Dim lat As String '= "40.748187"
    Dim lon As String '= "-74.154425"
    Dim cc As Integer = 0
    Dim xml_loc As String
    Public cur As String
    Dim xml As XmlTextReader
    'Dim listbox1 As New ListBox



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tno1 = Request.QueryString("tno")
        If Not Page.IsPostBack Then
            Try
                con = New SqlConnection
                con.ConnectionString = "server = don; integrated security=SSPI; database=master"
                con.Open()
                cmd = New SqlCommand
                cmd.Parameters.Clear()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "select * from truck_geo where truck_no = '" & tno1.ToString & "'"
                cmd.Connection = con
                s1 = cmd.ExecuteReader
                While s1.Read
                    lat = s1(1)
                    lon = s1(2)
                End While
                s1.Close()

                cmd.Parameters.Clear()
                cmd.CommandText = "select distinct dt from truck_geo where truck_no= '" & tno1.ToString & "'"
                cmd.Connection = con
                s1 = cmd.ExecuteReader
                While s1.Read
                    If i = 0 Then
                        DropDownList1.Items.Add(Date.Parse(s1(0)).Date)
                        i = i + 1
                    End If

                    If i > 0 Then
                        If Date.Parse(s1(0)).Date = Date.Parse(DropDownList1.Items(i - 1).ToString).Date Then
                        Else
                            DropDownList1.Items.Add(Date.Parse(s1(0)).Date)
                            i = i + 1
                        End If
                    End If
                End While
                s1.Close()
                con.Close()

            Catch ex As Exception
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End Try
            
        End If
        'lat = "40.748187"
        'lon = "-74.154425"
        '   Label3.Text = ""
        TextBox1.Text = ""
        ' TextBox1.Text = "Current Location" & vbCrLf
        Try
            xml_loc = "http://imaptools.com/rgeo/?y=" & lat & "&x=" & lon

            xml = New XmlTextReader(xml_loc) '("http://imaptools.com/rgeo/?y=40.748187&x=-74.154425")
            Dim streetname As String = xml.NameTable.Add("streetAddress")
            While xml.Read
                If xml.NodeType = XmlNodeType.Element Then
                    If xml.Name.Equals(streetname) Then
                        TextBox1.Text &= xml.ReadString.Trim & vbCrLf
                        cc = ListBox1.Items.Count + 2
                    End If
                    ListBox1.Items.Add(xml.ReadString.Trim)
                End If

            End While
            xml.Close()


            TextBox1.Text &= ListBox1.Items(21).ToString & vbCrLf & ListBox1.Items(12).ToString & vbCrLf & ListBox1.Items(cc).ToString & vbCrLf & ListBox1.Items(3).ToString

        Catch ex As Exception
            TextBox1.Text &= vbCrLf & vbCrLf & "XML ERROR"
        End Try


        cur = "<script language=""javascript"">" & vbCrLf & _
        "function load_me(){" & vbCrLf & "var params = new Object();" & vbCrLf & _
  "params.latitude =" & lat & ";" & vbCrLf & _
  "params.longitude = " & lon & ";" & vbCrLf & _
  "params.zoomlevel = 16;" & vbCrLf & _
  "params.mapstyle = Msn.VE.MapStyle.Road;" & vbCrLf & _
  "params.showScaleBar = true;" & vbCrLf & _
  "params.showDashboard = true;" & vbCrLf & _
  "params.dashboardSize = Msn.VE.DashboardSize.Normal;" & vbCrLf & _
  "params.dashboardX = 3;" & vbCrLf & _
  "params.dashboardY = 3; " & vbCrLf & _
  "params.obliqueEnabled = true;" & vbCrLf & _
  "params.obliqueUrl = ""http://local.live.com/imagery.ashx"";" & vbCrLf & _
  "map = 	new Msn.VE.MapControl(document.getElementById(""myMap""), params);" & vbCrLf & _
  "map.Init();" & vbCrLf & _
  "map.ClearPushpins();" & vbCrLf & _
  "map.AddPushpin('pin'," & lat & "," & lon & ",65,75,'pin','',2);" & vbCrLf & _
  "map.SetCenter(" & lat & "," & lon & "); }" & vbCrLf & _
  "</script>" & vbCrLf

        '   Page.RegisterStartupScript("Steal", cur)
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Response.Redirect("track_all.aspx?tno3=" & tno1.ToString)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect("track_all.aspx?tno3=" & tno1.ToString + "&dt=" & DropDownList1.SelectedItem.Text)
    End Sub
End Class
