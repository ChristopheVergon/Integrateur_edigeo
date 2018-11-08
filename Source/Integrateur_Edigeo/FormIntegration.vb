
Imports System.Threading

Public Class FormIntegration
    Private MonLecteur As Lecteur
    Private DicListe As ListeZone
    Private MCDListe As ListeZone
    Private VECListe As ListeZone

    Private mFlog As System.IO.StreamWriter
    Public Property Flog() As System.IO.StreamWriter
        Get
            Return mFlog
        End Get
        Set(ByVal value As System.IO.StreamWriter)
            mFlog = value
        End Set
    End Property



    Private mTHFS As System.Collections.Generic.List(Of String)

    Public Property THFS() As System.Collections.Generic.List(Of String)
        Get
            Return mTHFS
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            mTHFS = value
        End Set
    End Property

    Private mLotForWork As THF = Nothing

  
   
   
    Private Sub IntegrationEdigeo(ByVal thf As String, ByVal count As Integer)

        MonLecteur = New Lecteur(thf.ToString, Me)
        Dim arg1(1) As Object
        arg1(0) = "Intégration du lot " & count
        arg1(1) = 0
        Me.Invoke(TitreInstance, arg1)

        If MonLecteur.THF.VerifieBOMEOM("Analyse et Vérification THF", 15) Then
            If MonLecteur.THF.Initialise() Then
                If MonLecteur.THF.ChargeZone() Then

                    MonLecteur.THF.InitDictionary()
                    Flog.WriteLine("EDIGEO INTEGRE OK")
                    Flog.WriteLine("******************************************************")
                Else
                    'MsgBox("Abandon")
                    Exit Sub
                End If
            Else
                'MsgBox("Abandon")
                Exit Sub
            End If

        Else
            'MsgBox("Abandon")
            Exit Sub
        End If
    End Sub
    Private nblot_traites As Integer = 0

    Private Sub RealiseProgres()
        Label1.Text = nblot_traites
    End Sub

    Private Delegate Sub Progres()

    Private ProgresInstance As New Progres(AddressOf RealiseProgres)

    Public Delegate Sub AvancementDelegate(ByVal chaine As String, ByVal position As Single)

    Public DeuxCarreInstance As New AvancementDelegate(AddressOf DeuxCarre)
    Public TitreInstance As New AvancementDelegate(AddressOf TitreLot)
    Public CarreRougeInstance As New AvancementDelegate(AddressOf CarreRouge)
    Public CarreVertInstance As New AvancementDelegate(AddressOf CarreVert)

    Public Function testinvoke(del As System.Delegate, ByVal ParamArray obj() As Object) As Boolean
        If arret_general Then
            Return True
        Else
            Me.Invoke(del, obj)
            Return False
        End If
    End Function



    Private Sub TitreLot(ByVal chaine As String, ByVal position As Single)
        mBuffer.Graphics.Clear(Color.White)
        Dim f As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel)
        mBuffer.Graphics.DrawString(chaine, New Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Black, 0, 0)
        f.Dispose()
        PictureBox1.Refresh()
    End Sub
    Private Sub DeuxCarre(ByVal chaine As String, ByVal position As Single)

        Dim f As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel)


        mBuffer.Graphics.FillRectangle(Brushes.Red, 0, position, 10, 10)
        mBuffer.Graphics.DrawString(chaine, f, Brushes.Black, 15, position)
        mBuffer.Graphics.FillRectangle(Brushes.Green, 0, position - 15, 10, 10)

        f.Dispose()
        PictureBox1.Refresh()
    End Sub

    Private Sub CarreRouge(ByVal chaine As String, ByVal position As Single)
        Dim f As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel)


        mBuffer.Graphics.FillRectangle(Brushes.Red, 0, position, 10, 10)
        mBuffer.Graphics.DrawString(chaine, f, Brushes.Black, 15, position)

        f.Dispose()

        PictureBox1.Refresh()
    End Sub
    Private Sub CarreVert(ByVal chaibne As String, ByVal position As Single)
        mBuffer.Graphics.FillRectangle(Brushes.Green, 0, position - 15, 10, 10)
        PictureBox1.Refresh()
    End Sub

    Private mBuffer As BufferedGraphics

    Private Sub FormIntegration_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        mBuffer.Dispose()
    End Sub
    Public Sub thread_stop()
        thr_edigeo.Abort()
        'System.Threading.Thread.CurrentThread.Abort()
    End Sub
    Private Sub FormIntegration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim BB As New BufferedGraphicsContext

        Using myg As Graphics = PictureBox1.CreateGraphics
            mBuffer = BB.Allocate(myg, PictureBox1.ClientRectangle)
            mBuffer.Graphics.Clear(Color.White)
        End Using

        BB.Dispose()


    End Sub



    Private Sub Annuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private thr_edigeo As Thread



    Private gros As Integer = 0
    Private petit As Integer = 0
    Private Shared verrou_lot As New Object
    Private mGrosLot As Boolean
    Public Property GrosLot() As Boolean
        Get
            Return mGrosLot
        End Get
        Set(ByVal value As Boolean)
            mGrosLot = value
        End Set
    End Property

    Private Function cherche_lot() As Integer
        SyncLock verrou_lot

            If OrderedTHF Is Nothing Then
                Return 0
            End If
            If OrderedTHF._Col.Count = 0 Then
                Return 0
            End If

            If mGrosLot Then


                mLotForWork = New THF(OrderedTHF._Col(OrderedTHF._Col.Count - 1).Chemin & "\" & OrderedTHF._Col(OrderedTHF._Col.Count - 1).Nom)
                OrderedTHF._Col.RemoveAt(OrderedTHF._Col.Count - 1)


            Else

                petit = petit + 1
                mLotForWork = New THF(OrderedTHF._Col(0).Chemin & "\" & OrderedTHF._Col(0).Nom)
                OrderedTHF._Col.RemoveAt(0)


            End If
            Return 1
        End SyncLock
    End Function

    Private Sub integre_edigeo_unlocked()

        Dim arg1(0) As Object

        If cherche_lot() = 0 And finedigeo Then
            Exit Sub
        End If

        Dim count As Integer = 1
        If OrderedTHF Is Nothing Then



        Else
            Do While OrderedTHF._Col.Count > 0 Or mLotForWork IsNot Nothing

                mFlog = New IO.StreamWriter(mLotForWork.Chemin & "\" & mLotForWork.Nom.Substring(0, mLotForWork.Nom.Length - 4) & "_EDIGEO_LOG.txt")
                mFlog.AutoFlush = True
                mFlog.WriteLine("")
                mFlog.WriteLine("********************************************************************************")
                mFlog.WriteLine(mLotForWork.Chemin & "\" & mLotForWork.Nom.Substring(0, mLotForWork.Nom.Length - 4))

                Dim la As Echange_object
                la = IntegrationEdigeo_seule(mLotForWork.Chemin & "\" & mLotForWork.Nom, count)

                If la Is Nothing And arret_general Then
                    Exit Do
                End If
                count = count + 1
                arg1(0) = count
                mFlog.Close()

                Me.Invoke(ProgresInstance)

                Do While Tampon.Count >= tailletampon - 1
                    System.Threading.Thread.Sleep(10)
                Loop

                MDIParent1.GereTampon(la, True, 0)
                nblot_traites = nblot_traites + 1
                'Me.Invoke(ProgresInstance)
                testinvoke(ProgresInstance)
                mLotForWork = Nothing
                If cherche_lot() = 0 Then
                    finedigeo = True
                End If
            Loop
        End If



    End Sub

    Private Function IntegrationEdigeo_seule(ByVal thf As String, ByVal count As Integer) As Echange_object

        MonLecteur = New Lecteur(thf.ToString, Me)
        Dim arg1(1) As Object
        arg1(0) = "Intégration du lot " & count
        arg1(1) = 0
        Me.Invoke(TitreInstance, arg1)

        If MonLecteur.THF.VerifieBOMEOM("Analyse et Vérification THF", 15) Then
            If MonLecteur.THF.Initialise() Then
                If MonLecteur.THF.ChargeZone() Then

                    MonLecteur.THF.InitDictionary()
                    Flog.WriteLine("EDIGEO INTEGRE OK")
                    Flog.WriteLine("******************************************************")
                Else
                    Return Nothing
                    Exit Function
                End If
            Else
                'MsgBox("Abandon")
                Return Nothing
            End If

        Else
            Return Nothing
            Exit Function
        End If
        If arret_general Then
            Return Nothing
        Else
            Dim la As New Echange_object(MonLecteur.THF.ListeCouche, MonLecteur.THF.NomLot, thf.ToString.Substring(0, thf.ToString.Length - 4))

            Return la
        End If
        

    End Function

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        Dim hdc As IntPtr = e.Graphics.GetHdc()
        mBuffer.Render(hdc)
        e.Graphics.ReleaseHdc(hdc)
    End Sub

    Public Sub IntgrationPara()
        'ProgressBar1.Value = 0.Count
        'Label1.Text = "Nb de lots : " & Me.THFS.Count
        PictureBox1.Refresh()

        'Dim THRIntegration As New Thread(AddressOf Integration_Edigeo_topo_unlocked)

        'THRIntegration.Start()

        Integration_Edigeo_topo_unlocked()
    End Sub
    Public Sub Integration_Edigeo_topo_unlocked()

        'Me.Label1.Text = "Nbde lots : " & Me.THFS.Count
        'PictureBox1.Refresh()

        thr_edigeo = New Thread(AddressOf integre_edigeo_unlocked)

        thr_edigeo.Start()
    End Sub

    Public Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class