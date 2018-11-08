Imports Wkb.Serialization
<WkbMultiPolygon("ListePolyg")> _
Public Class MultiPOLYGON
    Inherits Geometrie
    Private mListePolyg As System.Collections.Generic.List(Of POLYGON)
    Public Property ListePolyg() As System.Collections.Generic.List(Of POLYGON)
        Get
            Return mListePolyg
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of POLYGON))
            mListePolyg = value
        End Set
    End Property
    Public Sub New()
        mListePolyg = New System.Collections.Generic.List(Of POLYGON)
    End Sub

    Public Function GetTextValue() As String

        Dim res As String = "MULTIPOLYGON("

        For Each r In mListePolyg

            res = res & r.GetTextValue & ","

        Next

        res = res.Remove(res.Length - 1, 1)
        res = res & ")"
        Return res
    End Function
End Class
