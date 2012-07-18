Imports System.Net
Imports System.Diagnostics
Imports System.Text
Imports System.Net.Sockets
Imports System.IO

Imports GeoFramework.Gps.Nmea
Imports GeoFramework.Licensing
Imports GeoFramework.Controls


Public Class Form1
    Inherits System.Windows.Forms.Form

    Const portNum As Integer = 10116
    Dim networkStream As networkStream


    Dim WithEvents GpsDevice As GeoFramework.Gps.Nmea.NmeaInterpreter
    'Dim setkey As GeoFrameworksLicense
    Dim WithEvents satelliteviewer As GeoFramework.Controls.SatelliteViewer
    Dim WithEvents satelliteSignalBar As GeoFramework.Controls.SatelliteSignalBar

    Dim WithEvents pm As PHIDGET.PhidgetManager
    Dim WithEvents rf1 As PHIDGET.PhidgetRFID
    Dim previousTag As String

    Dim t As Integer = 0
    Dim i As Integer = 0

    Dim s1_lat, s2_lat As String


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Timer1_tick As System.Windows.Forms.Timer
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Timer1_tick = New System.Windows.Forms.Timer(Me.components)
        Me.Label5 = New System.Windows.Forms.Label
        Me.ListBox2 = New System.Windows.Forms.ListBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox1.Location = New System.Drawing.Point(8, 192)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.ListBox1.Size = New System.Drawing.Size(424, 65)
        Me.ListBox1.TabIndex = 0
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 263)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(624, 22)
        Me.StatusBar1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 23)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Latitude"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 23)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Longitude"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(72, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 23)
        Me.Label3.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(72, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 23)
        Me.Label4.TabIndex = 6
        '
        'Timer1_tick
        '
        Me.Timer1_tick.Interval = 1000
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(32, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 40)
        Me.Label5.TabIndex = 7
        '
        'ListBox2
        '
        Me.ListBox2.Location = New System.Drawing.Point(440, 32)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(176, 225)
        Me.ListBox2.TabIndex = 8
        '
        'ComboBox1
        '
        Me.ComboBox1.Items.AddRange(New Object() {"T01", "T02", "T03", "T04", "T05", "T06", "T07", "T08", "T09", "T10", "T11"})
        Me.ComboBox1.Location = New System.Drawing.Point(120, 160)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(464, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(128, 23)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "RFID Tag Reader"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 160)
        Me.Label7.Name = "Label7"
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Select a Truck:"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(624, 285)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.ListBox1)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Client : GPS Program"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub connect()
        'Dim p As Process = New Process
        'p.StartInfo = New ProcessStartInfo("rasdial", " cingular ISPDA@CINGULARGPRS.COM CINGULAR1")
        'p.StartInfo.UseShellExecute = True
        'p.Start()
        'p.Close()
        Dim shell
        shell = CreateObject("WScript.Shell")
        shell.run("rasdial cingular ISPDA@CINGULARGPRS.COM CINGULAR1")
    End Sub

    Public Sub disconnect()
        Dim shell
        shell = CreateObject("WScript.Shell")
        shell.run("rasdial /disconnect")
    End Sub

    Public Sub gps_main()
        Try
            GpsDevice = New GeoFramework.Gps.Nmea.NmeaInterpreter
            satelliteviewer = New GeoFramework.Controls.SatelliteViewer
            satelliteSignalBar = New GeoFramework.Controls.SatelliteSignalBar

            Me.Controls.Add(Me.satelliteviewer)
            Me.Controls.Add(Me.satelliteSignalBar)

            StatusBar1.Panels.Add("Negotiating with Host..").AutoSize = StatusBarPanelAutoSize.Contents
            GpsDevice.Start()
            StatusBar1.Panels.Clear()

            satelliteSignalBar.BackColor = System.Drawing.Color.Transparent
            satelliteSignalBar.FixColor = System.Drawing.Color.LimeGreen
            satelliteSignalBar.GapWidth = 4
            satelliteSignalBar.IsPaintingOnSeparateThread = True
            satelliteSignalBar.Location = New System.Drawing.Point(5, 50)
            satelliteSignalBar.Name = "satelliteSignalBar1"
            satelliteSignalBar.Size = New System.Drawing.Size(200, 60)

            satelliteviewer.BackColor = System.Drawing.Color.White
            satelliteviewer.Effect = GeoFramework.Controls.PolarControlEffect.Glass
            satelliteviewer.IsPaintingOnSeparateThread = True
            satelliteviewer.Location = New System.Drawing.Point(210, 0)
            satelliteviewer.RotationInterpolationMethod = GeoFramework.InterpolationMethod.CubicEaseInOut
            satelliteviewer.Name = "satelliteviewer1"
            satelliteviewer.Size = New System.Drawing.Size(150, 150)

            StatusBar1.Panels.Add("Host Connect..").AutoSize = StatusBarPanelAutoSize.Contents
            'Timer1_tick.Enabled = True

        Catch ex As Exception
            StatusBar1.Panels.Clear()
            StatusBar1.Panels.Add("Host not Connected").AutoSize = StatusBarPanelAutoSize.Contents
            MsgBox("No Device Connected")
            GpsDevice.Stop()
        End Try

    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'connect()
        gps_main()
        pm = New PHIDGET.PhidgetManager
        previousTag = ""
        Timer1_tick.Enabled = True
    End Sub

    Private Sub GpsDevice_SentenceReceived(ByVal sender As Object, ByVal e As GeoFramework.Gps.Nmea.NmeaSentenceEventArgs) Handles GpsDevice.SentenceReceived
        ListBox1.Items.Insert(0, e.Sentence.ToString())
        If ListBox1.Items.Count() > 5 Then
            ListBox1.Items.RemoveAt(5)
        End If
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        GpsDevice.Stop()
    End Sub

    Private Sub GpsDevice_PositionChanged(ByVal sender As Object, ByVal e As GeoFramework.PositionEventArgs) Handles GpsDevice.PositionChanged
        Label3.Text = e.Position.Latitude.DecimalDegrees.ToString
        Label4.Text = e.Position.Longitude.DecimalDegrees.ToString
    End Sub

    Private Sub Timer1_tick_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1_tick.Tick
        t = 60 - i
        Label5.Text = t.ToString
        i = i + 1
        If t = 0 Then
            'connect()
            Timer1_tick.Enabled = False
            If ComboBox1.Text = "" Then
                MsgBox("Select a truck")
            Else
                Dim tcpClient As New TcpClient
                Try
                    tcpClient.Connect("69.142.127.130", portNum)
                    networkStream = tcpClient.GetStream
                    If networkStream.CanWrite And networkStream.CanRead Then
                        Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes(ComboBox1.Text + "," + Label3.Text + "," + Label4.Text + "," + System.DateTime.Now.ToString + vbCrLf)
                        networkStream.Write(sendBytes, 0, sendBytes.Length)
                        networkStream.Flush()
                        'networkStream.Close()
                    End If
                    If ListBox2.Items.Count > 0 Then

                        '  Dim j As Integer
                        Dim rf As String
                        ' Dim i As Integer = ListBox2.Items.Count
                        Dim rfid As String = ""
                        rfid = ",RFID,"

                        For Each rf In ListBox2.Items
                            rfid &= rf
                            If rf = "" Then
                            Else
                                rfid &= ","
                            End If
                        Next
                        '     MsgBox(rfid.ToString)
                        'networkStream = tcpClient.GetStream
                    If networkStream.CanWrite And networkStream.CanRead Then
                        Dim sendBytes1 As [Byte]() = Encoding.ASCII.GetBytes(rfid.ToString)
                        networkStream.Write(sendBytes1, 0, sendBytes1.Length)
                            networkStream.Flush()
                            ListBox2.Items.Clear()
                            '                          networkStream.Close()
                        End If

                    End If
                    'networkStream.Close()
                    tcpClient.Close()
                    '     disconnect()
                Catch ex As Exception
                    '      tcpClient.Close()
                    ' disconnect()
                    'MsgBox(ex.ToString)
                End Try
            End If
            i = 0
            Timer1_tick.Enabled = True

        End If

    End Sub

    Private Sub pm_OnAttach(ByVal PHIDGET As PHIDGET.IPhidget) Handles pm.OnAttach
        'ListBox2.Items.Add("Device Type : " & PHIDGET.DeviceType)
        'ListBox2.Items.Add("Device Version : " & PHIDGET.DeviceVersion)
        'ListBox2.Items.Add("Library Version : " & PHIDGET.LibraryVersion)
        'ListBox2.Items.Add("Serial Number : " & PHIDGET.SerialNumber)
        rf1 = PHIDGET
        rf1.OutputState(3) = True
        ListBox2.Items.Clear()
    End Sub

    Private Sub rf1_OnTag(ByVal TagNumber As String) Handles rf1.OnTag
        If previousTag = TagNumber Or TagNumber = "0800000000" Then
            Exit Sub
        End If

        previousTag = TagNumber
        ListBox2.Items.Add(TagNumber)
    End Sub

    Private Sub pm_OnError(ByVal PHIDGET As PHIDGET.IPhidget, ByVal Description As String, ByVal SCODE As Integer) Handles pm.OnError
        MsgBox(SCODE)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox1.Enabled = False
    End Sub
End Class
