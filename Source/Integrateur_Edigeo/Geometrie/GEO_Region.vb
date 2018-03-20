Public Class GEO_Region
    Inherits GEO_Base
    Implements ICloneable

    Private mLeft As Double
    Public Property left() As Double
        Get
            Return mLeft
        End Get
        Set(ByVal value As Double)
            mLeft = value
        End Set
    End Property


    Private mRight As Double
    Public Property Right() As Double
        Get
            Return mRight
        End Get
        Set(ByVal value As Double)
            mRight = value
        End Set
    End Property


    Private mBottom As Double
    Public Property Bottom() As Double
        Get
            Return mBottom
        End Get
        Set(ByVal value As Double)
            mBottom = value
        End Set
    End Property

    Private mTop As Double
    Public Property Top() As Double
        Get
            Return mTop
        End Get
        Set(ByVal value As Double)
            mTop = value
        End Set
    End Property
    Public Function Intersection(ByVal r As GEO_Region) As GEO_Region
        Dim Inter As New GEO_Region
        If mLeft < r.left Then
            If mRight < r.left Then

                Inter.Bottom = 0
                Inter.left = 0
                Inter.Right = 0
                Inter.Top = 0
                Intersection = Inter
                Inter = Nothing
                Exit Function
            Else
                If mRight > r.Right Then
                    Inter.left = r.left
                    Inter.Right = r.Right
                Else
                    Inter.left = r.left
                    Inter.Right = mRight
                End If
            End If
        Else

            If mLeft > r.Right Then
                Inter.Bottom = 0
                Inter.left = 0
                Inter.Right = 0
                Inter.Top = 0
                Intersection = Inter
                Inter = Nothing
                Exit Function
            Else
                If mRight > r.Right Then
                    Inter.left = mLeft
                    Inter.Right = r.Right
                Else
                    Inter.left = mLeft
                    Inter.Right = mRight
                End If
            End If
        End If

        If mBottom < r.Bottom Then
            If mTop < r.Bottom Then

                Inter.Bottom = 0
                Inter.left = 0
                Inter.Right = 0
                Inter.Top = 0
                Intersection = Inter
                Inter = Nothing
                Exit Function
            Else
                If mTop > r.Top Then
                    Inter.Bottom = r.Bottom
                    Inter.Top = r.Top
                Else
                    Inter.Bottom = r.Bottom
                    Inter.Top = mTop
                End If
            End If
        Else

            If mBottom > r.Top Then
                Inter.Bottom = 0
                Inter.left = 0
                Inter.Right = 0
                Inter.Top = 0
                Intersection = Inter
                Inter = Nothing
                Exit Function
            Else
                If mTop > r.Top Then
                    Inter.Bottom = mBottom
                    Inter.Top = r.Top
                Else
                    Inter.Bottom = mBottom
                    Inter.Top = mTop
                End If
            End If
        End If
        Intersection = Inter
    End Function

    Public Function MyInter(ByVal r As GEO_Region) As GEO_Region
        Dim maxg = Math.Max(r.mLeft, mLeft)
        Dim mind = Math.Min(r.mRight, mRight)
        Dim maxb = Math.Max(r.mBottom, mBottom)
        Dim minh = Math.Min(r.mTop, mTop)
        If maxg <= mind AndAlso maxb <= minh Then
            Return New GEO_Region(maxg, maxb, mind, minh)
        End If
        Return New GEO_Region()
    End Function




    Public Function ToWKT() As String
        Return "LINESTRING(" & Me.left.ToString(System.Globalization.CultureInfo.InvariantCulture) _
        & " " & Me.Bottom.ToString(System.Globalization.CultureInfo.InvariantCulture) _
        & "," & Me.Right.ToString(System.Globalization.CultureInfo.InvariantCulture) & " " _
        & Me.Top.ToString(System.Globalization.CultureInfo.InvariantCulture) & ")"
    End Function

    Public Function EstInclus(ByVal R As GEO_Region) As Boolean
        If R.Bottom >= mBottom And R.Top <= mTop And R.left >= mLeft And R.Right <= mRight Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub Clone(ByVal r As GEO_Region)
        r.Bottom = mBottom
        r.left = mLeft
        r.Right = mRight
        r.Top = mTop
    End Sub

    Public Function EstVide() As Boolean
        If mBottom = mTop And mRight = mLeft Then
            EstVide = True
        Else
            EstVide = False
        End If

    End Function

    Public Sub ordonne()
        Dim x1 As Double


        If mBottom > mTop Then
            x1 = mTop
            mTop = mBottom
            mBottom = x1
        End If

        If mLeft > mRight Then
            x1 = mRight
            mRight = mLeft
            mLeft = x1
        End If

    End Sub

    Public Function Union(ByVal r As GEO_Region) As GEO_Region
        Dim un As New GEO_Region
        If mBottom = mTop And mLeft = mRight Then
            Union = r
            un = Nothing
            Exit Function
        End If

        If r.left < mLeft Then
            un.left = r.left
        Else
            un.left = mLeft
        End If

        If r.Right > mRight Then
            un.Right = r.Right
        Else
            un.Right = mRight
        End If

        If r.Bottom < mBottom Then
            un.Bottom = r.Bottom
        Else
            un.Bottom = mBottom
        End If

        If r.Top > mTop Then
            un.Top = r.Top
        Else
            un.Top = mTop
        End If
        Union = un
        un = Nothing
    End Function
    Public Function Egale(ByVal r As GEO_Region) As Boolean
        If r.Bottom = mBottom And r.Top = mTop And r.left = mLeft And r.Right = mRight Then
            Egale = True
        Else
            Egale = False
        End If

    End Function
    Public Function TabPoint() As GEO_Point()

        Dim t(3) As GEO_Point
        t(0) = New GEO_Point(mLeft, mBottom)
        t(1) = New GEO_Point(mLeft, mTop)

        t(2) = New GEO_Point(mRight, mTop)
        t(3) = New GEO_Point(mRight, mBottom)
        TabPoint = t
    End Function
    Public Function Milieu() As GEO_Point
        Dim p As New GEO_Point


        p.X = (mRight - mLeft) / 2D + mLeft
        p.Y = (mTop - mBottom) / 2D + mBottom

        Milieu = p
        p = Nothing

    End Function
    Public Function Inflate(ByVal Facteur As Double) As GEO_Region
        Dim p As New GEO_Point


        p.X = (mRight - mLeft) / 2 + mLeft
        p.Y = (mTop - mBottom) / 2 + mBottom

        Dim l As Double = (Width / 2) * Facteur
        Dim h As Double = (Height / 2) * Facteur

        Dim r As New GEO_Region

        r.left = p.X - l
        r.Right = p.X + l
        r.Bottom = p.Y - h
        r.Top = p.Y + h

        Return r

    End Function
    Public Function Largeur() As Double
        Largeur = Math.Abs(mRight - mLeft)
    End Function
    Public Function Hauteur() As Double
        Hauteur = Math.Abs(mTop - mBottom)
    End Function

    Public Sub New()

    End Sub

    Public Sub New(ByVal l As Double, ByVal b As Double, ByVal r As Double, ByVal t As Double)
        mLeft = l
        mTop = t
        mBottom = b
        mRight = r
    End Sub

    Public Overridable Sub dessine(ByVal mdc As Mdc)

    End Sub
    
    Public Function PinRegion(ByVal v As GEO_Vertex) As Boolean
        Dim p As New GEO_Point(v.X, v.Y)
        Return PinRegion(p)
    End Function
    Public Function PinRegion(ByVal p As GEO_Point) As Boolean

        If p.X < mLeft Then
            PinRegion = False
            Exit Function
        Else
            If p.X > mRight Then
                PinRegion = False
                Exit Function
            Else
                If p.Y < mBottom Then
                    PinRegion = False
                    Exit Function
                Else
                    If p.Y > mTop Then
                        PinRegion = False
                        Exit Function
                    Else

                        PinRegion = True

                    End If
                End If
            End If

        End If
    End Function

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New GEO_Region(mLeft, mBottom, mRight, mTop)
    End Function

    Public ReadOnly Property Height() As Double
        Get
            Return mTop - mBottom
        End Get
    End Property

    Public ReadOnly Property Width() As Double
        Get
            Return mRight - mLeft
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return String.Format("Geo_Region = B:{0:0.000} L:{1:0.000} H:{2:0.000} W:{3:0.000}", _
                             New Object() {Bottom, left, Width, Height})
    End Function

    Public Function GetTranslated(ByVal x As Double, ByVal y As Double) As GEO_Region
        Return New GEO_Region(mLeft + x, mBottom + y, mRight + x, mTop + y)
    End Function

    Public Overrides Function DessineWithOutSetMetric(ByVal mMDC As Mdc) As System.Drawing.Drawing2D.GraphicsPath

    End Function
End Class
