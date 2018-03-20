Imports Wkb.Serialization

<WkbLine("Points")> _
Public Class ARC
    Inherits Geometrie

    Private mPoints As System.Collections.Generic.List(Of Coordonee)
    Public Property Points() As System.Collections.Generic.List(Of Coordonee)
        Get
            Return mPoints
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Coordonee))
            mPoints = value
        End Set
    End Property


    Private mNoeudInitial As NOEUD
    Public Property NoeudInitial() As NOEUD
        Get
            Return mNoeudInitial
        End Get
        Set(ByVal value As NOEUD)
            mNoeudInitial = value
        End Set
    End Property


    Private mNoeudFinal As NOEUD
    Public Property NoeudFinal() As NOEUD
        Get
            Return mNoeudFinal
        End Get
        Set(ByVal value As NOEUD)
            mNoeudFinal = value
        End Set
    End Property


    Private mEstADroite As Boolean
    Public Property EstADroite() As Boolean
        Get
            Return mEstADroite
        End Get
        Set(ByVal value As Boolean)
            mEstADroite = value
        End Set
    End Property


    Private mFaceDroite As FACE
    Public Property FaceDroite() As FACE
        Get
            Return mFaceDroite
        End Get
        Set(ByVal value As FACE)
            mFaceDroite = value
        End Set
    End Property


    Private mFaceGauche As FACE
    Public Property FaceGauche() As FACE
        Get
            Return mFaceGauche
        End Get
        Set(ByVal value As FACE)
            mFaceGauche = value
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
    Public Sub New()
        mPoints = New System.Collections.Generic.List(Of Coordonee)
    End Sub
End Class
