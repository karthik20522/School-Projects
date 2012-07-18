Imports System.Data
Imports System.Data.SqlClient

Public Class admin
    Inherits System.Web.UI.Page

    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Dim sdr As SqlDataReader
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents DropDownList1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents DropDownList2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents DropDownList3 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents DropDownList4 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents Image2 As System.Web.UI.WebControls.Image
    Protected WithEvents Image3 As System.Web.UI.WebControls.Image
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Dim listbox1 As New ListBox

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Calendar1 As System.Web.UI.WebControls.Calendar

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
        con = New SqlConnection
        cmd = New SqlCommand
        con.ConnectionString = "server=don;database=master;integrated security=sspi"

        
        Dim i As Integer = 0
        If Page.IsPostBack = False Then
            Try
                con.Open()
                cmd.Parameters.Clear()
                cmd.CommandText = "select p_number from package_info"
                cmd.Connection = con
                sdr = cmd.ExecuteReader
                While sdr.Read
                    DropDownList1.Items.Add(sdr(0))
                End While
                sdr.Close()

                cmd.Parameters.Clear()
                cmd.CommandText = "select tracking_no from track_info"
                cmd.Connection = con
                sdr = cmd.ExecuteReader
                While sdr.Read
                    DropDownList3.Items.Add(sdr(0))
                End While
                sdr.Close()
                con.Close()
            Catch ex As Exception
                TextBox1.Text = ex.ToString
            End Try
        End If

        Try
            con.Open()
            cmd.Parameters.Clear()
            cmd.CommandText = "select distinct dt from truck_geo"
            cmd.Connection = con
            sdr = cmd.ExecuteReader
            While sdr.Read
                If i = 0 Then
                    listbox1.Items.Add(Date.Parse(sdr(0)).Date)
                    i = i + 1
                End If

                If i > 0 Then
                    If Date.Parse(sdr(0)).Date = Date.Parse(listbox1.Items(i - 1).ToString).Date Then
                    Else
                        listbox1.Items.Add(Date.Parse(sdr(0)).Date)
                        i = i + 1
                    End If
                End If
            End While
            sdr.Close()
            con.Close()
        Catch ex As Exception
            TextBox1.Text = ex.ToString
        End Try

        'End If
        
    End Sub

    Private Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles Calendar1.DayRender

        Dim vacationStyle As New Style
        With vacationStyle
            .Font.Bold = True

            .BackColor = System.Drawing.Color.Black
            '.BorderColor = System.Drawing.Color.Purple
            '.BorderWidth = New Unit(3)
        End With

        ' Vacation is from Nov 23, 2005 to Nov 30, 2005.
        Dim ss As String
        Dim i As Integer = ListBox1.Items.Count
        For x As Integer = 0 To i - 1
            If e.Day.Date = New Date(Date.Parse(ListBox1.Items(x).ToString).Year, Date.Parse(ListBox1.Items(x).ToString).Month, Date.Parse(ListBox1.Items(x).ToString).Day) Then '(2006, 3, 19) Then
                e.Cell.ApplyStyle(vacationStyle)
            End If
        Next
        

    End Sub

    Private Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged
        Response.Write("Button clicked")
    End Sub
End Class
