Attribute VB_Name = "Module2"
Public Function Decrypt(Key1 As String, txtcode As String)

Dim i, j, k As Integer, thekey
Dim a, b, CryptText As String
On Error Resume Next
    CryptText = ""
    If Key1 <> "" Then
        thekey = Key1
        i = 0
        For j = 1 To Len(txtcode)
            i = i + 1
            If i > Len(thekey) Then i = 1
            a = Mid(txtcode, j, 1)
            k = Asc(a)
            b = Mid(thekey, i, 1)
            k = k - Asc(b)
            If k < 0 Then k = k + 255
            CryptText = CryptText & Chr(k)
        Next j
        txtcode = CryptText
     Decrypt = txtcode
        End If
       End Function


