Public Class VEC_ARC
    

    Private mIdentificateur As String
    Public Property Identificateur() As String
        Get
            Return mIdentificateur
        End Get
        Set(ByVal value As String)
            mIdentificateur = value
        End Set
    End Property


    Private mRefSCD As Descripteur_Reference
    Public Property RefSCD() As Descripteur_Reference
        Get
            Return mRefSCD
        End Get
        Set(ByVal value As Descripteur_Reference)
            mRefSCD = value
        End Set
    End Property


    Private mCoordMini As Coordonee
    Public Property CoordMini() As Coordonee
        Get
            Return mCoordMini
        End Get
        Set(ByVal value As Coordonee)
            mCoordMini = value
        End Set
    End Property


    Private mCoordMaxi As Coordonee
    Public Property CoordMaxi() As Coordonee
        Get
            Return mCoordMaxi
        End Get
        Set(ByVal value As Coordonee)
            mCoordMaxi = value
        End Set
    End Property



    Private mArcType As TypeArc
    Public Property ArcType() As TypeArc
        Get
            Return mArcType
        End Get
        Set(ByVal value As TypeArc)
            mArcType = value
        End Set
    End Property


    Private mNbPoint As Integer
    Public Property NbPoint() As Integer
        Get
            Return mNbPoint
        End Get
        Set(ByVal value As Integer)
            mNbPoint = value
            mPoints = New System.Collections.Generic.List(Of Coordonee)
        End Set
    End Property


    Private mPoints As System.Collections.Generic.List(Of Coordonee)
    Public Property Points() As System.Collections.Generic.List(Of Coordonee)
        Get
            Return mPoints
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Coordonee))
            mPoints = value
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


    Public Sub Affecte(ByVal ZL As ListeZone, ByVal cur As Integer)
        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "RID" Then
            mErreur = ErreurStructure.VECARC
            Exit Sub
        End If

        mIdentificateur = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "SCP" Then
            mErreur = ErreurStructure.VECARC
            Exit Sub
        End If

        mRefSCD = New Descripteur_Reference
        mRefSCD = ZL.ListeZ(cur).GetDescripteur

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 = "CM1" Then
            mCoordMini = ZL.ListeZ(cur).GetCoordonnees
            cur = cur + 1
            If ZL.ListeZ(cur).Z1 = "CM2" Then
                mCoordMaxi = ZL.ListeZ(cur).GetCoordonnees
                cur = cur + 1
            End If
        End If

        If ZL.ListeZ(cur).Z1 <> "TYP" Then
            mErreur = ErreurStructure.VECARC
            Exit Sub
        End If

        mArcType = ZL.ListeZ(cur).Z6

        cur = cur + 1
        If ZL.ListeZ(cur).Z1 <> "PTC" Then
            mErreur = ErreurStructure.VECARC
            Exit Sub
        End If

        mNbPoint = ZL.ListeZ(cur).Z6



        mPoints = New System.Collections.Generic.List(Of Coordonee)

        cur = cur + 1
        For i = 0 To mNbPoint - 1
            Dim co As New Coordonee
            co = ZL.ListeZ(cur).GetCoordonnees
            mPoints.Add(co)
            cur = cur + 1
        Next

        If ZL.ListeZ(cur).Z1 <> "ATC" Then
            mErreur = ErreurStructure.VECARC
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
