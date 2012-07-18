VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form karthik 
   Caption         =   ">>>>> Stegnography <<<<<"
   ClientHeight    =   6735
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7995
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   6735
   ScaleWidth      =   7995
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox Text5 
      BackColor       =   &H8000000A&
      BorderStyle     =   0  'None
      Height          =   285
      Left            =   3600
      TabIndex        =   17
      Top             =   5280
      Width           =   1935
   End
   Begin MSComDlg.CommonDialog CommonDialog1 
      Left            =   0
      Top             =   6240
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton extract_msg 
      Caption         =   "Extract Message"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   4440
      TabIndex        =   16
      Top             =   6000
      Width           =   1935
   End
   Begin VB.CommandButton Hide_Msg 
      Caption         =   "Hide Message"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   1560
      TabIndex        =   14
      Top             =   6000
      Width           =   1935
   End
   Begin VB.Frame Frame2 
      Height          =   735
      Left            =   120
      TabIndex        =   11
      Top             =   4200
      Width           =   7455
      Begin VB.TextBox text4 
         Height          =   315
         Left            =   1440
         MaxLength       =   256
         TabIndex        =   13
         ToolTipText     =   "Enter the SECRET MESSAGE To Hide... Max Length 256."
         Top             =   240
         Width           =   5655
      End
      Begin VB.Label Label3 
         Caption         =   "Secret Message :"
         Height          =   255
         Left            =   120
         TabIndex        =   12
         Top             =   240
         Width           =   1335
      End
   End
   Begin VB.TextBox text3 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      IMEMode         =   3  'DISABLE
      Left            =   1560
      TabIndex        =   10
      ToolTipText     =   "Enter The SECRET PASSWORD...."
      Top             =   3480
      Width           =   5655
   End
   Begin VB.Frame Frame1 
      Height          =   735
      Left            =   120
      TabIndex        =   8
      Top             =   3240
      Width           =   7455
      Begin VB.Label Label2 
         Caption         =   "Encryption Key :"
         Height          =   255
         Left            =   120
         TabIndex        =   9
         Top             =   240
         Width           =   1215
      End
   End
   Begin VB.Frame Save_Location 
      Caption         =   "Save Location"
      Height          =   735
      Left            =   120
      TabIndex        =   5
      Top             =   2280
      Width           =   7455
      Begin VB.CommandButton Command2 
         Caption         =   "Browse"
         Height          =   375
         Left            =   5640
         TabIndex        =   7
         Top             =   240
         Width           =   1455
      End
      Begin VB.TextBox text2 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Left            =   120
         TabIndex        =   6
         Top             =   240
         Width           =   5295
      End
   End
   Begin VB.Frame Image_Location 
      Caption         =   "Image Location"
      Height          =   735
      Left            =   105
      TabIndex        =   2
      Top             =   1200
      Width           =   7455
      Begin VB.CommandButton Command1 
         Caption         =   "Browse"
         Height          =   375
         Left            =   5640
         Style           =   1  'Graphical
         TabIndex        =   4
         Top             =   240
         Width           =   1455
      End
      Begin VB.TextBox text1 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Left            =   120
         TabIndex        =   3
         Top             =   240
         Width           =   5295
      End
   End
   Begin VB.Frame Title 
      BackColor       =   &H00C00000&
      Height          =   1215
      Left            =   0
      TabIndex        =   0
      Top             =   -120
      Width           =   8295
      Begin VB.Label Title_text 
         BackColor       =   &H00C00000&
         Caption         =   "STEGNOGRAPHY"
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   20.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FFFFFF&
         Height          =   375
         Left            =   1680
         TabIndex        =   1
         Top             =   240
         Width           =   4575
      End
   End
   Begin VB.Label Label4 
      Caption         =   "Validation Key :"
      Height          =   255
      Left            =   2400
      TabIndex        =   15
      Top             =   5280
      Width           =   1215
   End
End
Attribute VB_Name = "Karthik"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()
Text1.Visible = True
CommonDialog1.Filter = "JPG Files|*.jpg|Bitmap Files|*.bmp|"
CommonDialog1.ShowOpen
If Len(CommonDialog1.FileTitle) > 0 Then
Text1.Text = CommonDialog1.FileName
Else
Exit Sub
End If

End Sub

Private Sub Command2_Click()
Text2.Visible = True
If Right(Text1.Text, 3) = "jpg" Or Right(Text1.Text, 3) = "JPG" Then
CommonDialog1.Filter = "Jpg File|*.jpg|"
Else
If Right(Text1.Text, 3) = "bmp" Or Right(Text1.Text, 3) = "BMP" Then
CommonDialog1.Filter = "Bitmap File|*.bmp|"
Else
Exit Sub
End If
End If

CommonDialog1.ShowSave
If Len(CommonDialog1.FileTitle) > 0 Then
Text2.Text = CommonDialog1.FileName
Else
Exit Sub
End If

End Sub

Private Sub extract_msg_Click()
Form1.Show

End Sub

Private Sub Hide_Msg_Click()

If Len(Text3.Text) < 3 Then
MsgBox "Encryption Key should be atleast 4 Characters", vbCritical, "User Error 01"
Exit Sub
End If

If Text1.Text = "" Or Text2.Text = "" Then
MsgBox "Please select the Source Image and Destination Image Location", vbCritical, "User error 02"
Exit Sub
End If


If Text4.Text = "" Then
MsgBox "Enter the secret message", vbCritical, "User Error"
Exit Sub
End If

Dim encryptor
encryptor = Encrypt(Text3.Text, Text4.Text)
Close #1
FileCopy Text1.Text, Text2.Text
Text5.Text = FileLen(Text1.Text)
Open Text2.Text For Binary As #1
Put #1, Val(Text5.Text) + 1, encryptor
Close #1
'Close #3
Text5.Text = Val(Text5.Text) + 4


End Sub
