Imports System.Runtime.InteropServices
Imports System.Math
Public Class Mdc
   
    Const PS_SOLID& = 0
    Const PS_DOT& = 2
    Const PS_DASH& = 1
    Const PS_DASHDOT& = 3
    Const PS_DASHDOTDOT& = 4
    Const MM_HIMETRIC& = 3

    Const FIXED_PITCH = 1
    Const TA_NOUPDATECP = 0
    Const TA_UPDATECP = 1
    Const TA_LEFT = 0
    Const TA_RIGHT = 2
    Const TA_CENTER = 6
    Const TA_TOP = 0
    Const TA_BOTTOM = 8
    Const TA_BASELINE = 24
    Const LF_FACESIZE = 32


    Private Const HORZSIZE& = 4
    Private Const TECHNOLOGY& = 2
    Private Const HORZRES& = 8
    Private Const VERTRES& = 10
    Private Const VERTSIZE& = 6
    Private Const LOGPIXELSX& = 88
    Private Const LOGPIXELSY& = 90
    Private Const BITSPIXEL& = 12
    Private Const PLANES& = 14
    Private Const NUMBRUSHES& = 16
    Private Const NUMCOLORS& = 24
    Private Const NUMMARKERS& = 20
    Private Const NUMFONTS& = 22
    Private Const NUMRESERVED& = 106
    Private Const NUMPENS& = 18
    Private Const ASPECTX& = 40
    Private Const ASPECTY& = 42
    Private Const ASPECTXY& = 44
    Private Const PDEVICESIZE& = 26
    Private Const CLIPCAPS& = 36
    Private Const SIZEPALETTE& = 104
    Private Const PHYSICALHEIGHT& = 111
    Private Const PHYSICALOFFSETX& = 112
    Private Const PHYSICALOFFSETY& = 113
    Private Const PHYSICALWIDTH& = 110
    Private Const RASTERCAPS& = 38
    Private Const DT_PLOTTER& = 0
    Private Const DT_RASCAMERA& = 3
    Private Const DT_RASDISPLAY& = 1
    Private Const DT_RASPRINTER& = 2
    Private Const DT_CHARSTREAM& = 4
    Private Const DT_METAFILE& = 5
    Private Const DT_DISPFILE& = 6
    Private Const SYSTEM_FONT& = 13
    Private Const R2_COPYPEN& = 13
    Private Const R2_XORPEN& = 7

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
   Public Structure LOGFONT
        Public lfHeight As Int32
        Public lfWidth As Int32
        Public lfEscapement As Int32
        Public lfOrientation As Int32
        Public lfWeight As Int32
        Public lfItalic As Byte
        Public lfUnderline As Byte
        Public lfStrikeOut As Byte
        Public lfCharSet As Byte
        Public lfOutPrecision As Byte
        Public lfClipPrecision As Byte
        Public lfQuality As Byte
        Public lfPitchAndFamily As Byte
        <MarshalAs(UnmanagedType.LPTStr, SizeConst:=32)> _
        Public lfFaceName As String
    End Structure

    Private Structure TEXTMETRIC
        Dim tmHeight As Long
        Dim tmAscent As Long
        Dim tmDescent As Long
        Dim tmInternalLeading As Long
        Dim tmExternalLeading As Long
        Dim tmAveCharWidth As Long
        Dim tmMaxCharWidth As Long
        Dim tmWeight As Long
        Dim tmOverhang As Long
        Dim tmDigitizedAspectX As Long
        Dim tmDigitizedAspectY As Long
        Dim tmFirstChar As Byte
        Dim tmLastChar As Byte
        Dim tmDefaultChar As Byte
        Dim tmBreakChar As Byte
        Dim tmItalic As Byte
        Dim tmUnderlined As Byte
        Dim tmStruckOut As Byte
        Dim tmPitchAndFamily As Byte
        Dim tmCharSet As Byte
    End Structure
    Private Structure SizeP
        Dim Cx As Long
        Dim Cy As Long
    End Structure
    Public Structure POINTAPI
        Dim x As Integer
        Dim y As Integer
        Public Sub New(ByVal xp As Integer, ByVal yp As Integer)
            Me.x = xp
            Me.y = yp
        End Sub
    End Structure

    Private Structure POINTGEO
        Dim x As Double
        Dim y As Double
    End Structure

    Private Structure RECTGEO
        Dim Left As Double
        Dim Top As Double
        Dim Right As Double
        Dim Bottom As Double
    End Structure

    <StructLayout(LayoutKind.Sequential)> Public Structure RectA
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Private Declare Function SetBkMode Lib "gdi32" (ByVal hdc As IntPtr, ByVal mode As Integer) As Integer
    Private Declare Function CreateFontIndirect Lib "gdi32" Alias "CreateFontIndirectA" (ByRef lpLogFont As LOGFONT) As IntPtr
    Private Declare Function GetTextFace Lib "gdi32" Alias "GetTextFaceA" (ByVal hdc As Integer, ByVal nCount As Integer, ByVal lpFacename As String) As Long
    Private Declare Function GetTextMetrics Lib "gdi32" Alias "GetTextMetricsA" (ByVal hdc As Integer, ByVal lpMetrics As TEXTMETRIC) As Long
    Private Declare Function GetTextExtentPoint32 Lib "gdi32" Alias "GetTextExtentPoint32A" (ByVal hdc As IntPtr, ByVal lpsz As String, ByVal cbString As Integer, <Out()> ByRef lpSize As Size) As IntPtr
    Private Declare Function TextOut Lib "gdi32" Alias "TextOutA" (ByVal hdc As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal lpString As String, ByVal nCount As Integer) As Boolean
    Private Declare Function SetTextAlign Lib "gdi32" (ByVal hdc As IntPtr, ByVal wFlags As UInteger) As IntPtr
    Private Declare Function DrawText Lib "user32" Alias "DrawTextA" (ByVal hdc As Integer, ByVal lpStr As String, ByVal nCount As Integer, ByVal lpRect As Rect, ByVal wFormat As Integer) As Long
    Private Declare Function GetDeviceCaps Lib "gdi32" (ByVal hdc As IntPtr, ByVal nIndex As IntPtr) As IntPtr
    Private Declare Function CreatePen Lib "gdi32" (ByVal nPenStyle As IntPtr, ByVal nWidth As IntPtr, ByVal crColor As IntPtr) As IntPtr
    Private Declare Function DPtoLP Lib "gdi32" (ByVal hdc As IntPtr, ByRef lpPoint As POINTAPI, ByVal nCount As IntPtr) As IntPtr
    Private Declare Function LPtoDP Lib "gdi32" (ByVal hdc As IntPtr, ByRef lpPoint As POINTAPI, ByVal nCount As IntPtr) As IntPtr
    Private Declare Function LPtoDP Lib "gdi32" (ByVal hdc As IntPtr, ByRef lpPoint As Point, ByVal nCount As IntPtr) As IntPtr
    Private Declare Function GetClientRect Lib "user32" (ByVal hwnd As IntPtr, ByRef lpRect As RectA) As IntPtr
    Private Declare Function SetMapMode Lib "gdi32" (ByVal hdc As IntPtr, ByVal nMapMode As IntPtr) As IntPtr
    Private Declare Function SetViewportOrgEx Lib "gdi32" (ByVal hdc As IntPtr, ByVal nX As IntPtr, ByVal nY As IntPtr, ByRef lpPoint As POINTAPI) As IntPtr
    Private Declare Function SetWindowOrgEx Lib "gdi32" (ByVal hdc As IntPtr, ByVal nX As IntPtr, ByVal nY As IntPtr, ByRef lpPoint As POINTAPI) As IntPtr
    Private Declare Function LineTo Lib "gdi32" (ByVal hdc As IntPtr, ByVal x As IntPtr, ByVal y As IntPtr) As IntPtr
    Private Declare Function MoveToEx Lib "gdi32" (ByVal hdc As IntPtr, ByVal x As IntPtr, ByVal y As IntPtr, ByRef lpPoint As POINTAPI) As IntPtr
    Private Declare Function CreateSolidBrush Lib "gdi32" (ByVal crColor As IntPtr) As IntPtr
    Private Declare Function Polyline Lib "gdi32" (ByVal hdc As IntPtr, ByRef lpPoint As POINTAPI, ByVal nCount As IntPtr) As IntPtr
    Private Declare Function PolylineTo Lib "gdi32" (ByVal hdc As IntPtr, ByRef lppt As POINTAPI, ByVal cCount As IntPtr) As IntPtr
    Private Declare Function Polygon Lib "gdi32" (ByVal hdc As IntPtr, ByRef lpPoint As POINTAPI, ByVal nCount As IntPtr) As IntPtr
    Private Declare Function RestoreDC Lib "gdi32" (ByVal hdc As IntPtr, ByVal nSavedDC As IntPtr) As IntPtr
    Private Declare Function SaveDC Lib "gdi32" (ByVal hdc As IntPtr) As IntPtr
    Private Declare Function SelectObject Lib "gdi32" (ByVal hdc As IntPtr, ByVal hObject As IntPtr) As IntPtr
    Private Declare Function DeleteObject Lib "gdi32" (ByVal hObject As IntPtr) As IntPtr
    Private Declare Function SetROP2 Lib "gdi32" (ByVal hdc As IntPtr, ByVal nDrawMode As IntPtr) As IntPtr
    Private Declare Function GetObjectAPI Lib "gdi32" Alias "GetObjectA" (ByVal hObject As Long, ByVal nCount As Long, ByVal lpObject As LOGFONT)
    Private Declare Function GetStockObject Lib "gdi32" (ByVal nIndex As IntPtr) As IntPtr
    Private Declare Function Rectangle& Lib "gdi32" (ByVal hdc As IntPtr, ByVal x1 As IntPtr, ByVal y1 As IntPtr, ByVal X2 As IntPtr, ByVal Y2 As IntPtr)
    Private Declare Function SetPixelV& Lib "gdi32" (ByVal hdc As IntPtr, ByVal x As IntPtr, ByVal y As IntPtr, ByVal crColor As IntPtr)
    Private Declare Function Ellipse& Lib "gdi32" (ByVal hdc As IntPtr, ByVal x1 As IntPtr, ByVal y1 As IntPtr, ByVal X2 As IntPtr, ByVal Y2 As IntPtr)
    Private Declare Function CreateRectRgn& Lib "gdi32" (ByVal x1 As IntPtr, ByVal y1 As IntPtr, ByVal X2 As IntPtr, ByVal Y2 As IntPtr)
    Private Declare Function SelectClipRgn& Lib "gdi32" (ByVal hdc As IntPtr, ByVal hRgn As IntPtr)
    Private Declare Function GetROP2& Lib "gdi32" (ByVal hdc As IntPtr)
    Private Declare Function GetTextColor Lib "gdi32" (ByVal hdc As IntPtr) As IntPtr
    Private Declare Function SetTextColor Lib "gdi32" (ByVal hdc As IntPtr, ByVal crColor As IntPtr) As IntPtr
    Private Declare Function BitBlt Lib "gdi32" (ByVal hdcDest As IntPtr, ByVal nxDest As Int32, ByVal nyDest As Int32, ByVal nWidth As Int32, ByVal nHeight As Int32, _
                                                 ByVal hdcSrc As IntPtr, ByVal nXsrc As Int32, ByVal nYsrc As Int32, ByVal dwRop As UInt32) As Int32

    Private mEstLoupe As Boolean
    Private mMousepointer As Integer
    Private mrectdessin As RectA
    Private mypoint As POINTAPI
    Private MyGeoPoint As GEO_Point
    Private mDimensionEspaceLogique As Integer
    Private mYlog As Integer
    Private mXlog As Integer
    Private mYlogique As Integer
    Private mXlogique As Integer
    Private m_X As Double
    Private m_y As Double
    Private mXph As IntPtr
    Private mYph As IntPtr
    Private mViewOrgX As Integer
    Private mViewOrgY As Integer
    Private mWinOrgX As Integer
    Private mWinOrgY As Integer
    Private m_savedDC As Integer
    Private mrectText As Rect
    Private mespaceText As RECTGEO
    Private MaxlogPoint() As POINTAPI
    Private lpPoint() As POINTAPI
    Private mlpgeo() As GEO_Point
    Private dummy&

    Private bmp As Bitmap


    Private WithEvents mPicture As PictureBox
    Public ReadOnly Property PictureB() As PictureBox
        Get
            Return mPicture
        End Get

    End Property


    Private mIsPrinterdc As Boolean
    Public Property IsPrinterDC() As Boolean
        Get
            Return mIsPrinterdc
        End Get
        Set(ByVal value As Boolean)
            mIsPrinterdc = value
        End Set
    End Property

    Private mhdc As IntPtr
    Public ReadOnly Property HDC() As IntPtr
        Get
            Return mhdc
        End Get

    End Property

    Private mEspaceReel As GEO_Region
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Declenche l'evenement EspaceReelChanged</remarks>
    Public Property EspaceReel() As GEO_Region
        Get
            Return mEspaceReel
        End Get
        Private Set(ByVal value As GEO_Region)
            mEspaceReel = value

        End Set
    End Property

    Private mEchelle As Double
    Private mCentimeterScale As Double
    Public Property Echelle() As Double
        Get
            Return mEchelle
        End Get
        Set(ByVal value As Double)
            mEchelle = value
            mCentimeterScale = (10 ^ 5) * mEchelle
        End Set
    End Property

    Private mPrinterResX As Integer
    Public ReadOnly Property PrinterResX() As Integer
        Get
            Return mPrinterResX
        End Get

    End Property

    Private mPrinterResY As Integer
    Public ReadOnly Property PrinterResY() As Integer
        Get
            Return mPrinterResY
        End Get

    End Property

    Private mG As Graphics
    Public Property G() As Graphics
        Get
            Return mG
        End Get
        Set(ByVal value As Graphics)
            mG = value
        End Set
    End Property


    Private mVectorBuffer As Bitmap
    Private mVectorGraphics As Graphics
    Public ReadOnly Property VectorBuffer() As Graphics
        Get
            Return mVectorGraphics
        End Get

    End Property

    Public Sub New(ByVal Pict As PictureBox, ByVal e As System.Windows.Forms.PaintEventArgs)
        mPicture = Pict
        Echelle = 1 / 1000

        mEspaceReel = New GEO_Region
        mG = e.Graphics

        EspaceClient()
    End Sub
    Private mIsbmp As Boolean = False
    ''' <summary>
    ''' Indique si le Mdc concerne un bitmap en mémoire.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsBMP() As Boolean
        Get
            Return mIsbmp
        End Get
    End Property

    Private mEchelleImpression As Double

    ''' <summary>
    ''' Instancie un mdc à partir d'une image bitmap.
    ''' </summary>
    ''' <param name="bm">Bitmap à cloner</param>
    ''' <param name="ech"></param>
    ''' <param name="xworld"></param>
    ''' <param name="yworld"></param>
    ''' <param name="papertype"></param>
    ''' <remarks></remarks>
    

    
   
    Private Sub Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim s = DirectCast(sender, MenuItem)
        s.Tag.Invoke(Me)
    End Sub
    Private WithEvents tim As Timer
   
    Public Sub New(ByVal pict As PictureBox)



        mPicture = pict
        Echelle = (1 / 1000)
        mEspaceReel = New GEO_Region
        '******************************
        Dim BB As New BufferedGraphicsContext

        Using myg As Graphics = pict.CreateGraphics
            mVectorBuffer = New Bitmap(pict.Width, pict.Height, Imaging.PixelFormat.Format32bppPArgb)
            mVectorGraphics = Graphics.FromImage(mVectorBuffer)
            mRasterBuffer = BB.Allocate(myg, pict.ClientRectangle)
            mRasterBuffer.Graphics.Clear(Color.White)
        End Using

        mG = mVectorGraphics

        BB.Dispose()
        '*********************************
        EspaceClient()
    End Sub

    Public Sub New(ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Echelle = 1 / 1000 '0.024
        mEspaceReel = New GEO_Region
        mIsPrinterdc = True
        mG = e.Graphics
        EspaceClient()
    End Sub
    Public Sub SetBuffer()
        mG = mVectorGraphics
    End Sub
    Private Sub EspaceClient()

        If mIsPrinterdc Then
            mhdc = mG.GetHdc
            Dim h As String = GetDeviceCaps(mhdc, VERTRES)
            Dim l As String = GetDeviceCaps(mhdc, HORZRES)
            mrectdessin.Bottom = CInt(Val(h))
            mrectdessin.Right = CInt(Val(l))

            h = GetDeviceCaps(mhdc, LOGPIXELSX)
            l = GetDeviceCaps(mhdc, LOGPIXELSY)

            mPrinterResX = CInt(Val(h))
            mPrinterResY = CInt(Val(l))

            mG.ReleaseHdc()
        Else

            If mIsbmp Then

            Else
                Dim dummy As Integer = GetClientRect(mPicture.Handle, mrectdessin)
            End If

        End If

        mViewOrgY = mrectdessin.Bottom - mrectdessin.Top
        InitEspace()
    End Sub

    Private Sub InitEspace()
        'calcule les dimensions de l'espace réel
        Dim pt As POINTAPI
        SetMetrique()
        ReDim MaxlogPoint(0)



        MaxlogPoint(0).x = mrectdessin.Right
        MaxlogPoint(0).y = mrectdessin.Top




        dummy = DPtoLP(mhdc, MaxlogPoint(0), 1)
        mXlogique = MaxlogPoint(0).x
        mYlogique = MaxlogPoint(0).y
        pt.x = 0
        pt.y = 0
        mDimensionEspaceLogique = DistanceAPI(pt, MaxlogPoint(0))
       
        Dim p As New GEO_Point
        p = LtoR(pt)
        mEspaceReel.left = p.X
        mEspaceReel.Bottom = p.Y
        p = LtoR(MaxlogPoint(0))
        mEspaceReel.Right = p.X
        mEspaceReel.Top = p.Y
        ExitMetric()
    End Sub

    Public Sub SetMetrique()
        'G = Graphics.FromImage(mPicture.InitialImage)
        mhdc = mG.GetHdc
        m_savedDC = SaveDC(mhdc)
        Dim dummy As IntPtr = SetMapMode(mhdc, MM_HIMETRIC)
        dummy = SetViewportOrgEx(mhdc, mViewOrgX, mViewOrgY, mypoint)
        dummy = SetWindowOrgEx(mhdc, mWinOrgX, mWinOrgY, mypoint)
    End Sub

    Public Sub SetMetrique(ByVal hdc As IntPtr)
        'G = Graphics.FromImage(mPicture.InitialImage)        
        m_savedDC = SaveDC(hdc)
        Dim dummy As IntPtr = SetMapMode(hdc, MM_HIMETRIC)
        dummy = SetViewportOrgEx(hdc, mViewOrgX, mViewOrgY, mypoint)
        dummy = SetWindowOrgEx(hdc, mWinOrgX, mWinOrgY, mypoint)
    End Sub

    Public Sub ExitMetric()
        Dim dummy As IntPtr = RestoreDC(mhdc, m_savedDC)
        mG.ReleaseHdc()
    End Sub

    Public Sub ExitMEtric(ByVal hdc As IntPtr)
        Dim dummy As IntPtr = RestoreDC(hdc, m_savedDC)
    End Sub


    Public Sub Refresh()
   
        mPicture.Refresh()

    End Sub

    Private Function DistanceAPI(ByVal p1 As POINTAPI, ByVal p2 As POINTAPI) As Double
        Dim X, Y As Double

        X = p2.x - p1.x
        Y = p2.y - p1.y
        DistanceAPI = Sqrt(X * X + Y * Y)

    End Function

    Private Function DistanceGEO(ByVal p1 As GEO_Point, ByVal p2 As GEO_Point) As Double
        Dim X, Y As Double

        X = p2.X - p1.X
        Y = p2.Y - p1.Y
        DistanceGEO = Sqrt(X * X + Y * Y)
    End Function

    Private Function LtoR(ByVal p As POINTAPI) As GEO_Point
        If Echelle = 0 Then
            LtoR = Nothing
            Exit Function
        End If

        Dim Gp As New GEO_Point

        Gp.X = p.x / (mEchelle * 10 ^ 5) + mEspaceReel.left
        Gp.Y = p.y / (mEchelle * 10 ^ 5) + mEspaceReel.Bottom

        LtoR = Gp
    End Function

    Private Function LtoR(ByVal p() As POINTAPI) As GEO_Point()
        If Echelle = 0 Then
            LtoR = Nothing
            Exit Function
        End If

        Dim Gp() As GEO_Point
        ReDim Gp(p.Count - 1)

        Dim ech As Double = mEchelle * 10 ^ 5

        For i = 0 To p.Count - 1
            Gp(i).X = p(i).x / ech + mEspaceReel.left
            Gp(i).Y = p(i).y / ech + mEspaceReel.Bottom
        Next

        LtoR = Gp
    End Function

    ' SetMetric doit être effectuée
    Public Function GEO_ReelVersPeripherique(ByVal p As System.Collections.Generic.List(Of GEO_Vertex)) As System.Drawing.Point()

        Dim R() As Point
        ReDim R(p.Count - 1)
        Dim ech As Double = mEchelle * 10 ^ 5

        For i = 0 To p.Count - 1
            R(i).X = CInt((p(i).X - mEspaceReel.left) * ech)
            R(i).Y = CInt((p(i).Y - mEspaceReel.Bottom) * ech)
        Next

        Dim dummy As IntPtr = LPtoDP(mhdc, R(0), R.Count)


        Return R

    End Function
    Public Function GEO_ReelVersPeripherique(ByVal p As System.Collections.Generic.List(Of GEO_Point)) As System.Drawing.Point()

        Dim R() As Point
        ReDim R(p.Count - 1)
        Dim ech As Double = mEchelle * 10 ^ 5

        For i = 0 To p.Count - 1
            R(i).X = CInt((p(i).X - mEspaceReel.left) * ech)
            R(i).Y = CInt((p(i).Y - mEspaceReel.Bottom) * ech)
        Next

        Dim dummy As IntPtr = LPtoDP(mhdc, R(0), R.Count)
        

        Return R

    End Function
    

    Public Function GEO_ReelVersPeripherique(ByVal p As GEO_Point) As System.Drawing.Point()

        Dim R(0) As Point

        Dim ech As Double = mEchelle * 10 ^ 5


        R(0).X = CInt((p.X - mEspaceReel.left) * ech)
        R(0).Y = CInt((p.Y - mEspaceReel.Bottom) * ech)


        Dim dummy As IntPtr = LPtoDP(mhdc, R(0), 1)


        Return R

    End Function

    Private Function RtoL(ByVal p As GEO_Point) As POINTAPI
        RtoL.x = CInt((p.X - mEspaceReel.left) * mCentimeterScale)
        RtoL.y = CInt((p.Y - mEspaceReel.Bottom) * mCentimeterScale)
    End Function

    Private Function RtoL(ByVal p() As GEO_Point) As POINTAPI()
        Dim R() As POINTAPI
        ReDim R(p.Count - 1)
        Dim ech As Double = mEchelle * 10 ^ 5
        For i = 0 To p.Count - 1
            R(i).x = CInt((p(i).X - mEspaceReel.left) * ech)
            R(i).y = CInt((p(i).Y - mEspaceReel.Bottom) * ech)
        Next
        RtoL = R
    End Function

    Public Function ReelLogique(ByVal P As GEO_Point) As Point
        Dim P1 As New Point
        P1.X = CInt((P.X - mEspaceReel.left) * 10 ^ 5 * mEchelle)
        P1.Y = CInt((P.Y - mEspaceReel.Bottom) * 10 ^ 5 * mEchelle)
        ReelLogique = P1
    End Function


    Public Function ReelLogique(ByVal x As Double, ByVal y As Double) As Point
        Dim P1 As New Point
        P1.X = CInt((x - mEspaceReel.left) * mCentimeterScale)
        P1.Y = CInt((y - mEspaceReel.Bottom) * mCentimeterScale)
        ReelLogique = P1
    End Function

    Public Function ReelLogique(ByVal P() As GEO_Point) As Point()
        SetMetrique()
        Dim P1() As Point
        ReDim P1(P.Count - 1)
        Dim ech As Double = mEchelle * 10 ^ 5
        For i = 0 To P.Count - 1
            P1(i) = New Point
            P1(i).X = CInt((P(i).X - mEspaceReel.left) * ech)
            P1(i).Y = CInt((P(i).Y - mEspaceReel.Bottom) * ech)
        Next
        ExitMetric()
        ReelLogique = P1
    End Function

    Public Function ReelLogique(ByVal h As GEO_Region) As Rect
        SetMetrique()
        Dim ech As Double = mEchelle * 10 ^ 5
        Dim p1 As New Point
        p1.X = CInt((h.left - mEspaceReel.left) * ech)
        p1.Y = CInt((h.Bottom - mEspaceReel.Bottom) * ech)
        Dim p2 As New Point
        p2.X = CInt((h.Right - mEspaceReel.left) * ech)
        p2.Y = CInt((h.Top - mEspaceReel.Bottom) * ech)

        Dim r As New Rect(p1.X, p2.Y, p2.X - p1.X, p2.Y - p1.Y)

        ExitMetric()
        ReelLogique = r
    End Function

    Public Function ReelLogiqueDes(ByVal h As GEO_Region) As Rect
        SetMetrique()
        Dim p1 As New Point
        p1.X = CInt((h.left - mEspaceReel.left) * 10 ^ 5 * mEchelle)
        p1.Y = CInt((h.Bottom - mEspaceReel.Bottom) * 10 ^ 5 * mEchelle)
        Dim p2 As New Point
        p2.X = CInt((h.Right - mEspaceReel.left) * 10 ^ 5 * mEchelle)
        p2.Y = CInt((h.Top - mEspaceReel.Bottom) * 10 ^ 5 * mEchelle)

        Dim r As New Rect(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y)

        ExitMetric()
        ReelLogiqueDes = r
    End Function

#If Wkb = 1 Then
    Public Function ReelPeriph(ByVal P As System.Collections.Generic.List(Of Vect_Vertex)) As Point()
        SetMetrique()

        Dim GP(P.Count - 1) As GEO_Point
        ReDim lpPoint(P.Count - 1)

        For i = 0 To P.Count - 1
            Dim p2 As New GEO_Point(P(i).X, P(i).Y)
            GP(i) = p2
        Next i

        lpPoint = RtoL(GP)

        Dim dummy As IntPtr = LPtoDP(mhdc, lpPoint(0), lpPoint.Count)
        Dim p1(lpPoint.Count - 1) As Point
        For i = 0 To lpPoint.Count - 1
            p1(i) = New Point(lpPoint(i).x, lpPoint(i).y)
        Next

        ExitMetric()

        ReelPeriph = p1

    End Function


    Public Function ReelPeriph(ByVal P As System.Collections.Generic.List(Of System.Collections.Generic.List(Of Vect_Vertex))) As Point()

        SetMetrique()
        Dim k As Integer = 0

        Dim GP() As GEO_Point
        For i = 0 To P.Count - 1
            For j = 0 To P(i).Count - 1
                Dim p2 As New GEO_Point(P(i)(j).X, P(i)(j).Y)
                ReDim Preserve GP(k)
                GP(k) = p2
                k = k + 1
            Next
        Next i
        ReDim lpPoint(k - 1)
        lpPoint = RtoL(GP)

        Dim dummy As IntPtr = LPtoDP(mhdc, lpPoint(0), lpPoint.Count)
        Dim p1(lpPoint.Count - 1) As Point
        For i = 0 To lpPoint.Count - 1
            p1(i) = New Point(lpPoint(i).x, lpPoint(i).y)
        Next

        ExitMetric()

        ReelPeriph = p1
    End Function

    Public Function ReelLogique(ByVal Lp As System.Collections.Generic.List(Of Vect_Vertex)) As Point()
        SetMetrique()
        Dim P1() As Point
        ReDim P1(Lp.Count - 1)

        Dim ech As Double = mEchelle * 10 ^ 5
        For i = 0 To Lp.Count - 1
            P1(i) = New Point
            P1(i).X = CInt((Lp(i).X - mEspaceReel.left) * ech)
            P1(i).Y = CInt((Lp(i).Y - mEspaceReel.Bottom) * ech)
        Next

        ExitMetric()
        ReelLogique = P1
    End Function

    Public Function ReelLogique(ByVal scalar As Single) As Integer
        Return CInt(Math.Floor(scalar * mCentimeterScale))
    End Function

    Public Function ReelLogique(ByVal Lp As System.Collections.Generic.List(Of System.Collections.Generic.List(Of Vect_Vertex))) As Point()
        SetMetrique()
        Dim P1() As Point


        Dim k As Integer = 0
        Dim ech As Double = mEchelle * 10 ^ 5

        For i = 0 To Lp.Count - 1
            For j = 0 To Lp(i).Count - 1
                ReDim Preserve P1(k)
                P1(k) = New Point
                P1(k).X = CInt((Lp(i)(j).X - mEspaceReel.left) * ech)
                P1(k).Y = CInt((Lp(i)(j).Y - mEspaceReel.Bottom) * ech)
                k = k + 1
            Next
        Next

        ExitMetric()
        ReelLogique = P1
    End Function
#End If

    Public Sub zoomreel(ByVal r As GEO_Region)
        EspaceClient()
        SetMetrique()
        ReDim lpPoint(1)
        ReDim mlpgeo(1)

        mlpgeo(0) = New GEO_Point(mEspaceReel.left, mEspaceReel.Bottom)
        mlpgeo(1) = New GEO_Point(mEspaceReel.Right, mEspaceReel.Top)


        lpPoint(0) = RtoL(mlpgeo(0))
        lpPoint(1) = RtoL(mlpgeo(1))

        ReDim mlpgeo(1)
        mlpgeo(0) = New GEO_Point(r.left, r.Bottom)
        mlpgeo(1) = New GEO_Point(r.Right, r.Top)

        mEspaceReel.left = r.left
        mEspaceReel.Bottom = r.Bottom
        Echelle = mDimensionEspaceLogique / (DistanceGEO(mlpgeo(0), mlpgeo(1)) * 10 ^ 5)
        mlpgeo(0) = LtoR(lpPoint(0))
        mlpgeo(1) = LtoR(lpPoint(1))

        mEspaceReel.left = mlpgeo(0).X
        mEspaceReel.Right = mlpgeo(1).X
        mEspaceReel.Bottom = mlpgeo(0).Y
        mEspaceReel.Top = mlpgeo(1).Y
        ExitMetric()
        EspaceReel = mEspaceReel
    End Sub

    Public Sub zoomReel(ByVal X1 As Double, ByVal Y1 As Double, ByVal X2 As Double, ByVal Y2 As Double)

        If X1 = 0 And X2 = 0 And Y1 = 0 And Y2 = 0 Then Exit Sub

        EspaceClient()
        SetMetrique()
        ReDim lpPoint(1)
        ReDim mlpgeo(1)

        mlpgeo(0) = New GEO_Point(mEspaceReel.left, mEspaceReel.Bottom)
        mlpgeo(1) = New GEO_Point(mEspaceReel.Right, mEspaceReel.Top)


        lpPoint(0) = RtoL(mlpgeo(0))
        lpPoint(1) = RtoL(mlpgeo(1))

        ReDim mlpgeo(1)
        mlpgeo(0) = New GEO_Point(X1, Y1)
        mlpgeo(1) = New GEO_Point(X2, Y2)

        mEspaceReel.left = X1
        mEspaceReel.Bottom = Y1
        Echelle = mDimensionEspaceLogique / (DistanceGEO(mlpgeo(0), mlpgeo(1)) * 10 ^ 5)
        mlpgeo(0) = LtoR(lpPoint(0))
        mlpgeo(1) = LtoR(lpPoint(1))

        mEspaceReel.left = mlpgeo(0).X
        mEspaceReel.Right = mlpgeo(1).X
        mEspaceReel.Bottom = mlpgeo(0).Y
        mEspaceReel.Top = mlpgeo(1).Y
        ExitMetric()
        EspaceReel = mEspaceReel
    End Sub

    Public Sub ZoomYReel(ByVal X1 As Double, ByVal Y1 As Double, ByVal X2 As Double, ByVal Y2 As Double)
        If Y1 = Y2 Then Exit Sub
        EspaceClient()
        SetMetrique()
        ReDim lpPoint(1)
        ReDim mlpgeo(1)

        mlpgeo(0) = New GEO_Point(mEspaceReel.left, mEspaceReel.Bottom)
        mlpgeo(1) = New GEO_Point(mEspaceReel.Right, mEspaceReel.Top)


        lpPoint(0) = RtoL(mlpgeo(0))
        lpPoint(1) = RtoL(mlpgeo(1))

        ReDim mlpgeo(1)
        mlpgeo(0) = New GEO_Point(X1, Y1)
        mlpgeo(1) = New GEO_Point(X2, Y2)

        mEspaceReel.left = X1
        mEspaceReel.Bottom = Y1
        Echelle = mYlogique / ((mlpgeo(1).Y - mlpgeo(0).Y) * 10 ^ 5)
        mlpgeo(0) = LtoR(lpPoint(0))
        mlpgeo(1) = LtoR(lpPoint(1))

        mEspaceReel.left = mlpgeo(0).X
        mEspaceReel.Right = mlpgeo(1).X
        mEspaceReel.Bottom = mlpgeo(0).Y
        mEspaceReel.Top = mlpgeo(1).Y
        ExitMetric()
        EspaceReel = mEspaceReel
    End Sub

    Public Sub ZoomYreelCentre(ByVal R As GEO_Region)
        EspaceClient()
        SetMetrique()
        ReDim lpPoint(2)
        ReDim mlpgeo(1)

        mlpgeo(0) = New GEO_Point(mEspaceReel.left, mEspaceReel.Bottom)
        mlpgeo(1) = New GEO_Point(mEspaceReel.Right, mEspaceReel.Top)


        lpPoint(0) = RtoL(mlpgeo(0))
        lpPoint(1) = RtoL(mlpgeo(1))
        lpPoint(2) = RtoL(mEspaceReel.Milieu)

        ReDim mlpgeo(1)
        mlpgeo(0) = New GEO_Point(R.left, R.Bottom)
        mlpgeo(1) = New GEO_Point(R.Right, R.Top)

        Echelle = mYlogique / ((mlpgeo(1).Y - mlpgeo(0).Y) * 10 ^ 5)
        mEspaceReel.left = R.Milieu.X - lpPoint(2).x / (mEchelle * 10 ^ 5)
        mEspaceReel.Bottom = R.Bottom

        mlpgeo(0) = LtoR(lpPoint(0))
        mlpgeo(1) = LtoR(lpPoint(1))

        mEspaceReel.left = mlpgeo(0).X
        mEspaceReel.Right = mlpgeo(1).X
        mEspaceReel.Bottom = mlpgeo(0).Y
        mEspaceReel.Top = mlpgeo(1).Y
        ExitMetric()
        EspaceReel = mEspaceReel

    End Sub

    Public Sub ZoomXreelCentre(ByVal R As GEO_Region)
        If R Is Nothing Then Exit Sub
        EspaceClient()
        SetMetrique()
        ReDim lpPoint(2)
        ReDim mlpgeo(1)

        mlpgeo(0) = New GEO_Point(mEspaceReel.left, mEspaceReel.Bottom)
        mlpgeo(1) = New GEO_Point(mEspaceReel.Right, mEspaceReel.Top)


        lpPoint(0) = RtoL(mlpgeo(0))
        lpPoint(1) = RtoL(mlpgeo(1))
        lpPoint(2) = RtoL(mEspaceReel.Milieu)

        ReDim mlpgeo(1)
        mlpgeo(0) = New GEO_Point(R.left, R.Bottom)
        mlpgeo(1) = New GEO_Point(R.Right, R.Top)

        Echelle = mXlogique / ((mlpgeo(1).X - mlpgeo(0).X) * 10 ^ 5)
        mEspaceReel.Bottom = R.Milieu.Y - lpPoint(2).y / (mEchelle * 10 ^ 5)
        mEspaceReel.left = R.left

        mlpgeo(0) = LtoR(lpPoint(0))
        mlpgeo(1) = LtoR(lpPoint(1))

        mEspaceReel.left = mlpgeo(0).X
        mEspaceReel.Right = mlpgeo(1).X
        mEspaceReel.Bottom = mlpgeo(0).Y
        mEspaceReel.Top = mlpgeo(1).Y
        ExitMetric()
        EspaceReel = mEspaceReel
    End Sub

    Public Function PeriphReel(ByVal P As Point) As GEO_Point
        Dim pr As New GEO_Point
        SetMetrique()
        ReDim lpPoint(0)
        lpPoint(0).x = P.X
        lpPoint(0).y = P.Y
        Dim dummy As IntPtr = DPtoLP(mhdc, lpPoint(0), 1)
        pr = LtoR(lpPoint(0))
        ExitMetric()
        PeriphReel = pr
    End Function

    Public Function ReelPeriph(ByVal Pr As GEO_Point) As Point
        ReDim lpPoint(0)
        SetMetrique()
        lpPoint(0) = RtoL(Pr)
        Dim dummy As IntPtr = LPtoDP(mhdc, lpPoint(0), 1)
        Dim p As New Point(lpPoint(0).x, lpPoint(0).y)
        ExitMetric()

        ReelPeriph = p
    End Function

    Public Function ReelPeriph(ByVal x As Double, ByVal y As Double) As Point
        Dim p(0) As POINTAPI
        p(0) = RtoL(New GEO_Point(x, y))
        Dim dummy As IntPtr = LPtoDP(mhdc, p(0), 1)
        Return New Point(p(0).x, p(0).y)
    End Function

   
    Public Function ReelPeriph(ByVal GR As GEO_Region) As Rectangle
        ReDim lpPoint(1)
        Dim Rp(1) As GEO_Point
        Rp(0) = New GEO_Point(GR.left, GR.Bottom)
        Rp(1) = New GEO_Point(GR.Right, GR.Top)

        SetMetrique()
        lpPoint = RtoL(Rp)
        Dim dummy As IntPtr = LPtoDP(mhdc, lpPoint(0), 2)
        ExitMetric()

        Dim r As New Rectangle(lpPoint(0).x, lpPoint(1).y, lpPoint(1).x - lpPoint(0).x, Abs(lpPoint(1).y - lpPoint(0).y))
        ReelPeriph = r
    End Function

    '**************************a modifier*************************************
    Public Sub LinePeriph(ByVal P1 As Point, ByVal P2 As Point, Optional ByVal mode As Integer = 13)
        Dim OldPen As IntPtr
        Dim UsePen As IntPtr
        Dim dummy As IntPtr
        Dim OldMode As IntPtr
        Dim ap(1) As POINTAPI
        Dim ap1(0) As POINTAPI
        ap(0).x = P1.X
        ap(0).y = P1.Y
        ap(1).x = P2.X
        ap(1).y = P2.Y


        SetMetrique()

        OldMode = SetROP2(mhdc, mode)

        UsePen = CreatePen(0, 1, 255)
        OldPen = SelectObject(mhdc, UsePen)

        dummy = DPtoLP(mhdc, ap(0), 2)
        Dim x As IntPtr = ap(0).x


        dummy = MoveToEx(mhdc, ap(0).x, ap(0).y, ap1(0))
        dummy = LineTo(mhdc, ap(1).x, ap(1).y)



        dummy = SelectObject(mhdc, OldPen)
        dummy = DeleteObject(UsePen)
        dummy = SetROP2(mhdc, OldMode)

        ExitMetric()
    End Sub

    Public Sub linereal(ByVal p1 As GEO_Point, ByVal p2 As GEO_Point, ByVal couleur As Long, Optional ByVal mode As Long = 13)

        Dim ap1 As POINTAPI
        Dim ap2 As POINTAPI
        Dim OldPen As IntPtr
        Dim UsePen As IntPtr
        Dim dummy As IntPtr
        Dim OldMode As IntPtr

        SetMetrique()

        OldMode = SetROP2(mhdc, mode)

        UsePen = CreatePen(PS_SOLID, 1, couleur)
        OldPen = SelectObject(mhdc, UsePen)
        ap1 = RtoL(p1)
        ap2 = RtoL(p2)

        'Debug.Print "Xd " & Str(ap1.x) & "    Yd " & Str(ap1.Y)
        dummy = MoveToEx(mhdc, ap1.x, ap1.y, ap1)
        dummy = LineTo(mhdc, ap2.x, ap2.y)

        'Debug.Print "Xf " & Str(ap2.x) & "    Yf " & Str(ap2.Y)

        dummy = SelectObject(mhdc, OldPen)
        dummy = DeleteObject(UsePen)
        dummy = SetROP2(mhdc, OldMode)
        ExitMetric()

    End Sub

    Public Sub CadrePeriph(ByVal P1 As Point, ByVal p2 As Point, ByVal coul As IntPtr, Optional ByVal mode As Integer = 13)

        Dim OldPen As IntPtr
        Dim UsePen As IntPtr
        Dim dummy As IntPtr
        Dim OldMode As IntPtr
        Dim ap(1) As POINTAPI
        Dim ap1(0) As POINTAPI

        ap(0).x = P1.X
        ap(0).y = P1.Y
        ap(1).x = p2.X
        ap(1).y = p2.Y

        SetMetrique()

        OldMode = SetROP2(mhdc, mode)

        UsePen = CreatePen(0, 1, coul)
        OldPen = SelectObject(mhdc, UsePen)

        dummy = DPtoLP(mhdc, ap(0), 2)

        dummy = MoveToEx(mhdc, ap(0).x, ap(0).y, ap1(0))
        dummy = LineTo(mhdc, ap(1).x, ap(0).y)
        dummy = LineTo(mhdc, ap(1).x, ap(1).y)
        dummy = LineTo(mhdc, ap(0).x, ap(1).y)
        dummy = LineTo(mhdc, ap(0).x, ap(0).y)

        dummy = SelectObject(mhdc, OldPen)
        dummy = DeleteObject(UsePen)
        dummy = SetROP2(mhdc, OldMode)

        ExitMetric()
    End Sub

    Public Sub RectanglePeriph(ByVal P1 As Point, ByVal P2 As Point, ByVal Coul As IntPtr, Optional ByVal Mode As Integer = 13)

        Dim dummy As IntPtr
        Dim OldMode As IntPtr
        Dim OldBrush As IntPtr
        Dim UseBrush As IntPtr
        Dim ap(3) As POINTAPI


        ap(0).x = P1.X
        ap(0).y = P1.Y
        ap(1).x = P1.X
        ap(1).y = P2.Y
        ap(2).x = P2.X
        ap(2).y = P2.Y
        ap(3).x = P2.X
        ap(3).y = P1.Y

        SetMetrique()

        OldMode = SetROP2(mhdc, Mode)


        UseBrush = CreateSolidBrush(Coul)
        OldBrush = SelectObject(mhdc, UseBrush)


        dummy = DPtoLP(mhdc, ap(0), 4)
        dummy = Polygon(mhdc, ap(0), 4)


        dummy = SelectObject(mhdc, OldBrush)
        dummy = DeleteObject(UseBrush)
        dummy = SetROP2(mhdc, OldMode)

        ExitMetric()
    End Sub

    Public Sub RectangleReel(ByVal PR1 As GEO_Point, ByVal PR2 As GEO_Point, ByVal Coul As IntPtr, Optional ByVal mode As Long = 13)
        Dim dummy As IntPtr

        Dim OldBrush As IntPtr
        Dim UseBrush As IntPtr
        Dim oldmode As IntPtr

        SetMetrique()
        oldmode = SetROP2(mhdc, mode)

        UseBrush = CreateSolidBrush(Coul)
        OldBrush = SelectObject(mhdc, UseBrush)

        Dim pl(3) As POINTAPI
        pl(0) = RtoL(PR1)
        pl(2) = RtoL(PR2)
        pl(1).x = pl(2).x
        pl(1).y = pl(0).y
        pl(3).x = pl(0).x
        pl(3).y = pl(2).y

        dummy = Polygon(mhdc, pl(0), 4)


        dummy = SelectObject(mhdc, OldBrush)
        dummy = DeleteObject(UseBrush)
        dummy = SetROP2(mhdc, oldmode)

        ExitMetric()

    End Sub

    Public Sub RectangleReel(ByVal GR As GEO_Region, ByVal Coul As IntPtr, Optional ByVal mode As Long = 13)
        Dim dummy As IntPtr

        Dim OldBrush As IntPtr
        Dim UseBrush As IntPtr
        Dim oldmode As IntPtr

        SetMetrique()
        oldmode = SetROP2(mhdc, mode)

        UseBrush = CreateSolidBrush(Coul)
        OldBrush = SelectObject(mhdc, UseBrush)

        Dim pl(3) As POINTAPI
        pl = RtoL(GR.TabPoint)

        dummy = Polygon(mhdc, pl(0), 4)

        dummy = SelectObject(mhdc, OldBrush)
        dummy = DeleteObject(UseBrush)
        dummy = SetROP2(mhdc, oldmode)

        ExitMetric()

    End Sub

    Public Sub FixeEchelle(ByVal P As GEO_Point, ByVal ech As Double)

        EspaceClient()

        SetMetrique()

        mEspaceReel.left = P.X
        mEspaceReel.Bottom = P.Y
        Echelle = 1 / ech

        ExitMetric()
        EspaceClient()

    End Sub
    Public Sub translate(ByVal dx As Double, ByVal dy As Double)

        mEspaceReel.Bottom = mEspaceReel.Bottom + dy
        mEspaceReel.Top = mEspaceReel.Top + dy
        mEspaceReel.left = mEspaceReel.left + dx
        mEspaceReel.Right = mEspaceReel.Right + dy

        EspaceClient()
        EspaceReel = mEspaceReel

    End Sub
    Public Sub OffsetPeriph(ByVal P1 As Point, ByVal P2 As Point)
        EspaceClient()
        SetMetrique()
        ReDim lpPoint(1)
        ReDim mlpgeo(1)
        lpPoint(1).x = P2.X
        lpPoint(1).y = P2.Y
        lpPoint(0).x = P1.X
        lpPoint(0).y = P1.Y

        dummy& = DPtoLP(mhdc, lpPoint(0), 2)
        mlpgeo(0) = LtoR(lpPoint(0))
        mlpgeo(1) = LtoR(lpPoint(1))
        mEspaceReel.Bottom = mEspaceReel.Bottom + (mlpgeo(0).Y - mlpgeo(1).Y)
        mEspaceReel.left = mEspaceReel.left + (mlpgeo(0).X - mlpgeo(1).X)
        mEspaceReel.Right = mEspaceReel.Right + (mlpgeo(0).X - mlpgeo(1).X)
        mEspaceReel.Top = mEspaceReel.Top + (mlpgeo(0).Y - mlpgeo(1).Y)
        ExitMetric()
        EspaceReel = mEspaceReel
    End Sub

    Public Sub writetext(ByVal MyText As String, ByVal x As Double, ByVal y As Double, ByVal nompolice As String, ByVal Taille As Double, ByVal align As Integer, ByVal angle As Double, ByVal Affiche As Boolean, Optional ByVal Color& = 0, Optional ByVal weight As Long = 0, Optional ByVal underline As Byte = 0)
        Dim lf As LOGFONT
        Dim oldfont As Integer
        Dim alignorigin As Integer
        Dim newfont As Integer
        Dim Oldcolor As Integer
        Dim Usecolor As Integer
        Dim di As Integer
        Dim pointattache As POINTAPI
        Dim pointlog As POINTAPI
        Dim p As New GEO_Point
        Dim SI As New Size
        Dim oldbk As Integer

        SetMetrique()

        p.X = x
        p.Y = y
        pointattache = RtoL(p)
        p.X = x + Taille
        p.Y = y + Taille
        pointlog = RtoL(p)


        Dim f As Font = New Font(nompolice, Taille, FontStyle.Regular, GraphicsUnit.Point)

        lf = New LOGFONT

      


        lf.lfHeight = pointlog.y - pointattache.y
        lf.lfEscapement = -1 * Round(angle * 10, 0)
        lf.lfFaceName = f.Name
        If weight <> 0 Then
            lf.lfWeight = weight
        End If

        If underline = 1 Then
            lf.lfUnderline = 1
        End If
        mrectText = New Rect
        newfont = CreateFontIndirect(lf)

        oldfont = SelectObject(mhdc, newfont)
        di = GetTextExtentPoint32(mhdc, MyText, Len(MyText), SI)
        mrectText.Bottom = pointattache.y
        mrectText.Top = mrectText.Bottom + SI.Height
        'stocke l'alignement d'origine
        'calcul l'ancrex en fonction de l'alignement
        'pas de y car pas d'alignement verticale Bottom obligatoire

        Select Case align
            Case 0
                alignorigin = SetTextAlign(mhdc, TA_LEFT Or TA_BOTTOM Or TA_UPDATECP)
                mrectText.Left = pointattache.x
            Case 1
                alignorigin = SetTextAlign(mhdc, TA_RIGHT Or TA_BOTTOM Or TA_UPDATECP)
                mrectText.Left = pointattache.x - SI.Width
            Case 2
                alignorigin = SetTextAlign(mhdc, TA_CENTER Or TA_BOTTOM Or TA_UPDATECP)
                mrectText.Left = pointattache.x - SI.Width / 2
            Case 3
                alignorigin = SetTextAlign(mhdc, TA_CENTER Or TA_CENTER Or TA_UPDATECP)
                mrectText.Left = pointattache.x - SI.Width / 2

        End Select


        mrectText.Right = mrectText.Left + SI.Width
        'ConvertEspaceText()

        If Affiche Then
            oldbk = SetBkMode(mhdc, 1)
            Oldcolor = GetTextColor(mhdc)
            Usecolor = Color&
            dummy& = SetTextColor(mhdc, Usecolor)
            di = MoveToEx(mhdc, pointattache.x, pointattache.y, pointlog)
            di = TextOut(mhdc, 0, 0, MyText, Len(MyText))
            Usecolor = SetTextColor(mhdc, Oldcolor)
            di = SetBkMode(mhdc, oldbk)
        End If


        di = SetTextAlign(mhdc, alignorigin)
        di = SelectObject(mhdc, oldfont)

        DeleteObject(newfont)

        ExitMetric()
    End Sub

    Public Sub WritetextWithoutMetric(ByVal MyText As String, ByVal x As Double, ByVal y As Double, ByVal nompolice As String, ByVal Taille As Double, ByVal align As Integer, ByVal angle As Double, ByVal Affiche As Boolean, Optional ByVal Color& = 0, Optional ByVal weight As Long = 0, Optional ByVal underline As Byte = 0)
        Dim lf As LOGFONT
        Dim oldfont As Integer
        Dim alignorigin As Integer
        Dim newfont As Integer
        Dim Oldcolor As Integer
        Dim Usecolor As Integer
        Dim di As Integer
        Dim pointattache As POINTAPI
        Dim pointlog As POINTAPI
        Dim p As New GEO_Point
        Dim SI As New Size
        Dim oldbk As Integer

        p.X = x
        p.Y = y
        pointattache = RtoL(p)
        p.X = x + Taille
        p.Y = y + Taille
        pointlog = RtoL(p)


        Dim f As Font = New Font(nompolice, Taille, FontStyle.Regular, GraphicsUnit.Point)

        lf = New LOGFONT

        


        lf.lfHeight = pointlog.y - pointattache.y
        lf.lfEscapement = -1 * Round(angle * 10, 0)
        lf.lfFaceName = f.Name
        If weight <> 0 Then
            lf.lfWeight = weight
        End If

        If underline = 1 Then
            lf.lfUnderline = 1
        End If
        mrectText = New Rect
        newfont = CreateFontIndirect(lf)

        oldfont = SelectObject(mhdc, newfont)
        di = GetTextExtentPoint32(mhdc, MyText, Len(MyText), SI)
        mrectText.Bottom = pointattache.y
        mrectText.Top = mrectText.Bottom + SI.Height
        'stocke l'alignement d'origine
        'calcul l'ancrex en fonction de l'alignement
        'pas de y car pas d'alignement verticale Bottom obligatoire

        Select Case align
            Case 0
                alignorigin = SetTextAlign(mhdc, TA_LEFT Or TA_BOTTOM Or TA_UPDATECP)
                mrectText.Left = pointattache.x
            Case 1
                alignorigin = SetTextAlign(mhdc, TA_RIGHT Or TA_BOTTOM Or TA_UPDATECP)
                mrectText.Left = pointattache.x - SI.Width
            Case 2
                alignorigin = SetTextAlign(mhdc, TA_CENTER Or TA_BOTTOM Or TA_UPDATECP)
                mrectText.Left = pointattache.x - SI.Width / 2
            Case 3
                alignorigin = SetTextAlign(mhdc, TA_CENTER Or TA_CENTER Or TA_UPDATECP)
                mrectText.Left = pointattache.x - SI.Width / 2

        End Select


        mrectText.Right = mrectText.Left + SI.Width
        'ConvertEspaceText()

        If Affiche Then
            oldbk = SetBkMode(mhdc, 1)
            Oldcolor = GetTextColor(mhdc)
            Usecolor = Color&
            dummy& = SetTextColor(mhdc, Usecolor)
            di = MoveToEx(mhdc, pointattache.x, pointattache.y, pointlog)
            di = TextOut(mhdc, 0, 0, MyText, Len(MyText))
            Usecolor = SetTextColor(mhdc, Oldcolor)
            di = SetBkMode(mhdc, oldbk)
        End If


        di = SetTextAlign(mhdc, alignorigin)
        di = SelectObject(mhdc, oldfont)

        DeleteObject(newfont)

    End Sub

    Public Function PixelScreen() As Double
        Dim pa As POINTAPI()
        ReDim pa(1)
        pa(0).x = 0
        pa(0).y = 0
        pa(1).x = 1
        pa(1).y = 0

        SetMetrique()
        Dim dummy As Integer = DPtoLP(mhdc, pa(0), 2)
        Dim p1 As New GEO_Point
        p1 = LtoR(pa(0))
        Dim p2 As New GEO_Point
        p2 = LtoR(pa(1))

        ExitMetric()
        Return p2.X - p1.X
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()

        mVectorGraphics.Dispose()
        mVectorBuffer.Dispose()

        If (mRasterBuffer IsNot Nothing) Then
            mRasterBuffer.Dispose()
        End If
        

    End Sub

    Public Sub PictureBoxSizeChanged(ByVal pict As PictureBox)
        'mPicture.Height = pict.Height
        'mPicture.Width = pict.Width
        'Echelle = (1 / 1000)
        'mEspaceReel = New GEO_Region
        '******************************

        mVectorGraphics.Dispose()
        mVectorBuffer.Dispose()
        If (mRasterBuffer IsNot Nothing) Then
            mRasterBuffer.Dispose()
        End If
        Dim BB As New BufferedGraphicsContext

        mVectorBuffer = New Bitmap(pict.Width, pict.Height, Imaging.PixelFormat.Format32bppPArgb)
        mVectorGraphics = Graphics.FromImage(mVectorBuffer)

        Using myg As Graphics = pict.CreateGraphics
            mRasterBuffer = BB.Allocate(myg, pict.ClientRectangle)
            mRasterBuffer.Graphics.Clear(Color.White)
        End Using

        'mG = mPicture.CreateGraphics
        mG = mVectorGraphics

        BB.Dispose()
        '*********************************
        EspaceClient()
    End Sub
    Private mRasterBuffer As BufferedGraphics
    Public ReadOnly Property RasterBuffer() As BufferedGraphics
        Get
            Return mRasterBuffer
        End Get
    End Property

    Private Sub SetHDCToMetric(ByVal hdc As IntPtr)
        SetViewportOrgEx(hdc, 0, PictureB.ClientRectangle.Height, New POINTAPI())
        SetMapMode(hdc, MM_HIMETRIC)
    End Sub

    Public Event EspaceReelChanged(ByVal sender As Object, ByVal e As GeoRegionArgs)
    Public Overridable Sub OnEspaceReelChanged(ByVal e As GeoRegionArgs)
        RaiseEvent EspaceReelChanged(Me, e)
    End Sub

    Public Sub Render(ByVal g As Graphics)
        If mRasterBuffer IsNot Nothing Then mRasterBuffer.Render(g)
        g.DrawImage(mVectorBuffer, Point.Empty)
    End Sub

    Public ReadOnly Property DeviceHeight() As Integer
        Get
            Return Math.Abs(mrectdessin.Bottom - mrectdessin.Top)
        End Get
    End Property

    Public ReadOnly Property DeviceWidth() As Integer
        Get
            Return mrectdessin.Right
        End Get
    End Property

    

    Public Event MdcClick(ByVal mdc As Mdc, ByVal e As MdcCoordEventArgs)
    Public Event MdcDblClick(ByVal mdc As Mdc, ByVal e As MdcCoordEventArgs)
    Public Event MdcMouseDown(ByVal mdc As Mdc, ByVal e As MdcCoordEventArgs)
    Public Event MdcMouseMove(ByVal mdc As Mdc, ByVal e As MdcCoordEventArgs)
    Public Event MdcMouseUp(ByVal mdc As Mdc, ByVal e As MdcCoordEventArgs)
    Public Event MdcMouseWheel(ByVal mdc As Mdc, ByVal e As MdcCoordEventArgs)
    Public Event MdcRefreshed(ByVal mdc As Mdc, ByVal e As GeoRegionArgs)

    Public Overridable Sub OnMdcRefreshed(ByVal e As GeoRegionArgs)
        RaiseEvent MdcRefreshed(Me, e)
    End Sub


    Private Function GenererCoord(ByVal e As MouseEventArgs) As MdcCoordEventArgs
        Dim l = PeriphReel(e.Location)
        Dim lo = ReelLogique(l)
        Dim ev As New MdcCoordEventArgs() With {.WordlCoords = l, .LogicalCoord = lo, .PeriphCoord = e.Location, .MouseEvent = e}
        Return ev
    End Function
    Private Sub mPicture_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mPicture.MouseClick
        RaiseEvent MdcClick(Me, GenererCoord(e))
    End Sub

    Private Sub mPicture_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mPicture.MouseDoubleClick
        RaiseEvent MdcDblClick(Me, GenererCoord(e))
    End Sub

    Private Sub mPicture_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mPicture.MouseDown
        RaiseEvent MdcMouseDown(Me, GenererCoord(e))
    End Sub

    Private Sub mPicture_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mPicture.MouseMove
        RaiseEvent MdcMouseMove(Me, GenererCoord(e))
    End Sub

    Private Sub mPicture_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mPicture.MouseUp
        RaiseEvent MdcMouseUp(Me, GenererCoord(e))
    End Sub

    Private Sub mPicture_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mPicture.MouseWheel
        RaiseEvent MdcMouseWheel(Me, GenererCoord(e))
    End Sub

    ''' <summary>
    ''' Disposes les buffers.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Release()
        If mRasterBuffer IsNot Nothing Then
            mRasterBuffer.Dispose()
        End If
        If mVectorBuffer IsNot Nothing Then
            mVectorGraphics.Dispose()
            mVectorBuffer.Dispose()
        End If
    End Sub



End Class



Public Class MdcCoordEventArgs
    Inherits EventArgs

    Private m_wordCoord As GEO_Point
    Public Property WordlCoords() As GEO_Point
        Get
            Return m_wordCoord
        End Get
        Set(ByVal value As GEO_Point)
            m_wordCoord = value
        End Set
    End Property


    Private m_PeriphCoord As Point
    Public Property PeriphCoord() As Point
        Get
            Return m_PeriphCoord
        End Get
        Set(ByVal value As Point)
            m_PeriphCoord = value
        End Set
    End Property


    Private m_LogicalCoord As Point
    Public Property LogicalCoord() As Point
        Get
            Return m_LogicalCoord
        End Get
        Set(ByVal value As Point)
            m_LogicalCoord = value
        End Set
    End Property



    Private m_MouseEvent As MouseEventArgs
    Public Property MouseEvent() As MouseEventArgs
        Get
            Return m_MouseEvent
        End Get
        Set(ByVal value As MouseEventArgs)
            m_MouseEvent = value
        End Set
    End Property

End Class