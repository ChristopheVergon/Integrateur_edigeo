Public Class SCD_RelSemantique
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

    Private mCardMini As Integer
    Public Property CardMini() As Integer
        Get
            Return mCardMini
        End Get
        Set(ByVal value As Integer)
            mCardMini = value
        End Set
    End Property


    Private mCardMaxi As Integer
    Public Property CardMaxi() As Integer
        Get
            Return mCardMaxi
        End Get
        Set(ByVal value As Integer)
            mCardMaxi = value
        End Set
    End Property


    Private mNbTypeObjet As Integer
    Public Property NbTypeObjet() As Integer
        Get
            Return mNbTypeObjet
        End Get
        Set(ByVal value As Integer)
            mNbTypeObjet = value
            If value > 0 Then
                mRefSCDTypeObjet = New ListeDescripteurReference
            End If
        End Set
    End Property


    Private mRefSCDTypeObjet As ListeDescripteurReference
    Public Property RefSCDTypeObjet() As ListeDescripteurReference
        Get
            Return mRefSCDTypeObjet
        End Get
        Set(ByVal value As ListeDescripteurReference)
            mRefSCDTypeObjet = value
        End Set
    End Property

    Private mNbTypeAttribut As Integer
    Public Property NbTypeAttribut() As Integer
        Get
            Return mNbTypeAttribut
        End Get
        Set(ByVal value As Integer)
            mNbTypeAttribut = value
            If value > 0 Then
                mRefSCDTypeAttribut = New ListeDescripteurReference
            End If
        End Set
    End Property


    Private mRefSCDTypeAttribut As ListeDescripteurReference
    Public Property RefSCDTypeAttribut() As ListeDescripteurReference
        Get
            Return mRefSCDTypeAttribut
        End Get
        Set(ByVal value As ListeDescripteurReference)
            mRefSCDTypeAttribut = value
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
            mErreur = ErreurStructure.SCDRELSEM
            Exit Sub
        End If

        mIdentificateur = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "DIP" Then
            mErreur = ErreurStructure.SCDRELSEM
            Exit Sub
        End If
        mRefNomenclature = New Descripteur_Reference
        mRefNomenclature = ZL.ListeZ(cur).GetDescripteur

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "CA1" Then
            mErreur = ErreurStructure.SCDRELSEM
            Exit Sub
        End If

        mCardMini = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "CA2" Then
            mErreur = ErreurStructure.SCDRELSEM
            Exit Sub
        End If

        mCardMaxi = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "SCC" Then
            mErreur = ErreurStructure.SCDRELSEM
            Exit Sub
        End If

        mNbTypeObjet = ZL.ListeZ(cur).Z6

        If mNbTypeObjet > 0 Then
            mRefSCDTypeObjet = New ListeDescripteurReference
        Else
            mErreur = ErreurStructure.SCDRELSEM
            Exit Sub
        End If

        cur = cur + 1
        For i = 0 To mNbTypeObjet - 1
            If ZL.ListeZ(cur).Z1 <> "SCP" Then
                mErreur = ErreurStructure.SCDRELSEM
                Exit Sub
            End If
            Dim des As New Descripteur_Reference
            des = ZL.ListeZ(cur).GetDescripteur
            cur = cur + 1
            If ZL.ListeZ(cur).Z1 <> "OCC" Then
                mErreur = ErreurStructure.SCDRELSEM
                Exit Sub
            End If
            des.NbOccurence = ZL.ListeZ(cur).Z6
            mRefSCDTypeObjet.Add(des)
            cur = cur + 1
        Next

        If ZL.ListeZ(cur).Z1 <> "AAC" Then
            mErreur = ErreurStructure.SCDRELSEM
            Exit Sub
        End If
        mNbTypeAttribut = ZL.ListeZ(cur).Z6

        If mNbTypeAttribut > 0 Then
            mRefSCDTypeAttribut = New ListeDescripteurReference
            cur = cur + 1
            For i = 0 To mNbTypeAttribut - 1
                If ZL.ListeZ(cur).Z1 <> "AAP" Then
                    mErreur = ErreurStructure.SCDRELSEM
                    Exit Sub
                End If
                Dim des As New Descripteur_Reference
                des = ZL.ListeZ(cur).GetDescripteur
                mRefSCDTypeAttribut.Add(des)
                cur = cur + 1
            Next
        End If



    End Sub

End Class
