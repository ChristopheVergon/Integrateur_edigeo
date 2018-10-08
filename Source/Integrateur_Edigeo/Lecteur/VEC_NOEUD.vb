Public Class VEC_NOEUD
    Inherits VEC_ABS

    Private mRefSCD As Descripteur_Reference
    Public Property RefSCD() As Descripteur_Reference
        Get
            Return mRefSCD
        End Get
        Set(ByVal value As Descripteur_Reference)
            mRefSCD = value
        End Set
    End Property


    Private mNoeudType As TypeNoeud
    Public Property NoeudType() As TypeNoeud
        Get
            Return mNoeudType
        End Get
        Set(ByVal value As TypeNoeud)
            mNoeudType = value
        End Set
    End Property


    Private mPoint As Coordonee
    Public Property Point() As Coordonee
        Get
            Return mPoint
        End Get
        Set(ByVal value As Coordonee)
            mPoint = value
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
                mSCDAttribut = New ListeDescripteurReference

                mValeurAtt = New System.Collections.Generic.List(Of String)
            End If
        End Set
    End Property


    Private mSCDAttribut As ListeDescripteurReference
    Public Property SCDAttribut() As ListeDescripteurReference
        Get
            Return mSCDAttribut
        End Get
        Set(ByVal value As ListeDescripteurReference)
            mSCDAttribut = value
        End Set
    End Property

    Private mValeurAtt As System.Collections.Generic.List(Of String)
    Public Property ValeurAtt() As System.Collections.Generic.List(Of String)
        Get
            Return mValeurAtt
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            mValeurAtt = value
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


    Public Overrides Sub Affecte(ByVal ZL As ListeZone, ByVal cur As Integer)
        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "RID" Then
            mErreur = ErreurStructure.VECNOEUD
            Exit Sub
        End If

        Identificateur = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "SCP" Then
            mErreur = ErreurStructure.VECNOEUD
            Exit Sub
        End If

        mRefSCD = New Descripteur_Reference
        mRefSCD = ZL.ListeZ(cur).GetDescripteur

        cur = cur + 1


        If ZL.ListeZ(cur).Z1 <> "TYP" Then
            mErreur = ErreurStructure.VECNOEUD
            Exit Sub
        End If

        mNoeudType = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "COR" Then
            mErreur = ErreurStructure.VECNOEUD
            Exit Sub
        End If

        mPoint = ZL.ListeZ(cur).GetCoordonnees

        cur = cur + 1
        If ZL.ListeZ(cur).Z1 <> "ATC" Then
            mErreur = ErreurStructure.VECNOEUD
            Exit Sub
        End If

        mNbAttribut = ZL.ListeZ(cur).Z6

        If mNbAttribut > 0 Then
            mSCDAttribut = New ListeDescripteurReference
            mValeurAtt = New System.Collections.Generic.List(Of String)
        End If

        cur = cur + 1

        For i = 0 To NbAttribut - 1
            Dim des As New Descripteur_Reference
            des = ZL.ListeZ(cur).GetDescripteur
            mSCDAttribut.Add(des)
            cur = cur + 1
            mValeurAtt.Add(ZL.ListeZ(cur).Z6)
            cur = cur + 1
        Next

    End Sub
End Class
