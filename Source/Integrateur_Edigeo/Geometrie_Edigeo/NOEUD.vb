Imports Wkb.Serialization

<WkbPoint("X", "Y")> _
Public Class NOEUD
    Inherits Geometrie


    Public Property X() As Double
        Get
            Return mPoint.X
        End Get
        Set(ByVal value As Double)
            mPoint.X = value
        End Set
    End Property

    Public Property Y() As Double
        Get
            Return mPoint.Y
        End Get
        Set(ByVal value As Double)
            mPoint.Y = value
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

    Private mEstInclusDans As FACE
    Public Property EstInclusDans() As FACE
        Get
            Return mEstInclusDans
        End Get
        Set(ByVal value As FACE)
            mEstInclusDans = value
        End Set
    End Property

    Private mAppartientA As ARC
    Public Property AppartientA() As ARC
        Get
            Return mAppartientA
        End Get
        Set(ByVal value As ARC)
            mAppartientA = value
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
        mPoint = New Coordonee
    End Sub
End Class
