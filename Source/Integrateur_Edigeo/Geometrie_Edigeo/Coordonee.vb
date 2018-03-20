Imports Wkb.Serialization

<BbPoint("X", "Y")> _
Public Class Coordonee

    Private mX As Double
    Public Property X() As Double
        Get
            Return mX
        End Get
        Set(ByVal value As Double)
            mX = value
        End Set
    End Property


    Private mY As Double
    Public Property Y() As Double
        Get
            Return mY
        End Get
        Set(ByVal value As Double)
            mY = value
        End Set
    End Property


    Private mZ As Double
    Public Property Z() As Double
        Get
            Return mZ
        End Get
        Set(ByVal value As Double)
            mZ = value
        End Set
    End Property
    Public Sub New()

    End Sub

    Public Function Egale(ByVal P As Coordonee) As Boolean
        Return mX = P.mX And mY = P.mY
        
    End Function
End Class
