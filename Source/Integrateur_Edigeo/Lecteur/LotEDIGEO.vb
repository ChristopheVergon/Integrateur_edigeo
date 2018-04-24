Public Class LotEDIGEO
    Inherits FichierEDIGEO


    Private mNom As String
    Public ReadOnly Property Nom() As String
        Get
            Return mNom
        End Get

    End Property

    


    Public Sub New(ByVal THFFileName As String, f As FormIntegration)
        If Not THFFileName.EndsWith("THF") Then
            MsgBox("Mauvais fichier .THF attendu", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim res() As String = THFFileName.Split("\")

        For i = 0 To res.Count - 2

            MyBase.Chemin = MyBase.Chemin & res(i) & "\"
        Next

        mNom = res(res.Count - 1).Substring(0, res(res.Count - 1).Length - 4)

        MyBase.FileName = MyBase.Chemin & mNom & ".THF"

        Fint = f
    End Sub

    Private mErreur As ErreurLectureEDIGEO
    Public ReadOnly Property Erreur() As ErreurLectureEDIGEO
        Get
            Return mErreur
        End Get

    End Property

    Private mNomLot As String
    Public ReadOnly Property NomLot() As String
        Get
            Return mNomLot
        End Get

    End Property


    Private mDICO As Dic
    Public ReadOnly Property DICO() As Dic
        Get
            Return mDICO
        End Get

    End Property

    Private mMCD As SCD
    Public Property MCD() As SCD
        Get
            Return mMCD
        End Get
        Set(ByVal value As SCD)
            mMCD = value
        End Set
    End Property


    Private mListeVEC As System.Collections.Generic.List(Of VEC)
    Public Property ListeVEC() As System.Collections.Generic.List(Of VEC)
        Get
            Return mListeVEC
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of VEC))
            mListeVEC = value
        End Set
    End Property
    Public Function ChercheObjSCD() As System.ComponentModel.BindingList(Of SCD_Objet)
        Return mMCD.ListeObjet
    End Function
    Public Function ChercheObjetSCDname(ByVal name As String) As System.Collections.Generic.List(Of VEC_OBJ)
        Dim l As New System.Collections.Generic.List(Of VEC_OBJ)

        For Each v In mListeVEC
            l.AddRange(v.ChercheSCDName(name))

        Next
        Return l
    End Function
    Public Function Initialise() As Boolean
        'Form1.Label1.Text = "Initialisation des fichiers dépendants"
        Dim z As ZoneEdigeo
        z = MyBase.ZoneListe.ChercheZ6("GTL")

        If z Is Nothing Then
            mErreur = ErreurLectureEDIGEO.GTL
            Fint.Flog.WriteLine("WARNING ERREUR GTL ")
            Return False
        End If

        Dim i As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)

        z = MyBase.ZoneListe.ListeZ(i + 2)

        mNomLot = z.Z6

        Fint.Flog.WriteLine("Nom du lot : " & mNomLot)

        Dim ID_N As New ID_Nom
        z = MyBase.ZoneListe.ListeZ(i + 10)
        ID_N.Nom = z.Z6
        z = MyBase.ZoneListe.ListeZ(i + 11)
        ID_N.Identificateur = z.Z6
        mDICO = New DIC(MyBase.Chemin, mNomLot, ID_N, Fint)

        ID_N = New ID_Nom
        z = MyBase.ZoneListe.ListeZ(i + 12)
        ID_N.Nom = z.Z6
        z = MyBase.ZoneListe.ListeZ(i + 13)
        ID_N.Identificateur = z.Z6

        mMCD = New SCD(MyBase.Chemin, mNomLot, ID_N, Fint)

        Dim count As Integer = MyBase.ZoneListe.ListeZ(i + 14).Z6

        mListeVEC = New System.Collections.Generic.List(Of VEC)

        i = i + 14

        For j = 0 To count - 1

            ID_N = New ID_Nom
            i = i + 1
            z = MyBase.ZoneListe.ListeZ(i)
            ID_N.Nom = z.Z6
            i = i + 1
            z = MyBase.ZoneListe.ListeZ(i)
            ID_N.Identificateur = z.Z6

            Dim Tvec As New VEC(MyBase.Chemin, mNomLot, ID_N, Fint)

            mListeVEC.Add(Tvec)
        Next

        If Not System.IO.File.Exists(mDICO.FileName) Then
            mErreur = ErreurLectureEDIGEO.NODIC
            Fint.Flog.WriteLine("WARNING PAS DE DICTIONNAIRE")
            Return False
        End If

        If Not System.IO.File.Exists(mMCD.FileName) Then
            mErreur = ErreurLectureEDIGEO.NOSCD
            Fint.Flog.WriteLine("WARNING PAS DE SCD")
            Return False
        End If

        For j = 0 To count - 1
            If Not System.IO.File.Exists(mListeVEC(j).FileName) Then
                mErreur = ErreurLectureEDIGEO.NOVEC
                Fint.Flog.WriteLine("WARNING PAS DE VECTEUR")
                Return False
            End If
        Next

        mErreur = ErreurLectureEDIGEO.NON
        Fint.Flog.WriteLine("OK Fichiers dépendants présents")
        Return True

    End Function


    Public Function ChargeZone() As Boolean
        If mDICO.VerifieBOMEOM("Vérification et chargement dictionnaires", Y) Then
            If mMCD.VerifieBOMEOM("Vérification et chargement MCD", Y) Then
                Dim i As Integer = 1
                For Each F In mListeVEC
                    If F.VerifieBOMEOM("Vérification et chargement fichier vecteur " & i, Y) Then
                        i = i + 1
                    Else
                        Return False
                    End If
                Next

                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Protected Y As Single = 30
    Public Overrides Sub InitDictionary()
        Dim f As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel)

        Dim arg1(1) As Object
        arg1(0) = "Initialisation dictionnaire Nomenclature"
        arg1(1) = Y

        If Fint.testinvoke(Fint.CarreRougeInstance, arg1) Then Exit Sub
        Y = Y + 15


        mDICO.InitDictionary()
        mDICO.ZoneListe.ListeZ.Clear()

        arg1(0) = "Initialisation dictionnaire MCD"
        arg1(1) = Y
        If Fint.testinvoke(Fint.DeuxCarreInstance, arg1) Then Exit Sub


        Y = Y + 15
        mMCD.DICO = mDICO
        mMCD.InitDictionary()

        mMCD.ZoneListe.ListeZ.Clear()


        arg1(0) = "Initialisation dictionnaire VECTEURS"
        arg1(1) = Y
        If Fint.testinvoke(Fint.DeuxCarreInstance, arg1) Then Exit Sub

        Y = Y + 15
        mListeVEC.ForEach(AddressOf init)

        arg1(1) = Y
        If Fint.testinvoke(Fint.CarreVertInstance, arg1) Then Exit Sub

        Y = mListeVEC(0).Y

        arg1(0) = "Initialisation des sous-dictionnaires de type "
        arg1(1) = Y
        If Fint.testinvoke(Fint.CarreRougeInstance, arg1) Then Exit Sub

        Y = Y + 15
        mListeVEC.ForEach(AddressOf initsousdic)

        arg1(0) = "Initialisation Géomètrie"
        arg1(1) = Y
        If Fint.testinvoke(Fint.DeuxCarreInstance, arg1) Then Exit Sub


        Y = Y + 15
        initgeometrie()

        arg1(0) = "Initialisation dictionnaire ObjetEDIGEO"
        arg1(1) = Y
        If Fint.testinvoke(Fint.DeuxCarreInstance, arg1) Then Exit Sub


        Y = Y + 15

        InitObjetEDIGEO()


        arg1(0) = "Initialisation de la Semantique"
        arg1(1) = Y
        If Fint.testinvoke(Fint.DeuxCarreInstance, arg1) Then Exit Sub

        Y = Y + 15

        GenereSemantique()

        arg1(0) = "Initialisation couche objet métier"
        arg1(1) = Y
        If Fint.testinvoke(Fint.DeuxCarreInstance, arg1) Then Exit Sub


        Y = Y + 15

        ConstruitCouches()

        arg1(1) = Y
        If Fint.testinvoke(Fint.CarreVertInstance, arg1) Then Exit Sub


        'mFint.PictureBox1.CreateGraphics.DrawString("Fin de l'intégration Edigéo", f, Brushes.Black, 15, Y)
        Y = Y + 15

    End Sub



    Private mListeCouche As System.Collections.Generic.List(Of LayerEDIGEO)
    Public ReadOnly Property ListeCouche() As System.Collections.Generic.List(Of LayerEDIGEO)
        Get
            Return mListeCouche
        End Get

    End Property

    Private Sub ConstruitCouches()
        mListeCouche = New System.Collections.Generic.List(Of LayerEDIGEO)

        For Each O In mMCD.DictionaryObjet.Values

            mListeCouche.Add(mdicobjet.ConstruitCouche(O.Identificateur))
        Next



    End Sub

    Public Function FindCouche(ByVal nomcouche As String) As LayerEDIGEO        
        For Each C In mListeCouche
            If C.LayerName = nomcouche Then
                Return C
            End If
        Next
        Return Nothing
    End Function

    Private Sub GenereSemantique()
        For Each V In mListeVEC

            For Each rel In V.DictionarySEM.Values

                Dim OB As ObjetEDIGEO

                If mdicobjet.TryGetValue(rel.ElementRelation(0).Modele, rel.ElementRelation(0)._ID, OB) Then

                    Dim ass As New Association
                    ass.RefSCD = rel.RefSCD

                    For i = 1 To rel.ElementRelation.Count - 1
                        Dim OB1 As New ObjetEDIGEO
                        If mdicobjet.TryGetValue(rel.ElementRelation(i).Modele, rel.ElementRelation(i)._ID, OB1) Then
                            ass.ListeObj.Add(OB1)
                        End If

                        Dim REV As ObjetEDIGEO

                        If mdicobjet.TryGetValue(rel.ElementRelation(i).Modele, rel.ElementRelation(i)._ID, REV) Then
                            Dim assrev As New Association
                            assrev.RefSCD = rel.RefSCD
                            assrev.ListeObj.Add(OB)

                            REV.EstAssocieA.Add(assrev)
                        End If

                    Next

                    OB.EstAssocieA.Add(ass)
                End If

            Next

        Next
    End Sub

    Private mdicnoeud As DictionaryGeometrie
    Private mdicarc As DictionaryGeometrie
    Private mdicface As DictionaryGeometrie

    Private mdicobjet As DictionaryObjetEDIGEO

    Private Sub initdicgeometrie()
        mdicnoeud = New DictionaryGeometrie
        mdicnoeud = InitialiseDicNoeud()
        mdicarc = New DictionaryGeometrie
        mdicarc = InitialiseDicArc()
        mdicface = New DictionaryGeometrie
        mdicface = InitialiseDicFace()
    End Sub
    Public Sub InitObjetEDIGEO()
        initObjGeo()
        GenereRepresentePar()
        GenereRepresentePlusPar()
    End Sub
    Public Sub initgeometrie()
        initdicgeometrie()
        GenereNoeudInitial()
        GenereNoeudFinal()
        GenereFaceDroite()
        GenereFaceGauche()
        GenereAppartientA()
        GenereInclusDans()
    End Sub


    Private Sub initObjGeo()
        mdicobjet = New DictionaryObjetEDIGEO
        mdicobjet = InitialiseDicObjGeo
    End Sub
    Private Sub GenereRepresentePar()
        For Each V In mListeVEC
            For Each rel In V.DictionaryIDB
                Dim OB As ObjetEDIGEO
                If mdicobjet.TryGetValue(rel.Value.ElementRelation(0).Modele, rel.Value.ElementRelation(0)._ID, OB) Then
                    Select Case OB.Nature
                        Case NatureObjetSCD.PCT
                            Dim OBP As ObjetEDIGEO_PCT
                            Dim P As NOEUD
                            If mdicnoeud.TryGetValue(rel.Value.ElementRelation(1).Modele, rel.Value.ElementRelation(1)._ID, P) Then
                                OBP = OB
                                OBP.Geom = P
                            End If

                        Case NatureObjetSCD.ARE
                            Dim OBF As ObjetEDIGEO_SURF
                            Dim F As FACE
                            If mdicface.TryGetValue(rel.Value.ElementRelation(1).Modele, rel.Value.ElementRelation(1)._ID, F) Then
                                OBF = OB
                                OBF.Geom = F
                            End If
                    End Select
                End If

            Next
        Next
    End Sub
    Private Sub GenereRepresentePlusPar()
        For Each V In mListeVEC
            For Each rel In V.DictionaryIDR
                Dim OB As ObjetEDIGEO
                If mdicobjet.TryGetValue(rel.Value.ElementRelation(0).Modele, rel.Value.ElementRelation(0)._ID, OB) Then


                    Dim OBP As ObjetEDIGEO_LIN
                    Dim P As ARC
                    If mdicarc.TryGetValue(rel.Value.ElementRelation(1).Modele, rel.Value.ElementRelation(1)._ID, P) Then
                        OBP = OB
                        OBP.Geom = P
                        OBP.Sens = rel.Value.SensComposition(1)
                    End If


                End If

            Next
        Next
    End Sub
    Private Sub GenereNoeudInitial()


        For Each V In mListeVEC

            For Each rel In V.DictionaryIND
                Dim ar As ARC
                If rel.Value.ElementRelation.Count = 1 Then
                    ' noeud isolé ?
                    MsgBox("Arret relation Noeud arc diff 2")
                Else
                    If mdicarc.TryGetValue(rel.Value.ElementRelation(0).Modele, rel.Value.ElementRelation(0)._ID, ar) Then

                        Dim no As NOEUD

                        If mdicnoeud.TryGetValue(rel.Value.ElementRelation(1).Modele, rel.Value.ElementRelation(1)._ID, no) Then
                            ar.NoeudInitial = no
                        End If

                    Else
                        MsgBox("Erreur génération noeud initial Arc" & rel.Value.ElementRelation(0)._ID)

                    End If
                End If

            Next

        Next


    End Sub
    Private Sub GenereNoeudFinal()


        For Each V In mListeVEC

            For Each rel In V.DictionaryFND
                Dim ar As ARC
                If rel.Value.ElementRelation.Count <> 2 Then
                    ' noeud isolé ?
                    MsgBox("Arret relation Noeud arc dif 2")
                Else
                    If mdicarc.TryGetValue(rel.Value.ElementRelation(0).Modele, rel.Value.ElementRelation(0)._ID, ar) Then
                        Dim no As NOEUD

                        If mdicnoeud.TryGetValue(rel.Value.ElementRelation(1).Modele, rel.Value.ElementRelation(1)._ID, no) Then
                            ar.NoeudFinal = no
                        End If

                    Else
                        MsgBox("Erreur génération noeud final Arc" & rel.Value.ElementRelation(0)._ID)

                    End If
                End If

            Next

        Next


    End Sub

    Private Sub GenereFaceDroite()
        For Each V In mListeVEC

            For Each rel In V.DictionaryRPO
                Dim ar As ARC
                If rel.Value.ElementRelation.Count <> 2 Then
                    ' noeud isolé ?
                    MsgBox("Arret relation  arc face dif 2")
                Else
                    If mdicarc.TryGetValue(rel.Value.ElementRelation(0).Modele, rel.Value.ElementRelation(0)._ID, ar) Then
                        Dim FD As FACE

                        If mdicface.TryGetValue(rel.Value.ElementRelation(1).Modele, rel.Value.ElementRelation(1)._ID, FD) Then
                            ar.FaceDroite = FD
                            FD.ListeARC.Add(ar)
                        End If

                    Else
                        MsgBox("Erreur génération Face droite" & rel.Value.ElementRelation(0)._ID)

                    End If
                End If

            Next

        Next
    End Sub


    Private Sub GenereFaceGauche()
        For Each V In mListeVEC

            For Each rel In V.DictionaryLPO
                Dim ar As ARC
                If rel.Value.ElementRelation.Count <> 2 Then
                    ' noeud isolé ?
                    MsgBox("Arret relation  arc face dif 2")
                Else
                    If mdicarc.TryGetValue(rel.Value.ElementRelation(0).Modele, rel.Value.ElementRelation(0)._ID, ar) Then
                        Dim FD As FACE

                        If mdicface.TryGetValue(rel.Value.ElementRelation(1).Modele, rel.Value.ElementRelation(1)._ID, FD) Then
                            ar.FaceGauche = FD
                            FD.ListeARC.Add(ar)
                        End If

                    Else
                        MsgBox("Erreur génération Face droite" & rel.Value.ElementRelation(0)._ID)

                    End If
                End If

            Next

        Next
    End Sub

    Private Sub GenereInclusDans()
        For Each V In mListeVEC

            For Each rel In V.DictionaryILI
                Dim no As NOEUD

                If rel.Value.ElementRelation.Count <> 2 Then
                    ' noeud isolé ?
                    MsgBox("Arret relation  Node face dif 2")
                Else
                    If mdicnoeud.TryGetValue(rel.Value.ElementRelation(0).Modele, rel.Value.ElementRelation(0)._ID, no) Then
                        Dim FD As FACE

                        If mdicface.TryGetValue(rel.Value.ElementRelation(1).Modele, rel.Value.ElementRelation(1)._ID, FD) Then
                            no.EstInclusDans = FD

                        End If

                    Else
                        MsgBox("Erreur génération Node in Face" & rel.Value.ElementRelation(0)._ID)

                    End If
                End If


            Next

        Next
    End Sub
    Private Sub GenereAppartientA()
        For Each V In mListeVEC

            For Each rel In V.DictionaryILI
                Dim no As NOEUD

                If rel.Value.ElementRelation.Count <> 2 Then
                    ' noeud isolé ?
                    MsgBox("Arret relation  Node Inclus ARC dif 2")
                Else
                    If mdicnoeud.TryGetValue(rel.Value.ElementRelation(0).Modele, rel.Value.ElementRelation(0)._ID, no) Then
                        Dim FD As ARC

                        If mdicarc.TryGetValue(rel.Value.ElementRelation(1).Modele, rel.Value.ElementRelation(1)._ID, FD) Then
                            no.AppartientA = FD

                        End If

                    Else
                        MsgBox("Erreur génération Node inclus ds ARC" & rel.Value.ElementRelation(0)._ID)

                    End If
                End If


            Next

        Next
    End Sub
    Private Sub init(ByVal V As VEC)
        V.Y = Y
        V.InitDictionary()
        V.ZoneListe.ListeZ.Clear()


    End Sub


    Private Sub initsousdic(ByVal V As VEC)
        V.InitSousDicRelation(MCD.DictionaryRelConstruction)
    End Sub

    Private Function InitialiseDicArc() As DictionaryGeometrie
        Dim dic As New DictionaryGeometrie

        For Each V In mListeVEC

            For Each VA In V.DictionaryArc
                Dim ar As New ARC

                ar.Identificateur = VA.Key
                ar.Points = VA.Value.Points
                ar.NbAttribut = VA.Value.NbAttribut
                ar.SCDAttribut = VA.Value.SCDAttribut
                ar.ValeurAtt = VA.Value.ValeurAtt

                dic.add(V.Info.Identificateur, ar)
            Next

        Next

        For Each v In mListeVEC
            v.DictionaryArc.Clear()
        Next

        Return dic

    End Function
    Private Function InitialiseDicNoeud() As DictionaryGeometrie
        Dim dic As New DictionaryGeometrie

        For Each V In mListeVEC

            For Each VA In V.DictionaryNoeud
                Dim ar As New NOEUD

                ar.Identificateur = VA.Key
                ar.Point = VA.Value.Point
                ar.NbAttribut = VA.Value.NbAttribut
                ar.SCDAttribut = VA.Value.SCDAttribut
                ar.ValeurAtt = VA.Value.ValeurAtt

                dic.add(V.Info.Identificateur, ar)
            Next

        Next

        For Each v In mListeVEC
            v.DictionaryNoeud.Clear()
        Next

        Return dic

    End Function

    Private Function InitialiseDicFace() As DictionaryGeometrie
        Dim dic As New DictionaryGeometrie

        For Each V In mListeVEC

            For Each VA In V.DictionaryFace
                Dim ar As New FACE

                ar.Identificateur = VA.Key

                ar.NbAttribut = VA.Value.NbAttribut
                ar.SCDAttribut = VA.Value.SCDAttribut
                ar.ValeurAtt = VA.Value.ValeurAtt

                dic.add(V.Info.Identificateur, ar)
            Next

        Next

        For Each v In mListeVEC
            v.DictionaryFace.Clear()
        Next

        Return dic

    End Function
    
    Private Function InitialiseDicObjGeo() As DictionaryObjetEDIGEO
        Dim dic As New DictionaryObjetEDIGEO

        For Each V In mListeVEC

            For Each VA In V.DictionaryObj

                Dim SCO As SCD_Objet = mMCD.DictionaryObjet.Item(VA.Value.RefSCD._ID)
                
                Select Case SCO.Nature

                    Case NatureObjetSCD.PCT

                        Dim p As New ObjetEDIGEO_PCT
                        p.ID_Objet = VA.Value.Identificateur
                        p.SCDAttribut = VA.Value.SCDAttribut
                        p.NbAttribut = VA.Value.NbAttribut
                        p.ValeurAtt = VA.Value.ValeurAtt
                        p.NameSCD = SCO.Identificateur
                        p.NomLot = V.Info.Identificateur
                        dic.add(V.Info.Identificateur, p)

                    Case NatureObjetSCD.LIN

                        Dim p As New ObjetEDIGEO_LIN
                        p.ID_Objet = VA.Value.Identificateur
                        p.SCDAttribut = VA.Value.SCDAttribut
                        p.NbAttribut = VA.Value.NbAttribut
                        p.ValeurAtt = VA.Value.ValeurAtt
                        p.NameSCD = SCO.Identificateur
                        p.NomLot = V.Info.Identificateur
                        dic.add(V.Info.Identificateur, p)


                    Case NatureObjetSCD.ARE
                        Dim p As New ObjetEDIGEO_SURF
                        p.ID_Objet = VA.Value.Identificateur
                        p.SCDAttribut = VA.Value.SCDAttribut
                        p.NbAttribut = VA.Value.NbAttribut
                        p.ValeurAtt = VA.Value.ValeurAtt
                        p.NameSCD = SCO.Identificateur
                        p.NomLot = V.Info.Identificateur
                        dic.add(V.Info.Identificateur, p)

                    Case NatureObjetSCD.CPX
                        Dim p As New ObjetEDIGEO_CPX
                        p.ID_Objet = VA.Value.Identificateur
                        p.SCDAttribut = VA.Value.SCDAttribut
                        p.NbAttribut = VA.Value.NbAttribut
                        p.ValeurAtt = VA.Value.ValeurAtt
                        p.NameSCD = SCO.Identificateur
                        p.NomLot = V.Info.Identificateur
                        dic.add(V.Info.Identificateur, p)
                End Select


            Next

        Next

        For Each v In mListeVEC
            v.DictionaryObj.Clear()
        Next

        Return dic
    End Function
End Class
