Public Class DIC_Objet

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
    End Sub

    Private Sub ConcText(ByVal zll As ListeZone, ByVal ZN As String, ByRef cur As Integer, ByRef mval As String)
        If zll.ListeZ(cur).Z1 = "TEX" Then
            cur = cur + 1
        End If

        If zll.ListeZ(cur).Z1 <> ZN Then
            mErreur = ErreurStructure.DICOBJ
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
