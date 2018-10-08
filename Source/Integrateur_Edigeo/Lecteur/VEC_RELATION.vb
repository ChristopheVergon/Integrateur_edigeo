Public Class VEC_RELATION
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


    Private mNBElement As Integer
    Public Property NBElement() As Integer
        Get
            Return mNBElement
        End Get
        Set(ByVal value As Integer)
            mNBElement = value
            mElementRelation = New ListeDescripteurReference
        End Set
    End Property


    Private mSensComposition As System.Collections.Generic.List(Of String)
    Public Property SensComposition() As System.Collections.Generic.List(Of String)
        Get
            Return mSensComposition
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            mSensComposition = value
        End Set
    End Property


    Private mElementRelation As ListeDescripteurReference
    Public Property ElementRelation() As ListeDescripteurReference
        Get
            Return mElementRelation
        End Get
        Set(ByVal value As ListeDescripteurReference)
            mElementRelation = value
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
            mErreur = ErreurStructure.VECRELATION
            Exit Sub
        End If

        Identificateur = ZL.ListeZ(cur).Z6

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "SCP" Then
            mErreur = ErreurStructure.VECRELATION
            Exit Sub
        End If

        mRefSCD = New Descripteur_Reference
        mRefSCD = ZL.ListeZ(cur).GetDescripteur

        cur = cur + 1

        If ZL.ListeZ(cur).Z1 <> "FTC" Then
            mErreur = ErreurStructure.VECRELATION
            Exit Sub
        End If

        mNBElement = ZL.ListeZ(cur).Z6

        If NBElement < 2 Then
            mErreur = ErreurStructure.VECRELATION
        End If

        mElementRelation = New ListeDescripteurReference
        mSensComposition = New System.Collections.Generic.List(Of String)

        cur = cur + 1

        For i = 0 To NBElement - 1
            Dim des As New Descripteur_Reference
            des = ZL.ListeZ(cur).GetDescripteur
            mElementRelation.Add(des)
            cur = cur + 1

            If ZL.ListeZ(cur).Z1 = "SNS" Then
                mSensComposition.Add(ZL.ListeZ(cur).Z6)
                cur = cur + 1
            Else
                mSensComposition.Add("")
            End If
        Next


        If ZL.ListeZ(cur).Z1 <> "ATC" Then
            mErreur = ErreurStructure.VECRELATION
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
            Dim s As String = ""
            ConcText(ZL, "ATV", cur, s)
            mValeurAtt.Add(s)

        Next

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
