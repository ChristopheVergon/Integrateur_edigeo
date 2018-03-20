Imports System.Drawing.Drawing2D
Public MustInherit Class GEO_Base

    Public MustOverride Function DessineWithOutSetMetric(ByVal mMDC As Mdc) As GraphicsPath

    Private mGDItype As BrushType = BrushType.solidplus
    Public Property GDItype() As BrushType
        Get
            Return mGDItype
        End Get
        Set(ByVal value As BrushType)
            mGDItype = value

        End Set
    End Property
    Private toto As String = ""
    Private mBrushGDI As SolidBrush
    Public Property BrushGDI() As SolidBrush
        Get
            Return mBrushGDI
        End Get
        Set(ByVal value As SolidBrush)
            mBrushGDI = value
        End Set
    End Property

    Private mPenGDI As Pen
    Public Property PenGDI() As Pen
        Get
            Return mPenGDI
        End Get
        Set(ByVal value As Pen)
            mPenGDI = value
        End Set
    End Property

    Private mTextureBrushGDI As TextureBrush
    Public Property TextureBrushGDI() As TextureBrush
        Get
            Return mTextureBrushGDI
        End Get
        Set(ByVal value As TextureBrush)
            mTextureBrushGDI = value
        End Set
    End Property
    Private mFinligne As LineCap = LineCap.Flat
    Public Property FinLigne() As LineCap
        Get
            Return mFinligne
        End Get
        Set(ByVal value As LineCap)
            mFinligne = value
        End Set
    End Property


    Private mDebutLigne As LineCap = LineCap.Flat
    Public Property DebutLigne() As LineCap
        Get
            Return mDebutLigne
        End Get
        Set(ByVal value As LineCap)
            mDebutLigne = value
        End Set
    End Property

    Private mPenWidth As Integer = 1
    Public Property PenWidth() As Integer
        Get
            Return mPenWidth
        End Get
        Set(ByVal value As Integer)
            mPenWidth = value
        End Set
    End Property
    Private mPenStyle As PenStyle = PenStyle.PS_SOLID
    Public Property PenStyle() As PenStyle
        Get
            Return mPenStyle
        End Get
        Set(ByVal value As PenStyle)
            mPenStyle = value
        End Set
    End Property

    Private mVisible As Boolean = True
    Public Property Visible() As Boolean
        Get
            Return mVisible
        End Get
        Set(ByVal value As Boolean)
            mVisible = value
        End Set
    End Property

    Private mColumnName As String
    Public Property ColumnName() As String
        Get
            Return mColumnName
        End Get
        Set(ByVal value As String)
            mColumnName = value
        End Set
    End Property


    Private mIdentifiant As Integer
    Public Property Identifiant() As Integer
        Get
            Return mIdentifiant
        End Get
        Set(ByVal value As Integer)
            mIdentifiant = value
        End Set
    End Property

    Public Overridable Sub dessine(ByVal mMDC As Mdc)

    End Sub
    Public Overridable Sub dessineavecsommet(ByVal mMDC As Mdc)

    End Sub
    Public Overridable Sub dessineselected(ByVal mMDC As Mdc)

    End Sub
End Class
