Imports Wkb.Serialization
<WkbPolygon("ListeRing")> _
Public Class POLYGON
    Inherits Geometrie


    Private mListeRing As System.Collections.Generic.List(Of System.Collections.Generic.List(Of Coordonee))
    Public Property ListeRing() As System.Collections.Generic.List(Of System.Collections.Generic.List(Of Coordonee))
        Get
            Return mListeRing
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of System.Collections.Generic.List(Of Coordonee)))
            mListeRing = value
        End Set
    End Property

    Public Sub New()
        mListeRing = New System.Collections.Generic.List(Of System.Collections.Generic.List(Of Coordonee))
    End Sub

    Public Function CheckAndRepareGeometry() As Boolean

        For Each r In mListeRing


            If r.First.Egale(r.Last) Then

            Else
                Dim c As New Coordonee
                c.X = r.First.X
                c.Y = r.First.Y
                r.Add(c)
            End If

            If r.Count = 3 Then
                Dim c As New Coordonee
                c.X = r.First.X
                c.Y = r.First.Y
                r.Add(c)
            End If
        Next
    End Function
    
    Public Function Encadrante(ByVal m_col As System.Collections.Generic.List(Of Coordonee)) As GEO_Region
        Dim mEncadrante As New GEO_Region



        If m_col.count > 1 Then
            mEncadrante = Nothing
            mEncadrante = New GEO_Region
            Dim p As New Coordonee
            p = m_col(1)
            mEncadrante.left = p.x
            mEncadrante.Right = p.x
            mEncadrante.Bottom = p.y
            mEncadrante.Top = p.y
            p = Nothing
        Else

            Encadrante = Nothing
            Exit Function
        End If


        For Each p In m_col

            If p.x < mEncadrante.left Then
                mEncadrante.left = p.x
            End If
            If p.x > mEncadrante.Right Then
                mEncadrante.Right = p.x
            End If
            If p.y < mEncadrante.Bottom Then
                mEncadrante.Bottom = p.y
            End If
            If p.y > mEncadrante.Top Then
                mEncadrante.Top = p.y
            End If

        Next p

        Return mEncadrante
    End Function
End Class
