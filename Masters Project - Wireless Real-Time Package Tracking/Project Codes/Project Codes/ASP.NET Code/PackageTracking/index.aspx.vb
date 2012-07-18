Imports System.Data
Imports System.Data.SqlClient

Public Class WebForm1
    Inherits System.Web.UI.Page

    Dim con As SqlConnection
    Protected WithEvents HyperLink1 As System.Web.UI.WebControls.HyperLink
    Dim cmd As SqlCommand


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label

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
        'Put user code to initialize the page here
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            Dim s1, s2, s3 As SqlDataReader
            Dim tn As String = ""
            Dim flag As Integer

            con = New SqlConnection
            con.ConnectionString = "server = DON;integrated security = SSPI; database = master"
            cmd = New SqlCommand
            con.Open()
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select r.flag_no from rfid_info r, track_info t where t.p_number = r.p_number and t.tracking_no='" + TextBox1.Text + "'"
            cmd.Connection = con
            s3 = cmd.ExecuteReader
            While s3.Read
                flag = Integer.Parse(s3(0))
            End While
            con.Close()
            s3.Close()

            If flag = 3 Then
                Response.Redirect("delivered.aspx?tno5=" & TextBox1.Text)
            End If

            con.Open()
            cmd.Parameters.Clear()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from track_info where tracking_no = '" & TextBox1.Text & "'"
            cmd.Connection = con
            s1 = cmd.ExecuteReader
            If s1.Read Then
                con.Close()
                con.Open()
                cmd.Parameters.Clear()
                cmd.CommandText = "select a.truck_no from truck_track a, track_info b where a.tracking_no = '" & TextBox1.Text & "'"
                s2 = cmd.ExecuteReader
                While s2.Read
                    tn = s2(0)
                End While
                s2.Close()
                con.Close()
                s1.Close()
                Response.Redirect("track.aspx?tno=" & tn)
            End If


        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Label2.Text = ex.ToString

        End Try

    End Sub
End Class
