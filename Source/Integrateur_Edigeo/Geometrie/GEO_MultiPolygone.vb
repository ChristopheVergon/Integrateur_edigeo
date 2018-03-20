Imports Wkb.Serialization
<WkbMultiPolygon("ListePolyg")> _
Public Class GEO_MultiPolygone
    Inherits GEO_Base
    Private mListePolyg As System.Collections.Generic.List(Of GEO_Polygone)
    Public Property ListePolyg() As System.Collections.Generic.List(Of GEO_Polygone)
        Get
            Return mListePolyg
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of GEO_Polygone))
            mListePolyg = value
        End Set
    End Property
    Public Sub New()
        mListePolyg = New System.Collections.Generic.List(Of GEO_Polygone)
    End Sub

    Public Overrides Function DessineWithOutSetMetric(ByVal mMDC As Mdc) As System.Drawing.Drawing2D.GraphicsPath
        Dim Arrp As Point()
        Dim mypath As New System.Drawing.Drawing2D.GraphicsPath
        For j = 0 To mListePolyg.Count - 1
            For i = 0 To mListePolyg(j).Sommet.Count - 1
                Arrp = mMDC.GEO_ReelVersPeripherique(mListePolyg(j).Sommet(i))
                mypath.AddPolygon(Arrp)
            Next
        Next
        Return mypath

    End Function
End Class
