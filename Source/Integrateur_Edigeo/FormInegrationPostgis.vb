Public Class FormInegrationPostgis
    Private Shared verrou_commune As New Object
    Private Shared verrou_section As New Object
    Private nblot As Integer = 0

    Private mFlog As System.IO.StreamWriter
    Public Property Flog() As System.IO.StreamWriter
        Get
            Return mFlog
        End Get
        Set(ByVal value As System.IO.StreamWriter)
            mFlog = value
        End Set
    End Property
    Private mDB As BaseCadastre
    Public Property DB As BaseCadastre
        Get
            Return mDB
        End Get
        Set(ByVal value As BaseCadastre)
            mDB = value
        End Set
    End Property



    Private mMonLot As Echange_object
    Public Property MonLot() As Echange_object
        Get
            Return mMonLot
        End Get
        Set(ByVal value As Echange_object)
            mMonLot = value
        End Set
    End Property
    Private mTypeIntegration As Integer
    Public Property TypeIntegration() As String
        Get
            Return mTypeIntegration
        End Get
        Set(ByVal value As String)
            mTypeIntegration = value
        End Set
    End Property


    Private Sub RealiseProgres()

        Label1.Text = nblot & " lots intégrés"
    End Sub

    Private Delegate Sub Progres()

    Private ProgresInstance As New Progres(AddressOf RealiseProgres)


    Public Delegate Sub AvancementDelegate(ByVal chaine As String, ByVal position As Single)

    Public DeuxCarreInstance As New AvancementDelegate(AddressOf DeuxCarre)
    Public TitreInstance As New AvancementDelegate(AddressOf TitreLot)
    Public CarreRougeInstance As New AvancementDelegate(AddressOf CarreRouge)
    Public CarreVertInstance As New AvancementDelegate(AddressOf CarreVert)

   

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

    Private Sub TitreLot(ByVal chaine As String, ByVal position As Single)
        mBuffer.Graphics.Clear(Color.White)
        Dim f As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel)
        mBuffer.Graphics.DrawString(chaine, New Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Black, 0, 0)
        f.Dispose()
        PictureBox1.Refresh()
    End Sub

    Private mBuffer As BufferedGraphics

    Private Sub FormIntegration_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        mBuffer.Dispose()
    End Sub
    Private Sub FormIntegration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim BB As New BufferedGraphicsContext

        Using myg As Graphics = PictureBox1.CreateGraphics
            mBuffer = BB.Allocate(myg, PictureBox1.ClientRectangle)
            mBuffer.Graphics.Clear(Color.White)
        End Using

        BB.Dispose()


    End Sub
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        Dim hdc As IntPtr = e.Graphics.GetHdc()
        mBuffer.Render(hdc)
        e.Graphics.ReleaseHdc(hdc)
    End Sub
    Private thr_integre As System.Threading.Thread

    Public ReadOnly Property thr_integration() As System.Threading.Thread
        Get
            Return thr_integre
        End Get

    End Property

    Public Sub Integre()
        thr_integre = New System.Threading.Thread(AddressOf integration)
        thr_integre.Start()
    End Sub

    Private Function iscandidat() As Boolean
        SyncLock verrou_candidat
            If Not candidat Is Nothing Then
                mMonLot = New Echange_object(candidat.Couches, candidat.NomLot, candidat.rep)
                candidat = Nothing
                Return True
            Else
                Return False
            End If
        End SyncLock
    End Function
    Private Sub integration()

        Do While Not arret_general

            If iscandidat() Then

                mFlog = New IO.StreamWriter(mMonLot.rep & "_postgis_LOG.txt")
                mFlog.AutoFlush = True
                mFlog.WriteLine("")
                mFlog.WriteLine("********************************************************************************")
                


                If mDB Is Nothing Then
                    ' mDBtopo.Flog = mFlog
                Else
                    mDB.Flog = mFlog
                End If
                IntegrationPostgis(1)
                mMonLot = Nothing
            Else
                System.Threading.Thread.Sleep(10)
            End If

        Loop
        If arret_general Then
            mArret = True
        End If
    End Sub

    Private mArret As Boolean = False
    Public ReadOnly Property Arret() As Boolean
        Get
            Return mArret
        End Get

    End Property

    Private Sub IntegrationPostgis(ByVal count As Integer)

        Dim Y As Single = 0

        Dim arg1(1) As Object
        arg1(0) = "Intégration du lot " & count
        arg1(1) = Y


        Me.Invoke(TitreInstance, arg1)


        Y = Y + 15


        arg1(0) = "Intégration Parcelles"
        arg1(1) = Y
        Me.Invoke(CarreRougeInstance, arg1)

        Y = Y + 15

        Dim la As LayerEDIGEO = mMonLot.FindCouche("PARCELLE_id")

        Select Case TypeIntegration
            Case 1, 2


            Case 3
                mDB.CreateTableTempEdigeo(mMonLot.NomLot)
                mDB.PopulateParcelle(la.DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Intégration Sub Sections"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                la = mMonLot.FindCouche("SUBDSECT_id")
                mDB.PopulateSubSection(la.DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Intégration Section"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                la = mMonLot.FindCouche("SECTION_id")

                'SyncLock verrou_section
                    mDB.PopulateSection(la.DictionaryObj, mMonLot.NomLot)
                'End SyncLock

                arg1(0) = "Intégration Commune"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                la = mMonLot.FindCouche("COMMUNE_id")
                SyncLock verrou_commune
                    mDB.PopulateCommune(la.DictionaryObj, mMonLot.NomLot)
                End SyncLock

                arg1(0) = "Intégration Bâtiments"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                la = mMonLot.FindCouche("BATIMENT_id")
                mDB.PopulateBatiment(la.DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Intégration Tronçons Fluviaux"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15

                la = mMonLot.FindCouche("TRONFLUV_id")
                mDB.PopulateTronFluv(la.DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Intégration Zones de Communication"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                la = mMonLot.FindCouche("ZONCOMMUNI_id")
                mDB.PopulateZoneCommuni(la.DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Intégration Détails topographiques linéaires"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                la = mMonLot.FindCouche("TLINE_id")
                mDB.PopulateTopoLine(la.DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Intégration Détails voie privées"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                la = mMonLot.FindCouche("VOIEP_id")
                mDB.PopulateVoiep(la.DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Intégration Lieudits"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                la = mMonLot.FindCouche("LIEUDIT_id")
                mDB.PopulateLieuDit(la.DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Intégration Détails topographiques surfaciques"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                la = mMonLot.FindCouche("TSURF_id")
                mDB.PopulateTsurf(la.DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Intégration Détails topographiques ponctuels"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)


                Y = Y + 15
                la = mMonLot.FindCouche("TPOINT_id")
                mDB.PopulateTpoint(la.DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Integration tronçons routiers"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                mDB.PopulateTronRoute(mMonLot.FindCouche("TRONROUTE_id").DictionaryObj, mMonLot.NomLot)

                arg1(0) = "Intégration Géographique Terminée"
                arg1(1) = Y
                Me.Invoke(DeuxCarreInstance, arg1)

                Y = Y + 15
                mDB.DeleteTableTempEdigeo(mMonLot.NomLot)
        End Select
        

        arg1(0) = "Intégration Géographique Terminée"
        arg1(1) = Y
        Me.Invoke(DeuxCarreInstance, arg1)

        

        mMonLot = Nothing

        nblot = nblot + 1
        nblottraites = nblottraites + 1
        Me.Invoke(ProgresInstance)

        '********************
    End Sub

   

End Class