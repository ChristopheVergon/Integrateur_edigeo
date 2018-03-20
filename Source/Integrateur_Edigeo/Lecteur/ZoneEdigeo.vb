Public Class ZoneEdigeo

    Private mZ1 As String
    Public ReadOnly Property Z1() As String
        Get
            Return mZ1
        End Get
        
    End Property

    Private mZ2 As String
    Public ReadOnly Property Z2() As String
        Get
            Return mZ2
        End Get
        
    End Property
    Private mZ3 As String
    Public ReadOnly Property Z3() As String
        Get
            Return mZ3
        End Get

    End Property
    Private mZ4 As String
    Public ReadOnly Property Z4() As String
        Get
            Return mZ4
        End Get

    End Property

    Private mZ5 As String
    Public ReadOnly Property Z5() As String
        Get
            Return mZ5
        End Get

    End Property

    Private mZ6 As String
    Public ReadOnly Property Z6() As String
        Get
            Return mZ6
        End Get

    End Property

    Private mErreur As ErreurLectureEDIGEO
    Public Property Erreur() As ErreurLectureEDIGEO
        Get
            Return mErreur
        End Get
        Set(ByVal value As ErreurLectureEDIGEO)
            mErreur = value
        End Set
    End Property

    Public Sub New(ByVal L As String)
        If L = "" Then
            mErreur = ErreurLectureEDIGEO.VIDE
            Exit Sub
        End If
        If L.Length < 8 Then
            mErreur = ErreurLectureEDIGEO.LEN8
            Exit Sub
        End If
        If L.Substring(7, 1) <> ":" Then
            mErreur = ErreurLectureEDIGEO.PT2
            Exit Sub
        End If
        mZ5 = ":"
        

        mZ1 = L.Substring(0, 3)
        mZ2 = L.Substring(3, 1)
        mZ3 = L.Substring(4, 1)
        mZ4 = L.Substring(5, 2)
        If L.Length > 8 Then
            mZ6 = L.Substring(8, L.Length - 8)
        Else
            mZ6 = ""
        End If



        If mZ6.Length <> Val(mZ4) Then
            'mErreur = ErreurLectureEDIGEO.LEND
            mErreur = ErreurLectureEDIGEO.NON
        End If

        If mErreur = 0 Then
            mErreur = ErreurLectureEDIGEO.NON
        End If

    End Sub

    Public Function GetDescripteur() As Descripteur_Reference
        Dim des As New Descripteur_Reference

        If mZ3 <> "P" Then
            MsgBox("Ce champs n'est pas un descripteur de référence", MsgBoxStyle.Critical)
            Return Nothing
        End If

        Dim res() As String = mZ6.Split(";")

        If res.Count <> 4 Then
            mErreur = ErreurLectureEDIGEO.DESC4
            Return Nothing
        End If

        des.Groupe = res(0)
        des.Modele = res(1)
        des.DescripteurType = res(2)
        des._ID = res(3)

        Return des
    End Function
    Public Function GetNatureObjet() As NatureObjetSCD

        Select Case mZ6
            Case "CPX"
                Return NatureObjetSCD.CPX
            Case "PCT"
                Return NatureObjetSCD.PCT
            Case "LIN"
                Return NatureObjetSCD.LIN
            Case "ARE"
                Return NatureObjetSCD.ARE
        End Select

    End Function

    Public Function GetNatureRelation() As NatureRel
       
        Select Case mZ6
            Case "ICO"
                Return NatureRel.ICO
            Case "IDB"
                Return NatureRel.IDB
            Case "IDR"
                Return NatureRel.IDR
            Case "IND"
                Return NatureRel.IND
            Case "FND"
                Return NatureRel.FND
            Case "LPO"
                Return NatureRel.LPO
            Case "RPO"
                Return NatureRel.RPO
            Case "ILI"
                Return NatureRel.ILI
            Case "BET"
                Return NatureRel.BET
        End Select
    End Function

    Public Function GetCoordonnees() As Coordonee
        Dim co As New Coordonee
        If mZ3 <> "C" Then
            MsgBox("La ligne n'est pas un champs coordonnées", MsgBoxStyle.Critical)
            Return Nothing
        End If

        If mZ6 = "" Then
            Return Nothing
        End If

        Dim res() As String = mZ6.Split(";")
        co.X = Val(res(0))
        co.Y = Val(res(1))
        If res.Count > 2 And res(2) <> "" Then
            co.Z = Val(res(2))
        End If

        Return co
    End Function
End Class
