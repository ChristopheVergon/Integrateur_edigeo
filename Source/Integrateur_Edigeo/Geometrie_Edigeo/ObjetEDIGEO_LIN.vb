Public Class ObjetEDIGEO_LIN
    Inherits ObjetEDIGEO

    Private mGeom As ARC
    Public Property Geom() As ARC
        Get
            Return mGeom
        End Get
        Set(ByVal value As ARC)
            mGeom = value
        End Set
    End Property
    Public Sub New()
        MyBase.Nature = NatureObjetSCD.LIN
    End Sub


    Private mSens As String
    Public Property Sens() As String
        Get
            Return mSens
        End Get
        Set(ByVal value As String)
            mSens = value
        End Set
    End Property

End Class
