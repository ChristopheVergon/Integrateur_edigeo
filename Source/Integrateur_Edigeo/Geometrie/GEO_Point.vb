Imports Wkb.Serialization
Imports System.Drawing.Drawing2D
<Wkb.Serialization.WkbPoint("X", "Y")> _
Public Class GEO_Point
    Inherits GEO_Base


    Private mGEOType As GEO_PointType = GEO_PointType.normal
    Public Property GEOType() As GEO_PointType
        Get
            Return mGEOType
        End Get
        Set(ByVal value As GEO_PointType)
            mGEOType = value
        End Set
    End Property


    Private mTaille As Double
    Public Property Taille() As Double
        Get
            Return mTaille
        End Get
        Set(ByVal value As Double)
            mTaille = value
        End Set
    End Property

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

    Public Sub Rotation(ByVal x As Double, ByVal y As Double, ByVal deltagis As Double)        
        deltagis = deltagis * (Math.PI / 200D)
        Dim p1 As New GEO_Point(m_X - x, m_Y - y)
        m_X = p1.X * Math.Cos(deltagis) + p1.Y * Math.Sin(deltagis) + x
        m_Y = -p1.X * Math.Sin(deltagis) + p1.Y * Math.Cos(deltagis) + y
    End Sub
    Public Overrides Function dessineWithOutSetMetric(ByVal mMDC As Mdc) As GraphicsPath
        Dim l As New System.Collections.Generic.List(Of GEO_Point)
        l.Add(Me)

        Dim g As New GraphicsPath
        Dim Arrp As Point()

        Select Case mGEOType

            Case GEO_PointType.normal
                l.Add(New GEO_Point(m_X + Taille, m_Y))
                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                Dim t As Integer = Arrp(1).X - Arrp(0).X
                Dim r As New Rectangle(Arrp(0).X - t, Arrp(0).Y - t, 2 * t, 2 * t)

                g.AddEllipse(r)

            Case GEO_PointType.stat

                l.Add(New GEO_Point(m_X - Taille, m_Y))
                l.Add(New GEO_Point(m_X + Taille, m_Y))
                l.Add(New GEO_Point(m_X, m_Y - Taille))
                l.Add(New GEO_Point(m_X, m_Y + Taille))
                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                g.StartFigure()
                g.AddLine(Arrp(1), Arrp(2))
                g.StartFigure()
                g.AddLine(Arrp(3), Arrp(4))

            Case GEO_PointType.appui

                l.Add(New GEO_Point(m_X, m_Y + Taille * 1 / Math.Sqrt(3)))  '1
                l.Add(New GEO_Point(m_X - 1 / 2 * Taille, m_Y - Taille * 1 / (2 * Math.Sqrt(3)))) '2
                l.Add(New GEO_Point(m_X + 1 / 2 * Taille, m_Y - Taille * 1 / (2 * Math.Sqrt(3)))) '3
                l.Add(New GEO_Point(m_X - 0.1 * Taille, m_Y)) '4
                l.Add(New GEO_Point(m_X + 0.1 * Taille, m_Y)) '5
                l.Add(New GEO_Point(m_X, m_Y - 0.1 * Taille)) '6
                l.Add(New GEO_Point(m_X, m_Y + 0.1 * Taille)) '7
                l.Add(New GEO_Point(m_X - Taille * 1 / (2 * Math.Sqrt(3)), m_Y - Taille * 1 / (2 * Math.Sqrt(3))))

                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                g.StartFigure()
                g.AddLine(Arrp(1), Arrp(2))
                g.AddLine(Arrp(2), Arrp(3))
                g.AddLine(Arrp(3), Arrp(1))
                g.StartFigure()
                g.AddLine(Arrp(4), Arrp(5))
                g.StartFigure()
                g.AddLine(Arrp(6), Arrp(7))
                Dim r As New Rectangle(Arrp(8).X, Arrp(8).Y, 2 * (Arrp(0).X - Arrp(8).X), 2 * (Arrp(0).Y - Arrp(8).Y))
                g.AddEllipse(r)

            Case GEO_PointType.calvaire

                l.Add(New GEO_Point(m_X + 0.5 * Taille, m_Y)) '1
                l.Add(New GEO_Point(m_X, m_Y + 0.5 * Taille)) '2
                l.Add(New GEO_Point(m_X, m_Y + 3.5 * Taille)) '3
                l.Add(New GEO_Point(m_X - Taille, m_Y + 2.5 * Taille)) '4
                l.Add(New GEO_Point(m_X + Taille, m_Y + 2.5 * Taille)) '5
                'l.Add(New GEO_Point(m_X, m_Y + 3.5 * Taille)) '6

                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                Dim t As Integer = Arrp(1).X - Arrp(0).X
                Dim r As New Rectangle(Arrp(0).X - t, Arrp(0).Y - t, 2 * t, 2 * t)

                g.AddEllipse(r)
                g.AddLine(Arrp(2), Arrp(3))
                g.StartFigure()
                g.AddLine(Arrp(4), Arrp(5))

            Case GEO_PointType.carre
                l.Remove(l.Item(0))
                l.Add(New GEO_Point(m_X + 0.5 * Taille, m_Y + 0.5 * Taille))
                l.Add(New GEO_Point(m_X - 0.5 * Taille, m_Y + 0.5 * Taille))
                l.Add(New GEO_Point(m_X - 0.5 * Taille, m_Y - 0.5 * Taille))
                l.Add(New GEO_Point(m_X + 0.5 * Taille, m_Y - 0.5 * Taille))
                l.Add(New GEO_Point(m_X + 0.5 * Taille, m_Y + 0.5 * Taille))
                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                g.AddPolygon(Arrp)

            Case GEO_PointType.carremur
                l.Remove(l.Item(0))
                l.Add(New GEO_Point(m_X - 1.5 * Taille, m_Y - 0.5 * Taille))
                l.Add(New GEO_Point(m_X + 1.5 * Taille, m_Y - 0.5 * Taille))
                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                g.AddLine(Arrp(0), Arrp(1))
                l.Clear()
                l.Add(New GEO_Point(m_X + 0.5 * Taille, m_Y + 0.5 * Taille))
                l.Add(New GEO_Point(m_X - 0.5 * Taille, m_Y + 0.5 * Taille))
                l.Add(New GEO_Point(m_X - 0.5 * Taille, m_Y - 0.5 * Taille))
                l.Add(New GEO_Point(m_X + 0.5 * Taille, m_Y - 0.5 * Taille))
                l.Add(New GEO_Point(m_X + 0.5 * Taille, m_Y + 0.5 * Taille))
                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                g.AddPolygon(Arrp)

            Case GEO_PointType.rond

                l.Add(New GEO_Point(m_X + 0.5 * Taille, m_Y))
                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                Dim t As Integer = Arrp(1).X - Arrp(0).X
                If t = 0 Then t = 1
                Dim r As New Rectangle(Arrp(0).X - t, Arrp(0).Y - t, 2 * t, 2 * t)

                g.AddEllipse(r)

            Case GEO_PointType.pyramide

                l.Add(New GEO_Point(m_X + 0.5 * Taille, m_Y))
                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                Dim t As Integer = Arrp(1).X - Arrp(0).X
                If t = 0 Then t = 1
                Dim r As New Rectangle(Arrp(0).X - t, Arrp(0).Y - t, 2 * t, 2 * t)
                g.AddPie(r, -115, 50)

            Case GEO_PointType.rondmur

                l.Add(New GEO_Point(m_X + 0.5 * Taille, m_Y))
                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                Dim t As Integer = Arrp(1).X - Arrp(0).X
                If t = 0 Then t = 1
                Dim r As New Rectangle(Arrp(0).X - t, Arrp(0).Y - t, 2 * t, 2 * t)

                g.AddEllipse(r)
                l.Clear()
                l.Add(New GEO_Point(m_X - 1.5 * Taille, m_Y - 0.5 * Taille))
                l.Add(New GEO_Point(m_X + 1.5 * Taille, m_Y - 0.5 * Taille))
                Arrp = mMDC.GEO_ReelVersPeripherique(l)
                g.AddLine(Arrp(0), Arrp(1))

        End Select

        Return g
    End Function

    Public Overrides Sub dessine(ByVal mMDC As Mdc)


        mMDC.SetMetrique()

        Dim g As GraphicsPath = dessineWithOutSetMetric(mMDC)


        mMDC.ExitMetric()

        mMDC.G.DrawPath(PenGDI, g)


        Select Case mGEOType

            Case GEO_PointType.carre, GEO_PointType.carremur, GEO_PointType.rond, GEO_PointType.pyramide
                mMDC.G.DrawPath(PenGDI, g)
                mMDC.G.FillPath(BrushGDI, g)

        End Select
        g.Dispose()


    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("GeoPoint [{0} {1}]", m_X, m_Y)
    End Function
End Class
