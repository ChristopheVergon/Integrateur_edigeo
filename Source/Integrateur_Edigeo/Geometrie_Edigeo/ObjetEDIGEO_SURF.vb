Public Class ObjetEDIGEO_SURF
    Inherits ObjetEDIGEO
    Private mGeom As FACE
    Private mMultiGeom As MultiPOLYGON
    Public ReadOnly Property Geom() As FACE
        Get
            Return mGeom
        End Get
        
    End Property


    Public ReadOnly Property Polygone() As POLYGON
        Get
            Return mGeom.GetPolygone
        End Get

    End Property
    Public ReadOnly Property MultiPolygone() As MultiPOLYGON
        Get
            Return mMultiGeom
        End Get
    End Property
    Public Sub AddGeom(F As FACE)
        mGeom = F
        mMultiGeom.ListePolyg.Add(F.GetPolygone)
    End Sub

    Public Sub New()
        MyBase.Nature = NatureObjetSCD.ARE
        mMultiGeom = New MultiPOLYGON
    End Sub
End Class
