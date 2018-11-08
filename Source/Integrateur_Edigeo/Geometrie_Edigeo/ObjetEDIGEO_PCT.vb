Public Class ObjetEDIGEO_PCT
    Inherits ObjetEDIGEO

    Private mGeom As NOEUD
    Public Property Geom() As NOEUD
        Get
            Return mGeom
        End Get
        Set(ByVal value As NOEUD)
            mGeom = value
        End Set
    End Property

    Public Sub New()
        MyBase.Nature = NatureObjetSCD.PCT
    End Sub
End Class
