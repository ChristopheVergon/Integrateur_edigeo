Imports Wkb.Serialization
<Wkb.Serialization.BbPoint("X", "Y")> _
Public Class GEO_Vertex
    Private m_X As Double
    Public Property X() As Double
        Get
            Return m_X
        End Get
        Set(ByVal value As Double)
            m_X = value
        End Set
    End Property


    Private m_Y As Double
    Public Property Y() As Double
        Get
            Return m_Y
        End Get
        Set(ByVal value As Double)
            m_Y = value
        End Set
    End Property
    Public Sub New()

    End Sub

  
    Public Sub New(ByVal X As Double, ByVal Y As Double)
        m_X = X
        m_Y = Y
  
    End Sub
End Class
