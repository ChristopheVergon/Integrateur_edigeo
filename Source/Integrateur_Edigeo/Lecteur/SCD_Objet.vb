Public Class SCD_Objet
    

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


    Private mNature As NatureObjetSCD
    Public Property Nature() As NatureObjetSCD
        Get
            Return mNature
        End Get
        Set(ByVal value As NatureObjetSCD)
            mNature = value
        End Set
    End Property


    Private mNbAttribut As Integer
    Public Property NbAttribut() As Integer
        Get
            Return mNbAttribut
        End Get
        Set(ByVal value As Integer)
            mNbAttribut = value
            If mNbAttribut > 0 Then
                mListeRefAttribut = New ListeDescripteurReference
            End If
        End Set
    End Property

    Private mListeRefAttribut As ListeDescripteurReference
    Public Property ListeRefAttribut() As ListeDescripteurReference
        Get
            Return mListeRefAttribut
        End Get
        Set(ByVal value As ListeDescripteurReference)
            mListeRefAttribut = value
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
            mErreur = ErreurStructure.SCDOBJ
            Exit Sub
        End If

        mIdentificateur = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "DIP" Then
            mErreur = ErreurStructure.SCDOBJ
            Exit Sub
        End If
        mRefNomenclature = New Descripteur_Reference
        mRefNomenclature = ZL.ListeZ(cur).GetDescripteur

        cur = cur + 1
        If ZL.ListeZ(cur).Z1 <> "KND" Then
            mErreur = ErreurStructure.SCDOBJ
            Exit Sub
        End If

        mNature = ZL.ListeZ(cur).GetNatureObjet

        cur = cur + 1
        If ZL.ListeZ(cur).Z1 <> "AAC" Then
            mErreur = ErreurStructure.SCDOBJ
            Exit Sub
        End If

        mNbAttribut = ZL.ListeZ(cur).Z6

        If mNbAttribut > 0 Then
            mListeRefAttribut = New ListeDescripteurReference
            cur = cur + 1
            For i = 0 To mNbAttribut - 1
                If ZL.ListeZ(cur).Z1 <> "AAP" Then
                    mErreur = ErreurStructure.SCDOBJ
                    Exit Sub
                End If
                Dim des As New Descripteur_Reference
                des = ZL.ListeZ(cur).GetDescripteur
                mListeRefAttribut.Add(des)
                cur = cur + 1
            Next
        End If
        

    End Sub

    Private Sub ConcText(ByVal zll As ListeZone, ByVal ZN As String, ByRef cur As Integer, ByRef mval As String)
        If zll.ListeZ(cur).Z1 = "TEX" Then
            cur = cur + 1
        End If

        If zll.ListeZ(cur).Z1 <> ZN Then
            mErreur = ErreurStructure.SCDOBJ
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
