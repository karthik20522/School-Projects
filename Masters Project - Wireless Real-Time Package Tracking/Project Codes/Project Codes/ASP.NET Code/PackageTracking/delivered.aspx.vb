Imports System.Data.SqlClient

Public Class delivered
    Inherits System.Web.UI.Page

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim ds As SqlDataReader

    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label

    Dim tno6 As String = ""

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

        tno6 = Request.QueryString("tno5")

        con = New SqlConnection("server=don;integrated security=SSPI;database=master")
        Try
            cmd = New SqlCommand
            con.Open()
            cmd.Parameters.Clear()
            cmd.CommandText = "select p.d_c_name,p.d_c_address,t.dt from package_info p, rfid_info r, track_info f, truck_geo t, truck_track tt where f.p_number = p.p_number and f.p_number = r.p_number and f.tracking_no = tt.tracking_no and tt.truck_no = t.truck_no and f.tracking_no='" + tno6.ToString + "' and r.flag_no=3"
            cmd.Connection = con
            ds = cmd.ExecuteReader
            While ds.Read
                Label6.Text = tno6.ToString
                Label7.Text = ds(0)
                Label8.Text = ds(1)
                Label9.Text = DateTime.Parse(ds(2))
            End While
            con.Close()
            ds.Close()

        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
                ds.Close()
            End If
        End Try

    End Sub

End Class
