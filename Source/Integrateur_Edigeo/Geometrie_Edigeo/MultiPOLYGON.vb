Imports Wkb.Serialization
<WkbMultiPolygon("ListePolyg")> _
Public Class MultiPOLYGON

    Private mListePolyg As System.Collections.Generic.List(Of Vect_Polygone)
    Public Property ListePolyg() As System.Collections.Generic.List(Of Vect_Polygone)
        Get
            Return mListePolyg
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Vect_Polygone))
            mListePolyg = value
        End Set
    End Property
    Public Sub New()
        mListePolyg = New System.Collections.Generic.List(Of Vect_Polygone)
    End Sub
End Class
