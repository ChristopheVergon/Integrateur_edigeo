Public Class VEC
    Inherits FichierEDIGEO



    Private mDictionaryArc As System.Collections.Generic.Dictionary(Of String, VEC_ARC)
    Public Property DictionaryArc() As System.Collections.Generic.Dictionary(Of String, VEC_ARC)
        Get
            Return mDictionaryArc
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, VEC_ARC))
            mDictionaryArc = value
        End Set
    End Property

    Private mDictionaryNoeud As System.Collections.Generic.Dictionary(Of String, VEC_NOEUD)
    Public Property DictionaryNoeud() As System.Collections.Generic.Dictionary(Of String, VEC_NOEUD)
        Get
            Return mDictionaryNoeud
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, VEC_NOEUD))
            mDictionaryNoeud = value
        End Set
    End Property


    Private mDictionaryFace As System.Collections.Generic.Dictionary(Of String, VEC_FACE)
    Public Property DictionaryFace() As System.Collections.Generic.Dictionary(Of String, VEC_FACE)
        Get
            Return mDictionaryFace
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, VEC_FACE))
            mDictionaryFace = value
        End Set
    End Property


    Private mDictionaryObj As System.Collections.Generic.Dictionary(Of String, VEC_OBJ)
    Public Property DictionaryObj() As System.Collections.Generic.Dictionary(Of String, VEC_OBJ)
        Get
            Return mDictionaryObj
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, VEC_OBJ))
            mDictionaryObj = value
        End Set
    End Property

    Private mListeObj As System.Collections.Generic.List(Of VEC_OBJ)
    Public ReadOnly Property ListeObj() As System.Collections.Generic.List(Of VEC_OBJ)
        Get
            If mListeObj Is Nothing Then
                InitBindingObj()
            End If
            Return mListeObj
        End Get

    End Property

    

    Private Sub InitBindingObj()
        mListeObj = New System.Collections.Generic.List(Of VEC_OBJ)
        For Each obj As KeyValuePair(Of String, VEC_OBJ) In mDictionaryObj
            mListeObj.Add(obj.Value)
        Next
    End Sub


    Private mListeFace As System.Collections.Generic.List(Of VEC_FACE)
    Public ReadOnly Property ListeFace() As System.Collections.Generic.List(Of VEC_FACE)
        Get
            If mListeFace Is Nothing Then
                InitListeFace()
            End If
            Return mListeFace
        End Get

    End Property

    Private Sub InitListeFace()
        mListeFace = New System.Collections.Generic.List(Of VEC_FACE)
        For Each obj As KeyValuePair(Of String, VEC_FACE) In mDictionaryFace
            mListeFace.Add(obj.Value)
        Next
    End Sub

    Private mDictionaryRelation As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
    Public Property DictionaryRelation() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionaryRelation
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, VEC_RELATION))
            mDictionaryRelation = value
        End Set
    End Property

    Public Sub New(ByVal chemin As String, ByVal nomlot As String, ByVal nom As ID_Nom, f As FormIntegration)
        If chemin.EndsWith("\") Then
        Else
            chemin = chemin & "\"
        End If
        MyBase.Chemin = chemin

        MyBase.Info = New ID_Nom
        MyBase.Info = nom

        Fint = f
        MyBase.FileName = MyBase.Chemin & nomlot & nom.Nom & ".VEC"
        
    End Sub
    Public Y As Single
    Public Overrides Sub InitDictionary()


        'FormIntegration.PictureBox1.CreateGraphics.FillRectangle(Brushes.Red, 0, Y, 10, 10)
        'FormIntegration.PictureBox1.CreateGraphics.DrawString("Initialisation dictionnaire ARC", f, Brushes.Black, 15, Y)
        Init("PAR", mDictionaryArc)
        Init("PFE", mDictionaryFace)
        Init("PNO", mDictionaryNoeud)
        Init("FEA", mDictionaryObj)
        Init("LNK", mDictionaryRelation)
        'InitVECArc()
        ''FormIntegration.PictureBox1.CreateGraphics.FillRectangle(Brushes.Green, 0, Y - 15, 10, 10)

        ''FormIntegration.PictureBox1.CreateGraphics.FillRectangle(Brushes.Red, 0, Y, 10, 10)
        ''FormIntegration.PictureBox1.CreateGraphics.DrawString("Initialisation dictionnaire FACE", f, Brushes.Black, 15, Y)

        'InitVECFACE()
        ''FormIntegration.PictureBox1.CreateGraphics.FillRectangle(Brushes.Green, 0, Y - 15, 10, 10)

        ''FormIntegration.PictureBox1.CreateGraphics.FillRectangle(Brushes.Red, 0, Y, 10, 10)
        ''FormIntegration.PictureBox1.CreateGraphics.DrawString("Initialisation dictionnaire NOEUD", f, Brushes.Black, 15, Y)

        'InitVECNoeud()
        ''FormIntegration.PictureBox1.CreateGraphics.FillRectangle(Brushes.Green, 0, Y - 15, 10, 10)

        ''FormIntegration.PictureBox1.CreateGraphics.FillRectangle(Brushes.Red, 0, Y, 10, 10)
        ''FormIntegration.PictureBox1.CreateGraphics.DrawString("Initialisation dictionnaire OBJET", f, Brushes.Black, 15, Y)

        'InitVECOBJ()
        ''FormIntegration.PictureBox1.CreateGraphics.FillRectangle(Brushes.Green, 0, Y - 15, 10, 10)

        ''FormIntegration.PictureBox1.CreateGraphics.FillRectangle(Brushes.Red, 0, Y, 10, 10)
        ''FormIntegration.PictureBox1.CreateGraphics.DrawString("Initialisation dictionnaire RELATION", f, Brushes.Black, 15, Y)

        'InitVECREL()
        'FormIntegration.PictureBox1.CreateGraphics.FillRectangle(Brushes.Green, 0, Y - 15, 10, 10)

    End Sub

    Private Sub Init(Of T As {New, VEC_ABS})(z6 As String, ByRef dic As Dictionary(Of String, T))
        dic = New Dictionary(Of String, T)()
        For Each z In ZoneListe.ChercheToutZ6(z6)
            Dim foo As New T
            foo.Affecte(ZoneListe, z.Index)
            If Not dic.ContainsKey(foo.Identificateur) Then
                dic.Add(foo.Identificateur, foo)
                'Else
                '    dic.Add(foo.Identificateur & "*", foo)
            End If
        Next
    End Sub

    Private Sub InitVECArc()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        zl = MyBase.ZoneListe.ChercheToutZ6("PAR")

        mDictionaryArc = New System.Collections.Generic.Dictionary(Of String, VEC_ARC)

        zl.ForEach(AddressOf AjouteArc)

        If mDictionaryArc.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            MsgBox("Erreur structure Arc dans VEC")
        End If

    End Sub

    Private Sub AjouteArc(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New VEC_ARC
        dic_o.Affecte(MyBase.ZoneListe, cur)
        If Not mDictionaryArc.ContainsKey(dic_o.Identificateur) Then
            mDictionaryArc.Add(dic_o.Identificateur, dic_o)
            'Else
            '   mDictionaryArc.Add(dic_o.Identificateur & "*", dic_o)
        End If
    End Sub

    Private Sub InitVECNoeud()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        zl = MyBase.ZoneListe.ChercheToutZ6("PNO")

        mDictionaryNoeud = New System.Collections.Generic.Dictionary(Of String, VEC_NOEUD)

        zl.ForEach(AddressOf AjouteNoeud)

        If mDictionaryNoeud.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            MsgBox("Erreur structure Noeud dans VEC")
        End If

    End Sub

    Private Sub AjouteNoeud(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New VEC_NOEUD
        dic_o.Affecte(MyBase.ZoneListe, cur)
        If Not mDictionaryNoeud.ContainsKey(dic_o.Identificateur) Then
            mDictionaryNoeud.Add(dic_o.Identificateur, dic_o)
            'Else
            '    mDictionaryNoeud.Add(dic_o.Identificateur & "*", dic_o)
        End If

    End Sub

    Private Sub InitVECFACE()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        zl = MyBase.ZoneListe.ChercheToutZ6("PFE")

        mDictionaryFace = New System.Collections.Generic.Dictionary(Of String, VEC_FACE)

        zl.ForEach(AddressOf AjouteFace)

        If mDictionaryFace.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            MsgBox("Erreur structure Face dans VEC")
        End If

    End Sub

    Private Sub AjouteFace(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New VEC_FACE
        dic_o.Affecte(MyBase.ZoneListe, cur)

        If Not mDictionaryFace.ContainsKey(dic_o.Identificateur) Then
            mDictionaryFace.Add(dic_o.Identificateur, dic_o)
        Else
            'mDictionaryFace.Add(dic_o.Identificateur & "*", dic_o)
        End If
    End Sub

    Private Sub InitVECOBJ()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        zl = MyBase.ZoneListe.ChercheToutZ6("FEA")

        mDictionaryObj = New System.Collections.Generic.Dictionary(Of String, VEC_OBJ)

        zl.ForEach(AddressOf AjouteOBJ)

        If mDictionaryObj.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            MsgBox("Erreur structure OBJ dans VEC")
        End If

    End Sub

    Private Sub AjouteOBJ(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New VEC_OBJ
        dic_o.Affecte(MyBase.ZoneListe, cur)
        Try
            mDictionaryObj.Add(dic_o.Identificateur, dic_o)
        Catch ex As Exception
            'mDictionaryObj.Add(dic_o.Identificateur & "*", dic_o)
        End Try

    End Sub

    Private Sub InitVECREL()

        Dim zl As New System.Collections.Generic.List(Of ZoneEdigeo)

        zl = MyBase.ZoneListe.ChercheToutZ6("LNK")

        mDictionaryRelation = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

        zl.ForEach(AddressOf AjouteREL)

        If mDictionaryRelation.Where(Function(zone) zone.Value.Erreur <> ErreurStructure.NONE).Count > 0 Then
            MsgBox("Erreur structure Relation dans VEC")
        End If

    End Sub

    Private Sub AjouteREL(ByVal z As ZoneEdigeo)
        Dim cur As Integer = MyBase.ZoneListe.ListeZ.IndexOf(z)
        Dim dic_o As New VEC_RELATION
        dic_o.Affecte(MyBase.ZoneListe, cur)
        Try
            mDictionaryRelation.Add(dic_o.Identificateur, dic_o)
        Catch ex As Exception
            'mDictionaryRelation.Add(dic_o.Identificateur & "*", dic_o)
        End Try

    End Sub

    Public Function ChercheRElationObjet(ByVal obj As String) As System.Collections.Generic.List(Of VEC_RELATION)

        Dim l As New System.Collections.Generic.List(Of Descripteur_Reference)
        Dim res As New System.Collections.Generic.List(Of VEC_RELATION)
        For Each rel In mDictionaryRelation.Values
            l = rel.ElementRelation.ChercheTouteID(obj)
            If l.Count > 0 Then
                res.Add(rel)
            End If
        Next
        Return res
    End Function

    Public Function ChercheSCDName(ByVal na As String) As System.Collections.Generic.List(Of VEC_OBJ)
        Dim l As New System.Collections.Generic.List(Of VEC_OBJ)
        For Each obj In mDictionaryObj.Values
            If obj.RefSCD._ID = na Then
                l.Add(obj)
            End If
        Next
        Return l
    End Function


    Private mDictionaryICO As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

    Public Property DictionaryICO() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionaryICO
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of String, VEC_RELATION))
            mDictionaryICO = value
        End Set
    End Property

    Private mDictionaryIDB As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

    Public ReadOnly Property DictionaryIDB() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionaryIDB
        End Get

    End Property

    Private mDictionaryIDR As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

    Public ReadOnly Property DictionaryIDR() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionaryIDR
        End Get

    End Property

    Private mDictionaryIND As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

    Public ReadOnly Property DictionaryIND() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionaryIND
        End Get

    End Property

    Private mDictionaryFND As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

    Public ReadOnly Property DictionaryFND() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionaryFND
        End Get

    End Property

    Private mDictionaryLPO As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

    Public ReadOnly Property DictionaryLPO() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionaryLPO
        End Get

    End Property

    Private mDictionaryRPO As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

    Public ReadOnly Property DictionaryRPO() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionaryRPO
        End Get

    End Property

    Private mDictionaryILI As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

    Public ReadOnly Property DictionaryILI() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionaryILI
        End Get

    End Property

    Private mDictionaryBET As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

    Public ReadOnly Property DictionaryBET() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionaryBET
        End Get

    End Property
    Private mDictionarySEM As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

    Public ReadOnly Property DictionarySEM() As System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        Get
            Return mDictionarySEM
        End Get

    End Property
    Public Sub InitSousDicRelation(ByVal DICMCD As System.Collections.Generic.Dictionary(Of String, SCD_RelConstruction))
        mDictionaryBET = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        mDictionaryFND = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        mDictionaryICO = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        mDictionaryIDB = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        mDictionaryIDR = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        mDictionaryILI = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        mDictionaryIND = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        mDictionaryLPO = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        mDictionaryRPO = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)
        mDictionarySEM = New System.Collections.Generic.Dictionary(Of String, VEC_RELATION)

        For Each rels As KeyValuePair(Of String, VEC_RELATION) In mDictionaryRelation
            Dim rel As VEC_RELATION = rels.Value
            Dim scdrel As New SCD_RelConstruction
            If DICMCD.TryGetValue(rel.RefSCD._ID, scdrel) Then

                Select Case scdrel.NatureRelation

                    Case NatureRel.BET
                        mDictionaryBET.Add(rel.Identificateur, rel)

                    Case NatureRel.FND
                        mDictionaryFND.Add(rel.Identificateur, rel)

                    Case NatureRel.ICO
                        mDictionaryICO.Add(rel.Identificateur, rel)

                    Case NatureRel.IDB
                        mDictionaryIDB.Add(rel.Identificateur, rel)

                    Case NatureRel.IDR
                        mDictionaryIDR.Add(rel.Identificateur, rel)

                    Case NatureRel.ILI
                        mDictionaryILI.Add(rel.Identificateur, rel)

                    Case NatureRel.IND
                        mDictionaryIND.Add(rel.Identificateur, rel)

                    Case NatureRel.LPO
                        mDictionaryLPO.Add(rel.Identificateur, rel)

                    Case NatureRel.RPO
                        mDictionaryRPO.Add(rel.Identificateur, rel)
                End Select
            Else
                mDictionarySEM.Add(rel.Identificateur, rel)
            End If
        Next

        mDictionaryRelation.Clear()
    End Sub


    Protected Overrides Sub Finalize()

        MyBase.Finalize()
    End Sub
End Class
