Public Class Rect

    Private mLeft As Integer
    Public Property Left() As Integer
        Get
            Return mLeft
        End Get
        Set(ByVal value As Integer)
            mLeft = value
        End Set
    End Property


    Private mRight As Intptr
    Public Property Right() As Integer
        Get
            Return mRight
        End Get
        Set(ByVal value As Integer)
            mRight = value
        End Set
    End Property


    Private mTop As Intptr
    Public Property Top() As Integer
        Get
            Return mTop
        End Get
        Set(ByVal value As Integer)
            mTop = value
        End Set
    End Property


    Private mBottom As Intptr
    Public Property Bottom() As Integer
        Get
            Return mBottom
        End Get
        Set(ByVal value As Integer)
            mBottom = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal L As Integer, ByVal B As Integer, ByVal R As Integer, ByVal T As Integer)
        mLeft = L
        mBottom = B
        mTop = T
        mRight = R
    End Sub
End Class
