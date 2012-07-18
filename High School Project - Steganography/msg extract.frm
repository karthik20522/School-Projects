VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form Form1 
   Caption         =   " Message Extraction..."
   ClientHeight    =   3990
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8250
   LinkTopic       =   "Form1"
   ScaleHeight     =   3990
   ScaleWidth      =   8250
   StartUpPosition =   3  'Windows Default
   Begin MSComDlg.CommonDialog CommonDialog1 
      Left            =   7080
      Top             =   3480
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Extract"
      Height          =   375
      Left            =   6360
      TabIndex        =   10
      Top             =   1680
      Width           =   1695
   End
   Begin VB.Frame Frame3 
      Caption         =   "Secret Message : "
      Height          =   1215
      Left            =   120
      TabIndex        =   8
      Top             =   2640
      Width           =   6135
      Begin VB.TextBox Text4 
         Appearance      =   0  'Flat
         BackColor       =   &H8000000A&
         Height          =   855
         Left            =   120
         TabIndex        =   9
         Top             =   240
         Width           =   5895
      End
   End
   Begin VB.Frame Frame2 
      Height          =   1575
      Left            =   120
      TabIndex        =   3
      Top             =   960
      Width           =   6135
      Begin VB.TextBox Text3 
         Height          =   375
         Left            =   1440
         TabIndex        =   7
         Top             =   960
         Width           =   3015
      End
      Begin VB.TextBox Text2 
         Height          =   375
         Left            =   1440
         TabIndex        =   6
         Top             =   240
         Width           =   4455
      End
      Begin VB.Label Label2 
         Caption         =   "Validation Key :"
         Height          =   375
         Left            =   120
         TabIndex        =   5
         Top             =   960
         Width           =   1215
      End
      Begin VB.Label Label1 
         Caption         =   "Encryption Key :"
         Height          =   375
         Left            =   120
         TabIndex        =   4
         Top             =   240
         Width           =   1335
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Encrypted Image Location :"
      Height          =   735
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   8055
      Begin VB.CommandButton Command1 
         Caption         =   "Browse"
         Height          =   375
         Left            =   6120
         TabIndex        =   2
         Top             =   240
         Width           =   1695
      End
      Begin VB.TextBox Text1 
         Height          =   375
         Left            =   120
         TabIndex        =   1
         Top             =   240
         Width           =   5775
      End
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()
CommonDialog1.Filter = "Jpg File|*.jpg|Bitmap File|*.bmp|"
CommonDialog1.ShowOpen
If Len(CommonDialog1.FileTitle) > 0 Then
Text1.Text = CommonDialog1.FileName
Else
Exit Sub
End If

End Sub

Private Sub Command2_Click()

If Text1.Text = "" Then
MsgBox "Please select the image file first", vbCritical, "User Error 01"
Exit Sub
End If

If Text2.Text = "" Or Len(Text2.Text) < 5 Then
MsgBox "The length of Encryption Key cannot be less than 5", vbCritical, "User error 02"
Exit Sub
End If
If Text3.Text = "" Then
MsgBox "Validation code is not provided", vbCritical, "User Error 03"
Exit Sub
End If

Clipboard.Clear
Dim validcode01 As Long
Dim validcode02 As Long
Dim extmessage As Byte
Dim dycrypted
Dim i As Long
validcode01 = Text3.Text
validcode02 = FileLen(Text1.Text)
Close #1
Text4.Text = ""


Open Text1.Text For Binary As #1

For i = validcode01 To validcode02

Get #1, i, extmessage
Clipboard.Clear
Clipboard.SetText extmessage

Text4.Text = Text4.Text + Chr(Clipboard.GetText)
Next i
Close #1
decrypted = Decrypt(Text2.Text, Text4.Text)
Text4.Text = decrypted

Exit Sub

End Sub
