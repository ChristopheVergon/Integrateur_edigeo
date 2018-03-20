Public Class SCD_Attribut

    Private mIdentificateur As String
    Public Property Identificateur() As String
        Get
            Return mIdentificateur
        End Get
        Set(ByVal value As String)
            mIdentificateur = value
        End Set
    End Property

    Private mRefNomenclature As Descripteur_Reference
    Public Property RefNomenclature() As Descripteur_Reference
        Get
            Return mRefNomenclature
        End Get
        Set(ByVal value As Descripteur_Reference)
            mRefNomenclature = value
        End Set
    End Property


    Private mNbChar As Integer
    Public Property NbChar() As Integer
        Get
            Return mNbChar
        End Get
        Set(ByVal value As Integer)
            mNbChar = value
        End Set
    End Property


    Private mNbDec As Integer
    Public Property NbDec() As Integer
        Get
            Return mNbDec
        End Get
        Set(ByVal value As Integer)
            mNbDec = value
        End Set
    End Property

    Private mNbExp As Integer
    Public Property NbExp() As Integer
        Get
            Return mNbExp
        End Get
        Set(ByVal value As Integer)
            mNbExp = value
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

    Private mMini As String
    Public Property Mini() As String
        Get
            Return mMini
        End Get
        Set(ByVal value As String)
            mMini = value
        End Set
    End Property


    Private mMaxi As String
    Public Property Maxi() As String
        Get
            Return mMaxi
        End Get
        Set(ByVal value As String)
            mMaxi = value
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


    Public Sub Affecte(ByVal ZL As ListeZone, ByVal cur As Integer, ByVal dico As DIC)
        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "RID" Then
            mErreur = ErreurStructure.SCDATT
            Exit Sub
        End If

        mIdentificateur = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "DIP" Then
            mErreur = ErreurStructure.SCDATT
            Exit Sub
        End If
        mRefNomenclature = New Descripteur_Reference
        mRefNomenclature = ZL.ListeZ(cur).GetDescripteur

        cur = cur + 1
        If ZL.ListeZ(cur).Z1 <> "CAN" Then
            mErreur = ErreurStructure.SCDATT
            Exit Sub
        End If

        mNbChar = ZL.ListeZ(cur).Z6

        Dim Att As DIC_Attribut
        Att = dico.DictionaryAttribut.Item(mRefNomenclature._ID)

        Select Case Att._Type
            Case "E"
                cur = cur + 1
                If ZL.ListeZ(cur).Z1 <> "CAD" Then
                    mErreur = ErreurStructure.SCDATT
                    Exit Sub
                End If
                mNbDec = ZL.ListeZ(cur).Z6
                cur = cur + 1
                If ZL.ListeZ(cur).Z1 <> "CAE" Then
                    mErreur = ErreurStructure.SCDATT
                    Exit Sub
                End If
                mNbExp = ZL.ListeZ(cur).Z6
            Case "R"
                cur = cur + 1
                If ZL.ListeZ(cur).Z1 <> "CAD" Then
                    mErreur = ErreurStructure.SCDATT
                    Exit Sub
                End If
                mNbDec = ZL.ListeZ(cur).Z6


        End Select

        cur = cur + 1
        If ZL.ListeZ(cur).Z1 <> "UNI" Then
        Else
            mUnite = ZL.ListeZ(cur).Z6
        End If

        cur = cur + 1
        If ZL.ListeZ(cur).Z1 <> "AV1" Then
        Else
            mMini = ZL.ListeZ(cur).Z6
        End If

        cur = cur + 1
        If ZL.ListeZ(cur).Z1 <> "AV2" Then
        Else
            mMaxi = ZL.ListeZ(cur).Z6
        End If

    End Sub

    
End Class
