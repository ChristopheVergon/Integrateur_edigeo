Imports Wkb.Serialization
Imports System.Drawing.Drawing2D

<Wkb.Serialization.WkbPolygon("Sommet")> _
Public Class GEO_Polygone
    Inherits GEO_Base

    Private mSommet As System.Collections.Generic.List(Of System.Collections.Generic.List(Of GEO_Vertex))
    Public Property Sommet() As System.Collections.Generic.List(Of System.Collections.Generic.List(Of GEO_Vertex))
        Get
            Return mSommet
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of System.Collections.Generic.List(Of GEO_Vertex)))
            mSommet = value
        End Set
    End Property
    Public Sub New()
        mSommet = New System.Collections.Generic.List(Of System.Collections.Generic.List(Of GEO_Vertex))
    End Sub

    Public Overrides Function DessineWithOutSetMetric(ByVal mMDC As Mdc) As GraphicsPath
        Dim Arrp As Point()
        Dim mypath As New System.Drawing.Drawing2D.GraphicsPath

        For i = 0 To mSommet.Count - 1
            Arrp = mMDC.GEO_ReelVersPeripherique(mSommet(i))

            mypath.AddPolygon(Arrp)

            
        Next


        Return mypath

    End Function
    Public Overrides Sub dessine(ByVal mMDC As Mdc)
        mMDC.SetMetrique()

        Dim g As GraphicsPath = DessineWithOutSetMetric(mMDC)
        Dim s As New GraphicsPath
        GetRectSommet(mMDC, s)

        mMDC.ExitMetric()

        mMDC.G.DrawPath(Pens.Black, g)
        Dim hb As New SolidBrush(Color.FromArgb(160, Color.AliceBlue.R, Color.AliceBlue.G, Color.AliceBlue.B))
        mMDC.G.FillPath(hb, g)
        hb.Dispose()
        mMDC.G.FillPath(Brushes.Blue, s)
        g.Dispose()
        s.Dispose()
    End Sub
    Private Sub GetRectSommet(ByVal mMDC As Mdc, ByVal gg As GraphicsPath)

        Dim Arrp As Point()

        For i = 0 To mSommet.Count - 1
            Arrp = mMDC.GEO_ReelVersPeripherique(mSommet(i))
            For j = 0 To Arrp.Count - 1

                Dim g As New GraphicsPath
                Dim so(4) As Point
                so(0) = New Point(Arrp(j).X - 2, Arrp(j).Y - 2)
                so(1) = New Point(Arrp(j).X - 2, Arrp(j).Y + 2)
                so(2) = New Point(Arrp(j).X + 2, Arrp(j).Y + 2)
                so(3) = New Point(Arrp(j).X + 2, Arrp(j).Y - 2)
                so(4) = New Point(Arrp(j).X - 2, Arrp(j).Y - 2)

                g.AddPolygon(so)
                gg.AddPath(g, False)
                g.Dispose()

            Next

        Next
    End Sub
    Public Function GetSommetInRegion(ByVal gr As GEO_Region) As GEO_Vertex
        For i = 0 To mSommet.Count - 1

            For j = 0 To mSommet(i).Count - 1

                If gr.PinRegion(mSommet(i)(j)) Then

                    Return mSommet(i)(j)
                End If


            Next

        Next

        Return Nothing
    End Function

    Public Function GetIndexsSommet(ByVal v As GEO_Vertex) As Integer()
        For i = 0 To mSommet.Count - 1

            For j = 0 To mSommet(i).Count - 1

                If v.X = mSommet(i)(j).X And v.Y = mSommet(i)(j).Y Then
                    Dim s(1) As Integer
                    s(0) = i
                    s(1) = j
                    Return s
                End If


            Next

        Next

        Return Nothing
    End Function

    Public Function GetIndexsSommet(ByVal v As GEO_Point) As Integer()
        For i = 0 To mSommet.Count - 1

            For j = 0 To mSommet(i).Count - 1

                If v.X = mSommet(i)(j).X And v.Y = mSommet(i)(j).Y Then
                    Dim s(1) As Integer
                    s(0) = i
                    s(1) = j
                    Return s
                End If


            Next

        Next

        Return Nothing
    End Function
End Class
