Public Class ListeObjetEDIGEO
    Inherits System.Collections.Generic.List(Of ObjetEDIGEO)


    Private mLayerName As String
    Public Property LayerName() As String
        Get
            Return mLayerName
        End Get
        Set(ByVal value As String)
            mLayerName = value
        End Set
    End Property

End Class
