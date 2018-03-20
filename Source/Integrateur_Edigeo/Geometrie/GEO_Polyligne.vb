Imports Wkb.Serialization

<Wkb.Serialization.WkbLine("Sommet")> _
Public Class GEO_Polyligne
    Inherits GEO_Base

    Private mSommet As System.Collections.Generic.List(Of GEO_Vertex)
    Public Property Sommet() As System.Collections.Generic.List(Of GEO_Vertex)
        Get
            Return mSommet
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of GEO_Vertex))
            mSommet = value
        End Set
    End Property
    Public Sub New()
        mSommet = New System.Collections.Generic.List(Of GEO_Vertex)

    End Sub
    Public Overrides Function DessineWithOutSetMetric(ByVal mMDC As Mdc) As System.Drawing.Drawing2D.GraphicsPath
        Dim Arrp As Point()
        Dim mypath As New System.Drawing.Drawing2D.GraphicsPath


        Arrp = mMDC.GEO_ReelVersPeripherique(mSommet)

        mypath.AddLines(Arrp)


        Return mypath

    End Function
    Public Function ToPolygone() As GEO_Polygone

        If mSommet(0).X = mSommet(mSommet.Count - 1).X And mSommet(0).Y = mSommet(mSommet.Count - 1).Y Then

            Dim po As New GEO_Polygone
            po.Sommet.Add(mSommet)
            Return po
        Else
            Return Nothing

        End If

    End Function

    Public Overrides Sub dessine(ByVal mMDC As Mdc)
        If mSommet.Count < 2 Then Exit Sub

        mMDC.SetMetrique()

        Dim Arrp As Point() = mMDC.GEO_ReelVersPeripherique(mSommet)

        mMDC.ExitMetric()

        mMDC.G.DrawLines(PenGDI, Arrp)

    End Sub
End Class
