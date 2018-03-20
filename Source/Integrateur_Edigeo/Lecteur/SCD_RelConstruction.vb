Public Class SCD_RelConstruction
   
    Private mIdentificateur As String
    Public Property Identificateur() As String
        Get
            Return mIdentificateur
        End Get
        Set(ByVal value As String)
            mIdentificateur = value
        End Set
    End Property


    Private mNatureRelation As NatureRel
    Public Property NatureRelation() As NatureRel
        Get
            Return mNatureRelation
        End Get
        Set(ByVal value As NatureRel)
            mNatureRelation = value
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


    Private mNbElementType As Integer
    Public Property NbElementType() As Integer
        Get
            Return mNbElementType
        End Get
        Set(ByVal value As Integer)
            mNbElementType = value
            If value > 0 Then
                mRefSCDElement = New ListeDescripteurReference
            End If
        End Set
    End Property


    Private mRefSCDElement As ListeDescripteurReference
    Public Property RefSCDElement() As ListeDescripteurReference
        Get
            Return mRefSCDElement
        End Get
        Set(ByVal value As ListeDescripteurReference)
            mRefSCDElement = value
        End Set
    End Property


    Private mNbOccElement As System.Collections.Generic.List(Of Integer)
    Public Property NbOccElement() As System.Collections.Generic.List(Of Integer)
        Get
            Return mNbOccElement
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Integer))
            mNbOccElement = value
        End Set
    End Property

    Private mNbAttribut As Integer
    Public Property NbAttribut() As Integer
        Get
            Return mNbAttribut
        End Get
        Set(ByVal value As Integer)
            mNbAttribut = value
            If value > 0 Then
                mRefSCDElement = New ListeDescripteurReference
            End If
        End Set
    End Property


    Private mRefSCDAttribut As ListeDescripteurReference
    Public Property RefSCDAttribut() As ListeDescripteurReference
        Get
            Return mRefSCDAttribut
        End Get
        Set(ByVal value As ListeDescripteurReference)
            mRefSCDAttribut = value
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
            mErreur = ErreurStructure.SCDRELCONSTR
            Exit Sub
        End If

        mIdentificateur = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "KND" Then
            mErreur = ErreurStructure.SCDRELCONSTR
            Exit Sub
        End If

        mNatureRelation = ZL.ListeZ(cur).GetNatureRelation

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "CA1" Then
            mErreur = ErreurStructure.SCDRELCONSTR
            Exit Sub
        End If

        mCardMini = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "CA2" Then
            mErreur = ErreurStructure.SCDRELCONSTR
            Exit Sub
        End If

        mCardMaxi = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "SCC" Then
            mErreur = ErreurStructure.SCDRELCONSTR
            Exit Sub
        End If

        mNbElementType = ZL.ListeZ(cur).Z6

        If mNbElementType > 0 Then
            mRefSCDElement = New ListeDescripteurReference
        Else
            mErreur = ErreurStructure.SCDRELCONSTR
            Exit Sub
        End If

        cur = cur + 1
        For i = 0 To mNbElementType - 1
            If ZL.ListeZ(cur).Z1 <> "SCP" Then
                mErreur = ErreurStructure.SCDRELCONSTR
                Exit Sub
            End If
            Dim des As New Descripteur_Reference
            des = ZL.ListeZ(cur).GetDescripteur
            cur = cur + 1
            If ZL.ListeZ(cur).Z1 <> "OCC" Then
                mErreur = ErreurStructure.SCDRELCONSTR
                Exit Sub
            End If
            des.NbOccurence = ZL.ListeZ(cur).Z6
            mRefSCDElement.Add(des)
            cur = cur + 1
        Next

        If ZL.ListeZ(cur).Z1 <> "AAC" Then
            mErreur = ErreurStructure.SCDRELCONSTR
            Exit Sub
        End If
        mNbAttribut = ZL.ListeZ(cur).Z6

        If mNbAttribut > 0 Then
            mRefSCDAttribut = New ListeDescripteurReference
            cur = cur + 1
            For i = 0 To mNbAttribut - 1
                If ZL.ListeZ(cur).Z1 <> "AAP" Then
                    mErreur = ErreurStructure.SCDRELCONSTR
                    Exit Sub
                End If
                Dim des As New Descripteur_Reference
                des = ZL.ListeZ(cur).GetDescripteur
                mRefSCDAttribut.Add(des)
                cur = cur + 1
            Next
        End If



    End Sub
End Class
