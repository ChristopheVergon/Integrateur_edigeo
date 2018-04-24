Imports System.Windows.Forms

Public Class MDIParent1

    Private mabase As BasePostGis

    Private FIPG() As FormInegrationPostgis

    Private PIDT() As FormIntegration

    Private Thr_Candidat As System.Threading.Thread

    Private Delegate Sub Progres()

    Private InstanceTampon As New Progres(AddressOf etat_tampon)

    Private mTHFS As System.Collections.ObjectModel.ReadOnlyCollection(Of String)

   


    Private Sub etat_tampon()
        Label1.Text = Tampon.Count & " éléments dans le tampon"
        Label2.Text = nblottraites & " lots intégrés sur " & nblot_a_traiter
    End Sub


    Private Sub Recherche_Candidat()

        Do While Not arret_recherche

            If candidat Is Nothing Then

                ChercheCandidat()

                Me.Invoke(InstanceTampon)
                System.Threading.Thread.Sleep(10)

            End If

        Loop

        If arret_recherche Then
            Timer1.Stop()
            MsgBox("Fin de l'intégration", MsgBoxStyle.ApplicationModal)
        End If
    End Sub
    Public Sub GereTampon(E As Echange_object, ajout As Boolean, rang As Integer)

        SyncLock verrou_tampon
            If rang = -1 Then
                Tampon.Clear()
                Exit Sub
            End If

            If ajout Then

                Tampon.Add(E)

            Else
                Tampon.RemoveAt(rang)
            End If

        End SyncLock

    End Sub
    Private Sub ChercheCandidat()

        Dim res As Integer = -1
        Dim rang As Integer = -1


        If Tampon.Count = 0 Then
            If nblottraites >= nblot_a_traiter Then
                arret_general = True
                arret_recherche = True
                Exit Sub
            Else
                Exit Sub
            End If

        End If

        If typeintegration = 3 Then
            candidat = New Echange_object(Tampon(0).Couches, Tampon(0).NomLot, Tampon(0).rep)
            rang = 0
        Else
            'For i = 0 To Tampon.Count - 1

            '    res = Intersection(Tampon(i).Poly)

            '    If res = 0 Then
            '        candidat = New Echange_object(Tampon(i).Couches, Tampon(i).NomLot, Tampon(i).rep)
            '        rang = i
            '        Exit For
            '    End If

            'Next
        End If


        If rang <> -1 Then
            GereTampon(Nothing, False, rang)
        End If

    End Sub


    Private Function Intersection(pol As ObjetEDIGEO_SURF) As Integer

        Dim note As Integer = 0

        For i = 0 To nbprocposgis - 1
            If Not FIPG(i).MonLot Is Nothing Then

                note = note + mabase.Intersection(FIPG(i).MonLot.Poly, pol)

            End If


        Next
        Return note

    End Function



    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles ThfToolStripMenuItem.Click

        millesime = InputBox("Millesime des données")

        If millesime = "" Then
            MsgBox("abandon", MsgBoxStyle.Critical)
            Exit Sub
        End If


        FolderBrowserDialog1.Description = "Répertoire fichiers THF"
        If System.IO.Directory.Exists("\\serveur-ad\Archives_DGFiP\EDIGEO\") Then
            FolderBrowserDialog1.SelectedPath = "\\serveur-ad\Archives_DGFiP\EDIGEO\"
        Else
            FolderBrowserDialog1.SelectedPath = FolderBrowserDialog1.RootFolder
        End If




        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then



            PrefixeLot = FolderBrowserDialog1.SelectedPath & "_"

            Dim regex As New System.Text.RegularExpressions.Regex("[:\\\/\-%+*. ]")
            PrefixeLot = regex.Replace(PrefixeLot, "_")
            PrefixeLot = PrefixeLot.ToLower()

            Me.Cursor = Cursors.WaitCursor


            mTHFS = My.Computer.FileSystem.GetFiles(FolderBrowserDialog1.SelectedPath, FileIO.SearchOption.SearchAllSubDirectories, "*.THF")


            nblot_a_traiter = mTHFS.Count
            OrderedTHF = New THF_Liste(mTHFS)



            If Dialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

                For Each c As Control In Dialog1.GroupBox1.Controls


                    Dim r As RadioButton
                    r = CType(c, RadioButton)
                    If r.Checked Then
                        Select Case r.Text
                            Case "Topologique verrouillée"
                                typeintegration = 1
                                MsgBox("Non fournie nous contacter", MsgBoxStyle.Information)
                                Me.Cursor = Cursors.Default
                                Exit Sub
                            Case "Topologique "
                                typeintegration = 2
                                MsgBox("Non fournie nous contacter", MsgBoxStyle.Information)
                                Me.Cursor = Cursors.Default
                                Exit Sub
                            Case "Polygonale"
                                typeintegration = 3

                        End Select

                    End If

                Next

            Else
                MsgBox("Abandon", MsgBoxStyle.Critical)
                Me.Cursor = Cursors.Default
                Exit Sub

            End If

            MsgBox("Fichiers THF chargés" & vbCrLf & "Pensez à régler le nombre de process dans le menu option, prêt pour l'intégration", MsgBoxStyle.ApplicationModal)


        Else
            MsgBox("Abandon", MsgBoxStyle.Critical)
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Me.Cursor = Cursors.Default

        If FormSRID.ShowDialog = Windows.Forms.DialogResult.OK Then
        Else
            MsgBox("Abandon", MsgBoxStyle.Critical)
        End If

    End Sub
    Public Sub IntegrationPolygonale()

        ReDim PIDT(nbprocedigeo - 1)
        ReDim FIPG(nbprocposgis - 1)


        For i = 0 To nbprocedigeo_gros - 1
            PIDT(i) = New FormIntegration
            PIDT(i).MdiParent = Me
            PIDT(i).Text = "EDIGEO"
            PIDT(i).ControlBox = False
            PIDT(i).GrosLot = True
            PIDT(i).Show()
        Next

        For i = nbprocedigeo_gros To nbprocedigeo - 1
            PIDT(i) = New FormIntegration
            PIDT(i).MdiParent = Me
            PIDT(i).Text = "EDIGEO"
            PIDT(i).ControlBox = False

            PIDT(i).GrosLot = False
            PIDT(i).Show()
        Next

        For j = 0 To nbprocposgis - 1
            FIPG(j) = New FormInegrationPostgis
            FIPG(j).TypeIntegration = 3
            FIPG(j).DB = New BaseCadastre(DatabaseName, connectionstringB, SchemaName)
            FIPG(j).MdiParent = Me
            FIPG(j).Text = "POSTGIS"
            FIPG(j).ControlBox = False
        Next

        For j = 0 To nbprocposgis - 1
            FIPG(j).Show()
        Next

        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub
    





    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click

        If finedigeo = False Then

            If MsgBox("Processus en cours, " & vbCrLf & "voulez vous réellemnt quitter le programme ?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then

                nblottraites = nblot_a_traiter
                OrderedTHF._Col.Clear()
                GereTampon(Nothing, False, -1)
                finedigeo = True
                arret_general = True
                Thr_Candidat.Abort()
                For i = 0 To nbprocedigeo - 1
                    PIDT(i).thread_stop()
                Next

                th_arret = New System.Threading.Thread(AddressOf testarret)

                th_arret.Start()



            End If

        Else

            Me.Close()

        End If

    End Sub
    Private th_arret As System.Threading.Thread
    Private Function testarret() As Boolean
        
        Dim b = True
        Do While True
            For i = 0 To FIPG.Length - 1
                b = b And FIPG(i).Arret
            Next
            If b Then
                Exit Do
            Else
                b = True
            End If
        Loop
        Me.Invoke(Sub() Me.Close())


    End Function
    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Fermez tous les formulaires enfants du parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer

    Private Sub MDIParent1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 1000
        starttime = 0
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolStripSeparator3_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        starttime = starttime + 1
        Label3.Text = starttime & " secondes"
    End Sub

   

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Formoption.ShowDialog()
    End Sub
    Private Sub Charge_thf(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click

        If connectionstringB Is Nothing Then
            If Dialogconnection.ShowDialog = Windows.Forms.DialogResult.OK Then

            Else
                MsgBox("Abandon", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If

        Select Case typeintegration
            Case 1
               
            Case 2
               
            Case 3

                mabase = New BaseCadastre(DatabaseName, connectionstringB, SchemaName)

                If mabase.COnnFailed Then
                    connectionstringB = Nothing
                    MsgBox("Connection à la Base impossible", MsgBoxStyle.Critical)
                    Exit Sub
                End If


                IntegrationPolygonale()

                MsgBox("Commencer l'intégration", MsgBoxStyle.ApplicationModal)

                Timer1.Start()

                Thr_Candidat = New System.Threading.Thread(AddressOf Recherche_Candidat)
                Thr_Candidat.Start()

                For i = 0 To nbprocedigeo - 1
                    PIDT(i).IntgrationPara()
                Next

                For i = 0 To nbprocposgis - 1
                    FIPG(i).Integre()
                Next
        End Select


    End Sub
   
    Private Sub HelpMenu_Click(sender As Object, e As EventArgs) Handles HelpMenu.Click

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Using AboutBox1
            AboutBox1.ShowDialog()
        End Using

    End Sub

   
End Class
