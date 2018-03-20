Public Class SCD_Primitive
   
    Private mIdentificateur As String
    Public Property Identificateur() As String
        Get
            Return mIdentificateur
        End Get
        Set(ByVal value As String)
            mIdentificateur = value
        End Set
    End Property


    Private mNatureElement As NaturePrimitive
    Public Property NatureElement() As NaturePrimitive
        Get
            Return mNatureElement
        End Get
        Set(ByVal value As NaturePrimitive)
            mNatureElement = value
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
            mErreur = ErreurStructure.SCDPRIM
            Exit Sub
        End If

        mIdentificateur = ZL.ListeZ(cur).Z6



        

        cur = cur + 1
        If ZL.ListeZ(cur).Z1 <> "KND" Then
            mErreur = ErreurStructure.SCDPRIM
            Exit Sub
        End If

        mNatureElement = ZL.ListeZ(cur).GetNatureObjet

        cur = cur + 1
        If ZL.ListeZ(cur).Z1 <> "AAC" Then
            mErreur = ErreurStructure.SCDPRIM
            Exit Sub
        End If

        mNbAttribut = ZL.ListeZ(cur).Z6

        If mNbAttribut > 0 Then
            mListeRefAttribut = New ListeDescripteurReference
        End If
        cur = cur + 1
        For i = 0 To mNbAttribut - 1
            If ZL.ListeZ(cur).Z1 <> "AAP" Then
                mErreur = ErreurStructure.SCDPRIM
                Exit Sub
            End If
            Dim des As New Descripteur_Reference
            des = ZL.ListeZ(cur).GetDescripteur
            mListeRefAttribut.Add(des)
            cur = cur + 1
        Next

    End Sub

    

   
End Class
