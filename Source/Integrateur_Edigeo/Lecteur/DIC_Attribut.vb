Public Class DIC_Attribut
    Private mIdentificateur As String
    Public Property Identificateur() As String
        Get
            Return mIdentificateur
        End Get
        Set(ByVal value As String)
            mIdentificateur = value
        End Set
    End Property


    Private mCode As String
    Public Property Code() As String
        Get
            Return mCode
        End Get
        Set(ByVal value As String)
            mCode = value
        End Set
    End Property


    Private mDefinition As String
    Public Property Definition() As String
        Get
            Return mDefinition
        End Get
        Set(ByVal value As String)
            mDefinition = value
        End Set
    End Property


    Private mSourceDefinition As String
    Public Property SourceDefinition() As String
        Get
            Return mSourceDefinition
        End Get
        Set(ByVal value As String)
            mSourceDefinition = value
        End Set
    End Property


    Private mCategorie As String
    Public Property Categorie() As String
        Get
            Return mCategorie
        End Get
        Set(ByVal value As String)
            mCategorie = value
        End Set
    End Property


    Private mType As String
    Public Property _Type() As String
        Get
            Return mType
        End Get
        Set(ByVal value As String)
            mType = value
        End Set
    End Property


    Private mUnite As String
    Public Property Unite() As String
        Get
            Return mUnite
        End Get
        Set(ByVal value As String)
            mUnite = value
        End Set
    End Property


    Private mNbPreCode As Integer
    Public Property NbPreCode() As Integer
        Get
            Return mNbPreCode
        End Get
        Set(ByVal value As Integer)
            mNbPreCode = value
            If mNbPreCode > 0 Then
                mListePreCode = New System.Collections.Generic.List(Of Descripteur_PreCode)
            End If
        End Set
    End Property


    Private mListePreCode As System.Collections.Generic.List(Of Descripteur_PreCode)
    Public Property NewProperty() As System.Collections.Generic.List(Of Descripteur_PreCode)
        Get
            Return mListePreCode
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Descripteur_PreCode))
            mListePreCode = value
        End Set
    End Property

    Private mErreur As ErreurStructure
    Public Property Erreur() As ErreurStructure
        Get
            Return mErreur
        End Get
        Set(ByVal value As ErreurStructure)
            mErreur = value
        End Set
    End Property


    Public Sub Affecte(ByVal ZL As ListeZone, ByVal cur As Integer)
        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "RID" Then
            mErreur = ErreurStructure.DICOBJ
            Exit Sub
        End If

        mIdentificateur = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "LAB" Then
            mErreur = ErreurStructure.DICOBJ
            Exit Sub
        End If

        mCode = ZL.ListeZ(cur).Z6

        cur = cur + 1


        ConcText(ZL, "DEF", cur, mDefinition)

        ConcText(ZL, "ORI", cur, mSourceDefinition)

        If ZL.ListeZ(cur).Z1 <> "CAT" Then
            mErreur = ErreurStructure.DICOBJ
            Exit Sub
        End If

        mCategorie = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "TYP" Then
            mErreur = ErreurStructure.DICOBJ
            Exit Sub
        End If

        mType = ZL.ListeZ(cur).Z6

        cur = cur + 1

        ConcText(ZL, "UNI", cur, mUnite)

        If ZL.ListeZ(cur).Z1 <> "AVC" Then
            mErreur = ErreurStructure.DICOBJ
            Exit Sub
        End If

        mNbPreCode = ZL.ListeZ(cur).Z6

        If mNbPreCode > 0 Then
            mListePreCode = New System.Collections.Generic.List(Of Descripteur_PreCode)

            cur = cur + 1

            For i = 0 To mNbPreCode - 1
                Dim Pre As New Descripteur_PreCode

                If ZL.ListeZ(cur).Z1 <> "AVL" Then
                    mErreur = ErreurStructure.DICOBJ
                    Exit Sub
                End If

                Pre.Valeur = ZL.ListeZ(cur).Z6

                cur = cur + 1

                ConcText(ZL, "AVD", cur, Pre.Description)

                mListePreCode.Add(Pre)
            Next

        End If
    End Sub

    Private Sub ConcText(ByVal zll As ListeZone, ByVal ZN As String, ByRef cur As Integer, ByRef mval As String)
        If zll.ListeZ(cur).Z1 = "TEX" Then
            cur = cur + 1
        End If

        If zll.ListeZ(cur).Z1 <> ZN Then
            mErreur = ErreurStructure.DICATT
            Exit Sub
        End If

        mval = zll.ListeZ(cur).Z6

        cur = cur + 1

        Do While zll.ListeZ(cur).Z1 = "NEX"
            mval = mval & zll.ListeZ(cur).Z6
            cur = cur + 1
        Loop

    End Sub
End Class
